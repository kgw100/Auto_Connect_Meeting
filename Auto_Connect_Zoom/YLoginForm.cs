using MetroFramework.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text;

using System.Drawing;

namespace AccessZoom_try
{
    public delegate void DataPushEventHandler(int value);
    public delegate void DataGetEventHandler(string item);

    public partial class YLoginForm : MetroForm, IDisposable
    {
        public DataPushEventHandler dataSendEvent;
        public static int setPBMax = 0;
        public static int prograssCnt = 0;
        public static ChromeDriver driver=null;
        SpinPrograssBar spinPrograssBar = new SpinPrograssBar();

        public YLoginForm()
        {
            ////로그인 확인
            if (isAccountInfo())
            {
                //LectureManagerForm LMF = new LectureManagerForm();
                //Program.ac.MainForm = LMF;
                //this.Visible = false;
                //this.Close();
                //InitializeComponent();
            }
            else
            { 
                ////드라이버 로드
                Thread thread_loadDriver = new Thread(() => GetData.driver_Load(ref driver));
                thread_loadDriver.Start();
                InitializeComponent();
                btnLogin.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
                tbStdPW.GotFocus += new EventHandler(tbPw_OnGotFocus);
                tbStdPW.KeyDown += new KeyEventHandler(tbPw_KeyDown);
                //이벤트 핸드러 등록 
                //this.FormClosed += new FormClosedEventHandler(formClosing);
                btnLogin.MouseMove += new System.Windows.Forms.MouseEventHandler(btn_MouseMove);
                btnLogin.MouseLeave += new EventHandler(btn_MouseLeave);
                this.FormClosed += new FormClosedEventHandler(formClosed);
                Application.Run(new SpinPrograssBar());
              
            }

        }

        private void tbPw_OnGotFocus(object sender, EventArgs e)
        {
            tbStdPW.UseSystemPasswordChar = true;
            // 안내 상자가 보이기 위해 입력을 바로할 수 있도록 했던 최상위 포커싱을 해제한다.
            this.TopMost = false;
        }

        private void tbPw_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
                btnLogin_Click(sender, e);
        }
        private void tbStdPW_OnGotFocus(object sender, EventArgs e)
        {
            tbStdPW.UseSystemPasswordChar = true;
        }
        private void tbStdPW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
                btnLogin_Click(sender, e);
        }
        // 강의 가져오기 로그인
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string ID = tbStdID.Text;
            string PW = tbStdPW.Text;
            if (ID.Equals(""))
            {
                MessageBox.Show("학번란에 공백이 입력되었습니다. 다시 입력하세요.", "경고");
                return;
            }
            else if (PW.Equals(""))
            {
                MessageBox.Show("PW란에 공백이 입력되었습니다. 다시 입력하세요.", "경고");
                return;
            }

            bool isLogin = false;
            GetData getData = new GetData();
            Thread thread_getCourseData = new Thread(() => getData.getCourseData(tbStdID, tbStdPW, ref isLogin));
            thread_getCourseData.Start();

            //GetCourseProgressbar getCourseProgressbar = new GetCourseProgressbar();
            //getCourseProgressbar.Show();
            //멀티 스레드를 사용해야만 함 
            //processBar 시작     
            Thread thread_getCoursePB = new Thread(() => {
                GetCourseProgressbar getCourseProgressbar = new GetCourseProgressbar();
                Application.Run(getCourseProgressbar);
            });
            thread_getCoursePB.Start();
            thread_getCourseData.Join();
            thread_getCoursePB.Abort();
            thread_getCoursePB.Join();
            //로그인이 성공하고 작업이 잘완료됐을 때

            if (isLogin)
            {
                AppConfiguration.SetAppConfig("id", ID);
                AppConfiguration.SetAppConfig("password", Security.Encrypt(PW,ID));
                //using (StreamWriter file = new StreamWriter(@"ID.txt"))
                //{
                //    file.WriteLine(ID);
                //}
                //using (StreamWriter file = new StreamWriter(@"PW.txt"))
                //{
                //    file.WriteLine(Security.Encrypt(PW, ID));
                //}
                LectureManagerForm LMF = new LectureManagerForm();
                Program.ac.MainForm = LMF;
                this.Visible = false;
                LMF.ShowDialog(this);
                
             
                //this.Close();



            }
            ////로그인 실패시 로그인 폼을 유지하고 쓰레드 종료
            else
                thread_getCourseData.Interrupt();
        }
        public static bool isAccountInfo()
        {
            if (AppConfiguration.IsExistAppConfig("id") && AppConfiguration.IsExistAppConfig("password"))
            {
                return true;
            }
            else
                return false;
            //string idFile = "ID.txt";
            //FileInfo fileInfo = new FileInfo(idFile);
            ////ID파일이 있는지 확인해서 로그인 여부를 판단
            //if (fileInfo.Exists)
            //    return true;
            //else
            //    return false;
        }
        private void formClosing(object sender, FormClosedEventArgs e)
        {
            if (driver != null)
                driver.Quit();
        }
        private void formClosed(object sender, FormClosedEventArgs e)
        {
            if (YLoginForm.driver != null)
                YLoginForm.driver.Quit();
            if (!YLoginForm.isAccountInfo())
            {
                Application.ExitThread();
                Environment.Exit(0);
            }

        }
        private void btn_MouseMove(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.BackColor = Color.DodgerBlue;
        }
        private void btn_MouseLeave(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.BackColor = Color.DeepSkyBlue;
        }
        ~YLoginForm()
        {
            LectureManagerForm LMF = new LectureManagerForm();
            Program.ac.MainForm = LMF;
            this.Visible = false;
            LMF.ShowDialog(this);
        }
    }
}
