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
	/// This object represents the properties and methods of a AccountChargeAllot.
	/// 收费责任分配表
	/// </summary>
	[Serializable]
    [ORTableMapping("AccountChargeAllots")]
    [DataContract]
	public class AccountChargeAllot
	{		
		public AccountChargeAllot()
		{
		}		

		/// <summary>
		/// 申请ID
		/// </summary>
		[ORFieldMapping("ApplyID")]
        [DataMember]
		public string ApplyID
		{
			get;
            set;
		}

		/// <summary>
		/// 责任分配ID
		/// </summary>
		[ORFieldMapping("AllotID", PrimaryKey=true)]
        [DataMember]
		public string AllotID
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
		/// 教师类型（全职，兼职）
		/// </summary>
		[ORFieldMapping("TeacherType")]
        [DataMember]
		public string TeacherType
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
		/// 产品类型（一对一，班组...）
		/// </summary>
		[ORFieldMapping("CategoryType")]
        [DataMember]
		public string CategoryType
		{
			get;
            set;
		}

		/// <summary>
		/// 课时
		/// </summary>
		[ORFieldMapping("AllotAmount")]
        [DataMember]
		public decimal AllotAmount
		{
			get;
            set;
		}

		/// <summary>
		/// 金额
		/// </summary>
		[ORFieldMapping("AllotMoney")]
        [DataMember]
		public decimal AllotMoney
		{
			get;
            set;
		}
	}

    [Serializable]
    [DataContract]
    public class AccountChargeAllotCollection : EditableDataObjectCollectionBase<AccountChargeAllot>
    {
    }
}