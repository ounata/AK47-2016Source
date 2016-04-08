using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers
{
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
        Invalid = 9
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
}
