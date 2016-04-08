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
	/// This object represents the properties and methods of a AccountRefundPayment.
	/// 账户退费支付表
	/// </summary>
	[Serializable]
    [ORTableMapping("AccountRefundPayments")]
    [DataContract]
	public class AccountRefundPayment
	{		
		public AccountRefundPayment()
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
		/// 支付单ID
		/// </summary>
		[ORFieldMapping("PayID", PrimaryKey=true)]
        [DataMember]
		public string PayID
		{
			get;
            set;
		}

		/// <summary>
		/// 支付单号
		/// </summary>
		[ORFieldMapping("PayNo")]
        [DataMember]
		public string PayNo
		{
			get;
            set;
		}

		/// <summary>
		/// 支付时间
		/// </summary>
		[ORFieldMapping("PayTime")]
        [DataMember]
		public DateTime PayTime
		{
			get;
            set;
		}

		/// <summary>
		/// 支付金额
		/// </summary>
		[ORFieldMapping("PayMoney")]
        [DataMember]
		public decimal PayMoney
		{
			get;
            set;
		}

		/// <summary>
		/// 支付状态（参考缴费单）
		/// </summary>
		[ORFieldMapping("PayStatus")]
        [DataMember]
		public string PayStatus
		{
			get;
            set;
		}

		/// <summary>
		/// 支付说明
		/// </summary>
		[ORFieldMapping("PayMemo")]
        [DataMember]
		public string PayMemo
		{
			get;
            set;
		}

		/// <summary>
		/// 付款人ID
		/// </summary>
		[ORFieldMapping("PayerID")]
        [DataMember]
		public string PayerID
		{
			get;
            set;
		}

		/// <summary>
		/// 付款人姓名
		/// </summary>
		[ORFieldMapping("PayerName")]
        [DataMember]
		public string PayerName
		{
			get;
            set;
		}

		/// <summary>
		/// 付款人岗位ID
		/// </summary>
		[ORFieldMapping("PayerJobID")]
        [DataMember]
		public string PayerJobID
		{
			get;
            set;
		}

		/// <summary>
		/// 付款人岗位名称
		/// </summary>
		[ORFieldMapping("PayerJobName")]
        [DataMember]
		public string PayerJobName
		{
			get;
            set;
		}

		/// <summary>
		/// 
		/// </summary>
		[ORFieldMapping("Checked")]
        [DataMember]
		public int Checked
		{
			get;
            set;
		}

		/// <summary>
		/// 对账时间
		/// </summary>
		[ORFieldMapping("CheckTime")]
        [DataMember]
		public DateTime CheckTime
		{
			get;
            set;
		}

		/// <summary>
		/// 对账人ID
		/// </summary>
		[ORFieldMapping("CheckerID")]
        [DataMember]
		public string CheckerID
		{
			get;
            set;
		}

		/// <summary>
		/// 对账人姓名
		/// </summary>
		[ORFieldMapping("CheckerName")]
        [DataMember]
		public string CheckerName
		{
			get;
            set;
		}

		/// <summary>
		/// 对账人岗位ID
		/// </summary>
		[ORFieldMapping("CheckerJobID")]
        [DataMember]
		public string CheckerJobID
		{
			get;
            set;
		}

		/// <summary>
		/// 对账人岗位名称
		/// </summary>
		[ORFieldMapping("CheckerJobName")]
        [DataMember]
		public string CheckerJobName
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
    public class AccountRefundPaymentCollection : EditableDataObjectCollectionBase<AccountRefundPayment>
    {
    }
}