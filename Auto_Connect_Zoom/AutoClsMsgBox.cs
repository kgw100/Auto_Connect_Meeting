using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace AccessZoom_try
{
    class AutoClsMsgBox
    {
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        public static extern void CloseHandle(IntPtr hProcess);

        System.Threading.Timer _timeoutTimer; //쓰레드 타이머
        string _caption;

        const int WM_CLOSE = 0x0010; //close 명령 

        AutoClsMsgBox(string text, string caption, int timeout)
        {
            _caption = caption;
            _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
                null, timeout, System.Threading.Timeout.Infinite);
            MessageBox.Show(text, caption);
        }

        //생성자 함수
        public static void Show(string text, string caption, int timeout)
        {
            new AutoClsMsgBox(text, caption, timeout);
        }

        //시간이 다되면 close 메세지를 보냄
        void OnTimerElapsed(object state)
        {
            IntPtr mbWnd = FindWindow(null, _caption);
            if (mbWnd != IntPtr.Zero)
                SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            CloseHandle(mbWnd);
            Thread.Sleep(300);
            _timeoutTimer.Dispose();
        }
    }
}
