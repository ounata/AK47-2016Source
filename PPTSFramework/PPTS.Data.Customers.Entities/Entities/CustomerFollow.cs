using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common;
using PPTS.Data.Common.Entities;
using System;
using System.Runtime.Serialization;

namespace PPTS.Data.Customers.Entities
{
    /// <summary>
    /// This object represents the properties and methods of a CustomerFollow.
    /// 跟进信息表
    /// </summary>
    [Serializable]
    [ORTableMapping("CM.CustomerFollows")]
    [DataContract]
    public class CustomerFollow : IEntityWithCreator, IEntityWithModifier
    {
        public CustomerFollow()
        {
        }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        [ORFieldMapping("OrgID")]
        [DataMember]
        public string OrgID
        {
            get;
            set;
        }

        /// <summary>
        /// 组织机构名称
        /// </summary>
        [ORFieldMapping("OrgName")]
        [DataMember]
        public string OrgName
        {
            get;
            set;
        }

        /// <summary>
        /// 组织机构类型
        /// </summary>
        [ORFieldMapping("OrgType")]
        [DataMember]
        public OrgTypeDefine OrgType
        {
            get;
            set;
        }

        /// <summary>
        /// 客户ID
        /// </summary>
        [ORFieldMapping("CustomerID")]
        [DataMember]
        public string CustomerID
        {
            get;
            set;
        }

        /// <summary>
        /// 跟进ID
        /// </summary>
        [ORFieldMapping("FollowID", PrimaryKey = true)]
        [DataMember]
        public string FollowID
        {
            get;
            set;
        }

        /// <summary>
        /// 跟进时间
        /// </summary>
        [ORFieldMapping("FollowTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime FollowTime
        {
            get;
            set;
        }

        /// <summary>
        /// 跟进人ID
        /// </summary>
        [ORFieldMapping("FollowerID")]
        [DataMember]
        public string FollowerID
        {
            get;
            set;
        }

        /// <summary>
        /// 跟进人姓名
        /// </summary>
        [ORFieldMapping("FollowerName")]
        [DataMember]
        public string FollowerName
        {
            get;
            set;
        }

        /// <summary>
        /// 跟进人岗位ID
        /// </summary>
        [ORFieldMapping("FollowerJobID")]
        [DataMember]
        public string FollowerJobID
        {
            get;
            set;
        }

        /// <summary>
        /// 跟进人岗位名称
        /// </summary>
        [ORFieldMapping("FollowerJobName")]
        [DataMember]
        public string FollowerJobName
        {
            get;
            set;
        }

        /// <summary>
        /// 跟进方式代码
        /// </summary>
        [ORFieldMapping("FollowType")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_Customer_CRM_SaleContactType")]
        public string FollowType
        {
            get;
            set;
        }

        /// <summary>
        /// 跟进阶段代码
        /// </summary>
        [ORFieldMapping("FollowStage")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_Customer_CRM_SalePhase")]
        public SalesStageType FollowStage
        {
            get;
            set;
        }

        /// <summary>
        /// 跟进对象代码
        /// </summary>
        [ORFieldMapping("FollowObject")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_Customer_CRM_SaleContactTarget")]
        public string FollowObject
        {
            get;
            set;
        }

        /// <summary>
        /// 此次通电号码
        /// </summary>
        [ORFieldMapping("FollowPhone")]
        [DataMember]
        public string FollowPhone
        {
            get;
            set;
        }

        /// <summary>
        /// 沟通一级结果代码
        /// </summary>
        [ORFieldMapping("TalkMainResult")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_Customer_CRM_CommunicateResultFirstEx")]
        public string TalkMainResult
        {
            get;
            set;
        }

        /// <summary>
        /// 沟通二级结果代码
        /// </summary>
        [ORFieldMapping("TalkSubResult")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_Customer_CRM_CommunicateResultSecondEx")]
        public string TalkSubResult
        {
            get;
            set;
        }

        /// <summary>
        /// 客户级别代码
        /// </summary>
        [ORFieldMapping("CustomerLevel")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_Customer_CRM_CustomerLevelEx")]
        public string CustomerLevel
        {
            get;
            set;
        }

        /// <summary>
        /// 无效客户理由代码，当客户级别是D时候填写（A-空手机号，B，C，D）
        /// </summary>
        [ORFieldMapping("InvalidReason")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_Customer_CRM_InvaliCustomerType")]
        public string InvalidReason
        {
            get;
            set;
        }

        /// <summary>
        /// 购买意愿代码
        /// </summary>
        [ORFieldMapping("PurchaseIntention")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_Customer_CRM_PurchaseIntent")]
        public PurchaseIntentionDefine PurchaseIntention
        {
            get;
            set;
        }

        /// <summary>
        /// 想补习的科目
        /// </summary>
        [ORFieldMapping("IntensionSubjects")]
        [DataMember]
        public string IntensionSubjects
        {
            get;
            set;
        }

        /// <summary>
        /// 是否有效建档
        /// </summary>
        [ORFieldMapping("IsValidFiling")]
        [DataMember]
        public bool IsValidFiling
        {
            get;
            set;
        }

        /// <summary>
        /// 预计下次沟通时间
        /// </summary>
        [ORFieldMapping("NextFollowTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime NextFollowTime
        {
            get;
            set;
        }

        /// <summary>
        /// 预计上门确认时间
        /// </summary>
        [ORFieldMapping("PlanVerifyTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime PlanVerifyTime
        {
            get;
            set;
        }

        /// <summary>
        /// 预计签约日期
        /// </summary>
        [ORFieldMapping("PlanSignDate", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime PlanSignDate
        {
            get;
            set;
        }

        /// <summary>
        /// 实际上门人数代码
        /// </summary>
        [ORFieldMapping("VerifyPeoples")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_Customer_CRM_RealCallPersonNum")]
        public string VerifyPeoples
        {
            get;
            set;
        }

        /// <summary>
        /// 上门人员关系代码
        /// </summary>
        [ORFieldMapping("VerifyRelations")]
        [DataMember]
        public string VerifyRelations
        {
            get;
            set;
        }

        /// <summary>
        /// 是否其它机构辅导
        /// </summary>
        [ORFieldMapping("IsStudyThere")]
        [DataMember]
        public bool IsStudyThere
        {
            get;
            set;
        }

        /// <summary>
        /// 是否潜客阶段
        /// </summary>
        [ORFieldMapping("IsPotential")]
        [DataMember]
        public bool IsPotential
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
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
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
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
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
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update, DefaultExpression = "GETUTCDATE()", ForceUseDefaultExpression = true)]
        [DataMember]
        public DateTime ModifyTime
        {
            get;
            set;
        }

        /// <summary>
        /// 跟进情况备注
        /// </summary>
        [ORFieldMapping("FollowMemo")]
        [DataMember]
        public string FollowMemo
        {
            get;
            set;
        }

        public CustomerBase FillFollowSummary(CustomerBase summary)
        {
            summary.NullCheck("summary");

            summary.PurchaseIntention = this.PurchaseIntention;
            summary.FollowTime = this.FollowTime;
            summary.FollowStage = this.FollowStage;
            summary.NextFollowTime = this.NextFollowTime;
            summary.CustomerLevel = this.CustomerLevel;

            return summary;
        }
    }

    [Serializable]
    [DataContract]
    public class CustomerFollowCollection : EditableDataObjectCollectionBase<CustomerFollow>
    {
    }
}