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
	/// This object represents the properties and methods of a AccountsEx.
	/// 账户信息扩展表
	/// </summary>
	[Serializable]
    [ORTableMapping("AccountsEx")]
    [DataContract]
	public class AccountsEx
	{		
		public AccountsEx()
		{
		}		

		/// <summary>
		/// 账户ID
		/// </summary>
		[ORFieldMapping("AccountID", PrimaryKey=true)]
        [DataMember]
		public string AccountID
		{
			get;
            set;
		}

		/// <summary>
		/// 累计充值金额
		/// </summary>
		[ORFieldMapping("ChargeMoney")]
        [DataMember]
		public decimal ChargeMoney
		{
			get;
            set;
		}

		/// <summary>
		/// 累计转入金额
		/// </summary>
		[ORFieldMapping("TransferInMoney")]
        [DataMember]
		public decimal TransferInMoney
		{
			get;
            set;
		}

		/// <summary>
		/// 累计转出金额
		/// </summary>
		[ORFieldMapping("TransferOutMoney")]
        [DataMember]
		public decimal TransferOutMoney
		{
			get;
            set;
		}

		/// <summary>
		/// 累计退款金额
		/// </summary>
		[ORFieldMapping("RefundMoney")]
        [DataMember]
		public decimal RefundMoney
		{
			get;
            set;
		}

		/// <summary>
		/// 确认收入总额（包含服务费）
		/// </summary>
		[ORFieldMapping("IncomeMoney")]
        [DataMember]
		public decimal IncomeMoney
		{
			get;
            set;
		}

		/// <summary>
		/// 服务费总额
		/// </summary>
		[ORFieldMapping("ExpenseMoney")]
        [DataMember]
		public decimal ExpenseMoney
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
    public class AccountsExCollection : EditableDataObjectCollectionBase<AccountsEx>
    {
    }
}