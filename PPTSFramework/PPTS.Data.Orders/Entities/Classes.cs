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
	/// This object represents the properties and methods of a Class.
	/// 班组班级
	/// </summary>
	[Serializable]
    [ORTableMapping("Classes")]
    [DataContract]
	public class Classes
    {		
		public Classes()
		{
		}		

		/// <summary>
		/// 校区ID
		/// </summary>
		[ORFieldMapping("CampusID")]
        [DataMember]
		public string CampusID
		{
			get;
            set;
		}

		/// <summary>
		/// 校区名称
		/// </summary>
		[ORFieldMapping("CampusName")]
        [DataMember]
		public string CampusName
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
		/// 班级ID
		/// </summary>
		[ORFieldMapping("ClassID", PrimaryKey=true)]
        [DataMember]
		public string ClassID
		{
			get;
            set;
		}

		/// <summary>
		/// 班级编号
		/// </summary>
		[ORFieldMapping("ClassCode")]
        [DataMember]
		public string ClassCode
		{
			get;
            set;
		}

		/// <summary>
		/// 班级状态（0-新建，1-上部分，2-已上完，9-已删除）
		/// </summary>
		[ORFieldMapping("ClassStatus")]
        [DataMember]
		public string ClassStatus
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
		/// 教师编码
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
		/// 开始时间
		/// </summary>
		[ORFieldMapping("StartTime")]
        [DataMember]
		public DateTime StartTime
		{
			get;
            set;
		}

		/// <summary>
		/// 结束时间
		/// </summary>
		[ORFieldMapping("EndTime")]
        [DataMember]
		public DateTime EndTime
		{
			get;
            set;
		}

		/// <summary>
		/// 课次时长
		/// </summary>
		[ORFieldMapping("DurationValue")]
        [DataMember]
		public decimal DurationValue
		{
			get;
            set;
		}

		/// <summary>
		/// 课次总数
		/// </summary>
		[ORFieldMapping("LessonCount")]
        [DataMember]
		public int LessonCount
		{
			get;
            set;
		}

		/// <summary>
		/// 已上课次
		/// </summary>
		[ORFieldMapping("FinishedLessons")]
        [DataMember]
		public int FinishedLessons
		{
			get;
            set;
		}

		/// <summary>
		/// 班级人数
		/// </summary>
		[ORFieldMapping("ClassPeoples")]
        [DataMember]
		public int ClassPeoples
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
    public class ClassCollection : EditableDataObjectCollectionBase<Classes>
    {
    }
}