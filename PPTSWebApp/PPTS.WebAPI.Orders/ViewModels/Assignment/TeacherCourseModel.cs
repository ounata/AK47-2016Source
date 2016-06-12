using PPTS.Data.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.Assignment
{
    public class TeacherCourseModel
    {
        public IList<TWC> TchCM { get; set; }

        public DateTime StartTime { private get; set; }
        public DateTime EndTime { private get; set; }

        public string Msg { get; private set; }

        public IList<TWC> GetTchWeekCourse(AssignCollection ac)
        {
            IList<TWC> twc = new List<TWC>();
            if (ac == null || ac.Count() == 0)
                return twc;
            if (!this.ValidateDate())
                return twc;
            ///按照教师分组
            IEnumerable<IGrouping<string,Assign>> tchGroup = ac.GroupBy(p => p.TeacherID);
            foreach (var tchcourse in tchGroup)
            {
                twc.Add(this.GetWC(tchcourse));
            }
            return twc;
        }

        private TWC GetWC(IGrouping<string,Assign> courses)
        {
            TWC tchWeekCourse = new TWC();

            string[] weeks = new string[] { "星期一", "星期二", "星期三", "星期四", "星期五", "星期六", "星期日" };
            DateTime dt = this.StartTime;
            foreach (var w in weeks)
            {
                List<Course> row = new List<Course>();
                tchWeekCourse.WeekCourse.Add(string.Format("{0}|{1}/{2}", w,dt.Month,dt.Day), new List<Course>());
                dt = dt.AddDays(1);
            }
            var tc  = courses.OrderBy(p => p.StartTime);
            foreach (var course in tc)
            {
                tchWeekCourse.TeacherName = course.TeacherName; 
                tchWeekCourse.WeekCourse[ string.Format("{0}|{1}/{2}",ConvertChinaWeek(course.StartTime.DayOfWeek),course.StartTime.Month,course.StartTime.Day)].Add( new Course()
                {
                    CustomerName =course.CustomerName,
                    Schedule = string.Format("{0}-{1}", course.StartTime.ToString("HH:mm"), course.EndTime.ToString("HH:mm"))
                } );
            }
            return tchWeekCourse;
        }

     

        private string ConvertChinaWeek(DayOfWeek dfw)
        {
            string week = string.Empty;
            switch (dfw)
            {
                case DayOfWeek.Monday:
                    week = "星期一";
                    break;
                case DayOfWeek.Tuesday:
                    week = "星期二";
                    break;
                case DayOfWeek.Wednesday:
                    week = "星期三";
                    break;
                case DayOfWeek.Thursday:
                    week = "星期四";
                    break;
                case DayOfWeek.Friday:
                    week = "星期五";
                    break;
                case DayOfWeek.Saturday:
                    week = "星期六";
                    break;
                case DayOfWeek.Sunday:
                    week = "星期日";
                    break;
            }
            return week;
        }

        ///周课表，起始日期验证
        private bool ValidateDate()
        {
            if (this.StartTime.DayOfWeek == DayOfWeek.Monday && this.EndTime.DayOfWeek == DayOfWeek.Sunday)
                return true;
            else
                return false;
        }   
    }

    public class TWC
    {
        [DataMember]
        public string TeacherName { get; set; }

        [DataMember]
        public IDictionary<string,IList<Course>> WeekCourse { get; set; }

        public TWC()
        {
            WeekCourse = new Dictionary<string, IList<Course>>();
            //List<Course> row = new List<Course>();

            //string[] weeks = new string[] { "星期一", "星期二", "星期三", "星期四", "星期五", "星期六", "星期日" };
            //foreach (var w in weeks)
            //{
            //    row = new List<Course>();
            //    WeekCourse.Add(string.Format("{0}|{1}/{2}", w,),new List<Course>());
            //}
        }
    }

    public class Course
    {
        [DataMember]
        public string CustomerName { get; set; }
        [DataMember]
        public string Schedule { get; set; }
    }
}