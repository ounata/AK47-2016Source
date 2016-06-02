using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using PPTS.Data.Common.Entities;

namespace PPTS.Data.Orders.Entities
{
    /// This object represents the properties and methods of a AssignCondition.
    /// 排课条件表
    [Serializable]
    [ORTableMapping("OM.AssignConditions")]
    [DataContract]
    public class AssignCondition : IAssignConditionAttr, IAssignShareAttr, IEntityWithCreator, IEntityWithModifier
    {
        public AssignCondition()
        {
        }

        #region  IAssignCondition

        /// 排课条件ID
        [ORFieldMapping("ConditionID", PrimaryKey = true)]
        [DataMember]
        public string ConditionID
        {
            get;
            set;
        }

        /// 排课条件名称（学员视图排课条件名称（资产编码+科目+老师+年级））
        [ORFieldMapping("ConditionName4Customer")]
        [DataMember]
        public string ConditionName4Customer
        {
            get;
            set;
        }

        /// 排课条件名称（教师视图排课条件名称（资产编码+科目+学员+年级））
        [ORFieldMapping("ConditionName4Teacher")]
        [DataMember]
        public string ConditionName4Teacher
        {
            get;
            set;
        }

		/// 课程级别代码
		[ORFieldMapping("CourseLevel")]
        [DataMember]
        public string CourseLevel
        {
            get;
            set;
        }

        /// 课程级别名称
        [ORFieldMapping("CourseLevelName")]
        [DataMember]
        public string CourseLevelName
        {
            get;
            set;
        }

        /// 课次时长代码
        [ORFieldMapping("LessonDuration")]
        [DataMember]
        public string LessonDuration
        {
            get;
            set;
        }

        /// 课次时长名称
        [ORFieldMapping("LessonDurationValue")]
        [DataMember]
        public decimal LessonDurationValue
        {
            get;
            set;
        }

        #endregion

        #region IAssignShareAttr

        /// 学员ID
        [ORFieldMapping("CustomerID")]
        [DataMember]
        public string CustomerID
        {
            get;
            set;
        }

        /// 学员编码
        [ORFieldMapping("CustomerCode")]
        [DataMember]
       public string CustomerCode
        {
            get;
            set;
        }

        /// 学员姓名
        [ORFieldMapping("CustomerName")]
        [DataMember]
        public string CustomerName
        {
            get;
            set;
        }

        /// 资产ID
        [ORFieldMapping("AssetID")]
        [DataMember]
        public string AssetID
        {
            get;
            set;
        }

        /// 资产编码
        [ORFieldMapping("AssetCode")]
        [DataMember]
        public string AssetCode
        {
            get;
            set;
        }

        /// 产品ID
        [ORFieldMapping("ProductID")]
        [DataMember]
        public string ProductID
        {
            get;
            set;
        }

        /// 产品编码
        [ORFieldMapping("ProductCode")]
        [DataMember]
        public string ProductCode
        {
            get;
            set;
        }

        /// 产品名称
        [ORFieldMapping("ProductName")]
        [DataMember]
        public string ProductName
        {
            get;
            set;
        }

        /// 年级代码
        [ORFieldMapping("Grade")]
        [DataMember]
        public string Grade
        {
            get;
            set;
        }

        /// 年级名称
        [ORFieldMapping("GradeName")]
        [DataMember]
        public string GradeName
        {
            get;
            set;
        }

        /// 科目代码
        [ORFieldMapping("Subject")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_BO_Product_TeacherSubject")]
        public string Subject
        {
            get;
            set;
        }

        /// 科目名称
        [ORFieldMapping("SubjectName")]
        [DataMember]
        public string SubjectName
        {
            get;
            set;
        }

        /// <summary>
        /// 教室ID
        /// </summary>
        [ORFieldMapping("RoomID")]
        [DataMember]
        public string RoomID
        {
            get;
            set;
        }

        /// <summary>
        /// 教室编码
        /// </summary>
        [ORFieldMapping("RoomCode")]
        [DataMember]
        public string RoomCode
        {
            get;
            set;
        }

        /// <summary>
        /// 教室名称
        /// </summary>
        [ORFieldMapping("RoomName")]
        [DataMember]
        public string RoomName
        {
            get;
            set;
        }

        /// <summary>
        /// 教师ID
        /// </summary>
        [ORFieldMapping("TeacherID")]
        [DataMember]
        public string TeacherID
        {
            get;
            set;
        }

        /// <summary>
        /// 教师姓名
        /// </summary>
        [ORFieldMapping("TeacherName")]
        [DataMember]
        public string TeacherName
        {
            get;
            set;
        }

        /// <summary>
        /// 教师岗位ID
        /// </summary>
        [ORFieldMapping("TeacherJobID")]
        [DataMember]
        public string TeacherJobID
        { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [ORFieldMapping("CreateTime", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
        [DataMember]
        public DateTime CreateTime
        {
            get;
            set;
        }

        /// 最后修改时间
        [ORFieldMapping("ModifyTime", UtcTimeToLocal = true)]
        [SqlBehavior(DefaultExpression = "GETUTCDATE()", ForceUseDefaultExpression = true)]
        [DataMember]
        public DateTime ModifyTime
        {
            get;
            set;
        }

        [ORFieldMapping("TenantCode")]
        [DataMember]
        public string TenantCode
        {
            get;
            set;
        }

        ///教师学科组名称
        [ORFieldMapping("TeacherJobOrgName")]
        [DataMember]
        public string TeacherJobOrgName
        {
            get; set;
        }

        ///教师学科组ID
        [ORFieldMapping("TeacherJobOrgID")]
        [DataMember]
        public string TeacherJobOrgID
        {
            get; set;
        }

        ///教师，全职还是兼职
        [ConstantCategory("Common_TeacherType")]
        [ORFieldMapping("IsFullTimeTeacher")]
        [DataMember]
        public int IsFullTimeTeacher { get; set; }

        ///学员账户ID
        [ORFieldMapping("AccountID")]
        [DataMember]
        public string AccountID { get; set; }

        #endregion

        #region IEntityWithModifier
        /// <summary>
        /// 最后修改人ID
        /// </summary>
        [ORFieldMapping("ModifierID")]
        [DataMember]
        public string ModifierID
        {
            get;
            set;
        }

        /// <summary>
        /// 最后修改人姓名
        /// </summary>
        [ORFieldMapping("ModifierName")]
        [DataMember]
        public string ModifierName
        {
            get;
            set;
        }
        #endregion

        #region IEntityWithCreator
        /// <summary>
        /// 创建人ID
        /// </summary>
        [ORFieldMapping("CreatorID")]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
        [DataMember]
        public string CreatorID
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        [ORFieldMapping("CreatorName")]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
        [DataMember]
        public string CreatorName
        {
            get;
            set;
        }
        #endregion
    }

    [Serializable]
    [DataContract]
    public class AssignConditionCollection : EditableDataObjectCollectionBase<AssignCondition>
    {
    }


    public interface IAssignConditionAttr
    {
        #region

        /// 排课条件ID
        string ConditionID
        {
            get;
            set;
        }

        /// 排课条件名称（学员视图排课条件名称（资产编码+科目+老师+年级））
        string ConditionName4Customer
        {
            get;
            set;
        }

        /// 排课条件名称（教师视图排课条件名称（资产编码+科目+学员+年级））
        string ConditionName4Teacher
        {
            get;
            set;
        }

		/// 课程级别代码
        string CourseLevel
        {
            get;
            set;
        }

		/// 课程级别名称
        string CourseLevelName
        {
            get;
            set;
        }

        /// 课次时长代码
        string LessonDuration { get; set; }

        /// 课次时长名称
        decimal LessonDurationValue { get; set; }

      

      
        

        #endregion
    }
}