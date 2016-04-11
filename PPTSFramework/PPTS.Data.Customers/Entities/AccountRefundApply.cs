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
    /// This object represents the properties and methods of a AccountRefundApply.
    /// 账户退费申请表
    /// </summary>
    [Serializable]
    [ORTableMapping("AccountRefundApplies")]
    [DataContract]
    public class AccountRefundApply
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
        [DataMember]
        public string ApplyStatus
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
        /// 异步处理状态
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
        /// 退费类型（正常退费，坏账退费）
        /// </summary>
        [ORFieldMapping("RefundType")]
        [DataMember]
        public string RefundType
        {
            get;
            set;
        }

        /// <summary>
        /// 申请退费金额
        /// </summary>
        [ORFieldMapping("RefundMoney")]
        [DataMember]
        public decimal RefundMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 实际退费金额
        /// </summary>
        [ORFieldMapping("RealRefundMoney")]
        [DataMember]
        public decimal RealRefundMoney
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
        /// 学管师岗位名称
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
    public class AccountRefundApplyCollection : EditableDataObjectCollectionBase<AccountRefundApply>
    {
    }
}