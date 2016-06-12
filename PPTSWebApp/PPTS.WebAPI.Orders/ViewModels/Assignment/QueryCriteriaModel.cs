using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Orders.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.Assignment
{
    /// 按教师排课，获取教师列表，查询条件模型
    public class TeacherQCM : PageParamsBase
    {
        #region
        [ConditionMapping("TeacherName", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string TeacherName { get; set; }

        [ConditionMapping("GradeMemo", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string GradeMemo { get; set; }

        [ConditionMapping("SubjectMemo", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string SubjectMemo { get; set; }

        //[ConditionMapping("IsFullTime")]
        //public BooleanState IsFullTime { get; set;}

        [ConditionMapping("IsFullTime")]
        public int? IsFullTime { get; set; }

        [ConditionMapping("Gender")]
        public string Gender { get; set; }

        [ConditionMapping("CampusID")]
        public string CampusID { get; set; }

        [ConditionMapping("Birthday", Operation = ">=")]
        public DateTime MoreBirthday { get; set; }


        [ConditionMapping("Birthday", Operation = "<=")]
        public DateTime LessBirthday { get; set; }

        public TeacherQCM()
        {
            //this.IsFullTime = BooleanState.Unknown;
        }
        #endregion
    }

    ///学员视图，排课条件列表，查询条件模型
    public class AssignConditionQCM : PageParamsBase
    {
        [ConditionMapping("CustomerID")]
        public string CustomerID { get; set; }
    }

    public class AssignQCM : PageParamsBase
    {
        #region 学员

        [ConditionMapping("CustomerID")]
        public string CustomerID  { get; set;}

        [ConditionMapping("TeacherName", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string TeacherName { get; set;  }

        #endregion

        #region 教师

        [ConditionMapping("TeacherID")]
        public string TeacherID { get; set; }

        [ConditionMapping("TeacherJobID")]
        public string TeacherJobID  { get; set; }

        [ConditionMapping("CustomerName", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string CustomerName  {   get; set; }

        [InConditionMapping("IsFullTimeTeacher")]
        public int[] IsFullTimeTeacher { get; set; }

     

        #endregion

        #region share
        [ConditionMapping("StartTime", Operation = ">=")]
        public DateTime StartTime {  get; set; }

        [ConditionMapping("EndTime", Operation = "<")]
        public DateTime EndTime { get; set; }

        [ConditionMapping("Subject")]
        public string Subject { get; set; }

        [InConditionMapping("AssignStatus")]
        public int[] AssignStatus { get; set; }

        [InConditionMapping("AssignSource")]
        public int[] AssignSource { get; set; }

        [ConditionMapping("CustomerCode")]
        public string CustomerCode { get; set; }

        [ConditionMapping("EducatorName")]
        public string EducatorName { get; set; }

        [ConditionMapping("ConsultantName")]
        public string ConsultantName { get; set; }

        [ConditionMapping("Grade")]
        public string Grade { get; set; }

        #endregion

        [InConditionMapping("CampusID")]
        public string[] CampusID { get; set; }

        [ConditionMapping("AssetCode", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string AssetCode { get; set; }

        public AssignQCM()
        {
        }

    }
}