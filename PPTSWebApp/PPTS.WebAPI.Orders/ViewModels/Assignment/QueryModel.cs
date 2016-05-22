using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.Assignment
{
    public class CrumbsQM
    {
        public string CustomerID { get; set; }
        public string AssetID { get; set; }
        public string Subject { get; set; }
        public string Grade { get; set; }
    }


    ///按学员排课 周视图 查询排课信息模型
    public class StudentAssignQM : AssignQMBase
    {
        /// 学员ID
        public string CustomerID
        {
            get;
            set;
        }
        ///教师姓名
        public string TeacherName
        {
            get;
            set;
        }
    }

    ///按教师排课 周视图 查询排课信息模型
    public class TeacherAssignQM : AssignQMBase
    {
        public string TeacherID { get; set; }
        public string TeacherJobID { get; set; }
        public string CustomerName { get; set; }
    }

    public class AssignCopyQM
    {
        #region
        public string TeacherID { get; set; }
        public string TeacherJobID { get; set; }

        public string CustomerID { get; set; }

        /// <summary>
        /// 复制课表源开始日期
        /// </summary>
        [DataMember]
        public DateTime SrcDateStart { get; set; }
        /// <summary>
        /// 复制课表源结束日期
        /// </summary>
        [DataMember]
        public DateTime SrcDateEnd { get; set; }
        /// <summary>
        /// 复制课表目标开始日期
        /// </summary>
        [DataMember]
        public DateTime DestDateStart { get; set; }
        /// <summary>
        /// 复制课表目标结束日期
        /// </summary>
        [DataMember]
        public DateTime DestDateEnd { get; set; }
        #endregion
    }

    [Serializable]
    [DataContract]
    public class AssignResetQM
    {
        #region
        /// <summary>
        /// 排课ID
        /// </summary>
        [DataMember]
        public string AssignID
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Info
        {
            get;
            set;
        }
        /// <summary>
        /// 调课后日期
        /// </summary>
        [DataMember]
        public DateTime ReDate
        {
            get;
            set;
        }
        /// <summary>
        /// 调课后小时
        /// </summary>
        [DataMember]
        public string ReHour
        {
            get;
            set;
        }
        /// <summary>
        /// 调整后分钟
        /// </summary>
        [DataMember]
        public string ReMinute
        {
            get;
            set;
        }
        /// <summary>
        /// 原排课开始时间
        /// </summary>
        [DataMember]
        public DateTime StartTime
        {
            get;
            set;
        }
        /// <summary>
        /// 原排课结束时间
        /// </summary>
        [DataMember]
        public DateTime EndTime
        {
            get;
            set;
        }
        /// <summary>
        /// 该排课允许调整的最大时间
        /// 规则： 1.时间选择框只能选择当前日期后10日内的日期，例：今天是3月25日，只可选择4月3日（含）之前的日期 
        /// </summary>
        [DataMember]
        public DateTime AllowResetDateTime
        {
            get;
            set;
        }
        #endregion
    }

}