using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using OpenQA.Selenium;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AccessZoom_try
{
    partial class GetData
    {
        public static ChromeDriver mdriver = null;
        private static string id = "";
        private static string pw = "";
        public static bool isLogin;
        private static List<meeting> meetingDataList = new List<meeting>();

        public static void getMeetingData( ref List<isUsedZoomCourse> _isUsedZoomCourseList)
        {
            List<isUsedZoomCourse> isUsedZoomCourseList = new List<isUsedZoomCourse>();
            isUsedZoomCourseList = _isUsedZoomCourseList;
            string nowDate = DateTime.Now.ToString("M월 dd일");
            try
            {
                Thread thread_DCMB = new Thread(() => DirectAutoClsMSGBox.Show("자동 강의를 시작합니다.\n 강의  ID, PW 파싱 진행중입니다. 잠시만 기다려주세요.", "자동 강의 안내"));
                thread_DCMB.Start();
                //Zoom 사용 O, 고정형 ID X인 강의 리스트의 학정 번호를 불러온다.

                // 최종 자동시작 스케줄러가 추가되면 들어갈 부분 

                //List<isUsedZoomCourse> todayCourseList = new List<isUsedZoomCourse>();
                //string today = getDay(DateTime.Now);
                ////강의 id,pw 탐색          
                //foreach (isUsedZoomCourse isUsedZoomCourse in isUsedZoomCourseList)
                //{
                //    try
                //    {
                //        //오늘강의만 추가해주기
                //        foreach (string day in isUsedZoomCourse.dayList)
                //        {
                //            if (day == today)
                //            {
                //                todayCourseList.Add(isUsedZoomCourse);
                //            }
                //        }
                //    }
                //    catch(Exception ex)
                //    {
                //        MessageBox.Show("에러발생");
                //    }
                //}
                //MessageBox.Show("오늘강의 수:" + todayCourseList.Count.ToString());
                //foreach (isUsedZoomCourse td_iuzc in todayCourseList)

                foreach (isUsedZoomCourse iuzc in isUsedZoomCourseList)
                {
                    try
                    {
                        //if (!td_iuzc.isFixed) // 고정형 X
                        if (!iuzc.isFixed)
                        {
                            if (mdriver == null)
                            {
                                isLogin = false;
                                Thread thread_LoadDriver = new Thread(() => GetData.driver_Load(ref mdriver));
                                //driver 로드, driver 로드 시간이 기므로 쓰레드로 진행시킨다.
                                thread_LoadDriver.Start();
                                //사용자 계정 정보 불러오기 
                                getAccountInfo();
                                //로드까지 기다리기
                                thread_LoadDriver.Join();
                            }
                            //디버깅용
                            //AutoClsMsgBox.Show("회의 ID, PW를 수집합니다.", "안내", 1500);

                            //searchMeetingData(td_iuzc.courseId);
                            searchMeetingData(iuzc.courseId, nowDate);
                            if (meetingDataList.Count != 0)
                            {
                                startZoom sz = new startZoom();
                                Thread thread_ZoomStart = new Thread(() => sz.ZoomStart(meetingDataList[0].id, meetingDataList[0].pw));
                                thread_ZoomStart.Start();
                            }
                            else
                            {
                                MessageBox.Show("아직 강의 접속정보가 와이섹이 올라오지 않았습니다.");
                            }
                        }
                        else //고정형 O
                        {
                          
                            getFixedMeetingData(iuzc);
                            // 후 zoom접속 메소드 실행 
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.ToString());
                    }
                }

                //아무작업도 없으면 스레드가 실행안됨  그래서 sleep을 줬음 
                Thread.Sleep(50);
                DirectAutoClsMSGBox.Close("자동 강의 안내");
                MessageBox.Show("파싱 성공!, Zoom접속을 실행합니다.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            // 그 후 스레딩과 스케줄러를 통해 자동강의 시작 클래스 만들기 -AutoStartCourse


        }
        private static void getAccountInfo()
        {
            try
            {
                id = AppConfiguration.GetAppConfig("id");
                pw = Security.Decrypt(AppConfiguration.GetAppConfig("password"), id);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            //using (StreamReader idFIle = new StreamReader(@"ID.txt"))
            //{
            //    id = idFIle.ReadLine();
            //}
            //using (StreamReader pwFile = new StreamReader(@"PW.txt"))
            //{
            //    pw = Security.Decrypt(pwFile.ReadLine(), id);
            //}
        }
        private static void searchMeetingData(string isUsedZoomCourseId, string nowDate)
        {
            try
            {
                
                if (isLogin == false)
                    yscecLogin();
                else
                    mdriver.Navigate().GoToUrl("https://yscec.yonsei.ac.kr/my/");

                ReadOnlyCollection<IWebElement> courseNameLink_Elements = mdriver.FindElements(By.ClassName("coursename"));
                ReadOnlyCollection<IWebElement> courseId_Elements = mdriver.FindElements(By.ClassName("subject_id"));
                //묶어주기
                var course_Elements = courseNameLink_Elements.Zip(courseId_Elements, (nl, id) => new { NameLink = nl, Id = id });


                foreach (var courseElement in course_Elements)
                {
                    string courseId = courseElement.Id.Text;//강의이름
                    if (courseId == isUsedZoomCourseId)
                    {
                        IWebElement a_delink = courseElement.NameLink.FindElement(By.CssSelector("a"));

                        string deLink = a_delink.GetAttribute("href"); //강의세부링크 
                                                                       //청강 일때를 처리 
                        if (deLink == "" || deLink == null)
                            continue;
                        mdriver.Navigate().GoToUrl(deLink);
                        IWebElement notice_Section0 = mdriver.FindElement(By.CssSelector("li[id='section-0']"));
                        IWebElement notice_Content = notice_Section0.FindElement(By.CssSelector("div[class='content']"));
                        IWebElement notice_Activityinstance = notice_Content.FindElement(By.CssSelector("div[class='activityinstance']"));
                        IWebElement notice_alink = notice_Activityinstance.FindElement(By.CssSelector("a"));
                        string noticeLink = notice_alink.GetAttribute("href");
                        mdriver.Navigate().GoToUrl(noticeLink);
                        WebDriverWait wait = new WebDriverWait(mdriver, TimeSpan.FromSeconds(10));
                        wait.Until(d => d.FindElement(By.ClassName("thread-style")));
                        IWebElement ulPostTime = mdriver.FindElement(By.ClassName("thread-style-lists"));
                        ReadOnlyCollection<IWebElement> noticeElements = ulPostTime.FindElements(By.CssSelector("li"));
                        
                        //MessageBox.Show(DateTime.Today.AddDays(-91).ToString("M월 dd일"));

                      
                        foreach (IWebElement noticeElement in noticeElements)
                        {

                                IWebElement postTimeElement = mdriver.FindElement(By.CssSelector("span[class='thread-post-meta']"));
                        
                            string postTime = postTimeElement.Text;

                            //if (!postTime.Contains(DateTime.Today.AddDays(-91).ToString("M월 dd일")))//nowDate
                            //    break;
                            //else
                            if (postTime.Contains(DateTime.Today.ToString("M월 dd일"))) //DateTime.Today.AddDays(-91).ToString("M월 dd일")//DateTime.Today.ToString("M월 dd일")
                            {
                                try
                                {
                                    IWebElement postContentElement = noticeElement.FindElement(By.CssSelector("p[class='thread-post-content']"));
                                    string postContent = postContentElement.Text;


                                    string postContentStrip = postContent.Replace(" ", "").Replace("-", "");

                                    Match idMatch = Regex.Match(postContentStrip, "회의IW:([0-9])*");//바꾸기 
                                    Match pwMatch = Regex.Match(postContentStrip, "비밀번호:([0-9a-z])*");
                                    if (idMatch.Value != "")
                                    {
                                        string idFull = idMatch.Value;
                                        string pwFull = pwMatch.Value;
                                        string id = Regex.Match(idFull, "([0-9])*$").Value;

                                        string pw = Regex.Match(pwFull, "([0-9a-z])*$").Value;
                                        meetingDataList.Add(new meeting(id, pw));
                                        MessageBox.Show("원본:" + postContentStrip + "\n" +
                                            "id:" + id + " pw:" + pw);

                                    }
                                    else
                                    {

                                        Match linkIDMatch = Regex.Match(postContentStrip, "(\\/j\\/)([0-9])*");

                                        string linkIdFull = linkIDMatch.Value;


                                        string linkId = Regex.Match(linkIdFull, "([0-9])*$").Value;
                                        string linkpwFull = Regex.Match(postContentStrip, "pwd=(\\w)*").Value;
                                        string linkpw = Regex.Match(linkpwFull, "[0-9a-zA-z]*$").Value;

                                        if (linkId == "" && linkpw == "")
                                        { }
                                        else
                                        {
                                            if (meetingDataList.Count == 0)
                                            {
                                                MessageBox.Show("원본:" + postContentStrip + "\n" + "id:" + linkId + " pw:" + linkpw);
                                            }

                                            meetingDataList.Add(new meeting(linkId, linkpw));
                                        }


                                        //try
                                        //{

                                        //    postContentElement = mdriver.FindElement(By.CssSelector("p[class='thread-post-content']"));
                                        //}
                                        //catch
                                        //{
                                        //     postContentElement = noticeElement.FindElement(By.CssSelector("p[id='yui_3_17_2_1_1593401754138_106']"));

                                        //}



                                        //}
                                    }
                                }
                                catch (Exception e)
                                {
                                    //MessageBox.Show(e.ToString());
                                }


                            }
                            Thread.Sleep(500);


                        }




                        //IWebElement notice_jinotechboard = notice_Content.FindElements(By.CssSelector("ul[class='activity jinotechboard modtype_jinotechboard']"));

                        break;
                    }
                    Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }
        private static void yscecLogin()
        {
            mdriver.Navigate().GoToUrl("https://yscec.yonsei.ac.kr/login/index.php");
            WebDriverWait wait = new WebDriverWait(mdriver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(By.Id("loginbtn")));
            var idField = mdriver.FindElementById("username");
            var pwField = mdriver.FindElementById("password");
            idField.SendKeys(id);
            pwField.SendKeys(pw);
            mdriver.FindElement(By.Id("loginbtn")).Click();
            Thread.Sleep(10);
            string nextUrl = mdriver.Url;
            mdriver.Navigate().GoToUrl(nextUrl);
            isLogin = true;
        }
        //고정형 강의 
        private static void getFixedMeetingData(isUsedZoomCourse iuzc)
        {
            try
            {
                string filePath = AppDomain.CurrentDomain.BaseDirectory + @"meeting.json";
                string fileData = File.ReadAllText(filePath);
                JObject jObject = JObject.Parse(fileData);

                MeetingJson meetingJson = JsonConvert.DeserializeObject<MeetingJson>(jObject[iuzc.courseId].ToString());
                startZoom startZoom = new startZoom();
                startZoom.ZoomStart(meetingJson.id, meetingJson.pw);
          

            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
           
        }
        private static string getDay(DateTime dt)
        {
            string strDay = "";

            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    strDay = "월";
                    break;
                case DayOfWeek.Tuesday:
                    strDay = "화";
                    break;
                case DayOfWeek.Wednesday:
                    strDay = "수";
                    break;
                case DayOfWeek.Thursday:
                    strDay = "목";
                    break;
                case DayOfWeek.Friday:
                    strDay = "금";
                    break;
                case DayOfWeek.Saturday:
                    strDay = "토";
                    break;
                case DayOfWeek.Sunday:
                    strDay = "일";
                    break;
            }

            return strDay;
        }
    }
}
