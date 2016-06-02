using MCS.Library.Core;

namespace PPTS.Data.Customers
{
    /// <summary>
    /// 学员与员工关系
    /// </summary>
    public enum CustomerRelationType
    {
        [EnumItemDescription("咨询关系")]
        Consultant = 1,
        [EnumItemDescription("学管关系")]
        Educator = 2,
        [EnumItemDescription("教学关系")]
        Teacher = 3,
        [EnumItemDescription("电销关系")]
        Callcenter = 4,
        [EnumItemDescription("市场关系")]
        Market = 5
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
        /// 停课
        /// </summary>
        [EnumItemDescription("停课")]
        Stopped,

        /// <summary>
        /// 休学
        /// </summary>
        [EnumItemDescription("休学")]
        Pending,

        /// <summary>
        /// 冻结（高三毕业9月后）
        /// </summary>
        [EnumItemDescription("冻结")]
        Blocked,

        /// <summary>
        /// 结业
        /// </summary>
        [EnumItemDescription("结业")]
        Graduated
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
    /// 退费类型
    /// </summary>
    public enum PayTypeDefine
    {
        [EnumItemDescription("现金")]
        Cash = 1,

        [EnumItemDescription("电汇")]
        Telex = 2,

        [EnumItemDescription("POS刷卡")]
        POS = 3,

        [EnumItemDescription("支票")]
        Cheque = 4,

        [EnumItemDescription("其他Pos刷卡")]
        OtherPOS = 5,

        [EnumItemDescription("工行pos刷卡")]
        ICBC = 6,

        [EnumItemDescription("天猫短信码")]
        Tmall = 9,

        [EnumItemDescription("银联封顶pos刷卡")]
        UnionpayPos = 12,

        [EnumItemDescription("银联普通封顶")]
        UnionPayNotButtCapPos = 13,

        [EnumItemDescription("银联不封顶")]
        UnionPayNotButtCapIsNotPos = 14,

        [EnumItemDescription("快钱普通封顶")]
        FastMoneyNotButtCapPos = 15,

        [EnumItemDescription("快钱不封顶")]
        FastMoneyNotButtCapIsNot = 16
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
        No,
        /// <summary>
        /// 是
        /// </summary>
        Yes
    }
}
