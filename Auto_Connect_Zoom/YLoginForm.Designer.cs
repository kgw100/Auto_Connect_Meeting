namespace AccessZoom_try
{
    partial class YLoginForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbStdID = new System.Windows.Forms.TextBox();
            this.tbStdPW = new System.Windows.Forms.TextBox();
            this.btnLogin = new MetroFramework.Controls.MetroTile();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbStdID
            // 
            this.tbStdID.Location = new System.Drawing.Point(228, 103);
            this.tbStdID.Name = "tbStdID";
            this.tbStdID.Size = new System.Drawing.Size(169, 25);
            this.tbStdID.TabIndex = 3;
            // 
            // tbStdPW
            // 
            this.tbStdPW.Location = new System.Drawing.Point(228, 153);
            this.tbStdPW.Name = "tbStdPW";
            this.tbStdPW.Size = new System.Drawing.Size(169, 25);
            this.tbStdPW.TabIndex = 4;
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnLogin.CustomBackground = true;
            this.btnLogin.CustomForeColor = true;
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(464, 114);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(106, 56);
            this.btnLogin.TabIndex = 8;
            this.btnLogin.Text = "로그인";
            this.btnLogin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("08서울남산체 B", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(17, 26);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(162, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "접속해Zoom";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("굴림", 10F);
            this.label2.Location = new System.Drawing.Point(162, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "학번:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("굴림", 10F);
            this.label3.Location = new System.Drawing.Point(128, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 17);
            this.label3.TabIndex = 10;
            this.label3.Text = "비밀번호:";
            // 
            // YLoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 303);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.tbStdPW);
            this.Controls.Add(this.tbStdID);
            this.Controls.Add(this.label1);
            this.DisplayHeader = false;
            this.Font = new System.Drawing.Font("굴림", 9F);
            this.Name = "YLoginForm";
            this.Padding = new System.Windows.Forms.Padding(23, 35, 23, 22);
            this.Style = MetroFramework.MetroColorStyle.Blue;
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbStdID;
        private System.Windows.Forms.TextBox tbStdPW;
        private MetroFramework.Controls.MetroTile btnLogin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

