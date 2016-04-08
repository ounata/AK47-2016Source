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
	/// This object represents the properties and methods of a ClassLesson.
	/// 班组班级课次表
	/// </summary>
	[Serializable]
    [ORTableMapping("ClassLessons")]
    [DataContract]
	public class ClassLesson
	{		
		public ClassLesson()
		{
		}		

		/// <summary>
		/// 班级ID
		/// </summary>
		[ORFieldMapping("ClassID")]
        [DataMember]
		public string ClassID
		{
			get;
            set;
		}

		/// <summary>
		/// 顺序号
		/// </summary>
		[ORFieldMapping("SortNo")]
        [DataMember]
		public int SortNo
		{
			get;
            set;
		}

		/// <summary>
		/// 课次ID
		/// </summary>
		[ORFieldMapping("LessonID", PrimaryKey=true)]
        [DataMember]
		public string LessonID
		{
			get;
            set;
		}

		/// <summary>
		/// 课次编号
		/// </summary>
		[ORFieldMapping("LessonCode")]
        [DataMember]
		public string LessonCode
		{
			get;
            set;
		}

		/// <summary>
		/// 课次状态（1-排定，3-已上，9-已删除）
		/// </summary>
		[ORFieldMapping("LessonStatus")]
        [DataMember]
		public string LessonStatus
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
		/// 确认状态（参考排课确认状态）
		/// </summary>
		[ORFieldMapping("ConfirmStatus")]
        [DataMember]
		public string ConfirmStatus
		{
			get;
            set;
		}

		/// <summary>
		/// 最后确认时间
		/// </summary>
		[ORFieldMapping("ConfirmTime")]
        [DataMember]
		public DateTime ConfirmTime
		{
			get;
            set;
		}

		/// <summary>
		/// 已上课确认人数
		/// </summary>
		[ORFieldMapping("ConfirmedPeoples")]
        [DataMember]
		public int ConfirmedPeoples
		{
			get;
            set;
		}

		/// <summary>
		/// 本次课包含人数
		/// </summary>
		[ORFieldMapping("LessonPeoples")]
        [DataMember]
		public int LessonPeoples
		{
			get;
            set;
		}

		/// <summary>
		/// 本次课教室ID
		/// </summary>
		[ORFieldMapping("RoomID")]
        [DataMember]
		public string RoomID
		{
			get;
            set;
		}

		/// <summary>
		/// 本次课教师编码
		/// </summary>
		[ORFieldMapping("RoomCode")]
        [DataMember]
		public string RoomCode
		{
			get;
            set;
		}

		/// <summary>
		/// 本次课教师名称
		/// </summary>
		[ORFieldMapping("RoomName")]
        [DataMember]
		public string RoomName
		{
			get;
            set;
		}

		/// <summary>
		/// 本次课教师ID
		/// </summary>
		[ORFieldMapping("TeacherID")]
        [DataMember]
		public string TeacherID
		{
			get;
            set;
		}

		/// <summary>
		/// 本次课教师编码
		/// </summary>
		[ORFieldMapping("TeacherCode")]
        [DataMember]
		public string TeacherCode
		{
			get;
            set;
		}

		/// <summary>
		/// 本次课教师名称
		/// </summary>
		[ORFieldMapping("TeacherName")]
        [DataMember]
		public string TeacherName
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
    public class ClassLessonCollection : EditableDataObjectCollectionBase<ClassLesson>
    {
    }
}