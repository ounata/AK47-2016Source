﻿using MCS.Library.Core;
using PPTS.Data.Common;

namespace PPTS.Data.Customers
{
    /// <summary>
    /// 学员与员工关系
    /// </summary>
    public enum CustomerRelationType
    {
        [EnumItemDescription("咨询关系")]
        Consultant = JobTypeDefine.Consultant,
        [EnumItemDescription("学管关系")]
        Educator = JobTypeDefine.Educator,
        [EnumItemDescription("教学关系")]
        Teacher = JobTypeDefine.Teacher,
        [EnumItemDescription("电销关系")]
        Callcenter = JobTypeDefine.Callcenter,
        [EnumItemDescription("市场关系")]
        Market = JobTypeDefine.Marketing,
        [EnumItemDescription("建档关系")]
        Creator = 10,
    }

    /// <summary>
    /// 客户Vip类型
    /// </summary>
    public enum VipTypeDefine
    {
        [EnumItemDescription("关系Vip客户")]
        RelationVip = 1,

        [EnumItemDescription("大单Vip客户")]
        BigOrderVip = 2,

        [EnumItemDescription("非Vip客户")]
        NoVip = 3
    }

    /// <summary>
    /// 销售阶段
    /// </summary>
    public enum SalesStageType
    {
        [EnumItemDescription("未邀约")]
        NotInvited = 1,

        [EnumItemDescription("已邀约未上门")]
        InvitedNotVisited = 2,

        [EnumItemDescription("已上门")]
        Visited = 3,

        [EnumItemDescription("已签约")]
        Signed = 4
    }

    /// <summary>
    /// 客户状态(C_Code_Abbr_BO_CTI_CustomerStatus)
    /// </summary>
    public enum CustomerStatus
    {
        [EnumItemDescription("未确认客户信息")]
        NotConfirmed = 1,

        [EnumItemDescription("确认客户信息")]
        Confirmed = 2,

        [EnumItemDescription("无效用户")]
        Invalid = 9,

        [EnumItemDescription("正式学员")]
        Formal = 10,
    }

    /// <summary>
    /// 学员状态
    /// </summary>
    public enum StudentStatusDefine
    {
        /// <summary>
        /// 正常
        /// </summary>
        [EnumItemDescription("正常")]
        Normal = 11,

        /// <summary>
        /// 冻结（高三毕业9月后）
        /// </summary>
        [EnumItemDescription("冻结")]
        Blocked = 14,

        /// <summary>
        /// 解冻中
        /// </summary>
        [EnumItemDescription("解冻中")]
        Releasing = 141
    }

    public enum ThawReasonType
    {
        /// <summary>
        /// 高三复读
        /// </summary>
        [EnumItemDescription("高三复读")]
        Repeat = 1,
        /// <summary>
        /// 应届毕业生
        /// </summary>
        [EnumItemDescription("应届毕业生")]
        Grad = 2,
        /// <summary>
        /// 其他
        /// </summary>
        [EnumItemDescription("其他")]
        Other = 3
    }

    /// <summary>
    /// 购买意愿(C_CODE_ABBR_Customer_CRM_PurchaseIntent)
    /// </summary>
    public enum PurchaseIntentionDefine
    {
        [EnumItemDescription("强")]
        Strong = 1,

        [EnumItemDescription("中")]
        Moderate = 2,

        [EnumItemDescription("弱")]
        Weak = 3,

        [EnumItemDescription("无意愿")]
        NoIntention = 9
    }

    public enum StudentBranchType
    {
        [EnumItemDescription("文科")]
        LiberalArts = 1,

        [EnumItemDescription("理科")]
        Science = 2,

        [EnumItemDescription("未分科")]
        NoBranch = 3
    }

    /// <summary>
    /// 电话类型
    /// </summary>
    public enum PhoneTypeDefine
    {
        [EnumItemDescription("未知")]
        Unknown = 0,

        [EnumItemDescription("手机")]
        MobilePhone = 1,

        [EnumItemDescription("住宅电话")]
        HomePhone = 2,

        [EnumItemDescription("办公电话")]
        WorkPhone = 3
    }

    public static class PayTypeConsts
    {
        /// <summary>
        /// 现金
        /// </summary>
        public const string Cash = "1";

        /// <summary>
        /// 电汇
        /// </summary>
        public const string Telex = "2";

        /// <summary>
        /// 支票
        /// </summary>
        public const string Cheque = "4";
    }

    /// <summary>
    /// 对账状态
    /// </summary>
    public enum CheckStatusDefine
    {
        /// <summary>
        /// 未对帐
        /// </summary>
        [EnumItemDescription("未对帐")]
        UnCheck,

        /// <summary>
        /// 已对账
        /// </summary>
        [EnumItemDescription("已对账")]
        Checked
    }

    /// <summary>
    /// 缴费单审核状态
    /// </summary>
    public enum ChargeAuditStatus
    {
        /// <summary>
        /// 未审核
        /// </summary>
        [EnumItemDescription("未审核")]
        UnAudit,

        /// <summary>
        /// 已审核
        /// </summary>
        [EnumItemDescription("已审核")]
        Audited
    }

    /// <summary>
    /// 退费确认状态
    /// </summary>
    public enum RefundVerifyStatus
    {
        /// <summary>
        /// 待分出纳确认
        /// </summary>
        [EnumItemDescription("待分出纳确认")]
        WaitCashierVerify,

        /// <summary>
        /// 待分财务确认
        /// </summary>
        [EnumItemDescription("待分财务确认")]
        WaitFinanceVerify,

        /// <summary>
        /// 待分区域确认
        /// </summary>
        [EnumItemDescription("待分区域确认")]
        WaitRegionVerify,

        /// <summary>
        /// 已退款到账
        /// </summary>
        [EnumItemDescription("已退款到账")]
        Refunded
    }

    /// <summary>
    /// 退费确认操作
    /// </summary>
    public enum RefundVerifyAction
    {
        /// <summary>
        /// 分出纳确认
        /// </summary>
        [EnumItemDescription("分出纳确认")]
        CashierVerify = 1,

        /// <summary>
        /// 分财务确认
        /// </summary>
        [EnumItemDescription("分财务确认")]
        FinanceVerify = 2,

        /// <summary>
        /// 分区域确认
        /// </summary>
        [EnumItemDescription("分区域确认")]
        RegionVerify = 3
    }

    /// <summary>
    /// 申请状态
    /// </summary>
    public enum ApplyStatusDefine
    {
        /// <summary>
        /// 草稿
        /// </summary>
        [EnumItemDescription("草稿")]
        New,

        /// <summary>
        /// 审批中
        /// </summary>
        [EnumItemDescription("审批中")]
        Approving,

        /// <summary>
        /// 已完成
        /// </summary>
        [EnumItemDescription("已完成")]
        Approved,

        /// <summary>
        /// 已驳回
        /// </summary>
        [EnumItemDescription("已驳回")]
        Refused
    }

    /// <summary>
    /// 缴费类型
    /// </summary>
    public enum ChargeTypeDefine
    {
        /// <summary>
        /// 新签
        /// </summary>
        [EnumItemDescription("新签")]
        New = 1,

        /// <summary>
        /// 前期结课续费
        /// </summary>
        [EnumItemDescription("前期结课续费")]
        EarlyEndRenew = 2,

        /// <summary>
        /// 前期非结课续费（新签回款）
        /// </summary>
        [EnumItemDescription("前期非结课续费")]
        EarlyStudyRenew = 3,

        /// <summary>
        /// 后期结课续费
        /// </summary>
        [EnumItemDescription("后期结课续费")]
        LaterEndRenew = 4,

        /// <summary>
        /// 后期非结课续费
        /// </summary>
        [EnumItemDescription("后期非结课续费")]
        LaterStudyRenew = 5
    }

    /// <summary>
    /// 退费类型
    /// </summary>
    public enum RefundTypeDefine
    {
        /// <summary>
        /// 正常退费
        /// </summary>
        [EnumItemDescription("正常退费")]
        Regular = 1,

        /// <summary>
        /// 坏账退费
        /// </summary>
        [EnumItemDescription("坏账退费")]
        Irregular = 2,
    }

    /// <summary>
    /// 支付状态
    /// </summary>
    public enum PayStatusDefine
    {
        /// <summary>
        /// 未支付
        /// </summary>
        [EnumItemDescription("未支付")]
        Unpay,

        /// <summary>
        /// 已支付
        /// </summary>
        [EnumItemDescription("已支付")]
        Paid
    }

    /// <summary>
    /// 账户类型
    /// </summary>
    public enum AccountTypeDefine
    {
        /// <summary>
        /// 合同账户
        /// </summary>
        [EnumItemDescription("合同账户")]
        Contract,

        /// <summary>
        /// 拓路账户
        /// </summary>
        [EnumItemDescription("拓路账户")]
        Tunland
    }

    /// <summary>
    /// 账户状态
    /// </summary>
    public enum AccountStatusDefine
    {
        /// <summary>
        /// 不可充值
        /// </summary>
        [EnumItemDescription("不可充值")]
        Uncharged,

        /// <summary>
        /// 可充值
        /// </summary>
        [EnumItemDescription("可充值")]
        Chargable
    }

    /// <summary>
    /// 账户转让类型
    /// </summary>
    public enum AccountTransferType
    {
        /// <summary>
        /// 转出
        /// </summary>
        [EnumItemDescription("转出")]
        TransferOut = 1,

        /// <summary>
        /// 转入
        /// </summary>
        [EnumItemDescription("转入")]
        TransferIn = 2
    }

    /// <summary>
    /// 账户记录类型
    /// </summary>
    public enum AccountRecordType
    {
        /// <summary>
        /// 充值
        /// </summary>
        Charge = 1,

        /// <summary>
        /// 退费
        /// </summary>
        Refund = 2,

        /// <summary>
        /// 转入
        /// </summary>
        TransferIn = 3,

        /// <summary>
        /// 转出
        /// </summary>
        TransferOut = 4,

        /// <summary>
        /// 订购
        /// </summary>
        Order = 5,

        /// <summary>
        /// 退订
        /// </summary>
        Debook = 6,

        /// <summary>
        /// 服务费扣除
        /// </summary>
        Deduct = 7,

        /// <summary>
        /// 服务费返还
        /// </summary>
        Return = 8
    }

    /// <summary>
    /// 学员转学类型
    /// </summary>
    public enum StudentTransferType
    {
        /// <summary>
        /// 同分公司转学
        /// </summary>
        [EnumItemDescription("同分公司转学")]
        SameBranch = 1,

        /// <summary>
        /// 跨分公司转学
        /// </summary>
        [EnumItemDescription("跨分公司转学")]
        CrossBranch = 2,

    }

    /// <summary>
    /// 客服服务类型  Customer_ServiceType 
    /// </summary>
    public enum CustomerServiceType
    {
        /// <summary>
        /// 投诉
        /// </summary>
        [EnumItemDescription("投诉")]
        Complaint = 1,

        /// <summary>
        /// 退费
        /// </summary>
        [EnumItemDescription("退费")]
        Refund = 2,

        /// <summary>
        /// 咨询
        /// </summary>
        [EnumItemDescription("咨询")]
        Consultation = 3,

        /// <summary>
        /// 其它
        /// </summary>
        [EnumItemDescription("其它")]
        Other = 4
    }

    public enum CustomerServiceStatus
    {
        /// <summary>
        /// 未处理
        /// </summary>
        [EnumItemDescription("未处理")]
        Untreated = 1,
        /// <summary>
        /// 已处理
        /// </summary>
        [EnumItemDescription("已处理")]
        Processed = 2,
        /// <summary>
        /// 已转出
        /// </summary>
        [EnumItemDescription("已转出")]
        TransferredOut = 3
    }

    /// <summary>
    /// 学情会会议内容代码
    /// </summary>
    public enum ContentType
    {
        /// <summary>
        /// 解决方案
        /// </summary>
        [EnumItemDescription("解决方案")]
        Solution = 2,

        /// <summary>
        /// 反馈内容
        /// </summary>
        [EnumItemDescription("反馈内容")]
        FeedbackContent = 1
    }

    /// <summary>
    /// 接触方式
    /// </summary>
    public enum NewContactType
    {
        /// <summary>
        /// 呼入
        /// </summary>
        [EnumItemDescription("呼入")]
        CallIn = 1,

        /// <summary>
        /// 呼出
        /// </summary>
        [EnumItemDescription("呼出")]
        CallOut = 2,

        /// <summary>
        /// 直访
        /// </summary>
        [EnumItemDescription("直访")]
        Direct = 3,

        /// <summary>
        /// 在线咨询-乐语
        /// </summary>
        [EnumItemDescription("在线咨询-乐语")]
        OnlineLeYu = 4,

        /// <summary>
        /// 在线咨询-其他
        /// </summary>
        [EnumItemDescription("在线咨询-其他")]
        OnlineOhter = 5
    }

    /// <summary>
    /// 跟进对象代码
    /// </summary>
    public enum SaleContactTarget
    {
        /// <summary>
        /// 母亲
        /// </summary>
        [EnumItemDescription("母亲")]
        Mother = 1,

        /// <summary>
        /// 父亲
        /// </summary>
        [EnumItemDescription("父亲")]
        Father = 2,

        /// <summary>
        /// 学生本人
        /// </summary>
        [EnumItemDescription("学生本人")]
        Onself = 3,

        /// <summary>
        /// 其他联系人
        /// </summary>
        [EnumItemDescription("其他联系人")]
        Others = 4,
    }

    /// <summary>
    /// 停课休学类型
    /// </summary>
    public enum StopAlertType
    {
        /// <summary>
        /// 停课
        /// </summary>
        [EnumItemDescription("停课")]
        Stop,

        /// <summary>
        /// 休学
        /// </summary>
        [EnumItemDescription("休学")]
        DropOut,
    }

    /// <summary>
    /// 退费预警状态
    /// </summary>
    public enum RefundAlertStatus
    {
        /// <summary>
        /// 预警中
        /// </summary>
        Warning,

        /// <summary>
        /// 解除预警
        /// </summary>
        Warned
    }

    /// <summary>
    /// 跟进方式
    /// </summary>
    public enum SaleContactType
    {
        /// <summary>
        /// 短信
        /// </summary>
        [EnumItemDescription("短信")]
        Message = 1,
        /// <summary>
        /// 邮件
        /// </summary>
        [EnumItemDescription("邮件")]
        Email = 2,
        /// <summary>
        /// 致电
        /// </summary>
        [EnumItemDescription("致电")]
        Call = 3,
        /// <summary>
        /// 接听来电
        /// </summary>
        [EnumItemDescription("接听来电")]
        InCommingCall = 4,
        /// <summary>
        /// 面谈
        /// </summary>
        [EnumItemDescription("面谈")]
        ToFace = 5,
        /// <summary>
        /// 在线咨询
        /// </summary>
        [EnumItemDescription("在线咨询")]
        OnLine = 6
    }

    /// <summary>
    /// 成绩是否全部添加
    /// </summary>
    public enum ScoreIsAllAdded
    {
        /// <summary>
        /// 否
        /// </summary>
        [EnumItemDescription("否")]
        No,
        /// <summary>
        /// 是
        /// </summary>
        [EnumItemDescription("是")]
        Yes
    }

    /// <summary>
    /// 归属关系是否已分配
    /// </summary>
    public enum StaffRelationIsAssigned
    {
        /// <summary>
        /// 否
        /// </summary>
        [EnumItemDescription("否")]
        No,
        /// <summary>
        /// 是
        /// </summary>
        [EnumItemDescription("是")]
        Yes
    }

    #region 学员管理学员状态

    /// <summary>
    /// 学员状态
    /// </summary>
    public enum CustomerTypeDefine
    {
        /// <summary>
        /// 有效学员
        /// </summary>
        [EnumItemDescription("有效学员")]
        Valid = 1,
        /// <summary>
        /// 上课学员
        /// </summary>
        [EnumItemDescription("上课学员")]
        Attend,
        /// <summary>
        /// 停课学员
        /// </summary>
        [EnumItemDescription("停课学员")]
        Stop,
        /// <summary>
        /// 休学学员
        /// </summary>
        [EnumItemDescription("休学学员")]
        Suspend,
        /// <summary>
        /// 结课学员
        /// </summary>
        [EnumItemDescription("结课学员")]
        Completed,
        /// <summary>
        /// 无订单学员
        /// </summary>
        [EnumItemDescription("无订单学员")]
        NoOrder,
    }

    /// <summary>
    /// 有效学员二级
    /// </summary>
    public enum ValidDefine
    {
        /// <summary>
        /// 1对1有效
        /// </summary>
        [EnumItemDescription("1对1有效")]
        OneToOneValid = 1,
        /// <summary>
        /// 班组有效
        /// </summary>
        [EnumItemDescription("班组有效")]
        ClassValid,
        /// <summary>
        /// 非课收有效
        /// </summary>
        [EnumItemDescription("非课收有效")]
        OtherValid
    }

    /// <summary>
    /// 上课学员二级
    /// </summary>
    public enum AttendDefine
    {
        /// <summary>
        /// 1对1上课
        /// </summary>
        [EnumItemDescription("1对1上课")]
        OneToOneAttend = 1,
        /// <summary>
        /// 班组上课
        /// </summary>
        [EnumItemDescription("班组上课")]
        ClassAttend
    }

    /// <summary>
    /// 停课学员二级
    /// </summary>
    public enum StopDefine
    {
        /// <summary>
        /// 1对1停课
        /// </summary>
        [EnumItemDescription("1对1停课")]
        OneToOneStop = 1,
        /// <summary>
        /// 班组停课
        /// </summary>
        [EnumItemDescription("班组停课")]
        ClassStop
    }

    /// <summary>
    /// 休学学员二级
    /// </summary>
    public enum SuspendDefine
    {
        /// <summary>
        /// 1对1停课
        /// </summary>
        [EnumItemDescription("1对1休学")]
        OneToOneSuspend = 1,
        /// <summary>
        /// 班组停课
        /// </summary>
        [EnumItemDescription("班组休学")]
        ClassSuspend
    }

    /// <summary>
    /// 结课学员二级
    /// </summary>
    public enum CompletedDefine
    {
        /// <summary>
        /// 未结课
        /// </summary>
        [EnumItemDescription("未结课")]
        NotCompleted = 1,
        /// <summary>
        /// 消耗结课
        /// </summary>
        [EnumItemDescription("上课结课")]
        ConsumeCompleted,
        /// <summary>
        /// 退费结课
        /// </summary>
        [EnumItemDescription("退费结课")]
        ReturnCompleted,
        /// <summary>
        /// 转让结课
        /// </summary>
        [EnumItemDescription("转让结课")]
        TransferCompleted
    }

    /// <summary>
    /// 时长判断方式
    /// </summary>
    public enum DateTypeDefine
    {
        /// <summary>
        /// 按最后上课时间判断时长
        /// </summary>
        [EnumItemDescription("按最后上课时间判断时长")]
        LastCourse,
        /// <summary>
        /// 未上过课按付款时间判断时长
        /// </summary>
        [EnumItemDescription("未上过课按付款时间判断时长")]
        LastPay
    }

    /// <summary>
    /// 发票状态
    /// </summary>
    public enum InvoiceStatusDefine
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 已退
        /// </summary>
        Return = 2
    }
    ///发票记录状态
    public enum InvoiceRecordStatusDefine
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 已退
        /// </summary>
        Invalid = 2
    }


    #endregion

    #region 银联接口，支付数据来源，POS刷卡交易类型

    /// <summary>
    /// 支付数据来源
    /// </summary>
    public enum PaySourceType
    {
        /// <summary>
        /// 1--接口(实时接口)来源
        /// </summary>
        [EnumItemDescription("实时接口")]
        Sync = 1,

        /// <summary>
        /// 2--对账(异步接口)来源
        /// </summary>
        [EnumItemDescription("对账单")]
        Async = 2
    }

    /// <summary>
    /// POS交易类型，银联（1）通联（4）
    /// </summary>
    public enum POSTransactionType
    {
        /// <summary>
        /// 银联
        /// </summary>
        [EnumItemDescription("银联")]
        UnionPay = 1,

        /// <summary>
        /// 通联
        /// </summary>
        [EnumItemDescription("通联")]
        AllInPay = 4
    }

    #endregion

    #region 同步状态

    /// <summary>
    /// 同步状态
    /// </summary>
    public enum SynchroStatusDefine
    {
        /// <summary>
        /// 未同步
        /// </summary>
        [EnumItemDescription("未同步")]
        NotSynchronized = 0,
        /// <summary>
        /// 已同步
        /// </summary>
        [EnumItemDescription("已同步")]
        Synchronized = 1,
        /// <summary>
        /// 无需同步
        /// </summary>
        [EnumItemDescription("无需同步")]
        NoNeedSynchrozation = 2
    }

    #endregion
}
