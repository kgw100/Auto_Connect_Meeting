namespace AccessZoom_try
{
    partial class StartingSet
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
            this.checkBox_start = new System.Windows.Forms.CheckBox();
            this.btnMannual = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // checkBox_start
            // 
            this.checkBox_start.AutoSize = true;
            this.checkBox_start.Font = new System.Drawing.Font("굴림체", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.checkBox_start.Location = new System.Drawing.Point(80, 101);
            this.checkBox_start.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBox_start.Name = "checkBox_start";
            this.checkBox_start.Size = new System.Drawing.Size(165, 51);
            this.checkBox_start.TabIndex = 0;
            this.checkBox_start.Text = "해줌!";
            this.checkBox_start.UseVisualStyleBackColor = true;
            // 
            // btnMannual
            // 
            this.btnMannual.Location = new System.Drawing.Point(65, 186);
            this.btnMannual.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnMannual.Name = "btnMannual";
            this.btnMannual.Size = new System.Drawing.Size(195, 51);
            this.btnMannual.TabIndex = 12;
            this.btnMannual.Text = "바로가기도 만들어줌?";
            this.btnMannual.Click += new System.EventHandler(this.BtnMannual_Click);
            // 
            // StartingSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 266);
            this.Controls.Add(this.btnMannual);
            this.Controls.Add(this.checkBox_start);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "StartingSet";
            this.Padding = new System.Windows.Forms.Padding(23, 75, 23, 25);
            this.Text = "시작 프로그램 등록";
            this.Load += new System.EventHandler(this.StartingSet_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox_start;
        private MetroFramework.Controls.MetroButton btnMannual;
    }
}