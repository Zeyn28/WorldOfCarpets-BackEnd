using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Python.Runtime;
using IronPython.Hosting;
using ToDoAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.IO;


using System.ComponentModel;

namespace ToDoAPI.Models
{
     public class PythonClass
    {

        public static void PythonOperation()
        {
            //Initialize();
            PythonEngine.Initialize();
          


        }
        
        
        //public static void Initialize()
        //{


        //    string pythonDll = @"C:\Users\Zeynep Aygün\AppData\Local\Programs\Python\Python39\python39.dll";
        //    Environment.SetEnvironmentVariable("PYTHONNET_PYDLL", pythonDll);
        //    PythonEngine.Initialize();


        //}

        //public void Execute()

        //{

        //}


        //*******************************
        //private void run_cmd(string cmd, string args)
        //{
        //    ProcessStartInfo start = new ProcessStartInfo();
        //    start.FileName = "C:/Users/Zeynep Aygün/AppData/Local/Programs/Python/Python39/python.exe";//cmd is full path to python.exe
        //    start.Arguments = "C:/AndroidProjects/flutter_application_3/lib/model.py";
        //    start.UseShellExecute = false;
        //    start.RedirectStandardOutput = true;
        //    using (Process process = Process.Start(start))
        //    {
        //        using (StreamReader reader = process.StandardOutput)
        //        {
        //            string result = reader.ReadToEnd();
        //            Console.Write(result);
        //        }
        //    }
        //}



    }
}
