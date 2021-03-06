using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PPTS.Data.Customers.Entities
{
    /// <summary>
    /// This object represents the properties and methods of a Account.
    /// 账户信息表
    /// </summary>
    [Serializable]
    [ORTableMapping("CM.Accounts", "CM.Accounts_Current")]
    [DataContract]
    public class Account : IVersionDataObjectWithoutID, IEntityWithCreator, IEntityWithModifier
    {
        public Account()
        {
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
        /// 账户ID
        /// </summary>
        [ORFieldMapping("AccountID", PrimaryKey = true)]
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
        /// 账户类型（合同账户，拓路账户）
        /// </summary>
        [ORFieldMapping("AccountType")]
        [ConstantCategory("Account_AccountType")]
        [DataMember]
        public AccountTypeDefine AccountType
        {
            get;
            set;
        }

        /// <summary>
        /// 账户说明
        /// </summary>
        [ORFieldMapping("AccountMemo")]
        [DataMember]
        public string AccountMemo
        {
            get;
            set;
        }

        /// <summary>
        /// 账户状态（可充值，不可充值）
        /// </summary>
        [ORFieldMapping("AccountStatus")]
        [ConstantCategory("Account_AccountStatus")]
        [DataMember]
        public AccountStatusDefine AccountStatus
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
        /// 折扣ID
        /// </summary>
        [ORFieldMapping("DiscountID")]
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
        /// 折扣率
        /// </summary>
        [ORFieldMapping("DiscountRate")]
        [DataMember]
        public decimal DiscountRate
        {
            get;
            set;
        }

        /// <summary>
        /// 折扣基数
        /// </summary>
        [ORFieldMapping("DiscountBase")]
        [DataMember]
        public decimal DiscountBase
        {
            get;
            set;
        }

        /// <summary>
        /// 最近一笔缴费申请单ID
        /// </summary>
        [ORFieldMapping("ChargeApplyID")]
        [DataMember]
        public string ChargeApplyID
        {
            get;
            set;
        }

        /// <summary>
        /// 最近一笔缴费单支付时间
        /// </summary>
        [ORFieldMapping("ChargePayTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime ChargePayTime
        {
            get;
            set;
        }

        /// <summary>
        /// 首笔缴费申请单ID
        /// </summary>
        [ORFieldMapping("FirstChargeApplyID")]
        [DataMember]
        public string FirstChargeApplyID
        {
            get;
            set;
        }

        /// <summary>
        /// 首笔缴费单支付时间
        /// </summary>
        [ORFieldMapping("FirstChargePayTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime FirstChargePayTime
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
        [ORFieldMapping("CreateTime", UtcTimeToLocal = true)]
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
        [ORFieldMapping("ModifyTime", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update, DefaultExpression = "GETUTCDATE()", ForceUseDefaultExpression = true)]
        [DataMember]
        public DateTime ModifyTime
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
        
        /// <summary>
        /// 订购资金余额（剩余的资产的价值）
        /// </summary>
        [NoMapping]
        [DataMember]
        public decimal AssetMoney
        {
            set;
            get;
        }

        /// <summary>
        /// 账户价值（资产价值AssetMoney+账户余额AccountMoney）
        /// </summary>
        [NoMapping]
        [DataMember]
        public decimal AccountValue
        {
            get
            {
                return this.AccountMoney + this.AssetMoney;
            }
        }

    }

    [Serializable]
    [DataContract]
    public class AccountCollection : EditableDataObjectCollectionBase<Account>
    {
    }
}