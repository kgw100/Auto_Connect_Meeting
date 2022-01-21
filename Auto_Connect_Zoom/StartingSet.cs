using MetroFramework.Forms;
using System;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Configuration;
using IWshRuntimeLibrary;
using System.IO;

namespace AccessZoom_try
{
    public partial class StartingSet : MetroForm, IDisposable
    {
        private string initial_Num; // 사용자가 설정한 값 불러오기(시작 프로그램으로 설정 여부)

        public StartingSet()
        {
            InitializeComponent();
            init();
            checkBox_start.CheckedChanged += new EventHandler(checkBox1_CheckedChanged);

        }
        private void init()
        {

            if (AppConfiguration.GetAppConfig("inital_startup") == "1")
            {
                checkBox_start.Checked = true;
            }
            if (AppConfiguration.GetAppConfig("shortCut") == "1")
            {
                // 바로가기를 사용자가 삭제한 경우
                string shortCutPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\접속해Zoom.lnk";
                if (!new FileInfo(shortCutPath).Exists)
                {
                    AppConfiguration.SetAppConfig("shortCut", "0");
                    btnMannual.Enabled = true;
                }
                else
                {
                    btnMannual.Enabled = false;
                    btnMannual.Text = "바로가기 있음!";
                }
            }
        }
        WshShell wsh;
        private void StartingSet_Load(object sender, EventArgs e)   // 사용자가 설정한 값 불러오기(시작 프로그램으로 설정 여부)
        {
            this.initial_Num = ConfigurationManager.AppSettings["initial_startup"];
            
            if (initial_Num == "1") // 사용자가 시작프로그램으로 사용하겠다고 체크 한 경우
            {
                checkBox_start.Checked = true;
            }

            if (initial_Num == "0") // 시작프로그램으로 설정 x
            {
                checkBox_start.Checked = false;
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox_start.Checked == true && AppConfiguration.GetAppConfig("initial_startup") == "0") // 시작 프로그램으로 설정되었을 경우 -> 시작프로그램으로 등록
            {
                try
                {
                    // 시작프로그램의 레지스트리 관련
                    RegistryKey startup = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true); // C# 레지스트리 접근


                    startup.SetValue("Test app", Application.ExecutablePath.ToString());

                    // App.config 변경
                    AppConfiguration.SetAppConfig("initial_startup", "1");
                    MessageBox.Show("시작프로그램에 등록되었습니다.");
                }

                catch
                {
                    MessageBox.Show("Add startup Fail");
                }
            }

            if (checkBox_start.Checked == false) // 시작프로그램 설정 x or 시작프로그램 설정 해제
            {
                try
                {
                    RegistryKey startup = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true); // C# 레지스트리 접근

                    // 레지스트리값 제거
                    startup.DeleteValue("Test app", false); // 생성했던 레지스트리 제거

                    AppConfiguration.SetAppConfig("initial_startup", "0");

                    MessageBox.Show("시작프로그램에서 제거되었습니다.");
                }

                catch
                {
                    MessageBox.Show("Remove Startup Fail");
                }
            }
        }

        private void BtnMannual_Click(object sender, EventArgs e)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); // 데스크탑 내 바탕화면 디렉토리 경로
         
            try
            {
                wsh = new WshShell();
                IWshRuntimeLibrary.IWshShortcut myShortCut;
                myShortCut = (IWshRuntimeLibrary.IWshShortcut)wsh.CreateShortcut(path + "\\접속해Zoom.lnk"); // 바로가기에 대한 주소 생성

                myShortCut.TargetPath = Application.ExecutablePath; // 원본 파일의 경로
                myShortCut.Description = "ShortCut test"; // 설명 부분
                myShortCut.IconLocation = Application.StartupPath + @"\icon.ico"; // 바로가기 아이콘 지정

                myShortCut.Save();

                btnMannual.Text = "해줌!";
                btnMannual.Enabled = false;
                AppConfiguration.SetAppConfig("shortCut", "1");

                MessageBox.Show("바로가기가 생성되었습니다.");

            }

            catch (Exception ex)
            {
                MessageBox.Show("Fail");
            }
        }
    }
}
