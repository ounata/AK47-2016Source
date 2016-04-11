using System;
using System.Runtime.Serialization;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;

namespace PPTS.Data.Customers.Entities
{
    /// <summary>
    /// This object represents the properties and methods of a CustomerFollow.
    /// 跟进信息表
    /// </summary>
    [Serializable]
    [ORTableMapping("CustomerFollows")]
    [DataContract]
    public class CustomerFollow
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
        public string OrgType
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
        [ORFieldMapping("FollowTime")]
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
        public int FollowType
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
        public int FollowStage
        {
            get;
            set;
        }

        /// <summary>
        /// 跟进对象代码
        /// </summary>
        [ORFieldMapping("FollowObject")]
        [DataMember]
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
        public int TalkMainResult
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
        public int TalkSubResult
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
        public int CustomerLevel
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
        public int InvalidReason
        {
            get;
            set;
        }

        /// <summary>
        /// 购买意愿代码
        /// </summary>
        [ORFieldMapping("PurchaseIntension")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_Customer_CRM_PurchaseIntent")]
        public PurchaseIntentionDefine PurchaseIntension
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
        public int IsValidFiling
        {
            get;
            set;
        }

        /// <summary>
        /// 预计下次沟通时间
        /// </summary>
        [ORFieldMapping("NextTalkTime")]
        [DataMember]
        public DateTime NextTalkTime
        {
            get;
            set;
        }

        /// <summary>
        /// 预计上门确认时间
        /// </summary>
        [ORFieldMapping("PlanVerifyTime")]
        [DataMember]
        public DateTime PlanVerifyTime
        {
            get;
            set;
        }

        /// <summary>
        /// 预计签约日期
        /// </summary>
        [ORFieldMapping("PlanSignDate")]
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
        public int VerifyPeoples
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
        public int IsStudyThere
        {
            get;
            set;
        }

        /// <summary>
        /// 是否潜客阶段
        /// </summary>
        [ORFieldMapping("IsPotential")]
        [DataMember]
        public int IsPotential
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
    public class CustomerFollowCollection : EditableDataObjectCollectionBase<CustomerFollow>
    {
    }
}