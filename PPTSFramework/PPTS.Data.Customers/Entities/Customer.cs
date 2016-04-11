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
    /// This object represents the properties and methods of a Customer.
    /// 学员信息表
    /// </summary>
    [Serializable]
    [ORTableMapping("Customers")]
    [DataContract]
    public class Customer
    {
        public Customer()
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
        [ORFieldMapping("CustomerID", PrimaryKey = true)]
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
        /// 客户等级（A1,A2,A3...)
        /// </summary>
        [ORFieldMapping("CustomerLevel")]
        [DataMember]
        public string CustomerLevel
        {
            get;
            set;
        }

        /// <summary>
        /// 学员状态（枚举-  正常，休学，冻结（高三毕业9月后），结业）
        /// </summary>
        [ORFieldMapping("CustomerStatus")]
        [DataMember]
        public string CustomerStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 是否仅登记一个家长
        /// </summary>
        [ORFieldMapping("IsOneParent")]
        [DataMember]
        public int IsOneParent
        {
            get;
            set;
        }

        /// <summary>
        /// 学员描述
        /// </summary>
        [ORFieldMapping("Character")]
        [DataMember]
        public string Character
        {
            get;
            set;
        }

        /// <summary>
        /// 出生年月
        /// </summary>
        [ORFieldMapping("Birthday")]
        [DataMember]
        public DateTime Birthday
        {
            get;
            set;
        }

        /// <summary>
        /// 性别代码
        /// </summary>
        [ORFieldMapping("Gender")]
        [DataMember]
        public string Gender
        {
            get;
            set;
        }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        [ORFieldMapping("Email")]
        [DataMember]
        public string Email
        {
            get;
            set;
        }

        /// <summary>
        /// 证件类型
        /// </summary>
        [ORFieldMapping("IDType")]
        [DataMember]
        public string IDType
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
        /// VIP类型
        /// </summary>
        [ORFieldMapping("VipType")]
        [DataMember]
        public string VipType
        {
            get;
            set;
        }

        /// <summary>
        /// VIP级别
        /// </summary>
        [ORFieldMapping("VipLevel")]
        [DataMember]
        public string VipLevel
        {
            get;
            set;
        }

        /// <summary>
        /// 入学年级代码
        /// </summary>
        [ORFieldMapping("EntranceGrade")]
        [DataMember]
        public string EntranceGrade
        {
            get;
            set;
        }

        /// <summary>
        /// 当前年级代码
        /// </summary>
        [ORFieldMapping("Grade")]
        [DataMember]
        public string Grade
        {
            get;
            set;
        }

        /// <summary>
        /// 学年制代码（五年制，六年制等）
        /// </summary>
        [ORFieldMapping("ShoolYear")]
        [DataMember]
        public string ShoolYear
        {
            get;
            set;
        }

        /// <summary>
        /// 学科类型代码（文科，理科，不分科）
        /// </summary>
        [ORFieldMapping("SubjectType")]
        [DataMember]
        public string SubjectType
        {
            get;
            set;
        }

        /// <summary>
        /// 学员类型代码
        /// </summary>
        [ORFieldMapping("StudentType")]
        [DataMember]
        public string StudentType
        {
            get;
            set;
        }

        /// <summary>
        /// 接触方式代码
        /// </summary>
        [ORFieldMapping("ContactType")]
        [DataMember]
        public string ContactType
        {
            get;
            set;
        }

        /// <summary>
        /// 信息来源一级分类代码
        /// </summary>
        [ORFieldMapping("SourceMainType")]
        [DataMember]
        public string SourceMainType
        {
            get;
            set;
        }

        /// <summary>
        /// 信息来源二级分类代码
        /// </summary>
        [ORFieldMapping("SourceSubType")]
        [DataMember]
        public string SourceSubType
        {
            get;
            set;
        }

        /// <summary>
        /// 来源系统代码
        /// </summary>
        [ORFieldMapping("SourceSystem")]
        [DataMember]
        public string SourceSystem
        {
            get;
            set;
        }

        /// <summary>
        /// 转介绍员工ID
        /// </summary>
        [ORFieldMapping("ReferralStaffID")]
        [DataMember]
        public string ReferralStaffID
        {
            get;
            set;
        }

        /// <summary>
        /// 转介绍员工OA账号
        /// </summary>
        [ORFieldMapping("ReferralStaffCode")]
        [DataMember]
        public string ReferralStaffCode
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
        /// 转介绍学生ID
        /// </summary>
        [ORFieldMapping("ReferralCustomerID")]
        [DataMember]
        public string ReferralCustomerID
        {
            get;
            set;
        }

        /// <summary>
        /// 转介绍学生编号
        /// </summary>
        [ORFieldMapping("ReferralCustomerCode")]
        [DataMember]
        public string ReferralCustomerCode
        {
            get;
            set;
        }

        /// <summary>
        /// 转介绍学生姓名
        /// </summary>
        [ORFieldMapping("ReferralCustomerName")]
        [DataMember]
        public string ReferralCustomerName
        {
            get;
            set;
        }

        /// <summary>
        /// 购买意向
        /// </summary>
        [ORFieldMapping("PurchaseIntention")]
        [DataMember]
        public string PurchaseIntention
        {
            get;
            set;
        }

        /// <summary>
        /// 是否锁定
        /// </summary>
        [ORFieldMapping("Locked")]
        [DataMember]
        public int Locked
        {
            get;
            set;
        }

        /// <summary>
        /// 锁定描述（转学，毕业）
        /// </summary>
        [ORFieldMapping("LockMemo")]
        [DataMember]
        public long LockMemo
        {
            get;
            set;
        }

        /// <summary>
        /// 是否高三毕业
        /// </summary>
        [ORFieldMapping("Graduated")]
        [DataMember]
        public int Graduated
        {
            get;
            set;
        }

        /// <summary>
        /// 高三毕业年份
        /// </summary>
        [ORFieldMapping("GraduateYear")]
        [DataMember]
        public string GraduateYear
        {
            get;
            set;
        }

        /// <summary>
        /// 在读学校ID
        /// </summary>
        [ORFieldMapping("SchoolID")]
        [DataMember]
        public string SchoolID
        {
            get;
            set;
        }

        /// <summary>
        /// 是否复读
        /// </summary>
        [ORFieldMapping("IsStudyAgain")]
        [DataMember]
        public int IsStudyAgain
        {
            get;
            set;
        }

        /// <summary>
        /// 首次签约时间
        /// </summary>
        [ORFieldMapping("FirstSignTime")]
        [DataMember]
        public DateTime FirstSignTime
        {
            get;
            set;
        }

        /// <summary>
        /// 首次签约人ID
        /// </summary>
        [ORFieldMapping("FirstSignerID")]
        [DataMember]
        public string FirstSignerID
        {
            get;
            set;
        }

        /// <summary>
        /// 首次签约人
        /// </summary>
        [ORFieldMapping("FirstSignerName")]
        [DataMember]
        public string FirstSignerName
        {
            get;
            set;
        }

        /// <summary>
        /// 当前跟进时间
        /// </summary>
        [ORFieldMapping("FollowTime")]
        [DataMember]
        public DateTime FollowTime
        {
            get;
            set;
        }

        /// <summary>
        /// 当前跟进阶段
        /// </summary>
        [ORFieldMapping("FollowStage")]
        [DataMember]
        public string FollowStage
        {
            get;
            set;
        }

        /// <summary>
        /// 已跟进次数
        /// </summary>
        [ORFieldMapping("FollowedCount")]
        [DataMember]
        public int FollowedCount
        {
            get;
            set;
        }

        /// <summary>
        /// 下一次跟进时间
        /// </summary>
        [ORFieldMapping("NextFollowTime")]
        [DataMember]
        public DateTime NextFollowTime
        {
            get;
            set;
        }

        /// <summary>
        /// 当前回访时间
        /// </summary>
        [ORFieldMapping("VistTime")]
        [DataMember]
        public DateTime VistTime
        {
            get;
            set;
        }

        /// <summary>
        /// 已回访次数
        /// </summary>
        [ORFieldMapping("VisitedCount")]
        [DataMember]
        public int VisitedCount
        {
            get;
            set;
        }

        /// <summary>
        /// 下次回访时间
        /// </summary>
        [ORFieldMapping("NextVistTime")]
        [DataMember]
        public DateTime NextVistTime
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
    public class CustomerCollection : EditableDataObjectCollectionBase<Customer>
    {
    }
}