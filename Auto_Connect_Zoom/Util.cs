using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;
using System.Diagnostics;

namespace AccessZoom_try
{
    class Util
    {
        public static void Kill(string programName)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();

            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;

            process.EnableRaisingEvents = false;
            process.StartInfo = startInfo;
            process.Start();    //프로세스 시작
            process.StandardInput.Write("taskill /f /im " +programName+ Environment.NewLine);     //예를 들어 dir명령어를 입력
            process.StandardInput.Close();


            process.WaitForExit();
            process.Close();
        }
    }
}