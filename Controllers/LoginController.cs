using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TodoApi.Models;
using ToDoAPI.Models;
using ToDoAPI.Response;

namespace ToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly TodoContext _context;

        public LoginController(TodoContext context)
        {
            _context = context;
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("employee-login")]
        public ActionResult EmployeeLogin(AuthModel model)
        {

            if (string.IsNullOrWhiteSpace(model.Mail) || string.IsNullOrWhiteSpace(model.Password))
            {
                return BadRequest();
            }
            var employee = _context.Employees.ToList().Where(e => e.Mail == model.Mail && e.Password == model.Password);



            if (employee.IsNullOrEmpty())
            {
                return BadRequest();
            }
            else return Ok(employee);
            
        }

    }
}
