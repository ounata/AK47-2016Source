using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Authorization;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PPTS.Data.Customers.Entities
{
    /// <summary>
    /// This object represents the properties and methods of a AccountRefundApply.
    /// 账户退费申请表
    /// </summary>
    #region 数据范围权限(存入识别)
    [EntityAuth(RecordType = RecordType.AccountRefundApply)]
    #endregion
    [Serializable]
    [ORTableMapping("CM.AccountRefundApplies")]
    [DataContract]
    public class AccountRefundApply : IEntityWithCreator, IEntityWithModifier
    {
        public AccountRefundApply()
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
        [ORFieldMapping("ApplyID", PrimaryKey = true)]
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
        /// 退费原因
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
        public string ApplierJobType
        {
            get;
            set;
        }

        /// <summary>
        /// 异步处理状态
        /// </summary>
        [ORFieldMapping("ProcessStatus")]
        [ConstantCategory("Common_ProcessStatus")]
        [DataMember]
        public string ProcessStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 异步处理时间
        /// </summary>
        [ORFieldMapping("ProcessTime")]
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
        /// 退费类型（0-正常退费，1-坏账退费）
        /// </summary>
        [ORFieldMapping("RefundType")]
        [ConstantCategory("Account_RefundType")]
        [DataMember]
        public RefundTypeDefine RefundType
        {
            get;
            set;
        }

        /// <summary>
        /// 是否上课退费
        /// </summary>
        [ORFieldMapping("IsPeriodRefund")]
        [DataMember]
        public bool IsPeriodRefund
        {
            get;
            set;
        }

        /// <summary>
        /// 是否制度外退费
        /// </summary>
        [ORFieldMapping("IsExtraRefund")]
        [DataMember]
        public bool IsExtraRefund
        {
            get;
            set;
        }

        /// <summary>
        /// 申退金额
        /// </summary>
        [ORFieldMapping("ApplyRefundMoney")]
        [DataMember]
        public decimal ApplyRefundMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 应退金额
        /// </summary>
        [ORFieldMapping("OughtRefundMoney")]
        [DataMember]
        public decimal OughtRefundMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 实退金额
        /// </summary>
        [ORFieldMapping("RealRefundMoney")]
        [DataMember]
        public decimal RealRefundMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 已消耗课时价值
        /// </summary>
        [ORFieldMapping("ConsumptionValue")]
        [DataMember]
        public decimal ConsumptionValue
        {
            get;
            set;
        }

        /// <summary>
        /// 折扣返还金额
        /// </summary>
        [ORFieldMapping("ReallowanceMoney")]
        [DataMember]
        public decimal ReallowanceMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 制度外退费类型（综合服务费赔偿，争议性课时差价赔偿，服务不满意协商赔偿，已上课全额退款，大额赔偿）
        /// </summary>
        [ORFieldMapping("ExtraRefundType")]
        [ConstantCategory("Account_ExtraRefundType")]
        [DataMember]
        public string ExtraRefundType
        {
            get;
            set;
        }

        /// <summary>
        /// 制度外退款金额
        /// </summary>
        [ORFieldMapping("ExtraRefundMoney")]
        [DataMember]
        public decimal ExtraRefundMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 差价补偿金额
        /// </summary>
        [ORFieldMapping("CompensateMoney")]
        [DataMember]
        public decimal CompensateMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 退费前折扣ID
        /// </summary>
        [ORFieldMapping("ThatDiscountID")]
        [DataMember]
        public string ThatDiscountID
        {
            get;
            set;
        }

        /// <summary>
        /// 退费前折扣编码
        /// </summary>
        [ORFieldMapping("ThatDiscountCode")]
        [DataMember]
        public string ThatDiscountCode
        {
            get;
            set;
        }

        /// <summary>
        /// 退费前折扣基数
        /// </summary>
        [ORFieldMapping("ThatDiscountBase")]
        [DataMember]
        public decimal ThatDiscountBase
        {
            get;
            set;
        }

        /// <summary>
        /// 退费前折扣率
        /// </summary>
        [ORFieldMapping("ThatDiscountRate")]
        [DataMember]
        public decimal ThatDiscountRate
        {
            get;
            set;
        }

        /// <summary>
        /// 退费前账户价值
        /// </summary>
        [ORFieldMapping("ThatAccountValue")]
        [DataMember]
        public decimal ThatAccountValue
        {
            get;
            set;
        }

        /// <summary>
        /// 退费前账户余额
        /// </summary>
        [ORFieldMapping("ThatAccountMoney")]
        [DataMember]
        public decimal ThatAccountMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 退费后折扣ID
        /// </summary>
        [ORFieldMapping("ThisDiscountID")]
        [DataMember]
        public string ThisDiscountID
        {
            get;
            set;
        }

        /// <summary>
        /// 退费后折扣编码
        /// </summary>
        [ORFieldMapping("ThisDiscountCode")]
        [DataMember]
        public string ThisDiscountCode
        {
            get;
            set;
        }

        /// <summary>
        /// 退费后折扣基数
        /// </summary>
        [ORFieldMapping("ThisDiscountBase")]
        [DataMember]
        public decimal ThisDiscountBase
        {
            get;
            set;
        }

        /// <summary>
        /// 退费后折扣率
        /// </summary>
        [ORFieldMapping("ThisDiscountRate")]
        [DataMember]
        public decimal ThisDiscountRate
        {
            get;
            set;
        }

        /// <summary>
        /// 退费后账户价值
        /// </summary>
        [ORFieldMapping("ThisAccountValue")]
        [DataMember]
        public decimal ThisAccountValue
        {
            get;
            set;
        }

        /// <summary>
        /// 退费后账户余额
        /// </summary>
        [ORFieldMapping("ThisAccountMoney")]
        [DataMember]
        public decimal ThisAccountMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 领款人（默认是学员姓名）
        /// </summary>
        [ORFieldMapping("Drawer")]
        [DataMember]
        public string Drawer
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
        /// 咨询师姓名
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
        /// 提交人岗位类型
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
        /// 财务最后确认人ID
        /// </summary>
        [ORFieldMapping("VerifierID")]
        [DataMember]
        public string VerifierID
        {
            get;
            set;
        }

        /// <summary>
        /// 财务最后确认人姓名
        /// </summary>
        [ORFieldMapping("VerifierName")]
        [DataMember]
        public string VerifierName
        {
            get;
            set;
        }

        /// <summary>
        /// 财务最后确认人岗位ID
        /// </summary>
        [ORFieldMapping("VerifierJobID")]
        [DataMember]
        public string VerifierJobID
        {
            get;
            set;
        }

        /// <summary>
        /// 财务最后确认人岗位名称
        /// </summary>
        [ORFieldMapping("VerifierJobName")]
        [DataMember]
        public string VerifierJobName
        {
            get;
            set;
        }

        /// <summary>
        /// 财务最后确认状态
        /// </summary>
        [ORFieldMapping("VerifyStatus")]
        [ConstantCategory("Account_RefundVerifyStatus")]
        [DataMember]
        public RefundVerifyStatus VerifyStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 财务最后确认时间
        /// </summary>
        [ORFieldMapping("VerifyTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime VerifyTime
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
        /// 对账状态
        /// </summary>
        [ORFieldMapping("CheckStatus")]
        [ConstantCategory("Common_CheckStatus")]
        [DataMember]
        public CheckStatusDefine CheckStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 队长时间
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

        /// <summary>
        /// 是否已对账
        /// </summary>
        [NoMapping]
        [DataMember]
        public bool CanCheck
        {
            get
            {
                return this.VerifyStatus == RefundVerifyStatus.Refunded
                    && this.CheckStatus == CheckStatusDefine.UnCheck;
            }
        }

        /// <summary>
        /// 能否打印
        /// </summary>
        [NoMapping]
        [DataMember]
        public bool CanPrint
        {
            get
            {
                return this.VerifyStatus == RefundVerifyStatus.Refunded;
            }
        }

        /// <summary>
        /// 分出纳能否确认
        /// </summary>
        [NoMapping]
        [DataMember]
        public bool CashierCanVerify
        {
            get
            {
                return this.ApplyStatus == ApplyStatusDefine.Approved
                    && this.VerifyStatus == RefundVerifyStatus.WaitCashierVerify;
            }
        }

        /// <summary>
        /// 分财务能否确认
        /// </summary>
        [NoMapping]
        [DataMember]
        public bool FinanceCanVerify
        {
            get
            {
                return this.ApplyStatus == ApplyStatusDefine.Approved
                    && this.VerifyStatus == RefundVerifyStatus.WaitFinanceVerify;
            }
        }

        /// <summary>
        /// 分区域能否确认
        /// </summary>
        [NoMapping]
        [DataMember]
        public bool RegionCanVerify
        {
            get
            {
                return this.ApplyStatus == ApplyStatusDefine.Approved
                    && this.VerifyStatus == RefundVerifyStatus.WaitRegionVerify;
            }
        }
    }

    [Serializable]
    [DataContract]
    public class AccountRefundApplyCollection : EditableDataObjectCollectionBase<AccountRefundApply>
    {
    }
}