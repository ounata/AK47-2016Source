using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PPTS.Data.Common.Entities
{
	/// <summary>
	/// This object represents the properties and methods of a Teacher.
	/// 
	/// </summary>
	[Serializable]
    [ORTableMapping("Teachers")]
    [DataContract]
	public class Teacher
	{		
		public Teacher()
		{
		}		

		/// <summary>
		/// 教师ID
		/// </summary>
		[ORFieldMapping("TeacherID", PrimaryKey=true)]
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
		/// 教师OA编码
		/// </summary>
		[ORFieldMapping("TeacherOACode")]
        [DataMember]
		public string TeacherOACode
		{
			get;
            set;
		}

		/// <summary>
		/// 性别
		/// </summary>
		[ORFieldMapping("Gender")]
        [DataMember]
		public string Gender
		{
			get;
            set;
		}

		/// <summary>
		/// 出生日期
		/// </summary>
		[ORFieldMapping("Birthday")]
        [DataMember]
		public DateTime Birthday
		{
			get;
            set;
		}

		/// <summary>
		/// 教授年级（用逗号分割名称）
		/// </summary>
		[ORFieldMapping("GradeMemo")]
        [DataMember]
		public string GradeMemo
		{
			get;
            set;
		}

		/// <summary>
		/// 教授科目（用逗号分割名称）
		/// </summary>
		[ORFieldMapping("SubjectMemo")]
        [DataMember]
		public string SubjectMemo
		{
			get;
            set;
		}
	}

    [Serializable]
    [DataContract]
    public class TeacherCollection : EditableDataObjectCollectionBase<Teacher>
    {
    }
}