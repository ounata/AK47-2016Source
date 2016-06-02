using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PPTS.Data.Products.Entities
{
    /// <summary>
    /// This object represents the properties and methods of a Discount.
    /// 折扣表
    /// </summary>
    [Serializable]
    [ORTableMapping("[PM].[Discounts]")]
    [DataContract]
    public class Discount : IEntityWithCreator, IEntityWithModifier
    {
        public Discount()
        {
        }

        /// <summary>
        /// 折扣ID
        /// </summary>
        [ORFieldMapping("DiscountID", PrimaryKey = true)]
        [DataMember]
        public string DiscountID
        {
            get;
            set;
        }

        /// <summary>
        /// 折扣编码
        /// </summary>
        [ORFieldMapping("DiscountCode")]
        [DataMember]
        public string DiscountCode
        {
            get;
            set;
        }

        /// <summary>
        /// 折扣名称
        /// </summary>
        [ORFieldMapping("DiscountName")]
        [DataMember]
        public string DiscountName
        {
            get;
            set;
        }

        /// <summary>
        /// 状态（审批中，已完成，已拒绝）
        /// </summary>
        [ConstantCategory(Category = "C_CODE_ABBR_BO_Infra_DiscountStatus")]
        [ORFieldMapping("DiscountStatus")]
        [DataMember]
        public DiscountStatusDefine DiscountStatus
        {
            get;
            set;
        }

        [NoMapping]
        [ConstantCategory(Category = "C_CODE_ABBR_BO_Infra_CampusUseInfo")]
        public CampusUseInfoDefine CampusStatus { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        [ORFieldMapping("StartDate")]
        [DataMember]
        public DateTime StartDate
        {
            get;
            set;
        }

        /// <summary>
        /// 分工司ID
        /// </summary>
        [ORFieldMapping("BranchID")]
        [DataMember]
        public string BranchID
        {
            get;
            set;
        }

        /// <summary>
        /// 分公司名称
        /// </summary>
        [ORFieldMapping("BranchName")]
        [DataMember]
        public string BranchName
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
        [ORFieldMapping("SubmitterJobId")]
        [DataMember]
        public string SubmitterJobId
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
        [ORFieldMapping("CreateTime")]
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
        [ORFieldMapping("ModifyTime")]
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
    public class DiscountCollection : EditableDataObjectCollectionBase<Discount>
    {
    }
}