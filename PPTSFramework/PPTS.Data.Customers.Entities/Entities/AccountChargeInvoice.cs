using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;
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
    [ORTableMapping("CM.AccountChargeInvoices")]
    [DataContract]
	public class AccountChargeInvoice : IEntityWithCreator, IEntityWithModifier
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
		[ORFieldMapping("InvoiceTime", UtcTimeToLocal = true)]
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
        /// 开票状态（1-正常，2-已退）
        /// </summary>
        [ConstantCategory("C_CODE_ABBR_Account_InvoiceStatus")]
        [ORFieldMapping("InvoiceStatus")]
        [DataMember]
		public InvoiceStatusDefine InvoiceStatus
		{
			get;
            set;
		}

        /// <summary>
        /// 发票记录状态
        /// </summary>
        [ConstantCategory("C_CODE_ABBR_Account_InvoiceRecordStatus")]
        [ORFieldMapping("IsDiscarded")]
        [DataMember]
		public InvoiceRecordStatusDefine IsDiscarded
		{
			get;
            set;
		}

		/// <summary>
		/// 退票时间
		/// </summary>
		[ORFieldMapping("ReturnTime", UtcTimeToLocal = true)]
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
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
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
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
        [DataMember]
        public string CreatorName
        {
            get;
            set;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        [ORFieldMapping("CreateTime", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
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
        [ORFieldMapping("ModifyTime", UtcTimeToLocal = true)]
        [SqlBehavior(DefaultExpression = "GETUTCDATE()", ForceUseDefaultExpression = true)]
        [DataMember]
        public DateTime ModifyTime
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