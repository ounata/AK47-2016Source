using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PPTS.Data.Customers.Entities
{
	/// <summary>
	/// This object represents the properties and methods of a CustomerScoreItem.
	/// 学员成绩明细表
	/// </summary>
	[Serializable]
    [ORTableMapping("CustomerScoreItems")]
    [DataContract]
	public class CustomerScoreItem
	{		
		public CustomerScoreItem()
		{
		}		

		/// <summary>
		/// 成绩ID
		/// </summary>
		[ORFieldMapping("ScoreID")]
        [DataMember]
		public string ScoreID
		{
			get;
            set;
		}

		/// <summary>
		/// 成绩明细ID
		/// </summary>
		[ORFieldMapping("ItemID", PrimaryKey=true)]
        [DataMember]
		public string ItemID
		{
			get;
            set;
		}

		/// <summary>
		/// 显示顺序
		/// </summary>
		[ORFieldMapping("SortNo")]
        [DataMember]
		public int SortNo
		{
			get;
            set;
		}

		/// <summary>
		/// 科目
		/// </summary>
		[ORFieldMapping("Subject")]
        [DataMember]
		public string Subject
		{
			get;
            set;
		}

		/// <summary>
		/// 辅导老师ID
		/// </summary>
		[ORFieldMapping("TeacherID")]
        [DataMember]
		public string TeacherID
		{
			get;
            set;
		}

		/// <summary>
		/// 辅导老师姓名
		/// </summary>
		[ORFieldMapping("TeacherName")]
        [DataMember]
		public string TeacherName
		{
			get;
            set;
		}

		/// <summary>
		/// 卷面分
		/// </summary>
		[ORFieldMapping("PaperScore")]
        [DataMember]
		public decimal PaperScore
		{
			get;
            set;
		}

		/// <summary>
		/// 得分
		/// </summary>
		[ORFieldMapping("RealScore")]
        [DataMember]
		public decimal RealScore
		{
			get;
            set;
		}

		/// <summary>
		/// 年纪名词
		/// </summary>
		[ORFieldMapping("GradeRank")]
        [DataMember]
		public int GradeRank
		{
			get;
            set;
		}

		/// <summary>
		/// 班级名次
		/// </summary>
		[ORFieldMapping("ClassRank")]
        [DataMember]
		public int ClassRank
		{
			get;
            set;
		}

		/// <summary>
		/// 家长满意度
		/// </summary>
		[ORFieldMapping("Satisficing")]
        [DataMember]
		public string Satisficing
		{
			get;
            set;
		}

		/// <summary>
		/// 是否在该学校辅导
		/// </summary>
		[ORFieldMapping("IsStudyHere")]
        [DataMember]
		public bool IsStudyHere
		{
			get;
            set;
		}
	}

    [Serializable]
    [DataContract]
    public class CustomerScoreItemCollection : EditableDataObjectCollectionBase<CustomerScoreItem>
    {
    }
}