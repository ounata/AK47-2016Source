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
        public string CustomerID { get; set; }
        ///教师姓名
        public string TeacherName { get; set; }
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

        /// 复制课表源开始日期
        [DataMember]
        public DateTime SrcDateStart { get; set; }

        /// 复制课表源结束日期
        [DataMember]
        public DateTime SrcDateEnd { get; set; }

        /// 复制课表目标开始日期
        [DataMember]
        public DateTime DestDateStart { get; set; }

        /// 复制课表目标结束日期
        [DataMember]
        public DateTime DestDateEnd { get; set; }
        #endregion
    }

    [Serializable]
    [DataContract]
    public class AssignResetQM
    {
        #region

        /// 排课ID
        [DataMember]
        public string AssignID { get; set; }

        [DataMember]
        public string Info { get; set; }

        /// 调课后日期
        [DataMember]
        public DateTime ReDate { get; set; }

        /// 调课后小时
        [DataMember]
        public string ReHour { get; set; }

        /// 调整后分钟
        [DataMember]
        public string ReMinute { get; set; }

        /// 原排课开始时间
        [DataMember]
        public DateTime StartTime { get; set; }

        /// 原排课结束时间
        [DataMember]
        public DateTime EndTime { get; set; }

        ///学员ID
        [DataMember]
        public string CustomerID { get; set; }

        /// 该排课允许调整的最大时间
        /// 规则： 1.时间选择框只能选择当前日期后10日内的日期，例：今天是3月25日，只可选择4月3日（含）之前的日期 
        [DataMember]
        public DateTime AllowResetDateTime { get; set; }
        #endregion
    }

    public class ACCEditQM
    {
        public string CustomerID { get; set; }
        public string AccID { get; set; }
    }



}