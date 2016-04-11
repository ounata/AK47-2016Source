using MCS.Library.Data.Mapping;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PPTS.WebAPI.Customers.ViewModels.Accounts
{
    /// <summary>
    /// 账户信息
    /// </summary>
    [Serializable]
    public class AccountModel
    {
        /// <summary>
        /// 账户ID
        /// </summary>
        [DataMember]
        public string AccountID
        {
            get;
            set;
        }

        /// <summary>
        /// 账户编码
        /// </summary>
        [DataMember]
        public string AccountCode
        {
            get;
            set;
        }

        /// <summary>
        /// 账户余额
        /// </summary>
        [DataMember]
        public decimal AccountMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 折扣基数
        /// </summary>
        [DataMember]
        public decimal DiscountBase
        {
            get;
            set;
        }

        /// <summary>
        /// 折扣率
        /// </summary>
        [DataMember]
        public decimal DiscountRate
        {
            get;
            set;
        }

        /// <summary>
        /// 冻结金额（订购冻结）
        /// </summary>
        [DataMember]
        public decimal FrozenMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 账户价值（订购冻结金额FrozenMoney+账户余额AccountMoney）
        /// </summary>
        [DataMember]
        public decimal AccountValue
        {
            get
            {
                return this.AccountMoney + this.FrozenMoney;
            }
        }
    }
}