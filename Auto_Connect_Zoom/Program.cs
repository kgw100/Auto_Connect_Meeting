using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace AccessZoom_try
{
    static class Program
    {
        public static ApplicationContext ac = new ApplicationContext();

        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool bnew;
            Mutex mutex = new Mutex(true, "AccessZoom_try", out bnew);
            if (bnew)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                FileInfo courseFile = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"courseList.json");

                if (YLoginForm.isAccountInfo() && !courseFile.Exists)
                {
                    MessageBox.Show("강의 파일이 손상되었습니다. 다시 로그인이 필요합니다.");
                    AppConfiguration.RemoveAppConfig("id");
                    AppConfiguration.RemoveAppConfig("password");
                    Application.Restart();
                }
                mutex.ReleaseMutex();
                if (YLoginForm.isAccountInfo())
                {
                    LectureManagerForm LMF = new LectureManagerForm();
                    ac.MainForm = LMF;
                    Application.Run(ac);
                }
                else
                {
                    ac.MainForm = new YLoginForm();
                    Application.Run(ac);

                }
           
            

                ////로그인하지않고 그냥 종료시 충돌 처리
                //if (YLoginForm.isAccountInfo())
                //{
                //    MessageBox.Show("안녕");
                //    Application.Run(new LectureManagerForm());
                //    //남아있는 driver 메모리 소거
                //    Util.Kill("chromedriver.exe");
                //    YLoginForm.driver.Quit();

                //    //강의 정보 손상시


                //}
                //else// 남아있는 driver 메모리 소거
                //    Util.Kill("chromedriver.exe");

            }
            else
            {
                MessageBox.Show("프로그램이 이미 실행중입니다!");
                Application.Exit();
            }
            

        }
    }
}
