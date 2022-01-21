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
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AccessZoom_try
{
    public partial class GetData 
    {
        public static ChromeDriver driver;

        // course.json데이터 저장 
        private void Save_CourseData(List<Course> courseList)
        {
            // Json 형식 다시 잡아주기
            List<CourseJson> courseJsonList = new List<CourseJson>();
            foreach (Course course in courseList)
            {
                CourseJson tempCourseJson = new CourseJson();
                tempCourseJson.courseTime = new courseTime();
                tempCourseJson.courseTime.day = new List<string>();
                tempCourseJson.courseTime.startTime = new List<int>();
                tempCourseJson.courseTime.endTime = new List<int>();
                tempCourseJson.courseId = course.id;
                tempCourseJson.courseName = course.name;
                tempCourseJson.courseTime.day = course.dayList;
                tempCourseJson.courseTime.startTime = course.startTimeList;
                tempCourseJson.courseTime.endTime = course.endTimeList;
                tempCourseJson.isFixed = false;
                tempCourseJson.isUsed = false;
                courseJsonList.Add(tempCourseJson);
            }
            // 앞에 프로퍼티이름을 정해주기 위해 dictionary를 사용했음 
            var dict = new Dictionary<string, List<CourseJson>>();
            dict.Add("courseList", courseJsonList);
            //Json 형식 생성
            string json3 = JsonConvert.SerializeObject(dict);
            //json 보기 좋게 정렬해주기 
            var res = JObject.Parse(json3);

            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"courseList.json", res.ToString());
        }
        public static void driver_Load(ref ChromeDriver driver)
        {
            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;
            var chromeOptions = new ChromeOptions();
            //chrome option 설정 
            chromeOptions.AddArguments(new List<string>() {
                "no-sandbox", "disable-gpu","log-level=3","headless","no-console" });//"headless"
            chromeOptions.AddUserProfilePreference("profile.default_content_setting_values.images", 2);
            driver = new ChromeDriver(driverService, chromeOptions);
        }

    }

}
