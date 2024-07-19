using System;
using System.Diagnostics;
using System.IO;
using System.Threading;


namespace ToDoAPI.Models
{
    public class CmdService:IDisposable
    {
        private Process cmdProcess;
        private StreamWriter streamWriter;//StreamWriter, girişi komut istemine yönlendirmekten sorumlu
        private AutoResetEvent outputWaitHandle;
        private string cmdOutput;


        public CmdService(string cmdPath)
        {
            cmdProcess = new Process();
            outputWaitHandle= new AutoResetEvent(false);
            cmdOutput= String.Empty;

            ProcessStartInfo processStartInfo= new ProcessStartInfo();
            processStartInfo.FileName= cmdPath;
            processStartInfo.UseShellExecute= false;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardInput= true;
            processStartInfo.CreateNoWindow= true;

            cmdProcess.OutputDataReceived += CmdProcess_OutputDataReceived;

            cmdProcess.StartInfo= processStartInfo;
            cmdProcess.Start();

            streamWriter = cmdProcess.StandardInput;
            cmdProcess.BeginOutputReadLine();

        }

        public string getCmdOutput()
        {
            return cmdOutput;
        }

        public void Dispose()
        {
            cmdProcess.Close();
            cmdProcess.Dispose();
            streamWriter.Dispose();
            streamWriter.Close();

        }

        public string ExecuteCommand(string command)
        {
            cmdOutput=string.Empty;
            streamWriter.WriteLine("echo end");
            outputWaitHandle.WaitOne();
            return cmdOutput;



        }

        private void CmdProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {

            if (e.Data == null || e.Data == "end")
                outputWaitHandle.Set();
            else
                cmdOutput += e.Data + Environment.NewLine;



        }






    }

}
