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
    public partial class StartZoomForm : MetroForm, IDisposable
    {
      

        public StartZoomForm()
        {
            InitializeComponent();
            tbMID.MaxLength = 13;
            tbMID.KeyUp += new KeyEventHandler(tbMID_KeyUp);
            btnStart.Click += new EventHandler(btnStart_Click);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (tbMID.Text == "")
            {
                MessageBox.Show("회의 ID를 입력해주세요.");
                return;
            }
            if (tbMPW.Text == "")
            {
                if (!cbNonPW.Checked)
                {
                    MessageBox.Show("회의 PW를 입력해주세요.");
                    return;
                }
            }
             startZoom sz = new startZoom();
            {
                Thread thread_ZoomStart = new Thread(() => sz.ZoomStart(tbMID.Text, tbMPW.Text));
                thread_ZoomStart.Start();
            }
           
           
        }

       

        private string autoHyphen(string id)
        {
            string tmpId = id.Replace("-", "");
            string id1 = string.Empty;
            string id2 = string.Empty;
            string id3 = string.Empty;
            string id_total = string.Empty;
            if (cbNonPW.Checked)
            {
                if (tmpId.Length >= 2 && tmpId.Length < 8)
                {
                    if (tmpId.Length == 3)
                    {
                        id_total = tmpId + "-";
                    }
                    else if (tmpId.Length > 3 && tmpId.Length < 6)
                    {
                        id1 = tmpId.Substring(0, 3);
                        id2 = tmpId.Substring(3, tmpId.Length - 3);
                        id_total = id1 + "-" + id2;
                    }
                    else if (tmpId.Length > 3 && tmpId.Length > 6)
                    {
                        id1 = tmpId.Substring(0, 3);
                        id2 = tmpId.Substring(3, 3);
                        id3 = tmpId.Substring(6, tmpId.Length - 6);
                        id_total = id1 + "-" + id2 + "-" + id3;
                    }
                    else
                    {
                        id_total = id;
                    }
                }
                else if (tmpId.Length == 8)
                {
                    id1 = tmpId.Substring(0, 3);
                    id2 = tmpId.Substring(3, 3);
                    id3 = tmpId.Substring(6, 2);
                    id_total = id1 + "-" + id2 + "-" + id3;
                }
                else if (tmpId.Length == 9)
                {
                    id1 = tmpId.Substring(0, 3);
                    id2 = tmpId.Substring(3, 3);
                    id3 = tmpId.Substring(6, 3);
                    id_total = id1 + "-" + id2 + "-" + id3;
                }
                else if (tmpId.Length == 10)
                {
                    id1 = tmpId.Substring(0, 3);
                    id2 = tmpId.Substring(3, 3);
                    id3 = tmpId.Substring(6, 4);
                    id_total = id1 + "-" + id2 + "-" + id3;
                }
                else
                {
                    id_total = tmpId;
                }
            }
            else
            {
                if (tmpId.Length >= 2 && tmpId.Length < 8)
                {
                    if (tmpId.Length == 3)
                    {
                        id_total = tmpId + "-";
                    }
                    else if (tmpId.Length > 3 && tmpId.Length < 6)
                    {
                        id1 = tmpId.Substring(0, 3);
                        id2 = tmpId.Substring(3, tmpId.Length - 3);
                        id_total = id1 + "-" + id2;
                    }
                    else if (tmpId.Length > 3 && tmpId.Length > 6)
                    {
                        id1 = tmpId.Substring(0, 3);
                        id2 = tmpId.Substring(3, 3);
                        id3 = tmpId.Substring(6, tmpId.Length - 6);
                        id_total = id1 + "-" + id2 + "-" + id3;
                    }
                    else
                    {
                        id_total = id;
                    }
                }
                else if (tmpId.Length == 8)
                {
                    id1 = tmpId.Substring(0, 4);
                    id2 = tmpId.Substring(4, 4);
                    id_total = id1 + "-" + id2;
                }
                else if (tmpId.Length == 9)
                {
                    id1 = tmpId.Substring(0, 3);
                    id2 = tmpId.Substring(3, 4);
                    id3 = tmpId.Substring(7, 2);
                    id_total = id1 + "-" + id2 + "-" + id3;
                }
                else if (tmpId.Length == 10)
                {
                    id1 = tmpId.Substring(0, 3);
                    id2 = tmpId.Substring(3, 3);
                    id3 = tmpId.Substring(6, 4);
                    id_total = id1 + "-" + id2 + "-" + id3;
                }
                else if (tmpId.Length == 11)
                {
                    id1 = tmpId.Substring(0, 3);
                    id2 = tmpId.Substring(3, 4);
                    id3 = tmpId.Substring(7, 4);
                    id_total = id1 + "-" + id2 + "-" + id3;
                }
                else
                {
                    id_total = tmpId;
                }
            }

            return id_total;
        }

        private void tbMID_KeyUp(object sender, KeyEventArgs e)
        {

            tbMID.Text = autoHyphen(tbMID.Text);
            tbMID.SelectionStart = tbMID.Text.Length;
        }

        private void MannualStart_Load(object sender, EventArgs e)
        {

        }

        private void tbMID_TextChanged(object sender, EventArgs e)
        {
            tbMID.Text = tbMID.Text.Replace(" ", "-");
        }
    }
}
