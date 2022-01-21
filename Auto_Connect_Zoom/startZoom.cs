using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace AccessZoom_try
{
    class startZoom
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindowDC(IntPtr window);
        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern uint GetPixel(IntPtr dc, int x, int y);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int ReleaseDC(IntPtr window, IntPtr dc);

        [DllImport("user32.dll")]
        static extern bool IsWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern string GetWindowText(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hWnd1, IntPtr hWnd2, string lpsz1, string lpsz2);
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, int wParam, string lParam);
        [DllImport("user32.dll")]
        public static extern bool PostMessage(IntPtr hWnd, Int32 Msg, Int32 wParam, Int32 lParam);
        [DllImport("user32.dll")]
        public static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, string lParam);
        [DllImport("user32.dll")]
        public static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SetFocus(IntPtr hWnd);
        [DllImport("kernel32")]
        public static extern void CloseHandle(IntPtr hProcess);

        private const int SW_SHOWNORMAL = 1;
        private const int SW_SHOWMAXIMIZED = 3; //윈도우 창 최대크기로 해주는 값(3이 최대창으로 키워주는 값이다. 윈도우 API에서 지정한 값)
        private const uint WM_MOUSECLICKDOWN = 0x0203;   //왼쪽 마우스 클릭(마우스 눌림)을 나타내는 아스키코드 값
        private const uint WM_MOUSECLICKUP = 0x202; //왼쪽 마우스 클릭(마우스 떼짐)을 나타내는 아스키코드 값
        public void ZoomStart(string id, string pw)
        {

            string name = Environment.UserName; //컴퓨터 사용자 이름을 받아옴
            string fileName = @"C:/Users/" + name + "/AppData/Roaming/Zoom/bin/zoom.exe";
            Process process = new Process();
            process.StartInfo.FileName = fileName;
            process.Start();    //프로세스 시작(ZOOM APP 시작)
            IntPtr hd00;
            IntPtr hd01;
            Stopwatch sw = new Stopwatch();


            while (true)
            {
                hd00 = FindWindow("ZPPTEWndClass", "Zoom 클라우드 회의"); //ZOOM 처음 실행할 때 접속중인 ZOOM 윈도우창
                hd01 = FindWindow("ZPPTMainFrmWndClassEx", "Zoom"); //ZOOM 메인 윈도우창 찾기
                if (hd00 != IntPtr.Zero)
                {
                    continue;
                }

                if (hd01 != IntPtr.Zero)
                {
                    try
                    {
                        CloseHandle(hd00);
                        break;
                    }
                    catch { }

                }
                if (sw.Elapsed.Seconds == 10)
                {
                    MessageBox.Show("네트워크 상황을 다시 체크해주세요.");
                    try
                    {

                        CloseHandle(hd00);
                        CloseHandle(hd01);

                    }
                    catch { }

                    return;
                }

            }

            int height = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height;  //모니터 높이 구하기
            int width = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width;    //모니터 너비 구하기

            if (hd01 != IntPtr.Zero)
            {
                ShowWindow(hd01, SW_SHOWMAXIMIZED); //ZOOM APP 메인화면 최대창으로


                int i;
                int j;
                //int xstart = width / 3;
                int xstart = 400;
                int ystart = height / 3;
                int top = 30;   //홈버튼부분 있는 Y좌표(ZOOM 메인 앱 윗부분 담당)
                int x = 0;
                int y = 0;
                Color test;

                //ZOOM 메인화면에서 홈버튼 찾아서 클릭할 수 있게끔 반복문 돌리기
                //x축은 3, y축은 1씩 증감하는 이유는 홈버튼이 작고 검은색부분을 찾아야 하는데 좌표가 크게 늘어나면 검은색 RGB를 놓칠 수 있다
                //ZOOM 시작할 때 메인화면에서 홈버튼으로 활성화가 안되어있는 경우 홈버튼부분이 검은색 & 회색으로 되어있음(이 좌표의 색깔을 찾아서 탐색)
                for (i = 600; i <= width / 2; i += 3)
                {
                    for (j = top; j <= 60; j += 1)
                    {
                        test = GetColorAt(hd01, i, j);
                        if (test.R == 215 || test.R == 143 || test.R == 122 || test.R == 116)
                        {
                            x = i;
                            y = j;
                            break;
                        }
                        //홈버튼이 파란색인 경우(원래 ZOOM 메인화면에서 홈버튼으로 되어있는 경우)
                        if (test.R == 14 && test.G == 113)
                        {
                            x = i;
                            y = j;
                            break;
                        }
                    }


                }

                PostMessage(hd01, WM_MOUSECLICKDOWN, 0, MAKELPARAM(x, y));
                PostMessage(hd01, WM_MOUSECLICKUP, 0, MAKELPARAM(x, y));

                //SendMessage(hd01, WM_MOUSECLICKDOWN, 0, MAKELPARAM(x, y));  
                //SendMessage(hd01, WM_MOUSECLICKUP, 0, MAKELPARAM(x, y));

                //ZOOM 메인화면에서 참가버튼 좌표 찾을 수 있도록 반복문 돌리기
                for (i = xstart; i <= width / 2; i += 10)
                {
                    for (j = ystart; j <= height / 2; j += 10)
                    {
                        test = GetColorAt(hd01, i, j);
                        if (test.R == 14 && test.G == 113)
                        {
                            x = i;
                            y = j;
                            break;
                        }
                    }
                }


                PostMessage(hd01, WM_MOUSECLICKDOWN, 0, MAKELPARAM(x, y));  //마우스좌표(x, y)인 곳에 마우스클릭(왼쪽마우스 누름)
                PostMessage(hd01, WM_MOUSECLICKUP, 0, MAKELPARAM(x, y));    //마우스좌표(x, y)인 곳에 마우스클릭(왼쪽마우스 뗌)

                //SendMessage(hd01, WM_MOUSECLICKDOWN, 0, MAKELPARAM(x, y));  //마우스좌표(x, y)인 곳에 마우스클릭(왼쪽마우스 누름)
                //SendMessage(hd01, WM_MOUSECLICKUP, 0, MAKELPARAM(x, y));    //마우스좌표(x, y)인 곳에 마우스클릭(왼쪽마우스 뗌)


                Thread.Sleep(1000); //탐색하는 속도가 빠르기때문에 1초간 딜레이를 줘서 흐름대로 진행하게끔 해줌

                IntPtr hd02;
                sw.Restart();
                //회의 참가 윈도우 창을 찾을 때까지 반복문 돌리기
                //그냥 반복문을 안돌리고 프로그램을 실행할 시 참가버튼이 클릭되고 나서 회의참가 윈도우창을 찾지 못하는 경우 발생
                //프로그램 흐름이 너무 빨라 딜레이를 줘도 안되는 경우가 발생해서 찾을 때까지 반복문 돌리는 것임
                while (true)
                {
                    hd02 = FindWindow("zWaitHostWndClass", null);   //회의참가 윈도우창 찾기
                    if (hd02 != IntPtr.Zero)
                    {
                        break;
                    }
                    if (sw.Elapsed.Seconds == 5)
                    {
                        MessageBox.Show("네트워크 상황을 다시 체크해주세요.");
                        try
                        {

                            CloseHandle(hd01);
                            CloseHandle(hd02);

                        }
                        catch { }
                        return;

                    }

                }

                if (hd02 != IntPtr.Zero)
                {
                    //IntPtr hd = FindWindow("zWaitHostWndClass", "Zoom");
                    sw.Restart();
                    IntPtr hd03;
                    //회의 참가 윈도우 창의 자식클래스(회의 ID 입력부분)를 찾을 때까지 반복문 돌리기
                    //그냥 반복문을 안돌리고 자식클래스를 찾을 시 ZOOM 앱이 켜진상태(이미 실행이 된 상태)에서 프로그램을 실행할 시
                    //프로그램 흐름이 너무 빨라 자식클래스를 찾지 못해서 ID를 못넘겨주는 버그가 발생
                    //sleep(1000) 1초 딜레이를 줘도 안되는 경우가 발생해서 찾을 때까지 반복문 돌리는 것임
                    while (true)
                    {
                        hd03 = FindWindowEx(hd02, IntPtr.Zero, "MeetingNumberEditWnd", "");
                        if (hd03 != IntPtr.Zero)
                        {
                            break;
                        }
                        if (sw.Elapsed.Seconds == 5)
                        {
                            MessageBox.Show("네트워크 상황을 다시 체크해주세요.");

                            try
                            {

                                CloseHandle(hd01);
                                CloseHandle(hd02);
                                CloseHandle(hd03);
                            }
                            catch { }
                            return;
                        }
                    }

                    //Thread.Sleep(1000); //탐색하는 속도가 빠르기때문에 1초간 딜레이를 줘서 흐름대로 진행하게끔 해줌
                    if (hd03 != IntPtr.Zero)
                    {
                        SendMessage(hd03, 0x00c, 0, id); //textbox에 입력받은 회의ID 입력값을 보내준다
                        PostMessage(hd03, 0x0100, 0x0D, 0x1C001);   //엔터값(키보드에서 ENTER) 보내기
                        //Thread.Sleep(1000); //탐색하는 속도가 빠르기때문에 1초간 딜레이를 줘서 흐름대로 진행하게끔 해줌
                    }

                    IntPtr hd04;
                    sw.Restart();

                    while (true)
                    {
                        if (pw == "")
                        {
                            break;
                        }
                        hd04 = FindWindow("zWaitHostWndClass", "회의 비밀번호 입력");    //회의 비밀번호 입력 윈도우 창 찾기
                        if (hd04 != IntPtr.Zero)
                        {
                            SetForegroundWindow(hd04);  //회의비밀번호 입력창을 최상위 화면으로 활성화 시킴
                            SendKey(pw, 100);   //윈폼에 적은 회의 PW값을 보내주기
                            PostMessage(hd04, 0x0100, 0x0D, 0x1C001);   //엔터값(키보드에서 ENTER) 보내기
                            PostMessage(hd04, 0x0100, 0x0D, 0x1C001);   //총 엔터값을 두번 보내서 엔터값이 안눌린경우를 방지
                            try
                            {
                                CloseHandle(hd04);
                            }
                            catch { }
                            break;
                        }
                        if (sw.Elapsed.Seconds == 5)
                        {
                            MessageBox.Show("네트워크 상황을 다시 체크해주세요.");


                            try
                            {
                                CloseHandle(hd01);
                                CloseHandle(hd02);
                                CloseHandle(hd03);
                                CloseHandle(hd04);
                            }
                            catch { }
                            return;
                        }

                    }
                    try
                    {
                        CloseHandle(hd01);
                        CloseHandle(hd02);
                        CloseHandle(hd03);
                    }
                    catch { }
                }
            }
        }

        private int MAKELPARAM(int p, int p_2)
        {
            return ((p_2 << 16) | (p & 0xFFFF));
        }

        //RGB 색상 정보 가져오기 메서드
        private static Color GetColorAt(IntPtr desk, int x, int y)
        {

            IntPtr dc = GetWindowDC(desk);
            int a = (int)GetPixel(dc, x, y);
            ReleaseDC(desk, dc);
            return Color.FromArgb(255, (a >> 0) & 0xff, (a >> 8) & 0xff, (a >> 16) & 0xff);
        }

        private void SendKey(string keyword, int delay)
        {
            Thread.Sleep(500);  //0.5초 딜레이를 줘야 버그없이 PW값을 SendKeys()메서드를 이용해 정확하게 다 보낼 수 있다.
            char[] charArray = keyword.ToCharArray();
            foreach (char c in charArray)
            {
                SendKeys.SendWait(c.ToString());
                Thread.Sleep(delay);
            }
        }
    }
}
