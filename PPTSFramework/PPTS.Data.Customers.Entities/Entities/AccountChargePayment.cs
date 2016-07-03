using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common;
using PPTS.Data.Common.Authorization;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PPTS.Data.Customers.Entities
{
    /// <summary>
    /// This object represents the properties and methods of a AccountChargePayment.
    /// 账户缴费支付单表
    /// </summary>
    #region 数据范围权限(存入识别)
    [EntityAuth(RecordType = RecordType.AccountChargePayment)]
    #endregion
    [Serializable]
    [ORTableMapping("CM.AccountChargePayments")]
    [DataContract]
	public class AccountChargePayment : IEntityWithCreator, IEntityWithModifier
	{		
		public AccountChargePayment()
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
        /// 输入时间
        /// </summary>
        [ORFieldMapping("InputTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime InputTime
        {
            get;
            set;
        }

        /// <summary>
        /// 刷卡时间
        /// </summary>
        [ORFieldMapping("SwipeTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime SwipeTime
        {
            get;
            set;
        }

        /// <summary>
        /// 收款时间（很多规则在里面）
        /// </summary>
        [ORFieldMapping("PayTime", UtcTimeToLocal = true)]
        [DataMember]
		public DateTime PayTime
		{
			get;
            set;
		}

		/// <summary>
		/// 支付方式
        /// </summary>
		[ORFieldMapping("PayType")]
        [ConstantCategory(Category = "Account_PayType")]
        [DataMember]
		public string PayType
		{
			get;
            set;
		}

        /// <summary>
        /// 小票号
        /// </summary>
        [ORFieldMapping("PayTicket")]
        [DataMember]
        public string PayTicket
        {
            get;
            set;
        }
        /// <summary>
        /// 支付状态（未支付，已支付）
        /// </summary>
        [ORFieldMapping("PayStatus")]
        [DataMember]
		public PayStatusDefine PayStatus
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
        /// 打印状态
        /// </summary>
        [DataMember]
        [ConstantCategory(Category = "Common_PrintStatus")]
        public PrintStatusDefine PrintStatus
        {
            set;
            get;
        }
        
		/// <summary>
		/// 回款人
		/// </summary>
		[ORFieldMapping("Salesman")]
        [DataMember]
		public string Salesman
		{
			get;
            set;
		}

		/// <summary>
		/// 付款人（默认学员姓名）
		/// </summary>
		[ORFieldMapping("Payer")]
        [DataMember]
		public string Payer
		{
			get;
            set;
		}

		/// <summary>
		/// 收款人ID
		/// </summary>
		[ORFieldMapping("PayeeID")]
        [DataMember]
		public string PayeeID
		{
			get;
            set;
		}

		/// <summary>
		/// 收款人姓名
		/// </summary>
		[ORFieldMapping("PayeeName")]
        [DataMember]
		public string PayeeName
		{
			get;
            set;
		}

		/// <summary>
		/// 收款人岗位ID
		/// </summary>
		[ORFieldMapping("PayeeJobID")]
        [DataMember]
		public string PayeeJobID
		{
			get;
            set;
		}

		/// <summary>
		/// 收款人岗位名称
		/// </summary>
		[ORFieldMapping("PayeeJobName")]
        [DataMember]
		public string PayeeJobName
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
		/// 对账人岗位姓名
		/// </summary>
		[ORFieldMapping("CheckerJobName")]
        [DataMember]
		public string CheckerJobName
		{
			get;
            set;
		}

        /// <summary>
        /// 对账状态
        /// </summary>
        [ORFieldMapping("CheckStatus")]
        [ConstantCategory(Category = "Common_CheckStatus")]
        [DataMember]
        public CheckStatusDefine CheckStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 对账日期
        /// </summary>
        [ORFieldMapping("CheckTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime CheckTime
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
    public class AccountChargePaymentCollection : EditableDataObjectCollectionBase<AccountChargePayment>
    {
    }
}