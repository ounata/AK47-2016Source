using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Entities
{
    /// <summary>
    /// This object represents the properties and methods of a CustomerSearch.
    /// 学员信息表
    /// </summary>
    [Serializable]
    [ORTableMapping("SM.CustomerSearch")]
    [DataContract]
    public class CustomerSearch
    {
        public CustomerSearch()
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
        /// 家长ID
        /// </summary>
        [ORFieldMapping("ParentID")]
        [DataMember]
        public string ParentID
        {
            get;
            set;
        }

        /// <summary>
        /// 家长编号
        /// </summary>
        [ORFieldMapping("ParentCode")]
        [DataMember]
        public string ParentCode
        {
            get;
            set;
        }

        /// <summary>
        /// 家长姓名
        /// </summary>
        [ORFieldMapping("ParentName")]
        [DataMember]
        public string ParentName
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
        /// 学员状态（枚举-  正常-11，停课-12，休学-13，冻结-14（高三毕业9月后），结业-15）
        /// </summary>
        [ORFieldMapping("StudentStatus")]
        [DataMember]
        public string StudentStatus
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
        public decimal VipType
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
        /// 在读学校名称
        /// </summary>
        [ORFieldMapping("SchoolName")]
        [DataMember]
        public string SchoolName
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
        /// 上次回访时间
        /// </summary>
        [ORFieldMapping("VisitTime")]
        [DataMember]
        public DateTime VisitTime
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
        [ORFieldMapping("NextVisitTime")]
        [DataMember]
        public DateTime NextVisitTime
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
        /// 一对一最后订购日期
        /// </summary>
        [ORFieldMapping("LastOneToOneOrderTime")]
        [DataMember]
        public DateTime LastOneToOneOrderTime
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
        /// 归属组织机构类型
        /// </summary>
        [ORFieldMapping("OrgType")]
        [DataMember]
        public string OrgType
        {
            get;
            set;
        }

        /// <summary>
        /// 无效客户理由代码（参考跟进）
        /// </summary>
        [ORFieldMapping("InvalidReason")]
        [DataMember]
        public string InvalidReason
        {
            get;
            set;
        }

        /// <summary>
        /// 学年制(C_CODE_ABBR_ACDEMICYEAR)
        /// </summary>
        [ORFieldMapping("SchoolYear")]
        [DataMember]
        public string SchoolYear
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
        /// 购买意愿(C_CODE_ABBR_Customer_CRM_PurchaseIntent)
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
        public string LockMemo
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
        /// 创建者ID
        /// </summary>
        [ORFieldMapping("CreatorID")]
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

        /// <summary>
        /// 租户的ID
        /// </summary>
        [ORFieldMapping("TenantCode")]
        [DataMember]
        public string TenantCode
        {
            get;
            set;
        }

        /// <summary>
        /// 建档人岗位ID
        /// </summary>
        [ORFieldMapping("CreaterJobID")]
        [DataMember]
        public string CreaterJobID
        {
            get;
            set;
        }

        /// <summary>
        /// 建档人员工ID
        /// </summary>
        [ORFieldMapping("CreaterID")]
        [DataMember]
        public string CreaterID
        {
            get;
            set;
        }

        /// <summary>
        /// 建档人姓名
        /// </summary>
        [ORFieldMapping("CreaterName")]
        [DataMember]
        public string CreaterName
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
        /// 电销人员岗位ID
        /// </summary>
        [ORFieldMapping("CallCenterJobID")]
        [DataMember]
        public string CallCenterJobID
        {
            get;
            set;
        }

        /// <summary>
        /// 电销人员员工ID
        /// </summary>
        [ORFieldMapping("CallCenterID")]
        [DataMember]
        public string CallCenterID
        {
            get;
            set;
        }

        /// <summary>
        /// 电销人员姓名
        /// </summary>
        [ORFieldMapping("CallCenterName")]
        [DataMember]
        public string CallCenterName
        {
            get;
            set;
        }

        /// <summary>
        /// 市场人员岗位ID
        /// </summary>
        [ORFieldMapping("MarketingJobID")]
        [DataMember]
        public string MarketingJobID
        {
            get;
            set;
        }

        /// <summary>
        /// 市场人员员工ID
        /// </summary>
        [ORFieldMapping("MarketingID")]
        [DataMember]
        public string MarketingID
        {
            get;
            set;
        }

        /// <summary>
        /// 市场人员姓名
        /// </summary>
        [ORFieldMapping("MarketingName")]
        [DataMember]
        public string MarketingName
        {
            get;
            set;
        }

        /// <summary>
        /// 分配教师数量
        /// </summary>
        [ORFieldMapping("AssignTeacherNum")]
        [DataMember]
        public int AssignTeacherNum
        {
            get;
            set;
        }

        /// <summary>
        /// 主要监护人证件类型
        /// </summary>
        [ORFieldMapping("ParentIDType")]
        [DataMember]
        public string ParentIDType
        {
            get;
            set;
        }

        /// <summary>
        /// 主要监护人证件号码
        /// </summary>
        [ORFieldMapping("ParentIDNumber")]
        [DataMember]
        public string ParentIDNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 主要监护人称谓
        /// </summary>
        [ORFieldMapping("ParentRoleName")]
        [DataMember]
        public string ParentRoleName
        {
            get;
            set;
        }

        /// <summary>
        /// 学员称谓
        /// </summary>
        [ORFieldMapping("CustomerRoleName")]
        [DataMember]
        public string CustomerRoleName
        {
            get;
            set;
        }

        /// <summary>
        /// 学员主要联系电话类型
        /// </summary>
        [ORFieldMapping("CustomerPrimaryPhoneType")]
        [DataMember]
        public string CustomerPrimaryPhoneType
        {
            get;
            set;
        }

        /// <summary>
        /// 学员主要联系电话
        /// </summary>
        [ORFieldMapping("CustomerPrimaryPhoneNumber")]
        [DataMember]
        public string CustomerPrimaryPhoneNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 学员次要联系电话类型
        /// </summary>
        [ORFieldMapping("CustomerSecondaryPhoneType")]
        [DataMember]
        public string CustomerSecondaryPhoneType
        {
            get;
            set;
        }

        /// <summary>
        /// 学员次要联系电话
        /// </summary>
        [ORFieldMapping("CustomerSecondaryPhoneNumber")]
        [DataMember]
        public string CustomerSecondaryPhoneNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 家长主要联系电话类型
        /// </summary>
        [ORFieldMapping("ParentPrimaryPhoneType")]
        [DataMember]
        public string ParentPrimaryPhoneType
        {
            get;
            set;
        }

        /// <summary>
        /// 家长主要联系电话
        /// </summary>
        [ORFieldMapping("ParentPrimaryPhoneNumber")]
        [DataMember]
        public string ParentPrimaryPhoneNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 家长次要联系电话类型
        /// </summary>
        [ORFieldMapping("StudentSecondaryPhoneType")]
        [DataMember]
        public string StudentSecondaryPhoneType
        {
            get;
            set;
        }

        /// <summary>
        /// 家长次要联系电话
        /// </summary>
        [ORFieldMapping("StudentSecondaryPhoneNumber")]
        [DataMember]
        public string StudentSecondaryPhoneNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 当前在读学校ID
        /// </summary>
        [ORFieldMapping("CustomerSchoolID")]
        [DataMember]
        public string CustomerSchoolID
        {
            get;
            set;
        }

        /// <summary>
        /// 当前在读学校名称
        /// </summary>
        [ORFieldMapping("CustomerSchoolName")]
        [DataMember]
        public string CustomerSchoolName
        {
            get;
            set;
        }

        /// <summary>
        /// 上次上门时间
        /// </summary>
        [ORFieldMapping("VerifyTime")]
        [DataMember]
        public DateTime VerifyTime
        {
            get;
            set;
        }

        /// <summary>
        /// 上门次数
        /// </summary>
        [ORFieldMapping("VerifiedCount")]
        [DataMember]
        public int VerifiedCount
        {
            get;
            set;
        }

        /// <summary>
        /// 下次上门时间
        /// </summary>
        [ORFieldMapping("NextVerifyTime")]
        [DataMember]
        public DateTime NextVerifyTime
        {
            get;
            set;
        }

        /// <summary>
        /// 成绩单数量
        /// </summary>
        [ORFieldMapping("ScoreCount")]
        [DataMember]
        public int ScoreCount
        {
            get;
            set;
        }

        /// <summary>
        /// 首次缴费时间
        /// </summary>
        [ORFieldMapping("FirstAccountChargeApplyTime")]
        [DataMember]
        public DateTime FirstAccountChargeApplyTime
        {
            get;
            set;
        }

        /// <summary>
        /// 首次缴费金额
        /// </summary>
        [ORFieldMapping("FirstAccountChargeApplyMoney")]
        [DataMember]
        public decimal FirstAccountChargeApplyMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 最后一次缴费时间
        /// </summary>
        [ORFieldMapping("LastAccountChargeApplyTime")]
        [DataMember]
        public DateTime LastAccountChargeApplyTime
        {
            get;
            set;
        }

        /// <summary>
        /// 最后一次缴费金额
        /// </summary>
        [ORFieldMapping("LastAccountChargeApplyMoney")]
        [DataMember]
        public decimal LastAccountChargeApplyMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 最后一次退费时间
        /// </summary>
        [ORFieldMapping("LastAccountRefundTime")]
        [DataMember]
        public DateTime LastAccountRefundTime
        {
            get;
            set;
        }

        /// <summary>
        /// 最后一次退费金额
        /// </summary>
        [ORFieldMapping("LastAccountRefundMoney")]
        [DataMember]
        public decimal LastAccountRefundMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 账户余额
        /// </summary>
        [ORFieldMapping("AccountMoney")]
        [DataMember]
        public decimal AccountMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 账户折扣率
        /// </summary>
        [ORFieldMapping("AccountDiscountRate")]
        [DataMember]
        public decimal AccountDiscountRate
        {
            get;
            set;
        }

        /// <summary>
        /// 账户折扣基数
        /// </summary>
        [ORFieldMapping("AccountDiscountBase")]
        [DataMember]
        public decimal AccountDiscountBase
        {
            get;
            set;
        }

        /// <summary>
        /// 是否扣减服务费
        /// </summary>
        [ORFieldMapping("IsHasExpense")]
        [DataMember]
        public bool IsHasExpense
        {
            get;
            set;
        }

        /// <summary>
        /// 服务费扣减日期
        /// </summary>
        [ORFieldMapping("ExpenseTime")]
        [DataMember]
        public DateTime ExpenseTime
        {
            get;
            set;
        }

        /// <summary>
        /// 扣减服务费金额
        /// </summary>
        [ORFieldMapping("ExpenseFee")]
        [DataMember]
        public decimal ExpenseFee
        {
            get;
            set;
        }

        /// <summary>
        /// 转入金额
        /// </summary>
        [ORFieldMapping("AccountTransferInMoney")]
        [DataMember]
        public decimal AccountTransferInMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 最后一次转入日期
        /// </summary>
        [ORFieldMapping("LastAccountTransferInTime")]
        [DataMember]
        public DateTime LastAccountTransferInTime
        {
            get;
            set;
        }

        /// <summary>
        /// 转出金额
        /// </summary>
        [ORFieldMapping("AccountTransferOutMoney")]
        [DataMember]
        public decimal AccountTransferOutMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 最后一次转出日期
        /// </summary>
        [ORFieldMapping("LastAccountTransferOutTime")]
        [DataMember]
        public DateTime LastAccountTransferOutTime
        {
            get;
            set;
        }

        /// <summary>
        /// 剩余资产(1对1)数量
        /// </summary>
        [ORFieldMapping("AssetOneToOneAmount")]
        [DataMember]
        public decimal AssetOneToOneAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 剩余资产(班组)数量
        /// </summary>
        [ORFieldMapping("AssetClassAmount")]
        [DataMember]
        public decimal AssetClassAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 剩余资产(其他)数量
        /// </summary>
        [ORFieldMapping("AssetOtherAmount")]
        [DataMember]
        public decimal AssetOtherAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 剩余资产(1对1)金额
        /// </summary>
        [ORFieldMapping("AssetOneToOneMoney")]
        [DataMember]
        public decimal AssetOneToOneMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 剩余资产(班组)金额
        /// </summary>
        [ORFieldMapping("AssetClassMoney")]
        [DataMember]
        public decimal AssetClassMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 剩余资产(其他)金额
        /// </summary>
        [ORFieldMapping("AssetOtherMoney")]
        [DataMember]
        public decimal AssetOtherMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 排定资产(1对1)数量
        /// </summary>
        [ORFieldMapping("AssetOneToOneAssignedAmount")]
        [DataMember]
        public decimal AssetOneToOneAssignedAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 排定资产(班组)数量
        /// </summary>
        [ORFieldMapping("AssetClassAssignedAmount")]
        [DataMember]
        public decimal AssetClassAssignedAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 排定资产(1对1)价值
        /// </summary>
        [ORFieldMapping("AssetOneToOneAssignedMoney")]
        [DataMember]
        public decimal AssetOneToOneAssignedMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 排定资产(班组)价值
        /// </summary>
        [ORFieldMapping("AssetClassAssignedMoney")]
        [DataMember]
        public decimal AssetClassAssignedMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 消耗资产(1对1)数量
        /// </summary>
        [ORFieldMapping("AssetOneToOneConfirmedAmount")]
        [DataMember]
        public decimal AssetOneToOneConfirmedAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 消耗资产(班组)数量
        /// </summary>
        [ORFieldMapping("AssetClassConfirmedAmount")]
        [DataMember]
        public decimal AssetClassConfirmedAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 消耗资产(其他)数量
        /// </summary>
        [ORFieldMapping("AssetOtherConfirmedAmount")]
        [DataMember]
        public decimal AssetOtherConfirmedAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 消耗资产(1对1)价值
        /// </summary>
        [ORFieldMapping("AssetOneToOneConfirmedMoney")]
        [DataMember]
        public decimal AssetOneToOneConfirmedMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 消耗资产(班组)价值
        /// </summary>
        [ORFieldMapping("AssetClassConfirmedMoney")]
        [DataMember]
        public decimal AssetClassConfirmedMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 消耗资产(其他)价值
        /// </summary>
        [ORFieldMapping("AssetOtherConfirmedMoney")]
        [DataMember]
        public decimal AssetOtherConfirmedMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 消耗赠送资产(1对1)数量
        /// </summary>
        [ORFieldMapping("AssetPresentAmount")]
        [DataMember]
        public decimal AssetPresentAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 最后一次订购日期
        /// </summary>
        [ORFieldMapping("LastOrderTime")]
        [DataMember]
        public DateTime LastOrderTime
        {
            get;
            set;
        }

        /// <summary>
        /// 最后一次退订日期
        /// </summary>
        [ORFieldMapping("LastDebookOrderTime")]
        [DataMember]
        public DateTime LastDebookOrderTime
        {
            get;
            set;
        }

        /// <summary>
        /// 最后一次上课日期
        /// </summary>
        [ORFieldMapping("AssignTime")]
        [DataMember]
        public DateTime AssignTime
        {
            get;
            set;
        }

        /// <summary>
        /// 老合同最后一次退费时间
        /// </summary>
        [ORFieldMapping("LastAccountContractRefundTime")]
        [DataMember]
        public DateTime LastAccountContractRefundTime
        {
            get;
            set;
        }

        /// <summary>
        /// 老合同最后一次退费金额
        /// </summary>
        [ORFieldMapping("LastAccountContractRefundMoney")]
        [DataMember]
        public decimal LastAccountContractRefundMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 老合同账户余额
        /// </summary>
        [ORFieldMapping("AccountContractMoney")]
        [DataMember]
        public decimal AccountContractMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 老合同账户折扣率
        /// </summary>
        [ORFieldMapping("AccountContractDiscountRate")]
        [DataMember]
        public decimal AccountContractDiscountRate
        {
            get;
            set;
        }

        /// <summary>
        /// 老合同账户折扣基数
        /// </summary>
        [ORFieldMapping("AccountContractDiscountBase")]
        [DataMember]
        public decimal AccountContractDiscountBase
        {
            get;
            set;
        }

        /// <summary>
        /// 老合同剩余资产数量
        /// </summary>
        [ORFieldMapping("AccountContractAmount")]
        [DataMember]
        public decimal AccountContractAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 老合同排定资产数量
        /// </summary>
        [ORFieldMapping("AccountContractAssignedAmount")]
        [DataMember]
        public decimal AccountContractAssignedAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 老合同排定资产价值
        /// </summary>
        [ORFieldMapping("AccountContractAssignedMoney")]
        [DataMember]
        public decimal AccountContractAssignedMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 老合同消耗资产数量
        /// </summary>
        [ORFieldMapping("AccountContractConfirmedAmount")]
        [DataMember]
        public decimal AccountContractConfirmedAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 老合同消耗资产价值
        /// </summary>
        [ORFieldMapping("AccountContractConfirmedMoney")]
        [DataMember]
        public decimal AccountContractConfirmedMoney
        {
            get;
            set;
        }
    }

    [Serializable]
    [DataContract]
    public class CustomerSearchCollection : EditableDataObjectCollectionBase<CustomerSearch>
    {
    }
}
