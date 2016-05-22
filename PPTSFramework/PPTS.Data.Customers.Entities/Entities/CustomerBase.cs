using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using MCS.Library.Validation;
using PPTS.Data.Common;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Entities
{
    /// <summary>
    /// 学员和潜客信息的基类
    /// </summary>
    [Serializable]
    [DataContract]
    public abstract class CustomerBase : IVersionDataObjectWithoutID, IEntityWithCreator, IEntityWithModifier, IBasicCustomerInfo
    {
        /// <summary>
        /// 客户的ID
        /// </summary>
        [ORFieldMapping("CustomerID", PrimaryKey = true)]
        [DataMember]
        public string CustomerID
        {
            get;
            set;
        }

        /// <summary>
        /// 归属组织机构ID
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
        /// 客户名称
        /// </summary>
        [ORFieldMapping("CustomerName")]
        [StringLengthValidator(128, MessageTemplate = "客户名称的长度不能超过128个字符")]
        [StringEmptyValidator(MessageTemplate = "客户名称不允许为空")]
        [DataMember]
        public string CustomerName
        {
            get;
            set;
        }

        /// <summary>
        /// 客户ID命名规则：家长P+年份后两位+月+日+999999，学生S+年份后两位+月份+日期+999999
        /// </summary>
        [ORFieldMapping("CustomerCode")]
        [DataMember]
        public string CustomerCode
        {
            get;
            set;
        }

        /// <summary>
        /// 客户的级别A1, A2, A3
        /// </summary>
        [ORFieldMapping("CustomerLevel")]
        [DataMember]
        public string CustomerLevel
        {
            get;
            set;
        }

        /// <summary>
        /// 是否单亲
        /// </summary>
        [ORFieldMapping("IsOneParent")]
        [DataMember]
        public bool IsOneParent
        {
            get;
            set;
        }

        /// <summary>
        /// 入学大年级。对应类别C_CODE_ABBR_CUSTOMER_GRADE（年级）
        /// </summary>
        [ORFieldMapping("EntranceGrade")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_CUSTOMER_GRADE")]
        public string EntranceGrade
        {
            get;
            set;
        }

        private StudentBranchType _SubjectType = StudentBranchType.NoBranch;

        /// <summary>
        /// 文理科(C_CODE_ABBR_STUDENTBRANCH)。1:文科，2:理科，3:不分科
        /// </summary>
        [ORFieldMapping("SubjectType")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_STUDENTBRANCH")]
        public StudentBranchType SubjectType
        {
            get
            {
                return this._SubjectType;
            }
            set
            {
                this._SubjectType = value;
            }
        }

        /// <summary>
        /// 学年制(C_CODE_ABBR_ACDEMICYEAR)
        /// </summary>
        [ORFieldMapping("SchoolYear")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_ACDEMICYEAR")]
        public string SchoolYear
        {
            get;
            set;
        }

        /// <summary>
        /// 学生描述
        /// </summary>
        [ORFieldMapping("Character")]
        [DataMember]
        public string Character
        {
            get;
            set;
        }

        /// <summary>
        /// 学生生日
        /// </summary>
        [ORFieldMapping("Birthday", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime Birthday
        {
            get;
            set;
        }

        /// <summary>
        /// 邮件地址
        /// </summary>
        [ORFieldMapping("Email")]
        [DataMember]
        public string Email
        {
            get;
            set;
        }

        /// <summary>
        /// 当前年级。年级(C_CODE_ABBR_CUSTOMER_GRADE)
        /// </summary>
        [ORFieldMapping("Grade")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_CUSTOMER_GRADE")]
        public string Grade
        {
            get;
            set;
        }

        /// <summary>
        /// 学生类型(C_CODE_ABBR_CUSTOMER_STUDENTTYPE)，默认51
        /// </summary>
        [ORFieldMapping("StudentType")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_CUSTOMER_STUDENTTYPE")]
        public string StudentType
        {
            get;
            set;
        }

        /// <summary>
        /// 跟进阶段(C_CODE_ABBR_Customer_CRM_SalePhase)
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
        /// 购买意愿(C_CODE_ABBR_Customer_CRM_PurchaseIntent)
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
        /// 证件类型(C_CODE_ABBR_BO_Customer_CertificateType)
        /// </summary>
        [ORFieldMapping("IDType")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_BO_Customer_CertificateType")]
        public IDTypeDefine IDType
        {
            get;
            set;
        }

        /// <summary>
        /// 证件号码
        /// </summary>
        [ORFieldMapping("IDNumber")]
        [DataMember]
        public string IDNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 接触方式(C_CODE_ABBR_Customer_CRM_NewContactType)。"1：呼入 2：呼出 3：直访 4：在线咨询-乐语 5：在线咨询-其他"
        /// </summary>
        [ORFieldMapping("ContactType")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_Customer_CRM_NewContactType")]
        public string ContactType
        {
            get;
            set;
        }

        /// <summary>
        /// 信息来源一级分类(C_Code_Abbr_BO_Customer_Source)
        /// </summary>
        [ORFieldMapping("SourceMainType")]
        [DataMember]
        [ConstantCategory("C_Code_Abbr_BO_Customer_Source")]
        public string SourceMainType
        {
            get;
            set;
        }

        /// <summary>
        /// 信息来源二级分类(C_Code_Abbr_BO_Customer_Source)
        /// </summary>
        [ORFieldMapping("SourceSubType")]
        [DataMember]
        [ConstantCategory("C_Code_Abbr_BO_Customer_Source")]
        public string SourceSubType
        {
            get;
            set;
        }

        /// <summary>
        /// 来源系统
        /// </summary>
        [ORFieldMapping("SourceSystem")]
        [DataMember]
        public string SourceSystem
        {
            get;
            set;
        }

        /// <summary>
        /// 客户状态(C_Code_Abbr_BO_CTI_CustomerStatus)0=未确认客户信息, 1 = 确认客户信息, 9=无效用户（逻辑删除）
        /// </summary>
        [ORFieldMapping("CustomerStatus")]
        [DataMember]
        [ConstantCategory("C_Code_Abbr_BO_CTI_CustomerStatus")]
        public CustomerStatus Status
        {
            get;
            set;
        }

        /// <summary>
        /// 是否复读
        /// </summary>
        [ORFieldMapping("IsStudyAgain")]
        [DataMember]
        public bool IsStudyAgain
        {
            get;
            set;
        }

        /// <summary>
        /// 创建者ID
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
        /// 创建者名称
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
        /// 创建者ID
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
        /// 创建者名称
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
        /// 修改时间
        /// </summary>
        [ORFieldMapping("ModifyTime", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update, DefaultExpression = "GETUTCDATE()", ForceUseDefaultExpression = true)]
        [DataMember]
        public DateTime ModifyTime
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
        /// 校区ID
        /// </summary>
        [ORFieldMapping("SchoolID")]
        [DataMember]
        public string SchoolID
        {
            get;
            set;
        }

        /// <summary>
        /// 性别(C_CODE_ABBR_GENDER)。1--男，2--女
        /// </summary>
        [ORFieldMapping("Gender")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_GENDER")]
        public GenderType Gender
        {
            get;
            set;
        }

        /// <summary>
        /// vip客户(C_CODE_ABBR_CUSTOMER_VipType)。1:关系vip客户 2:大单vip客户
        /// </summary>
        [ORFieldMapping("VipType")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_CUSTOMER_VipType")]
        public VipTypeDefine VipType
        {
            get;
            set;
        }

        /// <summary>
        /// vip客户等级（C_CODE_ABBR_CUSTOMER_VipLevel）。1:A级 2:B级 3:C级
        /// </summary>
        [ORFieldMapping("VipLevel")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_CUSTOMER_VipLevel")]
        public string VipLevel
        {
            get;
            set;
        }

        /// <summary>
        /// 最后一次跟进时间
        /// </summary>
        [ORFieldMapping("FollowTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime FollowTime
        {
            get;
            set;
        }

        /// <summary>
        /// 预计下次回访时间
        /// </summary>
        [ORFieldMapping("NextFollowTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime NextFollowTime
        {
            get;
            set;
        }

        [ORFieldMapping("FollowedCount")]
        [DataMember]
        public int FollowedCount
        {
            get;
            set;
        }

        /// <summary>
        /// 转介绍员工岗位ID
        /// </summary>
        [ORFieldMapping("ReferralStaffID")]
        [DataMember]
        public string ReferralStaffID
        {
            get;
            set;
        }

        /// <summary>
        /// 转介绍员工姓名
        /// </summary>
        [ORFieldMapping("ReferralStaffName")]
        [DataMember]
        public string ReferralStaffName
        {
            get;
            set;
        }

        /// <summary>
        /// 转介绍员工岗位ID
        /// </summary>
        [ORFieldMapping("ReferralStaffJobID")]
        [DataMember]
        public string ReferralStaffJobID
        {
            get;
            set;
        }

        /// <summary>
        /// 转介绍员工岗位名称
        /// </summary>
        [ORFieldMapping("ReferralStaffJobName")]
        [DataMember]
        public string ReferralStaffJobName
        {
            get;
            set;
        }

        /// <summary>
        /// 转介绍员工OA编号
        /// </summary>
        [ORFieldMapping("ReferralStaffOACode")]
        [DataMember]
        public string ReferralStaffOACode
        {
            get;
            set;
        }

        /// <summary>
        /// 转介绍学员ID
        /// </summary>
        [ORFieldMapping("ReferralCustomerID")]
        [DataMember]
        public string ReferralCustomerID
        {
            get;
            set;
        }

        /// <summary>
        /// 转介绍学员编码
        /// </summary>
        [ORFieldMapping("ReferralCustomerCode")]
        [DataMember]
        public string ReferralCustomerCode
        {
            get;
            set;
        }

        /// <summary>
        /// 转介绍学员姓名
        /// </summary>
        [ORFieldMapping("ReferralCustomerName")]
        [DataMember]
        public string ReferralCustomerName
        {
            get;
            set;
        }

        /// <summary>
        /// 版本开始时间
        /// </summary>
        [DataMember]
        [ORFieldMapping("VersionStartTime", PrimaryKey = true, UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
        public DateTime VersionStartTime
        {
            get;
            set;
        }

        /// <summary>
        /// 版本结束时间
        /// </summary>
        [DataMember]
        [ORFieldMapping("VersionEndTime", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
        public DateTime VersionEndTime
        {
            get;
            set;
        }
    }
}
