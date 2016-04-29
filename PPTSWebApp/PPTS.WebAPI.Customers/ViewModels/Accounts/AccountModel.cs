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
    [DataContract]
    public class AccountModel : Account
    {
        /// <summary>
        /// 资产价值（剩余的资产的价值）
        /// </summary>
        [DataMember]
        public decimal AssetMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 账户价值（资产价值AssetMoney+账户余额AccountMoney）
        /// </summary>
        [DataMember]
        public decimal AccountValue
        {
            get
            {
                return this.AccountMoney + this.AssetMoney;
            }
        }

        public static AccountModel Load(Account account)
        {
            AccountModel model = AutoMapper.Mapper.DynamicMap<AccountModel>(account);
            model.AssetMoney = 0;

            return model;
        }
    }
}