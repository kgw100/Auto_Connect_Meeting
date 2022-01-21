using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using System.Threading;
using OpenQA.Selenium.Chrome;
using System.IO;

namespace AccessZoom_try
{
    public partial class SpinPrograssBar : MetroForm, IDisposable
    {
        delegate void SetFormCloseCallback();

        public SpinPrograssBar()
        {
                InitializeComponent();
                this.TopMost = true;
                //this.OnShown += new EventHandler(checkDriverLoad);
                Application.Idle += checkDriverLoad;
            
        }
        private void checkDriverLoad(object sender, EventArgs e)
        {

            Thread thread_CDL = new Thread(thread_checkDriverLoad);
            thread_CDL.Start();
        }
        private void thread_checkDriverLoad()
        {
            try
            {
                while (true)
                {
                    if (YLoginForm.driver != null)
                    {
                        this.Close();
                        break;

                    }
                    else
                    {
                        Thread.Sleep(80);
                    }
                }

            }
            catch (Exception ex)
            {
            }
        }
        private void SetClose()
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.InvokeRequired)
            {
                SetFormCloseCallback d = new SetFormCloseCallback(SetClose);
                this.Invoke(d, new object[] { });
            }
            else
            {
                if (YLoginForm.driver != null)
                {
                    this.Close();
                    //break;
                }
                else
                {
                    Thread.Sleep(500);
                }
            }
        }

    }
}
