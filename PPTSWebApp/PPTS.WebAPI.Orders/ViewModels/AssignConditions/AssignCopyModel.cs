using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace PPTS.WebAPI.Orders.ViewModels.AssignConditions
{
    public class AssignCopyModel
    {
        /// <summary>
        /// 学区ID
        /// </summary>
        [DataMember]
        public string CustomerCampusID
        {
            get; set;
        }
        /// <summary>
        /// 学员ID
        /// </summary>
        [DataMember]
        public string CustomerID
        {
            get;
            set;
        }
        /// <summary>
        /// 复制课表源开始日期
        /// </summary>
        [DataMember]
        public DateTime srcDateCourseStart { get; set; }
        /// <summary>
        /// 复制课表源结束日期
        /// </summary>
        [DataMember]
        public DateTime srcDateCourseEnd { get; set; }
        /// <summary>
        /// 复制课表目标开始日期
        /// </summary>
        [DataMember]
        public DateTime destDateCourseStart { get; set; }
        /// <summary>
        /// 复制课表目标结束日期
        /// </summary>
        [DataMember]
        public DateTime destDateCourseEnd { get; set; }
    }
}