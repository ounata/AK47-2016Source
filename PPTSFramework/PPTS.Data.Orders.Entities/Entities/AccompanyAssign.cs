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
	/// This object represents the properties and methods of a AccompanyAssign.
	/// 
	/// </summary>
	[Serializable]
    [ORTableMapping("AccompanyAssigns")]
    [DataContract]
	public class AccompanyAssign
	{		
		public AccompanyAssign()
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
		/// 排定 ID
		/// </summary>
		[ORFieldMapping("AssignID", PrimaryKey=true)]
        [DataMember]
		public string AssignID
		{
			get;
            set;
		}

		/// <summary>
		/// 排定时间
		/// </summary>
		[ORFieldMapping("AssignTime")]
        [DataMember]
		public DateTime AssignTime
		{
			get;
            set;
		}

		/// <summary>
		/// 排定状态
		/// </summary>
		[ORFieldMapping("AssignStatus")]
        [DataMember]
		public string AssignStatus
		{
			get;
            set;
		}

		/// <summary>
		/// 数量
		/// </summary>
		[ORFieldMapping("Amount")]
        [DataMember]
		public decimal Amount
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
	}

    [Serializable]
    [DataContract]
    public class AccompanyAssignCollection : EditableDataObjectCollectionBase<AccompanyAssign>
    {
    }
}