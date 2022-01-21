namespace AccessZoom_try
{
    partial class StartZoomForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbMID = new System.Windows.Forms.TextBox();
            this.tbMPW = new System.Windows.Forms.TextBox();
            this.cbNonPW = new System.Windows.Forms.CheckBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(67, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "강의 ID:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(60, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "강의 PW:";
            // 
            // tbMID
            // 
            this.tbMID.Location = new System.Drawing.Point(121, 75);
            this.tbMID.Name = "tbMID";
            this.tbMID.Size = new System.Drawing.Size(182, 21);
            this.tbMID.TabIndex = 3;
            this.tbMID.TextChanged += new System.EventHandler(this.tbMID_TextChanged);
            // 
            // tbMPW
            // 
            this.tbMPW.Location = new System.Drawing.Point(121, 110);
            this.tbMPW.Name = "tbMPW";
            this.tbMPW.Size = new System.Drawing.Size(182, 21);
            this.tbMPW.TabIndex = 4;
            // 
            // cbNonPW
            // 
            this.cbNonPW.AutoSize = true;
            this.cbNonPW.Location = new System.Drawing.Point(322, 115);
            this.cbNonPW.Name = "cbNonPW";
            this.cbNonPW.Size = new System.Drawing.Size(100, 16);
            this.cbNonPW.TabIndex = 5;
            this.cbNonPW.Text = "패스워드 없음";
            this.cbNonPW.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(322, 69);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(71, 31);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "시작";
            this.btnStart.UseVisualStyleBackColor = true;
            // 
            // StartZoomForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 178);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.cbNonPW);
            this.Controls.Add(this.tbMPW);
            this.Controls.Add(this.tbMID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Name = "StartZoomForm";
            this.Text = "수동 시작";
            this.Load += new System.EventHandler(this.MannualStart_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbMID;
        private System.Windows.Forms.TextBox tbMPW;
        private System.Windows.Forms.CheckBox cbNonPW;
        private System.Windows.Forms.Button btnStart;
    }
}