using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccessZoom_try
{
    class DirectAutoClsMSGBox
    {

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        public static extern void CloseHandle(IntPtr hProcess);

        const int WM_CLOSE = 0x0010; //close 명령 
        DirectAutoClsMSGBox(string text, string caption)
        {
            MessageBox.Show(text, caption);
        }
        //생성자 함수
        public static void Show(string text, string caption)
        {
            new DirectAutoClsMSGBox(text, caption);
        }
        //쓰레드 전용
        public static void Show(MessageBoxObject textObject)
        {
            string text = textObject.text;
            string caption = textObject.caption;
            new DirectAutoClsMSGBox(text, caption);
        }

        //창을 닫음 
        public static void Close(string caption)
        {
                IntPtr mbWnd = FindWindow(null, caption);
            if (mbWnd != IntPtr.Zero)
            {
                SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            }
            CloseHandle(mbWnd);
        }
    }
}
