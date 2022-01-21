using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessZoom_try
{
    public class isUsedZoomCourse
    {
        public string courseId { get; set; }
        public bool isFixed { get; set;}
        public List<string> dayList;
        public List<int> startTimeList;
        public List<int> endTimeList;
        public isUsedZoomCourse()
        {
            this.courseId = "";
            this.isFixed = false;
        }
        public isUsedZoomCourse(string courseId, bool isFixed,
            List<string>dayList,List<int>startTimeList,List<int>endTimeList)
        {
            dayList = new List<string>();
            startTimeList = new List<int>();
            endTimeList = new List<int>();
            this.courseId = courseId;
            this.isFixed = isFixed;
            this.dayList = dayList;
            this.startTimeList = startTimeList;
            this.endTimeList = endTimeList;
        }
      
    }
}
