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
	/// <summary>
	/// This object represents the properties and methods of a AssignCondition.
	/// 排课条件表
	/// </summary>
	[Serializable]
    [ORTableMapping("OM.AssignConditions")]
    [DataContract]
	public class AssignCondition : IAssignCondition, IAssignShareAttr, IEntityWithCreator, IEntityWithModifier
    {		 
		public AssignCondition()
		{
		}

        #region  IAssignCondition
        /// <summary>
        /// 排课条件ID
        /// </summary>
        [ORFieldMapping("ConditionID", PrimaryKey=true)]
        [DataMember]
		public string ConditionID
		{
			get;
            set;
		}

        /// <summary>
        /// 排课条件名称（资产编码+科目+老师+年级）
        /// </summary>
        [ORFieldMapping("ConditionName")]
        [DataMember]
		public string ConditionName
		{
			get;
            set;
		}

        /// <summary>
        /// 产品归属校区ID
        /// </summary>
        [ORFieldMapping("ProductCampusID")]
        [DataMember]
        public string ProductCampusID
        {
            get; set;
        }

        /// <summary>
        /// 产品归属校区名称
        /// </summary>
        [ORFieldMapping("ProductCampusName")]
        [DataMember]
        public string ProductCampusName
        {
            get; set;
        }

        /// <summary>
		/// 课程级别代码
		/// </summary>
		[ORFieldMapping("CourseLevel")]
        [DataMember]
        public string CourseLevel
        {
            get;
            set;
        }

        /// <summary>
		/// 课程级别名称
		/// </summary>
		[ORFieldMapping("CourseLevelName")]
        [DataMember]
        public string CourseLevelName
        {
            get;
            set;
        }


        /// <summary>
        /// 每周课次（预留）
        /// </summary>
        [ORFieldMapping("LessonsOfWeek")]
        [DataMember]
		public decimal LessonsOfWeek
		{
			get;
            set;
		}

		/// <summary>
		/// 首次上课时间（预留）
		/// </summary>
		[ORFieldMapping("FirstLessonTime", UtcTimeToLocal = true)]
        [DataMember]
		public DateTime FirstLessonTime
		{
			get;
            set;
		}
        #endregion

        #region IAssignShareAttr
        /// <summary>
        /// 学员ID
        /// </summary>
        [ORFieldMapping("CustomerID")]
        [DataMember]
        public string CustomerID
        {
            get;
            set;
        }

        /// <summary>
        /// 资产ID
        /// </summary>
        [ORFieldMapping("AssetID")]
        [DataMember]
        public string AssetID
        {
            get;
            set;
        }

        /// <summary>
        /// 资产编码
        /// </summary>
        [ORFieldMapping("AssetCode")]
        [DataMember]
        public string AssetCode
        {
            get;
            set;
        }

        /// <summary>
        /// 产品ID
        /// </summary>
        [ORFieldMapping("ProductID")]
        [DataMember]
        public string ProductID
        {
            get;
            set;
        }

        /// <summary>
        /// 产品编码
        /// </summary>
        [ORFieldMapping("ProductCode")]
        [DataMember]
        public string ProductCode
        {
            get;
            set;
        }

        /// <summary>
        /// 产品名称
        /// </summary>
        [ORFieldMapping("ProductName")]
        [DataMember]
        public string ProductName
        {
            get;
            set;
        }

        /// <summary>
        /// 年级代码
        /// </summary>
        [ORFieldMapping("Grade")]
        [DataMember]
        public string Grade
        {
            get;
            set;
        }

        /// <summary>
        /// 年级名称
        /// </summary>
        [ORFieldMapping("GradeName")]
        [DataMember]
        public string GradeName
        {
            get;
            set;
        }

        /// <summary>
        /// 科目代码
        /// </summary>
        [ORFieldMapping("Subject")]
        [DataMember]


        [ConstantCategory("C_CODE_ABBR_BO_Product_TeacherSubject")]
        public string Subject
        {
            get;
            set;
        }

        /// <summary>
        /// 科目名称
        /// </summary>
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

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [ORFieldMapping("ModifyTime", UtcTimeToLocal = true)]
        [SqlBehavior(DefaultExpression = "GETUTCDATE()", ForceUseDefaultExpression = true)]
        [DataMember]
        public DateTime ModifyTime
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [ORFieldMapping("TenantCode")]
        [DataMember]
        public string TenantCode
        {
            get;
            set;
        }
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


    public interface IAssignCondition
    {
        #region
        /// <summary>
        /// 排课条件ID
        /// </summary>
        string ConditionID
        {
            get;
            set;
        }

        /// <summary>
        /// 排课条件名称（资产编码+科目+老师+年级）
        /// </summary>
        string ConditionName
        {
            get;
            set;
        }

        /// <summary>
        /// 产品归属校区ID
        /// </summary>
        string ProductCampusID
        {
            get; set;
        }

        /// <summary>
        /// 产品归属校区名称
        /// </summary>
        string ProductCampusName
        {
            get; set;
        }

        /// <summary>
		/// 课程级别代码
		/// </summary>
        string CourseLevel
        {
            get;
            set;
        }

        /// <summary>
		/// 课程级别名称
		/// </summary>
        string CourseLevelName
        {
            get;
            set;
        }


        /// <summary>
        /// 每周课次（预留）
        /// </summary>
        decimal LessonsOfWeek
        {
            get;
            set;
        }

        /// <summary>
        /// 首次上课时间（预留）
        /// </summary>
        DateTime FirstLessonTime
        {
            get;
            set;
        }
        #endregion
    }
}