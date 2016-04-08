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
	/// This object represents the properties and methods of a AccountChargeInvoice.
	/// 账户缴费发票记录表
	/// </summary>
	[Serializable]
    [ORTableMapping("AccountChargeInvoices")]
    [DataContract]
	public class AccountChargeInvoice
	{		
		public AccountChargeInvoice()
		{
		}		

		/// <summary>
		/// 申请单ID
		/// </summary>
		[ORFieldMapping("ApplyID")]
        [DataMember]
		public string ApplyID
		{
			get;
            set;
		}

		/// <summary>
		/// 发票ID
		/// </summary>
		[ORFieldMapping("InvoiceID", PrimaryKey=true)]
        [DataMember]
		public string InvoiceID
		{
			get;
            set;
		}

		/// <summary>
		/// 发票号
		/// </summary>
		[ORFieldMapping("InvoiceNo")]
        [DataMember]
		public string InvoiceNo
		{
			get;
            set;
		}

		/// <summary>
		/// 发票金额
		/// </summary>
		[ORFieldMapping("InvoiceMoney")]
        [DataMember]
		public decimal InvoiceMoney
		{
			get;
            set;
		}

		/// <summary>
		/// 发票条目
		/// </summary>
		[ORFieldMapping("InvoiceClauses")]
        [DataMember]
		public string InvoiceClauses
		{
			get;
            set;
		}

		/// <summary>
		/// 发票抬头
		/// </summary>
		[ORFieldMapping("InvoiceHeader")]
        [DataMember]
		public string InvoiceHeader
		{
			get;
            set;
		}

		/// <summary>
		/// 开票时间
		/// </summary>
		[ORFieldMapping("InvoiceTime")]
        [DataMember]
		public DateTime InvoiceTime
		{
			get;
            set;
		}

		/// <summary>
		/// 修改说明
		/// </summary>
		[ORFieldMapping("InvoiceMemo")]
        [DataMember]
		public string InvoiceMemo
		{
			get;
            set;
		}

		/// <summary>
		/// 开票状态（正常，已退）
		/// </summary>
		[ORFieldMapping("InvoiceStatus")]
        [DataMember]
		public string InvoiceStatus
		{
			get;
            set;
		}

		/// <summary>
		/// 是否作废
		/// </summary>
		[ORFieldMapping("IsDiscarded")]
        [DataMember]
		public int IsDiscarded
		{
			get;
            set;
		}

		/// <summary>
		/// 退票时间
		/// </summary>
		[ORFieldMapping("ReturnTime")]
        [DataMember]
		public DateTime ReturnTime
		{
			get;
            set;
		}

		/// <summary>
		/// 退票说明
		/// </summary>
		[ORFieldMapping("ReturnMemo")]
        [DataMember]
		public string ReturnMemo
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
    public class AccountChargeInvoiceCollection : EditableDataObjectCollectionBase<AccountChargeInvoice>
    {
    }
}