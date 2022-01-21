using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;
using System.Windows.Forms;


namespace AccessZoom_try
{

    partial class GetData
    {

        public void getCourseData(TextBox tbStdID, TextBox tbStdPW, ref bool isLogin)
        {
            //Thread thread_directAutoMsgBox = new Thread(() => DirectAutoClsMSGBox.Show(
            //    "강의 정보 수집중입니다. 잠시만 기다려주세요 (최초 1회).\n \t         (자동으로 닫힙니다)", "안내"));
            //thread_directAutoMsgBox.Start();
            // 조금이라도 로딩 시간을 배분하기 위해 로그인폼을 불러오는 타이밍에 driver를 로드시킴 
            driver = YLoginForm.driver;
            //로그인
            try
            {
                List<Course> courseList = new List<Course>();
                //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.Navigate().GoToUrl("https://yscec.yonsei.ac.kr/login/index.php");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(d => d.FindElement(By.Id("loginbtn")));
                var idField = driver.FindElementById("username");
                var pwField = driver.FindElementById("password");

                idField.SendKeys(tbStdID.Text);
                pwField.SendKeys(tbStdPW.Text);
                driver.FindElement(By.Id("loginbtn")).Click();
                Thread.Sleep(10);
                if (driver.Url != "https://yscec.yonsei.ac.kr/my/")
                {
                    DirectAutoClsMSGBox.Close("안내");
                    MessageBox.Show("ID,또는 PW가 틀립니다. 다시 입력해주세요.", "안내");
                    tbStdPW.Text = "";
                    return;
                }
                else
                    //정상적으로 실행됨 
                    isLogin = true;
                //MessageBox.Show(driver.Url);
                string nextUrl = driver.Url;
                driver.Navigate().GoToUrl(nextUrl);
                ReadOnlyCollection<IWebElement> courseNameLink_Elements = driver.FindElements(By.ClassName("coursename"));
                ReadOnlyCollection<IWebElement> courseId_Elements = driver.FindElements(By.ClassName("subject_id"));
                // 두 개의 분리된 IWebElement List 하나로 묶어서 객체에 하나씩 넣어주어야하기 때문에 묶어준 것


                var course_Elements = courseNameLink_Elements.Zip(courseId_Elements, (nl, id) => new { NameLink = nl, Id = id });
                //강의 시간을 제외한 모든 강의 정보 데이터 수집
                foreach (var courseElement in course_Elements)
                {
                    Course tempCourse = new Course();
                    IWebElement a_delink = courseElement.NameLink.FindElement(By.CssSelector("a"));
                    tempCourse.name = courseElement.NameLink.Text;//강의이름
                    tempCourse.deLink = a_delink.GetAttribute("href"); //강의세부링크 
                    //청강 일때를 처리 
                    if (tempCourse.deLink == "" || tempCourse.deLink == null)
                        continue;
                    tempCourse.id = courseElement.Id.Text; //강의 학정번호 
                    courseList.Add(tempCourse);
                    Thread.Sleep(10);
                }
                YLoginForm.setPBMax = courseList.Count;
                //강의 시간 정보 추가 
                foreach (Course course in courseList.ToList()) // ToList()로 명시적으로 적어주면 Remove시 오류나지않음 
                {
                    // 강의 상세 페이지로 이동 
                    driver.Navigate().GoToUrl(course.deLink);

                    IWebElement courseTimeDeLink_Element;
                    try
                    {
                        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(6));
                        wait.Until(d => d.FindElement(By.ClassName("syllabus")));
                        courseTimeDeLink_Element = driver.FindElement(By.ClassName("syllabus"));
                    }
                    catch
                    {
                        //분반 경우? 처리
                        courseList.Remove(course);
                        continue;
                    }
                    string courseTimeDeLink = courseTimeDeLink_Element.FindElement(
                        By.CssSelector("a")).GetAttribute("href");
                    //수업계획서 페이지로 이동
                    driver.Navigate().GoToUrl(courseTimeDeLink);
                    course.courseTime = driver.FindElement(By.XPath("/html/body/form/table[2]/tbody/tr[4]/td[4]")).Text;
                    course.courseTime = course.courseTime.Replace(" ", "");

                    int startIndex = 0, endIndex = 0, dayTime = 0;
                    //요일 추출 정규식
                    MatchCollection dayMatches = Regex.Matches(course.courseTime, "[월화수목금]");
                    // ,를 구분하며 시간을 추출하는 정규식 
                    Match commaMatch = Regex.Match(course.courseTime, @",");
                    int dayCount = dayMatches.Count;
                    course.dayList = new List<string>();
                    course.startTimeList = new List<int>();
                    course.endTimeList = new List<int>();
                    //여러 요일에 걸쳐 있는 수업인지 아닌지 확인
                    if (dayCount == 0)
                    { } // pass
                    else if (dayCount == 1)
                    {
                        List<int> timeList = new List<int>();
                        course.dayList.Add(course.courseTime.Substring(0, 1)); // 요일 추출
                        if (!commaMatch.Success && startIndex == 0)
                            endIndex = course.courseTime.Length - 1; //문자열의 길이에서 1빼주면 요일을 제외한 숫자의 길이가 나옴 
                        else
                            endIndex = commaMatch.Index;
                        while (true)
                        {
                            // 강의 시간 추출 
                            if (!commaMatch.Success && startIndex == 0)//강의시간이 하나만 있는 경우 
                            {
                                dayTime = Int32.Parse(course.courseTime.Substring(startIndex + 1, endIndex - startIndex));
                                timeList.Add(dayTime);
                                break;
                            }
                            else if (!commaMatch.Success && startIndex != 0)// 강의 시간이 여러 개 있는 경우에서 맨 끝 시간 가져오기 
                            {
                                dayTime = Int32.Parse(course.courseTime.Substring(endIndex + 1, course.courseTime.Length - (endIndex + 1)));
                                timeList.Add(dayTime);
                                break;
                            }
                            else if (startIndex == 0) //  파싱시 
                                dayTime = Int32.Parse(course.courseTime.Substring(startIndex + 1, endIndex - (startIndex + 1)));//startIndex가 요일부터 시작하기때문에 +1      
                            else
                            {
                                endIndex = commaMatch.Index;
                                dayTime = Int32.Parse(course.courseTime.Substring(startIndex, endIndex - startIndex));
                            }
                            // 다음 , 찾기  
                            commaMatch = commaMatch.NextMatch();
                            startIndex = endIndex + 1;
                            timeList.Add(dayTime);
                        }
                        //시간 추출하기
                        course.startTimeList.Add(timeList[0] + 8);//수업 교시와 실제 시간의 차이는 8시간 차이이다.
                        course.endTimeList.Add(timeList[timeList.Count - 1] + 8 + 1); // 맨 마지막 시간                                          
                    }
                    else // dayCount >=2 , 
                    {
                        //괄호있는지,즉 실험시간이 있는 수업인지 확인 
                        bool isbracket = course.courseTime.Contains("(");
                        int dayIndex = 0;
                        endIndex = commaMatch.Index;
                        if (isbracket)
                        {
                            int breakNum = 0;
                            foreach (Match dayMatch in dayMatches)
                            {

                                course.dayList.Add(dayMatch.Value);
                                if (breakNum == 0)
                                    dayIndex = dayMatch.NextMatch().Index;
                                else if (breakNum == 1)
                                { }
                                else
                                    break;
                                List<int> timeList = new List<int>();
                                while (true)
                                {
                                    if (dayIndex < commaMatch.Index && breakNum == 0)
                                        break;
                                    // 강의 시간 추출 
                                    if (startIndex == 0)
                                        dayTime = Int32.Parse(course.courseTime.Substring(startIndex + 1, endIndex - (startIndex + 1)));
                                    else
                                    {
                                        endIndex = commaMatch.Index;
                                        // //endIndex==0의 의미는 ,가 없다는 뜻이다. 
                                        if (endIndex == 0 && course.dayList.Count == 1) //이 경우 월 1,2,화2 이런경우 즉, 두번째 요일의 시간이 괄호옆에 붙어 있는 날짜를 제외한 나머지 시간을 데이터로 수집한 상태이다,
                                            break;
                                        else if (endIndex == 0 && course.dayList.Count == 2 && timeList.Count != 0)
                                        {
                                            Match bracket = Regex.Match(course.courseTime, @"\(");
                                            dayTime = Int32.Parse(course.courseTime.Substring(startIndex, bracket.Index - startIndex));
                                            timeList.Add(dayTime);
                                            break;
                                        }
                                        else if (endIndex == 0 && course.dayList.Count == 2 && timeList.Count == 0)
                                        {
                                            Match bracket = Regex.Match(course.courseTime, @"\(");
                                            dayTime = Int32.Parse(course.courseTime.Substring(startIndex + 1, (bracket.Index - 1) - startIndex));
                                            timeList.Add(dayTime);
                                            break;
                                        }
                                        if (breakNum == 0)
                                            dayTime = Int32.Parse(course.courseTime.Substring(startIndex, endIndex - startIndex));
                                        else
                                            dayTime = Int32.Parse(course.courseTime.Substring(startIndex + 1, endIndex - (startIndex + 1)));
                                    }
                                    startIndex = endIndex + 1;
                                    timeList.Add(dayTime);
                                    commaMatch = commaMatch.NextMatch();
                                }
                                //시간 추출하기 
                                course.startTimeList.Add(timeList[0] + 8);//수업 교시와 실제 시간의 차이는 8시간 차이이다.
                                course.endTimeList.Add(timeList[timeList.Count - 1] + 8 + 1); // 맨 마지막 시간 끝 시간이므로 +1을 한다 .  
                                breakNum++;
                            }
                            //실험 시간 추가해주기 
                            bool daySelect;
                            string day1 = course.dayList[0];
                            if (daySelect = (day1 == course.dayList[2]))
                            {
                                course.endTimeList[0] = course.endTimeList[0] + 1;
                            }
                            else
                            {
                                course.endTimeList[1] = course.endTimeList[1] + 1;
                            }
                            course.dayList.RemoveAt(2); //중복된 값 지워주기 
                        }
                        else
                        {
                            int RepeatCount = 0;
                            foreach (Match dayMatch in dayMatches)
                            {
                                course.dayList.Add(dayMatch.Value);
                                if (RepeatCount == 0)
                                    dayIndex = dayMatch.NextMatch().Index;
                                else if (RepeatCount == 1)
                                { }
                                List<int> timeList = new List<int>();
                                while (true)
                                {
                                    if (dayIndex < commaMatch.Index && RepeatCount == 0)
                                        break;
                                    // 강의 시간 추출 
                                    if (startIndex == 0)
                                        dayTime = Int32.Parse(course.courseTime.Substring(startIndex + 1, endIndex - (startIndex + 1)));
                                    else
                                    {
                                        endIndex = commaMatch.Index;
                                        if (endIndex == 0 && course.dayList.Count == 1) //이 경우 월 1,2,화2 이런경우 즉, 두번째 요일의 시간이 괄호옆에 붙어 있는 날짜를 제외한 나머지 시간을 데이터로 수집한 상태이다,
                                            break;
                                        if (RepeatCount == 0)
                                            dayTime = Int32.Parse(course.courseTime.Substring(startIndex, endIndex - startIndex));
                                        else
                                        {
                                            if (endIndex == 0 && timeList.Count == 0)
                                            {
                                                dayTime = Int32.Parse(course.courseTime.Substring(startIndex + 1, course.courseTime.Length - (startIndex + 1)));
                                                timeList.Add(dayTime);
                                                break;
                                            }
                                            else if (endIndex == 0 && timeList.Count > 0)
                                            {
                                                dayTime = Int32.Parse(course.courseTime.Substring(startIndex, course.courseTime.Length - startIndex));
                                                timeList.Add(dayTime);
                                                break;
                                            }
                                            else
                                                dayTime = Int32.Parse(course.courseTime.Substring(startIndex + 1, endIndex - (startIndex + 1)));
                                        }
                                    }
                                    startIndex = endIndex + 1;
                                    timeList.Add(dayTime);
                                    commaMatch = commaMatch.NextMatch();

                                }
                                //시간 추출하기 
                                course.startTimeList.Add(timeList[0] + 8);//수업 교시와 실제 시간의 차이는 8시간 차이이다.
                                course.endTimeList.Add(timeList[timeList.Count - 1] + 8 + 1); // 맨 마지막 시간  
                                RepeatCount++;
                            }
                        }
                    }
                    YLoginForm.prograssCnt += 1;
                }
                //한번더 해주어야 프로세스 바 클래스 반복문 탈출 가능
                YLoginForm.prograssCnt += 1;
                driver.Close();
                driver.Quit(); //Quit을 사용해야 메모리에 남아있는 드라이버 메모리를 제거가능
                //DirectAutoClsMSGBox.Close("안내");
                //thread_directAutoMsgBox.Join();
                Save_CourseData(courseList);

                // 파일에서 데이터를 불러와 저장할 객체 

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
