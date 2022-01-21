namespace AccessZoom_try
{
    partial class LectureManagerForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvLec = new System.Windows.Forms.DataGridView();
            this.dgvCourseId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvLecture = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnMeetModify = new MetroFramework.Controls.MetroTile();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.tbZPW = new System.Windows.Forms.TextBox();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.tbZID = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnMannual = new MetroFramework.Controls.MetroTile();
            this.lbLecName = new MetroFramework.Controls.MetroLabel();
            this.lbLecDay = new MetroFramework.Controls.MetroLabel();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.lbNow = new MetroFramework.Controls.MetroLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnAuto = new MetroFramework.Controls.MetroTile();
            this.btnLogout = new MetroFramework.Controls.MetroTile();
            this.btnStartSet = new MetroFramework.Controls.MetroTile();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLec)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvLec
            // 
            this.dgvLec.AllowUserToAddRows = false;
            this.dgvLec.AllowUserToResizeColumns = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLec.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvLec.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLec.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvCourseId,
            this.dgvLecture,
            this.dgvTime});
            this.dgvLec.Location = new System.Drawing.Point(15, 198);
            this.dgvLec.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvLec.Name = "dgvLec";
            this.dgvLec.ReadOnly = true;
            this.dgvLec.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvLec.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvLec.RowTemplate.Height = 23;
            this.dgvLec.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLec.Size = new System.Drawing.Size(705, 336);
            this.dgvLec.TabIndex = 2;
            this.dgvLec.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvLec_CellClick);
            this.dgvLec.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLec_CellContentClick);
            // 
            // dgvCourseId
            // 
            this.dgvCourseId.HeaderText = "학정번호";
            this.dgvCourseId.Name = "dgvCourseId";
            this.dgvCourseId.ReadOnly = true;
            // 
            // dgvLecture
            // 
            this.dgvLecture.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvLecture.HeaderText = "강의명";
            this.dgvLecture.Name = "dgvLecture";
            this.dgvLecture.ReadOnly = true;
            // 
            // dgvTime
            // 
            this.dgvTime.HeaderText = "강의 시간";
            this.dgvTime.Name = "dgvTime";
            this.dgvTime.ReadOnly = true;
            this.dgvTime.Width = 174;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnMeetModify);
            this.groupBox1.Controls.Add(this.metroLabel3);
            this.groupBox1.Controls.Add(this.tbZPW);
            this.groupBox1.Controls.Add(this.metroLabel2);
            this.groupBox1.Controls.Add(this.tbZID);
            this.groupBox1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox1.Location = new System.Drawing.Point(572, 42);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(321, 148);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "강의 접속 정보";
            // 
            // btnMeetModify
            // 
            this.btnMeetModify.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnMeetModify.CustomBackground = true;
            this.btnMeetModify.CustomForeColor = true;
            this.btnMeetModify.ForeColor = System.Drawing.Color.White;
            this.btnMeetModify.Location = new System.Drawing.Point(140, 108);
            this.btnMeetModify.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnMeetModify.Name = "btnMeetModify";
            this.btnMeetModify.Size = new System.Drawing.Size(166, 31);
            this.btnMeetModify.TabIndex = 21;
            this.btnMeetModify.Text = "강의 접속 정보 저장";
            this.btnMeetModify.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnMeetModify.TileTextFontSize = MetroFramework.MetroTileTextSize.Small;
            this.btnMeetModify.Click += new System.EventHandler(this.btnMeetModify_Click);
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(7, 72);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(67, 20);
            this.metroLabel3.TabIndex = 9;
            this.metroLabel3.Text = "회의 PW:";
            // 
            // tbZPW
            // 
            this.tbZPW.Location = new System.Drawing.Point(82, 72);
            this.tbZPW.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbZPW.Name = "tbZPW";
            this.tbZPW.Size = new System.Drawing.Size(181, 25);
            this.tbZPW.TabIndex = 8;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(17, 30);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(59, 20);
            this.metroLabel2.TabIndex = 7;
            this.metroLabel2.Text = "회의 ID:";
            // 
            // tbZID
            // 
            this.tbZID.Location = new System.Drawing.Point(82, 30);
            this.tbZID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbZID.Name = "tbZID";
            this.tbZID.Size = new System.Drawing.Size(181, 25);
            this.tbZID.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnMannual);
            this.groupBox2.Controls.Add(this.lbLecName);
            this.groupBox2.Controls.Add(this.lbLecDay);
            this.groupBox2.Controls.Add(this.metroLabel5);
            this.groupBox2.Controls.Add(this.metroLabel4);
            this.groupBox2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox2.Location = new System.Drawing.Point(263, 42);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(303, 148);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "강의 정보";
            // 
            // btnMannual
            // 
            this.btnMannual.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnMannual.CustomBackground = true;
            this.btnMannual.CustomForeColor = true;
            this.btnMannual.ForeColor = System.Drawing.Color.White;
            this.btnMannual.Location = new System.Drawing.Point(148, 108);
            this.btnMannual.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnMannual.Name = "btnMannual";
            this.btnMannual.Size = new System.Drawing.Size(129, 31);
            this.btnMannual.TabIndex = 20;
            this.btnMannual.Text = "강의 수동 시작";
            this.btnMannual.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnMannual.TileTextFontSize = MetroFramework.MetroTileTextSize.Small;
            this.btnMannual.Click += new System.EventHandler(this.btnMannual_Click);
            // 
            // lbLecName
            // 
            this.lbLecName.AutoSize = true;
            this.lbLecName.Location = new System.Drawing.Point(66, 26);
            this.lbLecName.Name = "lbLecName";
            this.lbLecName.Size = new System.Drawing.Size(80, 20);
            this.lbLecName.TabIndex = 6;
            this.lbLecName.Text = "lbLecName";
            // 
            // lbLecDay
            // 
            this.lbLecDay.AutoSize = true;
            this.lbLecDay.Location = new System.Drawing.Point(77, 68);
            this.lbLecDay.Name = "lbLecDay";
            this.lbLecDay.Size = new System.Drawing.Size(66, 20);
            this.lbLecDay.TabIndex = 3;
            this.lbLecDay.Text = "lbLecDay";
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.Location = new System.Drawing.Point(2, 68);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(72, 20);
            this.metroLabel5.TabIndex = 1;
            this.metroLabel5.Text = "강의시간:";
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(5, 26);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(57, 20);
            this.metroLabel4.TabIndex = 0;
            this.metroLabel4.Text = "강의명:";
            // 
            // metroLabel6
            // 
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.Location = new System.Drawing.Point(2, 162);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(76, 20);
            this.metroLabel6.TabIndex = 14;
            this.metroLabel6.Text = "현재시각: ";
            // 
            // lbNow
            // 
            this.lbNow.AutoSize = true;
            this.lbNow.Location = new System.Drawing.Point(74, 162);
            this.lbNow.Name = "lbNow";
            this.lbNow.Size = new System.Drawing.Size(81, 20);
            this.lbNow.TabIndex = 15;
            this.lbNow.Text = "(현재 시각)";
            // 
            // btnAuto
            // 
            this.btnAuto.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnAuto.CustomBackground = true;
            this.btnAuto.CustomForeColor = true;
            this.btnAuto.ForeColor = System.Drawing.Color.White;
            this.btnAuto.Location = new System.Drawing.Point(746, 254);
            this.btnAuto.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAuto.Name = "btnAuto";
            this.btnAuto.Size = new System.Drawing.Size(138, 56);
            this.btnAuto.TabIndex = 18;
            this.btnAuto.Text = "강의 자동 시작";
            this.btnAuto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAuto.Click += new System.EventHandler(this.btnAuto_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnLogout.CustomBackground = true;
            this.btnLogout.CustomForeColor = true;
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(51, 93);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(135, 46);
            this.btnLogout.TabIndex = 19;
            this.btnLogout.Text = "로그아웃";
            this.btnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnStartSet
            // 
            this.btnStartSet.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnStartSet.CustomBackground = true;
            this.btnStartSet.CustomForeColor = true;
            this.btnStartSet.ForeColor = System.Drawing.Color.White;
            this.btnStartSet.Location = new System.Drawing.Point(731, 500);
            this.btnStartSet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnStartSet.Name = "btnStartSet";
            this.btnStartSet.Size = new System.Drawing.Size(161, 28);
            this.btnStartSet.TabIndex = 21;
            this.btnStartSet.Text = "시작 프로그램으로 설정";
            this.btnStartSet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnStartSet.TileTextFontSize = MetroFramework.MetroTileTextSize.Small;
            this.btnStartSet.Click += new System.EventHandler(this.btnStartSet_Click);
            // 
            // LectureManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(906, 555);
            this.Controls.Add(this.btnStartSet);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnAuto);
            this.Controls.Add(this.lbNow);
            this.Controls.Add(this.metroLabel6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvLec);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "LectureManagerForm";
            this.Padding = new System.Windows.Forms.Padding(23, 75, 23, 25);
            this.Text = "강의 관리해Zoom";
            ((System.ComponentModel.ISupportInitialize)(this.dgvLec)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvLec;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbZID;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private System.Windows.Forms.TextBox tbZPW;
        private System.Windows.Forms.GroupBox groupBox2;
        private MetroFramework.Controls.MetroLabel lbLecDay;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroLabel lbLecName;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        private MetroFramework.Controls.MetroLabel lbNow;
        private System.Windows.Forms.Timer timer1;
        private MetroFramework.Controls.MetroTile btnAuto;
        private MetroFramework.Controls.MetroTile btnLogout;
        private MetroFramework.Controls.MetroTile btnMeetModify;
        private MetroFramework.Controls.MetroTile btnMannual;
        private MetroFramework.Controls.MetroTile btnStartSet;
        private System.Windows.Forms.Button button1;
        private MetroFramework.Controls.MetroTile metroTile1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvCourseId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvLecture;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTime;
    }
}