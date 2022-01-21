using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessZoom_try
{
    class Course
    {
        public string id;
        public string name;
        public string deLink;
        public string courseTime; // 추후 없앨 수 있음 
        // 2개이상일 때를 대비해 List로 만들었음 
        public List<int> startTimeList = new List<int>();
        public List<int> endTimeList = new List<int>();
        public List<string> dayList = new List<string>();

        public bool isUsed = false; //초기 false로 설정 ..체크박스는 공백인상태부터 시작
        public bool isFixed = false;//초기 false로 설정
    }
}
