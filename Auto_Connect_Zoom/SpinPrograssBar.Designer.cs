namespace AccessZoom_try
{
    partial class SpinPrograssBar
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
            this.spinPB = new MetroFramework.Controls.MetroProgressSpinner();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // spinPB
            // 
            this.spinPB.Location = new System.Drawing.Point(74, 85);
            this.spinPB.Maximum = 100;
            this.spinPB.Name = "spinPB";
            this.spinPB.Size = new System.Drawing.Size(139, 133);
            this.spinPB.Speed = 2F;
            this.spinPB.TabIndex = 0;
            this.spinPB.Value = 60;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(115, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "구동중..";
            // 
            // SpinPrograssBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 270);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.spinPB);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SpinPrograssBar";
            this.Text = "접속해Zoom";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroProgressSpinner spinPB;
        private System.Windows.Forms.Label label1;
    }
}