namespace AccessZoom_try
{
    partial class GetCourseProgressbar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pb = new MetroFramework.Controls.MetroProgressBar();
            this.lbInfo = new MetroFramework.Controls.MetroLabel();
            this.lbwait = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // pb
            // 
            this.pb.Location = new System.Drawing.Point(82, 166);
            this.pb.MarqueeAnimationSpeed = 50;
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(359, 30);
            this.pb.TabIndex = 0;
            // 
            // lbInfo
            // 
            this.lbInfo.AutoSize = true;
            this.lbInfo.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lbInfo.Location = new System.Drawing.Point(138, 90);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(150, 25);
            this.lbInfo.TabIndex = 1;
            this.lbInfo.Text = "프로세스 진행도";
            // 
            // lbwait
            // 
            this.lbwait.AutoSize = true;
            this.lbwait.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lbwait.Location = new System.Drawing.Point(164, 129);
            this.lbwait.Name = "lbwait";
            this.lbwait.Size = new System.Drawing.Size(192, 25);
            this.lbwait.TabIndex = 2;
            this.lbwait.Text = "잠시만 기다려주세요.";
            // 
            // GetCourseProgressbar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 263);
            this.Controls.Add(this.lbwait);
            this.Controls.Add(this.lbInfo);
            this.Controls.Add(this.pb);
            this.Name = "GetCourseProgressbar";
            this.Text = "안내";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroProgressBar pb;
        private MetroFramework.Controls.MetroLabel lbInfo;
        private MetroFramework.Controls.MetroLabel lbwait;
    }
}