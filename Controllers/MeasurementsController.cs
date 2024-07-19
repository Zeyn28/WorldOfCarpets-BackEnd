using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.Models;
using TodoApi.Models;
using Python.Runtime;
using static IronPython.Modules._ast;
using IronPython.Hosting;
using CliWrap;
using CliWrap.Buffered;
using System.Text.Json;

namespace ToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementsController : ControllerBase

    {
        
        private readonly TodoContext _context;
        private readonly CmdService _cmdService;


        public MeasurementsController(TodoContext context)
        {
            _context = context;
           
        }

        // GET: api/Measurements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Measurement>>> GetMeasurement()
        {
            if (_context.Measurement == null)
            {
                return NotFound();
            }
            return await _context.Measurement.ToListAsync();
        }

        // GET: api/Measurements/5
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Measurement>> GetMeasurement(long id)
        {
            if (_context.Measurement == null)
            {
                return NotFound();
            }
            var measurement = await _context.Measurement.FindAsync(id);

            if (measurement == null)
            {
                return NotFound();
            }

            return measurement;
        }

        // PUT: api/Measurements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeasurement(long id, Measurement measurement)
        {
            if (id != measurement.MeasurementId)
            {
                return BadRequest();
            }

            _context.Entry(measurement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeasurementExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Measurements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [RequestFormLimits(MultipartBodyLengthLimit = int.MaxValue)]
        [HttpPost("{cmPerPixel}/{price}")]
        
        public async Task<ActionResult<List<string>>> UploadImage([FromForm(Name = "file")] IFormFile file,double cmPerPixel,double price)
        {
            
            
            if (file == null)
            {
                return Problem("Entity set 'TodoContext.Measurement'  is null.");
            }

            var model = new FileModel
            {
                FileName = file.FileName,
                //FilePath = "C:\\Hali" + file.FileName
                FilePath = Path.Combine("C:\\Hali", file.FileName)
            };

            var filesPath = "C:\\Hali";
            if (!Directory.Exists(filesPath))
            {
                Directory.CreateDirectory(filesPath);
            }

            var newFileName = file.FileName;
            var fileNewPath = Path.Combine(filesPath, newFileName);

            try
            {
                using (var fileStream = new FileStream(fileNewPath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);

                    var result = await Cli.Wrap("C:/Users/Zeynep Aygün/AppData/Local/Programs/Python/Python39/python.exe")
                                    .WithArguments(new[] { "C:/AndroidProjects/flutter_application_3/Python/model.py", fileNewPath, cmPerPixel.ToString() })
                                    .ExecuteBufferedAsync();
                   

                    var dirs = from dir in Directory.EnumerateDirectories("C:\\Users\\Zeynep Aygün\\source\\repos\\ToDoAPI\\ToDoAPI\\runs\\detect\\") select dir;
                    int max = 0;
                    foreach (var dir in dirs)
                    {
                        if (dir == "C:\\Users\\Zeynep Aygün\\source\\repos\\ToDoAPI\\ToDoAPI\\runs\\detect\\predict") continue;
                        int temp = Convert.ToInt32(dir.Split("\\").Last().Substring(7));
                        if (temp > max) max = temp; 
                    }

                    string imagePath = "C:\\Users\\Zeynep Aygün\\source\\repos\\ToDoAPI\\ToDoAPI\\runs\\detect\\predict" + max.ToString() + "\\" + file.FileName.Split("\\").Last();

                    Console.WriteLine(result.StandardOutput);
                    string[] carpetInfo = result.StandardOutput.Substring(1, result.StandardOutput.Length - 4).Split(",") ;

                    List<string> response = new List<string>
                    {
                        carpetInfo[0].Trim(),
                        carpetInfo[1].Trim(),
                        imagePath,
                        price.ToString()
                       
                    };
                    return Ok(response);
                    //return Ok(response);


                }
            }
            catch(Exception ex)
            {
                return Problem("hata");
            }
         
        }

        // DELETE: api/Measurements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeasurement(long id)
        {
            if (_context.Measurement == null)
            {
                return NotFound();
            }
            var measurement = await _context.Measurement.FindAsync(id);
            if (measurement == null)
            {
                return NotFound();
            }

            _context.Measurement.Remove(measurement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MeasurementExists(long id)
        {
            return (_context.Measurement?.Any(e => e.MeasurementId == id)).GetValueOrDefault();
        }

        public class FileModel
        {
            public string FileName { get; set; }
            public string FilePath { get; set; }
        }
    }
}
