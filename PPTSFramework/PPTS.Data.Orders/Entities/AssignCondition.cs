using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PPTS.Data.Orders.Entities
{
	/// <summary>
	/// This object represents the properties and methods of a AssignCondition.
	/// 排课条件表
	/// </summary>
	[Serializable]
    [ORTableMapping("AssignConditions")]
    [DataContract]
	public class AssignCondition
	{		
		public AssignCondition()
		{
		}		

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
		/// 排课条件编码
		/// </summary>
		[ORFieldMapping("ConditionCode")]
        [DataMember]
		public string ConditionCode
		{
			get;
            set;
		}

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
		/// 教师编码
		/// </summary>
		[ORFieldMapping("TeacherCode")]
        [DataMember]
		public string TeacherCode
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
		[ORFieldMapping("FirstLessonTime")]
        [DataMember]
		public DateTime FirstLessonTime
		{
			get;
            set;
		}

		/// <summary>
		/// 创建人ID
		/// </summary>
		[ORFieldMapping("CreatorID")]
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
        [DataMember]
		public string CreatorName
		{
			get;
            set;
		}

		/// <summary>
		/// 创建时间
		/// </summary>
		[ORFieldMapping("CreateTime")]
        [DataMember]
		public DateTime CreateTime
		{
			get;
            set;
		}

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

		/// <summary>
		/// 最后修改时间
		/// </summary>
		[ORFieldMapping("ModifyTime")]
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
	}

    [Serializable]
    [DataContract]
    public class AssignConditionCollection : EditableDataObjectCollectionBase<AssignCondition>
    {
    }
}