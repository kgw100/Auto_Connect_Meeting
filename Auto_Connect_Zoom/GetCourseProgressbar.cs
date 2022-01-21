using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using System.Threading;
using System.IO;

namespace AccessZoom_try
{
    public partial class GetCourseProgressbar : MetroForm, IDisposable
    {
        public DataGetEventHandler dataGetEvent;

        public GetCourseProgressbar()
        {
            InitializeComponent();
            init();
            //Thread thread_setPBMax = new Thread(new ParameterizedThreadStart(SetPBMax));
            Application.Idle += GetCourseProgressbar_Load ;
           
        }
        private void init()
        {
            lbInfo.Text = "계정 정보를 확인중입니다...";   
        }
        private void GetCourseProgressbar_Load(object sedner, EventArgs e)
        {
            Thread thread_addPBV = new Thread(AddPrograssBarValue);
            Application.Idle -= GetCourseProgressbar_Load;
            thread_addPBV.Start();

        }
        private void AddPrograssBarValue()
        {
            try
            {
                int addValue;
                int tempPrograssCnt=0; //그전에 카운트 값을 확인하는 용도로 프로세스 값을 증가시킬 말지 결정하는 변수
                while (true)
                {
                    if (YLoginForm.setPBMax != 0)
                    {
                        addValue = Convert.ToInt32(Math.Round(100 / (double)YLoginForm.setPBMax));
                        //소요 시간 계산
                        lbwait.Text = "약 "+(YLoginForm.setPBMax*2+1).ToString()+"초 정도 소요됩니다.";
                        break;
                    }
                    else
                        Thread.Sleep(500);
                       
                }
                
                lbwait.Location = new Point(lbwait.Location.X - 20, lbwait.Location.Y);
                lbInfo.Location = new Point(lbInfo.Location.X+5, lbInfo.Location.Y);
                while (YLoginForm.prograssCnt <= YLoginForm.setPBMax)
                {
                    if (YLoginForm.prograssCnt != tempPrograssCnt)
                    {
                        //부드럽게 채워주기  
                        int semiAdd = 0; //세부 증가값
                        while(semiAdd <= addValue)
                        {
                            pb.Value += 1;
                            lbInfo.Text = "강의 정보 수집중... " + pb.Value.ToString() + "%";
                            semiAdd++;
                            Thread.Sleep(10);
                        }
                        
                        tempPrograssCnt = YLoginForm.prograssCnt;
                    }
                    Thread.Sleep(500);
                }
                if (pb.Value <= 100)
                {
                    pb.Value += (100 - pb.Value); //100채워주기
                    lbInfo.Text = "강의 정보 수집중... " + pb.Value.ToString() + "%";
                    lbwait.Location = new Point(lbwait.Location.X - 30,lbwait.Location.Y);
                    lbwait.Text = "완료! 데이터 파일을 생성중입니다.";

                    Thread.Sleep(1500);
                }
                else
                    Thread.Sleep(150);
                //this.Close();
            }
            catch(Exception e)
            {
               
            }
        }
        ~GetCourseProgressbar()
        {
            GC.Collect();
        }
    }

}
