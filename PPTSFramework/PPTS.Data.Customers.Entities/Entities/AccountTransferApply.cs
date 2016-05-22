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
	/// This object represents the properties and methods of a AccountTransferApply.
	/// 账户转让申请表
	/// </summary>
	[Serializable]
    [ORTableMapping("CM.AccountTransferApplies")]
    [DataContract]
	public class AccountTransferApply : IEntityWithCreator, IEntityWithModifier
    {		
		public AccountTransferApply()
		{
		}		
        
		/// <summary>
		/// 申请单ID
		/// </summary>
		[ORFieldMapping("ApplyID", PrimaryKey=true)]
        [DataMember]
		public string ApplyID
		{
			get;
            set;
		}

		/// <summary>
		/// 申请单号
		/// </summary>
		[ORFieldMapping("ApplyNo")]
        [DataMember]
		public string ApplyNo
		{
			get;
            set;
		}

		/// <summary>
		/// 申请状态（参考缴费单）
		/// </summary>
		[ORFieldMapping("ApplyStatus")]
        [ConstantCategory("Common_ApplyStatus")]
        [DataMember]
		public ApplyStatusDefine ApplyStatus
		{
			get;
            set;
		}

		/// <summary>
		/// 转让原因
		/// </summary>
		[ORFieldMapping("ApplyMemo")]
        [DataMember]
		public string ApplyMemo
		{
			get;
            set;
		}

		/// <summary>
		/// 申请时间
		/// </summary>
		[ORFieldMapping("ApplyTime", UtcTimeToLocal = true)]
        [DataMember]
		public DateTime ApplyTime
		{
			get;
            set;
		}

		/// <summary>
		/// 申请人ID
		/// </summary>
		[ORFieldMapping("ApplierID")]
        [DataMember]
		public string ApplierID
		{
			get;
            set;
		}

		/// <summary>
		/// 申请人姓名
		/// </summary>
		[ORFieldMapping("ApplierName")]
        [DataMember]
		public string ApplierName
		{
			get;
            set;
		}

		/// <summary>
		/// 申请人岗位ID
		/// </summary>
		[ORFieldMapping("ApplierJobID")]
        [DataMember]
		public string ApplierJobID
		{
			get;
            set;
		}

		/// <summary>
		/// 申请人岗位名称
		/// </summary>
		[ORFieldMapping("ApplierJobName")]
        [DataMember]
		public string ApplierJobName
		{
			get;
            set;
		}

		/// <summary>
		/// 异步处理状态（参考订购）
		/// </summary>
		[ORFieldMapping("ProcessStatus")]
        [ConstantCategory("Common_ProcessStatus")]
        [DataMember]
		public ProcessStatusDefine ProcessStatus
		{
			get;
            set;
		}

		/// <summary>
		/// 异步处理时间
		/// </summary>
		[ORFieldMapping("ProcessTime", UtcTimeToLocal = true)]
        [DataMember]
		public DateTime ProcessTime
		{
			get;
            set;
		}

		/// <summary>
		/// 异步处理说明
		/// </summary>
		[ORFieldMapping("ProcessMemo")]
        [DataMember]
		public string ProcessMemo
		{
			get;
            set;
		}

        /// <summary>
        /// 转让类型
        /// </summary>
        [ORFieldMapping("TransferType")]
        [ConstantCategory("Account_TransferType")]
        [DataMember]
        public AccountTransferType TransferType
        {
            get;
            set;
        }

        /// <summary>
        /// 转让金额
        /// </summary>
        [ORFieldMapping("TransferMoney")]
        [DataMember]
		public decimal TransferMoney
		{
			get;
            set;
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
        /// 学员ID
        /// </summary>
        [ORFieldMapping("CustomerID")]
        [DataMember]
        public string CustomerID
        {
            get;
            set;
        }

        /// <summary>
        /// 学员编码
        /// </summary>
        [ORFieldMapping("CustomerCode")]
        [DataMember]
        public string CustomerCode
        {
            get;
            set;
        }

        /// <summary>
        /// 学员姓名
        /// </summary>
        [ORFieldMapping("CustomerName")]
        [DataMember]
        public string CustomerName
        {
            get;
            set;
        }

        /// <summary>
        /// 账户ID
        /// </summary>
        [ORFieldMapping("AccountID")]
        [DataMember]
        public string AccountID
        {
            get;
            set;
        }

        /// <summary>
        /// 账户编码
        /// </summary>
        [ORFieldMapping("AccountCode")]
        [DataMember]
        public string AccountCode
        {
            get;
            set;
        }

        /// <summary>
        /// 账户类型
        /// </summary>
        [ORFieldMapping("AccountType")]
        [DataMember]
        public AccountTypeDefine AccountType
        {
            get;
            set;
        }

        /// <summary>
        /// 转让前折扣ID
        /// </summary>
        [ORFieldMapping("ThatDiscountID")]
        [DataMember]
        public string ThatDiscountID
        {
            get;
            set;
        }

        /// <summary>
        /// 转让前折扣编码
        /// </summary>
        [ORFieldMapping("ThatDiscountCode")]
        [DataMember]
        public string ThatDiscountCode
        {
            get;
            set;
        }

        /// <summary>
        /// 转让前折扣基数
        /// </summary>
        [ORFieldMapping("ThatDiscountBase")]
        [DataMember]
        public decimal ThatDiscountBase
        {
            get;
            set;
        }

        /// <summary>
        /// 转让前折扣率
        /// </summary>
        [ORFieldMapping("ThatDiscountRate")]
        [DataMember]
        public decimal ThatDiscountRate
        {
            get;
            set;
        }

        /// <summary>
        /// 转让前账户价值
        /// </summary>
        [ORFieldMapping("ThatAccountValue")]
        [DataMember]
        public decimal ThatAccountValue
        {
            get;
            set;
        }

        /// <summary>
        /// 转让前账户余额
        /// </summary>
        [ORFieldMapping("ThatAccountMoney")]
        [DataMember]
        public decimal ThatAccountMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 转让后折扣ID
        /// </summary>
        [ORFieldMapping("ThisDiscountID")]
        [DataMember]
        public string ThisDiscountID
        {
            get;
            set;
        }

        /// <summary>
        /// 转让后折扣编码
        /// </summary>
        [ORFieldMapping("ThisDiscountCode")]
        [DataMember]
        public string ThisDiscountCode
        {
            get;
            set;
        }

        /// <summary>
        /// 转让后折扣基数
        /// </summary>
        [ORFieldMapping("ThisDiscountBase")]
        [DataMember]
        public decimal ThisDiscountBase
        {
            get;
            set;
        }

        /// <summary>
        /// 转让后折扣率
        /// </summary>
        [ORFieldMapping("ThisDiscountRate")]
        [DataMember]
        public decimal ThisDiscountRate
        {
            get;
            set;
        }

        /// <summary>
        /// 转让后账户价值
        /// </summary>
        [ORFieldMapping("ThisAccountValue")]
        [DataMember]
        public decimal ThisAccountValue
        {
            get;
            set;
        }

        /// <summary>
        /// 转让后账户余额
        /// </summary>
        [ORFieldMapping("ThisAccountMoney")]
        [DataMember]
        public decimal ThisAccountMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 转至校区ID
        /// </summary>
        [ORFieldMapping("BizCampusID")]
        [DataMember]
        public string BizCampusID
        {
            get;
            set;
        }

        /// <summary>
        /// 转至校区名称
        /// </summary>
        [ORFieldMapping("BizCampusName")]
        [DataMember]
        public string BizCampusName
        {
            get;
            set;
        }

        /// <summary>
        /// 转至学员ID
        /// </summary>
        [ORFieldMapping("BizCustomerID")]
        [DataMember]
        public string BizCustomerID
        {
            get;
            set;
        }

        /// <summary>
        /// 转至/转来学员编码
        /// </summary>
        [ORFieldMapping("BizCustomerCode")]
        [DataMember]
        public string BizCustomerCode
        {
            get;
            set;
        }

        /// <summary>
        /// 转至/转来学员姓名
        /// </summary>
        [ORFieldMapping("BizCustomerName")]
        [DataMember]
        public string BizCustomerName
        {
            get;
            set;
        }

        /// <summary>
        /// 转至/转来账户ID
        /// </summary>
        [ORFieldMapping("BizAccountID")]
        [DataMember]
        public string BizAccountID
        {
            get;
            set;
        }

        /// <summary>
        /// 转至/转来账户编码
        /// </summary>
        [ORFieldMapping("BizAccountCode")]
        [DataMember]
        public string BizAccountCode
        {
            get;
            set;
        }

        /// <summary>
        /// 转至/转来账户类型
        /// </summary>
        [ORFieldMapping("BizAccountType")]
        [DataMember]
        public AccountTypeDefine BizAccountType
        {
            get;
            set;
        }
        /// <summary>
        /// 转至/转来账户转让前的折扣ID
        /// </summary>
        [ORFieldMapping("BizThatDiscountID")]
        [DataMember]
        public string BizThatDiscountID
        {
            get;
            set;
        }

        /// <summary>
        /// 转至/转来账户转让前的折扣编码
        /// </summary>
        [ORFieldMapping("BizThatDiscountCode")]
        [DataMember]
        public string BizThatDiscountCode
        {
            get;
            set;
        }

        /// <summary>
        /// 转至/转来账户转让前的折扣基数
        /// </summary>
        [ORFieldMapping("BizThatDiscountBase")]
        [DataMember]
        public decimal BizThatDiscountBase
        {
            get;
            set;
        }

        /// <summary>
        /// 转至/转来账户转让前的折扣率
        /// </summary>
        [ORFieldMapping("BizThatDiscountRate")]
        [DataMember]
        public decimal BizThatDiscountRate
        {
            get;
            set;
        }

        /// <summary>
        /// 转至/转来账户转让前的账户价值
        /// </summary>
        [ORFieldMapping("BizThatAccountValue")]
        [DataMember]
        public decimal BizThatAccountValue
        {
            get;
            set;
        }

        /// <summary>
        /// 转至/转来账户转让前的账户余额
        /// </summary>
        [ORFieldMapping("BizThatAccountMoney")]
        [DataMember]
        public decimal BizThatAccountMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 转至/转来账户转让后的折扣ID
        /// </summary>
        [ORFieldMapping("BizThisDiscountID")]
        [DataMember]
        public string BizThisDiscountID
        {
            get;
            set;
        }

        /// <summary>
        /// 转至/转来账户转让后的折扣编码
        /// </summary>
        [ORFieldMapping("BizThisDiscountCode")]
        [DataMember]
        public string BizThisDiscountCode
        {
            get;
            set;
        }

        /// <summary>
        /// 转至/转来账户转让后的折扣基数
        /// </summary>
        [ORFieldMapping("BizThisDiscountBase")]
        [DataMember]
        public decimal BizThisDiscountBase
        {
            get;
            set;
        }

        /// <summary>
        /// 转至/转来账户转让后的折扣率
        /// </summary>
        [ORFieldMapping("BizThisDiscountRate")]
        [DataMember]
        public decimal BizThisDiscountRate
        {
            get;
            set;
        }

        /// <summary>
        /// 转至/转来账户转让后的账户价值
        /// </summary>
        [ORFieldMapping("BizThisAccountValue")]
        [DataMember]
        public decimal BizThisAccountValue
        {
            get;
            set;
        }

        /// <summary>
        /// 转至/转来账户转让后的账户余额
        /// </summary>
        [ORFieldMapping("BizThisAccountMoney")]
        [DataMember]
        public decimal BizThisAccountMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 提交人ID
        /// </summary>
        [ORFieldMapping("SubmitterID")]
        [DataMember]
		public string SubmitterID
		{
			get;
            set;
		}

		/// <summary>
		/// 提交人姓名
		/// </summary>
		[ORFieldMapping("SubmitterName")]
        [DataMember]
		public string SubmitterName
		{
			get;
            set;
		}

		/// <summary>
		/// 提交人岗位ID
		/// </summary>
		[ORFieldMapping("SubmitterJobID")]
        [DataMember]
		public string SubmitterJobID
		{
			get;
            set;
		}

		/// <summary>
		/// 提交人岗位名称
		/// </summary>
		[ORFieldMapping("SubmitterJobName")]
        [DataMember]
		public string SubmitterJobName
		{
			get;
            set;
		}

		/// <summary>
		/// 提交时间
		/// </summary>
		[ORFieldMapping("SubmitTime", UtcTimeToLocal = true)]
        [DataMember]
		public DateTime SubmitTime
		{
			get;
            set;
		}

		/// <summary>
		/// 最后审批人ID
		/// </summary>
		[ORFieldMapping("ApproverID")]
        [DataMember]
		public string ApproverID
		{
			get;
            set;
		}

		/// <summary>
		/// 最后审批人姓名
		/// </summary>
		[ORFieldMapping("ApproverName")]
        [DataMember]
		public string ApproverName
		{
			get;
            set;
		}

		/// <summary>
		/// 最后审批人岗位ID
		/// </summary>
		[ORFieldMapping("ApproverJobID")]
        [DataMember]
		public string ApproverJobID
		{
			get;
            set;
		}

		/// <summary>
		/// 最后审批人岗位名称
		/// </summary>
		[ORFieldMapping("ApproverJobName")]
        [DataMember]
		public string ApproverJobName
		{
			get;
            set;
		}

		/// <summary>
		/// 最后审批时间
		/// </summary>
		[ORFieldMapping("ApproveTime", UtcTimeToLocal = true)]
        [DataMember]
		public DateTime ApproveTime
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

        //是否能审批
        [NoMapping]
        [DataMember]
        public bool CanApprove
        {
            get
            {
                return this.ApplyStatus == ApplyStatusDefine.Approving;
            }
        }
    }

    [Serializable]
    [DataContract]
    public class AccountTransferApplyCollection : EditableDataObjectCollectionBase<AccountTransferApply>
    {
    }

    [Serializable]
    [ORTableMapping("CM.v_AccountTransferApplies")]
    [DataContract]
    public class AccountTransferApplyView : AccountTransferApply
    {

    }

    [Serializable]
    [DataContract]
    public class AccountTransferApplyViewCollection : EditableDataObjectCollectionBase<AccountTransferApplyView>
    {
    }

}