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
	/// This object represents the properties and methods of a TeacherTeaching.
	/// 教学教学表（老师教授-科目-年级）
	/// </summary>
	[Serializable]
    [ORTableMapping("TeacherTeachings")]
    [DataContract]
	public class TeacherTeaching
	{		
		public TeacherTeaching()
		{
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
		/// 
		/// </summary>
		[ORFieldMapping("TeachingID", PrimaryKey=true)]
        [DataMember]
		public string TeachingID
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
		/// 年级代码
		/// </summary>
		[ORFieldMapping("Grade")]
        [DataMember]
		public string Grade
		{
			get;
            set;
		}
	}

    [Serializable]
    [DataContract]
    public class TeacherTeachingCollection : EditableDataObjectCollectionBase<TeacherTeaching>
    {
    }
}