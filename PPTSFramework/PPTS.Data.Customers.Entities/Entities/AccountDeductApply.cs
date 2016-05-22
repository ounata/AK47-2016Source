using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PPTS.Data.Customers.Entities
{
	/// <summary>
	/// This object represents the properties and methods of a AccountDeductApply.
	/// 服务费扣减申请表
	/// </summary>
	[Serializable]
    [ORTableMapping("CM.AccountDeductApplies")]
    [DataContract]
	public class AccountDeductApply : IEntityWithCreator, IEntityWithModifier
    {		
		public AccountDeductApply()
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
        [DataMember]
		public ApplyStatusDefine ApplyStatus
		{
			get;
            set;
		}

		/// <summary>
		/// 申请说明
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
        /// 申请人岗位类型
        /// </summary>
        [ORFieldMapping("ApplierJobType")]
        [DataMember]
        public JobTypeDefine ApplierJobType
        {
            get;
            set;
        }

        /// <summary>
        /// 异步处理状态（参考订购）
        /// </summary>
        [ORFieldMapping("ProcessStatus")]
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
		/// 服务费ID
		/// </summary>
		[ORFieldMapping("ExpenseID")]
        [DataMember]
		public string ExpenseID
		{
			get;
            set;
		}

		/// <summary>
		/// 服务费类型（参考服务费表）
		/// </summary>
		[ORFieldMapping("ExpenseType")]
        [DataMember]
		public string ExpenseType
		{
			get;
            set;
		}

		/// <summary>
		/// 服务费金额
		/// </summary>
		[ORFieldMapping("ExpenseMoney")]
        [DataMember]
		public decimal ExpenseMoney
		{
			get;
            set;
		}

		/// <summary>
		/// 扣减前折扣ID
		/// </summary>
		[ORFieldMapping("ThatDiscountID")]
        [DataMember]
		public string ThatDiscountID
		{
			get;
            set;
		}

        /// <summary>
        /// 扣减前折扣编码
        /// </summary>
        [ORFieldMapping("ThatDiscountCode")]
        [DataMember]
        public string ThatDiscountCode
        {
            get;
            set;
        }

        /// <summary>
        /// 扣减前折扣基数
        /// </summary>
        [ORFieldMapping("ThatDiscountBase")]
        [DataMember]
		public decimal ThatDiscountBase
		{
			get;
            set;
		}

		/// <summary>
		/// 扣减前折扣率
		/// </summary>
		[ORFieldMapping("ThatDiscountRate")]
        [DataMember]
		public decimal ThatDiscountRate
		{
			get;
            set;
		}

		/// <summary>
		/// 扣减前账户价值
		/// </summary>
		[ORFieldMapping("ThatAccountValue")]
        [DataMember]
		public decimal ThatAccountValue
		{
			get;
            set;
		}

        /// <summary>
        /// 扣减前账户余额
        /// </summary>
        [ORFieldMapping("ThatAccountMoney")]
        [DataMember]
        public decimal ThatAccountMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 扣减后折扣ID
        /// </summary>
        [ORFieldMapping("ThisDiscountID")]
        [DataMember]
		public string ThisDiscountID
		{
			get;
            set;
		}

        /// <summary>
        /// 扣减后折扣编码
        /// </summary>
        [ORFieldMapping("ThisDiscountCode")]
        [DataMember]
        public string ThisDiscountCode
        {
            get;
            set;
        }

        /// <summary>
        /// 扣减后折扣基数
        /// </summary>
        [ORFieldMapping("ThisDiscountBase")]
        [DataMember]
		public decimal ThisDiscountBase
		{
			get;
            set;
		}

		/// <summary>
		/// 扣减后折扣率
		/// </summary>
		[ORFieldMapping("ThisDiscountRate")]
        [DataMember]
		public decimal ThisDiscountRate
		{
			get;
            set;
		}

		/// <summary>
		/// 扣减后账户价值
		/// </summary>
		[ORFieldMapping("ThisAccountValue")]
        [DataMember]
		public decimal ThisAccountValue
		{
			get;
            set;
		}

        /// <summary>
        /// 扣减后账户余额
        /// </summary>
        [ORFieldMapping("ThisAccountMoney")]
        [DataMember]
        public decimal ThisAccountMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 咨询师ID
        /// </summary>
        [ORFieldMapping("ConsultantID")]
        [DataMember]
		public string ConsultantID
		{
			get;
            set;
		}

		/// <summary>
		/// 咨询师名称
		/// </summary>
		[ORFieldMapping("ConsultantName")]
        [DataMember]
		public string ConsultantName
		{
			get;
            set;
		}

		/// <summary>
		/// 咨询师岗位ID
		/// </summary>
		[ORFieldMapping("ConsultantJobID")]
        [DataMember]
		public string ConsultantJobID
		{
			get;
            set;
		}

		/// <summary>
		/// 学管师ID
		/// </summary>
		[ORFieldMapping("EducatorID")]
        [DataMember]
		public string EducatorID
		{
			get;
            set;
		}

		/// <summary>
		/// 学管师姓名
		/// </summary>
		[ORFieldMapping("EducatorName")]
        [DataMember]
		public string EducatorName
		{
			get;
            set;
		}

		/// <summary>
		/// 学管师岗位ID
		/// </summary>
		[ORFieldMapping("EducatorJobID")]
        [DataMember]
		public string EducatorJobID
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
		/// 提交人岗位姓名
		/// </summary>
		[ORFieldMapping("SubmitterJobName")]
        [DataMember]
		public string SubmitterJobName
		{
			get;
            set;
		}

		/// <summary>
		/// 
		/// </summary>
		[ORFieldMapping("SubmitterJobType")]
        [DataMember]
		public string SubmitterJobType
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
    }

    [Serializable]
    [DataContract]
    public class AccountDeductApplyCollection : EditableDataObjectCollectionBase<AccountDeductApply>
    {
    }
}