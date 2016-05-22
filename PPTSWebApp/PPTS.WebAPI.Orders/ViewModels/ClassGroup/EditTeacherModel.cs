using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.ClassGroup
{
    public class EditTeacherModel
    {
        /// <summary>
        /// 班级ID
        /// </summary>
        public string ClassID { get; set; }

        /// <summary>
        /// 开始课次
        /// </summary>
        public int StartLessonNum { get; set; }

        /// <summary>
        /// 结束课次
        /// </summary>
        public int EndLessonNum { get; set; }


        /// <summary>
        /// 教师ID
        /// </summary>
        public string TeacherID
        {
            get;
            set;
        }

        /// <summary>
        /// 教师编号
        /// </summary>
        public string TeacherCode
        {
            get;
            set;
        }

        /// <summary>
        /// 教师名
        /// </summary>
        public string TeacherName
        {
            get;
            set;
        }

    }
}