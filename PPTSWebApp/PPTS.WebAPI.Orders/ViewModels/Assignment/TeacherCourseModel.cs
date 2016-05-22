using PPTS.Data.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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
            tchWeekCourse.TeacherName = courses.Key;
            foreach (var course in courses)
            {
                tchWeekCourse.WeekCourse[GetRowIndex(course.StartTime)][GetColIndex(course.StartTime)] = course.CustomerName;
            }
            return tchWeekCourse;
        }

        private int GetRowIndex(DateTime courseDate)
        {
            return 0;
        }
        private int GetColIndex(DateTime courseDate)
        {
            return 0;
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
        public string TeacherName { get; set; }
        public IList<string[]> WeekCourse { get; set; }

        public TWC()
        {
            WeekCourse = new List<string[]>();
            string[] row = new string[] { "星期", "8:00-10:00", "10:00-12:00", "13:00-15:00", "15:00-17:00", "17:00-19:00", "19:00-21:00" };
            WeekCourse.Add(row);
            string[] weeks = new string[] { "星期一", "星期二", "星期三", "星期四", "星期五", "星期六", "星期日" };
            foreach (var w in weeks)
            {
                row = new string[] { w, "", "", "", "", "", "" };
                WeekCourse.Add(row);
            }
        }
    }
}