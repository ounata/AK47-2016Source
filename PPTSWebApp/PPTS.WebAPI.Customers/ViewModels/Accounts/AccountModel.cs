using MCS.Library.Data.Mapping;
using PPTS.Data.Customers.Adapters;
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
        /// 订购资金余额（剩余的资产的价值）
        /// </summary>
        [DataMember]
        public decimal AssetMoney
        {
            set;
            get;
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

        /// <summary>
        /// 针对退费用，当前已发生资产价值（从最近一次签约充值开始计算）
        /// </summary>
        [DataMember]
        public decimal OccurenceValue
        {
            set;
            get;
        }

        /// <summary>
        /// 针对退费用，已消耗资产价值（最近一次充值或退费之后订购的产品截止当前消耗的课时价值，
        /// （系统判断订购单保存时间晚于该客户最近一次充值的充值日期、制度内退费、制度外退费都以分区域财务经理确认时间）
        /// </summary>
        [DataMember]
        public decimal ConsumptionValue
        {
            set;
            get;
        }

        public static AccountModel LoadByAccountID(string accountID)
        {
            return LoadByAccountID(accountID, false);
        }

        public static AccountModel LoadByAccountID(string accountID, bool isRefund)
        {
            Account account = AccountAdapter.Instance.LoadByAccountID(accountID);
            if (account == null)
                return null;

            AccountModel model = Load(account, isRefund);
            return model;
        }

        public static AccountModel LoadCurrentByAccountID(string customerID)
        {
            Account account = AccountAdapter.Instance.LoadCurrentByCustomerID(customerID);
            if (account != null)
                return Load(account, false);
            return null;
        }

        public static AccountModel LoadCurrentByCustomerID(string customerID, out decimal totalAccountValue)
        {
            List<AccountModel> models = LoadByCustomerID(customerID);
            totalAccountValue = models.Sum(x => x.AccountValue);
            return models.Where(x => x.IsLatest == true).SingleOrDefault();
        }

        public static List<AccountModel> LoadByCustomerID(string customerID)
        {
            return LoadByCustomerID(customerID, false);
        }
        public static List<AccountModel> LoadByCustomerID(string customerID, bool isRefund)
        {
            List<AccountModel> models = new List<AccountModel>();
            foreach (Account account in AccountAdapter.Instance.LoadCollectionByCustomerID(customerID))
            {
                AccountModel model = Load(account, isRefund);
                models.Add(model);
            }
            return models;
        }

        private static AccountModel Load(Account account, bool isRefund)
        {
            AccountModel model = AutoMapper.Mapper.DynamicMap<AccountModel>(account);
            model.AssetMoney = 0;
            if(isRefund)
            {
                model.OccurenceValue = 0;
                model.ConsumptionValue = 0;
            }
            return model;
        }
    }
}