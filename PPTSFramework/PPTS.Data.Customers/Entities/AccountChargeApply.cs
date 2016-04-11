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
    /// This object represents the properties and methods of a AccountChargeApply.
    /// 账户缴费申请表
    /// </summary>
    [Serializable]
    [ORTableMapping("AccountChargeApplys")]
    [DataContract]
    public class AccountChargeApply
    {
        public AccountChargeApply()
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
        /// 申请状态（审批中，已完成，已拒绝）
        /// </summary>
        [ORFieldMapping("ApplyStatus")]
        [DataMember]
        public string ApplyStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        [ORFieldMapping("ApplyMemo")]
        [DataMember]
        public string ApplyMemo
        {
            get;
            set;
        }

        /// <summary>
        /// 申请日期
        /// </summary>
        [ORFieldMapping("ApplyTime")]
        [DataMember]
        public DateTime ApplyTime
        {
            get;
            set;
        }

        /// <summary>
        /// 申请人ID
        /// </summary>
        [ORFieldMapping("ApplyrID")]
        [DataMember]
        public string ApplyrID
        {
            get;
            set;
        }

        /// <summary>
        /// 申请人姓名
        /// </summary>
        [ORFieldMapping("ApplyrName")]
        [DataMember]
        public string ApplyrName
        {
            get;
            set;
        }

        /// <summary>
        /// 申请人岗位ID
        /// </summary>
        [ORFieldMapping("ApplyrJobID")]
        [DataMember]
        public string ApplyrJobID
        {
            get;
            set;
        }

        /// <summary>
        /// 申请人岗位名称
        /// </summary>
        [ORFieldMapping("ApplyrJobName")]
        [DataMember]
        public string ApplyrJobName
        {
            get;
            set;
        }

        /// <summary>
        /// 异步处理状态（参考订购）
        /// </summary>
        [ORFieldMapping("ProcessStatus")]
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
        /// 缴费类型（新签，新签回款，前期结课续费，后期结课续费，后期非结课续费，前期非结课续费）
        /// </summary>
        [ORFieldMapping("ChargeType")]
        [DataMember]
        public string ChargeType
        {
            get;
            set;
        }

        /// <summary>
        /// 预缴费金额
        /// </summary>
        [ORFieldMapping("ChargeMoney")]
        [DataMember]
        public decimal ChargeMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 累计支付金额
        /// </summary>
        [ORFieldMapping("PaidMoney")]
        [DataMember]
        public decimal PaidMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 支付状态（未支付，已支付，已作废）
        /// </summary>
        [ORFieldMapping("PayStatus")]
        [DataMember]
        public string PayStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 是否首次缴费
        /// </summary>
        [ORFieldMapping("IsFirstCharge")]
        [DataMember]
        public int IsFirstCharge
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
        /// 咨询师岗位名称
        /// </summary>
        [ORFieldMapping("ConsultantJobName")]
        [DataMember]
        public string ConsultantJobName
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
        /// 学管师姓名
        /// </summary>
        [ORFieldMapping("EducatorJobName")]
        [DataMember]
        public string EducatorJobName
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
        /// 提交时间
        /// </summary>
        [ORFieldMapping("SubmitTime")]
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
        [ORFieldMapping("ApproveTime")]
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
    }

    [Serializable]
    [DataContract]
    public class AccountChargeApplyCollection : EditableDataObjectCollectionBase<AccountChargeApply>
    {
    }
}