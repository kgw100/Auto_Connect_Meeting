using MetroFramework.Forms;
using MetroFramework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace AccessZoom_try
{

    public partial class LectureManagerForm : MetroForm, IDisposable
    {
        //List<Course> courses = new List<Course>();
        List<isUsedZoomCourse> isUsedZoomCourseList = new List<isUsedZoomCourse>();
        bool isFile = true;
        string file = AppDomain.CurrentDomain.BaseDirectory + @"courseList.json";
        List<Course> loadCourseList = new List<Course>();

        public LectureManagerForm()
        {

            InitializeComponent();
            //timer1.Tick += Timer1_Tick;
            //폼로드되자마자 바로 렌더링하기 위해서
            Application.Idle += Timer1_Tick;
            timer1.Enabled = true;
            timer1.Interval = 1000;

            lbNow.TextAlign = ContentAlignment.MiddleCenter;
            init();
            initStyle();
        }

        public LectureManagerForm(YLoginForm yLoginForm)

        {
            yLoginForm.Close();
            InitializeComponent();
            //timer1.Tick += Timer1_Tick;
            //폼로드되자마자 바로 렌더링하기 위해서
            Application.Idle += Timer1_Tick;
            timer1.Enabled = true;
            timer1.Interval = 1000;

            lbNow.TextAlign = ContentAlignment.MiddleCenter;
            init();
            initStyle();
            //
            this.FormClosing += new FormClosingEventHandler(formClosing);
            this.FormClosed += new FormClosedEventHandler(formClosed);
        }
        private void init()
        {
            List<CourseJson> loadJson = new List<CourseJson>();

            lbLecName.Text = "";    //강의명 초기화

            //dgv 초기화
            dgvLec.Rows.Clear();

            Load_CourseData(loadJson); //dgv 세팅도 함께 진행


            //강의명 초기화
            lbLecName.Text = "";

            //강의요일,시간 초기화
            lbLecDay.Text = "";
            //자동강의 설정 확인
            if (AppConfiguration.IsExistAppConfig("autoStartCourse"))
            {
                if (AppConfiguration.GetAppConfig("autoStartCourse") == "1")
                {
                    setAutoStartBtn(true);
                }
            }
            //이벤트 핸들러 등록 
            this.FormClosing += new FormClosingEventHandler(formClosing);
            this.FormClosed += new FormClosedEventHandler(formClosed);
            tbZID.GotFocus += new EventHandler(tb_Gotfocus);
            tbZPW.GotFocus += new EventHandler(tb_Gotfocus);
            tbZID.LostFocus += new EventHandler(tb_Lostfocus);
            tbZPW.LostFocus += new EventHandler(tb_Lostfocus);


            //버튼 visible 이후에 추가할것 (자동시작/ 자동중지)

        }
        private void initStyle()
        {
            btnLogout.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            btnMannual.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            btnMeetModify.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            btnAuto.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            btnStartSet.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            //이벤트 핸드러 등록 
            btnLogout.MouseMove += new System.Windows.Forms.MouseEventHandler(btn_MouseMove);
            btnLogout.MouseLeave += new EventHandler(btn_MouseLeave);

            btnMannual.MouseMove += new System.Windows.Forms.MouseEventHandler(btn_MouseMove);
            btnMannual.MouseLeave += new EventHandler(btn_MouseLeave);

            btnStartSet.MouseMove += new System.Windows.Forms.MouseEventHandler(btn_MouseMove);
            btnStartSet.MouseLeave += new EventHandler(btn_MouseLeave);

            btnMeetModify.MouseMove += new System.Windows.Forms.MouseEventHandler(btn_MouseMove);
            btnMeetModify.MouseLeave += new EventHandler(btn_MouseLeave);

            btnAuto.MouseMove += new System.Windows.Forms.MouseEventHandler(btn_MouseMove);
            btnAuto.MouseLeave += new EventHandler(btn_MouseLeave);
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            lbNow.Text = DateTime.Now.ToString();
        }

        private void btnMannual_Click(object sender, EventArgs e)
        {
            StartZoomForm MSF = new StartZoomForm();
            MSF.ShowDialog();
            //this.Close();
            //init();
        }

        private void Load_CourseData(List<CourseJson> loadJson)
        {
            try
            {
                String fileData = System.IO.File.ReadAllText(file);
                JObject jObject = JObject.Parse(fileData);
                loadJson = JsonConvert.DeserializeObject<List<CourseJson>>(jObject["courseList"].ToString());

                Course course = new Course();
                //dgvLec checkBox 생성
                
                // 체크 박스 이벤트 등록 
                dgvLec.CellContentClick += new DataGridViewCellEventHandler(cb_CheckedChanged);
                DataGridViewCheckBoxColumn cbIsUsedZoom = new DataGridViewCheckBoxColumn();
                DataGridViewCheckBoxColumn cbIsFixed = new DataGridViewCheckBoxColumn();
                cbIsUsedZoom.HeaderText = "Zoom 여부";
                cbIsUsedZoom.Name = "cbIsUsedZoom";
                cbIsFixed.HeaderText = "ID,PW 고정 여부";
                cbIsFixed.Name = "cbIsFixed";
                dgvLec.Columns.Add(cbIsUsedZoom);
                dgvLec.Columns.Add(cbIsFixed);
                //cb_CheckedChanged(
                int isUsedZoomIndex = dgvLec.Columns["cbIsUsedZoom"].Index;
                int isFixedIndex = dgvLec.Columns["cbIsFixed"].Index;
                int cnt = 0;
                foreach (CourseJson i in loadJson)
                {

                    // 객체에 값 할당하기
                    course.id = i.courseId;
                    course.name = i.courseName;
                    course.isUsed = i.isUsed;
                    course.isFixed = i.isFixed;

                    //dgvLec 데이터 로드 
                    if (i.courseTime.day.Count == 1)
                    {
                        course.courseTime = i.courseTime.day[0] + "<" + i.courseTime.startTime[0] + "~" + i.courseTime.endTime[0] + ">";

                        course.startTimeList.Add(Convert.ToInt32(i.courseTime.startTime[0]));
                        course.endTimeList.Add(Convert.ToInt32(i.courseTime.endTime[0]));
                        course.dayList.Add(i.courseTime.day[0]);
                    }
                    else if (i.courseTime.day.Count == 2)
                    {
                        course.courseTime = i.courseTime.day[0] + "<" + i.courseTime.startTime[0] + "~" + i.courseTime.endTime[0] + ">"
                                            + ", " + i.courseTime.day[1] + "<" + i.courseTime.startTime[1] + "~" + i.courseTime.endTime[1] + ">";

                        course.startTimeList.Add(Convert.ToInt32(i.courseTime.startTime[0]));
                        course.startTimeList.Add(Convert.ToInt32(i.courseTime.startTime[1]));
                        course.endTimeList.Add(Convert.ToInt32(i.courseTime.endTime[0]));
                        course.endTimeList.Add(Convert.ToInt32(i.courseTime.endTime[1]));
                        course.dayList.Add(i.courseTime.day[0]);
                        course.dayList.Add(i.courseTime.day[1]);
                    }
                    else
                    {
                        course.courseTime = "강의시간없음";
                    }
                    // Rows.add 후 넣어주어야 값이 할당됨
                    dgvLec.Rows.Add(course.id, course.name, course.courseTime);
                    dgvLec[isUsedZoomIndex, cnt].Value = course.isUsed;
                    dgvLec[isFixedIndex, cnt].Value = course.isFixed;

                    //자동 접속 기능에서 현재 상태를 확인할 수 있도록 상태 클래스 로드
                    if (course.isUsed)//zoom 사용시
                    {
                        isUsedZoomCourse iuzc = new isUsedZoomCourse(course.id, course.isFixed,
                            course.dayList, course.startTimeList, course.endTimeList);
                        isUsedZoomCourseList.Add(iuzc);
                    }

                    //파싱하지않고 바로 사용할 용도로 저장
                    loadCourseList.Add(course);
                    cnt++;
                }
            }
            catch (System.IO.FileNotFoundException e)
            {
                isFile = false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        //dgv click event

        private void DgvLec_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            tbMeeting_Changed(e.RowIndex);

        }
        private void btnStartSet_Click(object sender, EventArgs e)
        {
            StartingSet ss = new StartingSet();
            ss.ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show(this, "계정정보,강의정보 모두가 삭제됩니다.\n로그아웃하시겠습니까?", "안내", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                //강의 정보 삭제
                List<string> delFileList = new List<string>(new string[]
                //{ "ID.txt", "PW.txt", "courseList.json", "meetingList.json" }
                {"courseList.json", "meetingList.json" }
                );
                foreach (string filename in delFileList)
                {
                    FileInfo deFile = new FileInfo(filename);
                    deFile.Delete();
                }
                //계정삭제
                AppConfiguration.RemoveAppConfig("id");
                AppConfiguration.RemoveAppConfig("password");
                AutoClsMsgBox.Show("정상 로그아웃 되셨습니다.프로그램을 종료합니다", "안내", 1400);
                //Application.Restart();
                Application.Exit();
            }
        }

        private void btnAuto_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Convert.ToBoolean(btnAuto.Tag))
                {
                    if (!isExistCourseIdList())
                        return;
                    //자동 강의 flag Setting
                    AppConfiguration.SetAppConfig("autoStartCourse", "1");
                    setAutoStartBtn(true);
                    GetData.getMeetingData(ref isUsedZoomCourseList);
                    //포커싱 다시 주기
                    this.TopMost = true;
                }
                else
                {
                    AppConfiguration.SetAppConfig("autoStartCourse", "0");
                    setAutoStartBtn(false);
                    if (GetData.mdriver != null)
                    {
                        GetData.mdriver.Quit();
                        GetData.mdriver = null;
                    }
                }
                this.TopMost = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }
        private void tbMeeting_Changed(int eRowIdx)
        {
            try
            {

                DataGridViewRow dgvRow = dgvLec.Rows[eRowIdx];
                int isUsedZoomIndex = dgvLec.Columns["cbIsUsedZoom"].Index;
                int isFixedIndex = dgvLec.Columns["cbIsFixed"].Index;
                string Dname = dgvRow.Cells["dgvLecture"].Value.ToString();
                string DDay = dgvRow.Cells["dgvTime"].Value.ToString();
                lbLecName.Text = Dname;
                lbLecDay.Text = DDay;
                if (Convert.ToBoolean(dgvLec[isUsedZoomIndex, eRowIdx].Value))
                {
                    bool fixedFlag = Convert.ToBoolean(dgvLec[isFixedIndex, eRowIdx].Value);

                    if (!fixedFlag)
                    {
                        tb_RevChange();
                        tbZID.Text = "비 고정형 회의";
                        tbZPW.Text = "비 고정형 회의";
                        tbZID.ReadOnly = true;
                        tbZPW.ReadOnly = true;

                    }
                    else
                    {

                      
                        string meetingJsonPath = AppDomain.CurrentDomain.BaseDirectory + @"meeting.json";
                        FileInfo fileInfo = new FileInfo(meetingJsonPath);
                        string courseId = dgvRow.Cells["dgvCourseId"].Value.ToString();
                        if (fileInfo.Exists)
                        {
                            string fileData = File.ReadAllText(meetingJsonPath);
                            JObject jObject = JObject.Parse(fileData);
                            MeetingJson meetingJson = JsonConvert.DeserializeObject<MeetingJson>(jObject[courseId].ToString());
                            tbZID.Text = meetingJson.id;
                            tbZPW.Text = meetingJson.pw;
                            if (tbZID.ReadOnly)
                            {
                                tbZID.TextChanged += new EventHandler(tb_TextChanged);
                                tbZPW.TextChanged += new EventHandler(tb_TextChanged);
                            }                           
                        }
                        else
                        {
                            tbZID.Text = "";
                            tbZPW.Text = "";
                        }
                        tbZID.ReadOnly = false;
                        tbZPW.ReadOnly = false;
                    }
                }
                else
                {
                    tb_RevChange();
                    tbZID.Text = "Zoom 미 사용 강의";
                    tbZPW.Text = "Zoom 미 사용 강의";
                    tbZID.ReadOnly = true;
                    tbZPW.ReadOnly = true;


                }
            }
            catch (ArgumentOutOfRangeException ie)// 선택된 모든 행을 ctrl click으로 취소시, 개념적으로 '없는'배열을 참조하려는 예외발생. >>try, catch로 초기화 처리
            {
                tbZID.Text = "";
                tbZPW.Text = "";

                lbLecName.Text = "";
                lbLecDay.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        private void tb_RevChange()
        {
            try
            {
                btnMeetModify.Tag = false;
                btnMeetModify.BackColor = Color.DeepSkyBlue;
                btnMeetModify.Text = "강의 접속 정보 저장";
                    tbZID.TextChanged -= new EventHandler(tb_TextChanged);
                    tbZPW.TextChanged -= new EventHandler(tb_TextChanged);
             

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        //체크박스 체크 값
        private void cb_CheckedChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dgvRow = dgvLec.Rows[e.RowIndex];
            string columnName = dgvLec.Columns[e.ColumnIndex].Name;
            if (dgvRow.Cells["dgvTime"].Value == "강의시간없음" && columnName == "cbIsUsedZoom")
            {
                MessageBox.Show("강의시간이 없는 강의는 Zoom 여부를 체크할 수 없습니다.");
                return;
            }
            try
            {
                bool isUsedZoom = Convert.ToBoolean(dgvRow.Cells["cbIsUsedZoom"].Value);
                bool isFixed = Convert.ToBoolean(dgvRow.Cells["cbIsFixed"].Value);
                //체크값이 바뀌기전상태이기때문에 false false상태일때, isFixed를 먼저 체크할때, 예외처리
                if (columnName == "cbIsFixed" && !isFixed && !isUsedZoom)
                {
                    MessageBox.Show("Zoom여부를 먼저 체크해주세요.", "안내");
                    return;
                }
                //둘다 체크 되어있는 상태에서 Zoom체크를 먼저 풀었을 때 처리
                else if (columnName == "cbIsUsedZoom" && isFixed && isUsedZoom)
                {
                    MessageBox.Show("Zoom여부를 먼저 해제할 수 없습니다.\n ID고정형회의 체크란 부터 해제해주세요.", "안내");
                    return;
                }
                string courseId = dgvRow.Cells["dgvCourseId"].Value.ToString();
                bool changed_IsChecked = !Convert.ToBoolean(dgvRow.Cells[columnName].Value);
                dgvRow.Cells[columnName].Value = changed_IsChecked;
                saveCheckedSatus(courseId, changed_IsChecked, columnName);
                if (columnName == "cbIsUsedZoom")
                {
                    if (changed_IsChecked)
                    {
                        addAutoStart_CourseIdList(dgvRow);
                    }
                    else //(!changed_IsChecked)
                        deleteAutoStart_CourseIdList(dgvRow);
                    
                }
                else //columnName=="cbIsFixed"
                {
                    //zoom을 사용하는 강의들 중에서 고정형 여부 값을 갱신해줌
                    foreach (isUsedZoomCourse isUsedZoomCourse in isUsedZoomCourseList)
                    {
                        if (isUsedZoomCourse.courseId == courseId)
                        {
                            isUsedZoomCourse.isFixed = changed_IsChecked;
                            break;
                        }
                    }
                }
                tbMeeting_Changed(e.RowIndex);
                // textbox에도 변경사항 적용시켜주기 
                //tbMeeting_Changed(e.RowIndex);


            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }
        private bool isExistCourseIdList()
        {
            if (isUsedZoomCourseList.Count == 0)
            {
                MessageBox.Show("현재 Zoom 강의가 없습니다.\n Zoom 여부를 다시 확인해주세요.", "안내");
                return false;
            }
            else
                return true;
        }
        //일단은 고정형 여부 관계없이 Zoom을 사용하는 강의를 리스트에 추가해줌
        private void addAutoStart_CourseIdList(DataGridViewRow row)
        {
            isUsedZoomCourse temp = new isUsedZoomCourse();
            temp.dayList = new List<string>();
            temp.startTimeList = new List<int>();
            temp.endTimeList = new List<int>();
            temp.courseId = row.Cells["dgvCourseId"].Value.ToString();
            foreach (Course course in loadCourseList)
            {
                if (course.id == temp.courseId)
                {
                    temp.dayList = course.dayList;
                    temp.startTimeList = course.startTimeList;
                    temp.endTimeList = course.endTimeList;
                }
            }

            isUsedZoomCourseList.Add(temp);
            // 추후 강의 시간까지 넣어서 강의 시작 분배
        }
        private void deleteAutoStart_CourseIdList(DataGridViewRow row)
        {
            string courseId = row.Cells["dgvCourseId"].Value.ToString();
            foreach (isUsedZoomCourse isUsedZoomCourse in isUsedZoomCourseList.ToList())
            {
                if (isUsedZoomCourse.courseId == courseId)
                    isUsedZoomCourseList.Remove(isUsedZoomCourse);
            }

        }
        private void saveCheckedSatus(string courseId, bool changedValue, string changedValueName)
        {
            try
            {
                //json 로드
                List<CourseJson> loadJson = new List<CourseJson>();
                String fileData = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"courseList.json");
                JObject jObject = JObject.Parse(fileData);
                loadJson = JsonConvert.DeserializeObject<List<CourseJson>>(jObject["courseList"].ToString());
                //체크 값 수정
                for (int index = 0; index < loadJson.Count; index++)
                {
                    if (loadJson[index].courseId == courseId)
                    {
                        if (changedValueName == "cbIsUsedZoom")
                            loadJson[index].isUsed = changedValue;
                        else //cbIsFixed
                            loadJson[index].isFixed = changedValue;
                        break;
                    }
                }
                //변경사항 저장 
                var dict = new Dictionary<string, List<CourseJson>>();
                dict.Add("courseList", loadJson);
                string json = JsonConvert.SerializeObject(dict);
                var res = JObject.Parse(json);
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"courseList.json", res.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        // 폼 종료시 driver 메모리 제거 처리 및 확인 절차
        private void formClosing(object sender, FormClosingEventArgs e)
        {
            if (AppConfiguration.GetAppConfig("autoStartCourse") == "1")
            {
                if (MessageBox.Show("자동강의시작 기능을 유지하고 종료하시겠습니까?", "안내", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    //자동강의시작 기능 off
                    AppConfiguration.SetAppConfig("autoStartCourse", "0");
                }
            }

        }
        private void formClosed(object sender, FormClosedEventArgs e)
        {
            if (GetData.mdriver != null)
                GetData.mdriver.Quit();
            Application.ExitThread();
            Environment.Exit(0);
        }
        private void btn_MouseMove(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (!Convert.ToBoolean(btn.Tag))
            {
                btn.BackColor = Color.DodgerBlue;
            }
            else
            {
                btn.BackColor = Color.Tomato;
            }
        }
        private void btn_MouseLeave(object sender, EventArgs e)
        {

            Button btn = sender as Button;
            if (!Convert.ToBoolean(btn.Tag))
            {
                btn.BackColor = Color.DeepSkyBlue;
            }
            else
            {
                btn.BackColor = Color.Orange;
            }
        }
        private void tb_TextChanged(object sender, EventArgs e)
        {
            btnMeetModify.Text = "강의 접속 정보 수정";
            btnMeetModify.BackColor = Color.Orange;
            btnMeetModify.Tag = true;
        }

        private void setAutoStartBtn(bool isAutoStart)
        {
            if (isAutoStart)
            {
                btnAuto.TileTextFontSize = MetroFramework.MetroTileTextSize.Small;
                btnAuto.Text = "강의 자동 시작 중지";
                btnAuto.Tag = true;
                btnAuto.BackColor = Color.DarkOrange;
            }
            else
            {
                btnAuto.TileTextFontSize = MetroFramework.MetroTileTextSize.Medium;
                btnAuto.Tag = false;
                btnAuto.Text = "강의 자동 시작";
                btnAuto.BackColor = Color.DeepSkyBlue;
            }

        }
        private void dgvLec_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void tb_Gotfocus(object sender, EventArgs e)
        {
            tbZID.TextChanged += new EventHandler(tb_TextChanged);
            tbZPW.TextChanged += new EventHandler(tb_TextChanged);
        }
        private void tb_Lostfocus(object sender, EventArgs e)
        {
            tbZID.TextChanged -= new EventHandler(tb_TextChanged);
            tbZPW.TextChanged -= new EventHandler(tb_TextChanged);
        }

        private void btnMeetModify_Click(object sender, EventArgs e)
        {
            if (tbZID.Text == "")
            {
                MessageBox.Show("ID란에 공백을 입력하셨습니다. \n 다시 확인해주세요.");
            }
            foreach (isUsedZoomCourse i in isUsedZoomCourseList)
            {
                if (i.courseId == dgvLec.SelectedRows[0].Cells[0].Value.ToString())
                {
                    if (!i.isFixed)
                    {
                        MessageBox.Show("고정형 강의만 저장하실 수 있습니다.");
                        return;
                    }
                    else
                    {
                        MeetingJson meetingJson = new MeetingJson();
                        string courseId = i.courseId;
                        string id = tbZID.Text;
                        string pw = tbZPW.Text;

                        JObject meetingJobj = new JObject(
                            new JProperty("id", id),
                            new JProperty("pw", pw)
                            );
                        JObject meetingData = new JObject(
                            new JProperty(courseId, meetingJobj));

                        File.WriteAllText(@AppDomain.CurrentDomain.BaseDirectory + @"meeting.json", meetingData.ToString());
                    }
                }
            }
            if (Convert.ToBoolean(btnMeetModify.Tag))
            {
                MessageBox.Show("변경 사항이 저장되었습니다.");
            }
            tb_RevChange();



        }

    }
}
