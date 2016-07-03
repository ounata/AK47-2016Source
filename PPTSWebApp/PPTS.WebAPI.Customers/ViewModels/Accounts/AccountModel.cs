using MCS.Library.Data.Mapping;
using PPTS.Contracts.Orders.Models;
using PPTS.Contracts.Proxies;
using PPTS.Data.Customers;
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
        public AccountModel()
        {
            this.DiscountRate = 1;
        }

        /// <summary>
        /// 当前已排定课时数量
        /// </summary>
        [DataMember]
        public decimal AssignedAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 当前已确认课时数量
        /// </summary>
        [DataMember]
        public decimal ConfirmedAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 课时消耗价值
        /// </summary>
        [DataMember]
        public decimal ConsumptionValue
        {
            set;
            get;
        }

        /// <summary>
        /// 计算折扣返还开始时间点
        /// </summary>
        [DataMember]
        public DateTime ReallowanceStartTime
        {
            set;
            get;
        }

        /// <summary>
        /// 退费使用的折扣ID
        /// </summary>
        [DataMember]
        public string RefundDiscountID
        {
            set;
            get;
        }

        /// <summary>
        /// 根据账户ID获取账户模型
        /// </summary>
        /// <param name="accountID"></param>
        /// <returns></returns>
        public static AccountModel LoadByAccountID(string accountID)
        {
            return LoadByAccountID(accountID, false);
        }
        public static AccountModel LoadByAccountID(string accountID, bool isRefund)
        {
            Account account = AccountAdapter.Instance.LoadByAccountID(accountID);
            if (account == null)
                return null;

            return BuildAssetMoney(account, isRefund);
        }

        /// <summary>
        /// 根据学员ID获取合同账户信息
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static AccountModel LoadContractByCustomerID(string customerID)
        {
            List<AccountModel> models = LoadByCustomerID(customerID);
            return models.Where(x => x.AccountType == AccountTypeDefine.Contract).SingleOrDefault();
        }

        /// <summary>
        /// 根据学员ID获取可充值的账户
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static AccountModel LoadChargableByCustomerID(string customerID)
        {
            decimal totalAccountValue;
            return LoadChargableByCustomerID(customerID, out totalAccountValue);
        }
        public static AccountModel LoadChargableByCustomerID(string customerID, out decimal totalAccountValue)
        {
            List<AccountModel> models = LoadByCustomerID(customerID);
            totalAccountValue = models.Sum(x => x.AccountValue);
            return models.Where(x => x.AccountStatus == AccountStatusDefine.Chargable).SingleOrDefault();
        }

        /// <summary>
        /// 获取账户总的价值
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static decimal GetAccountTotalValue(string customerID)
        {
            List<AccountModel> models = LoadByCustomerID(customerID);
            return models.Sum(x => x.AccountValue);
        }

        /// <summary>
        /// 为转让获取账户信息
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static List<AccountModel> Load4TransferByCustomerID(string customerID)
        {
            List<AccountModel> models = new List<AccountModel>();
            AccountCollection accounts = AccountAdapter.Instance.LoadCollectionByCustomerID(customerID);
            foreach (Account account in accounts)
            {
                if (account.AccountMoney != 0)
                {
                    AccountModel model = BuildAssetMoney(account, false);
                    models.Add(model);
                }
            }
            return models.OrderByDescending(x => x.CreateTime).ToList();
        }

        /// <summary>
        /// 为退费获取账户列表
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static List<AccountModel> Load4RefundByCustomerID(string customerID)
        {
            List<AccountModel> models = new List<AccountModel>();
            AccountCollection accounts = AccountAdapter.Instance.LoadCollectionByCustomerID(customerID);
            foreach (Account account in accounts)
            {
                if (account.AccountMoney != 0)
                {
                    AccountModel model = BuildAssetMoney(account, true);
                    if (model.AssetMoney == 0)
                        models.Add(model);
                }
            }
            return models.OrderByDescending(x => x.CreateTime).ToList();
        }

        /// <summary>
        /// 根据学员ID获取所有账户
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static List<AccountModel> LoadByCustomerID(string customerID)
        {
            List<AccountModel> models = new List<AccountModel>();
            AccountCollection accounts = AccountAdapter.Instance.LoadCollectionByCustomerID(customerID);
            foreach (Account account in accounts)
            {
                AccountModel model = BuildAssetMoney(account, false);
                models.Add(model);
            }
            return models.OrderByDescending(x => x.CreateTime).ToList();
        }

        private static AccountModel BuildAssetMoney(Account account, bool isRefund)
        {
            AccountModel model = account.ProjectedAs<AccountModel>();
            AssetStatisticQueryResult asset = PPTSAssetQueryServiceProxy.Instance.QueryAssetStatisticByAccountID(account.AccountID);
            if (asset != null)
            {
                model.AssetMoney = asset.AssetMoney;
                model.AssignedAmount = asset.AssignedAmount;
                model.ConfirmedAmount = asset.ConfirmedAmount;
            }
            if (isRefund)
            {
                model.RefundDiscountID = model.DiscountID;
                RefundConsumptionValueQueryCriteriaModel criteria = new RefundConsumptionValueQueryCriteriaModel();
                criteria.AccountID = account.AccountID;

                //获取最后退费审批时间
                List<AccountRefundApply> refunds = AccountRefundApplyAdapter.Instance.LoadVerifiedCollectionByAccountID(account.AccountID)
                    .OrderByDescending(x => x.ApproveTime).ToList();
                if (refunds.Count != 0)
                    criteria.LastestRefundDate = refunds[0].ApproveTime;

                //获取折扣率发生变法的缴费单支付时间（最新的和次新的）
                List<AccountChargeApply> charges = AccountChargeApplyAdapter.Instance.LoadPaidCollectionByAccountID(account.AccountID)
                    .Where(x => x.ThatDiscountRate != x.ThisDiscountRate).OrderByDescending(x => x.PayTime).ToList();
                if (charges.Count > 0)
                {
                    criteria.LastestChargeDate = charges[0].PayTime;
                    model.RefundDiscountID = charges[0].ThisDiscountID;
                }
                if (charges.Count > 1)
                    criteria.LastChargeDate = charges[1].PayTime;

                RefundConsumptionValueQueryResult result = PPTSAssetQueryServiceProxy.Instance.QueryConsumptionValue(criteria);
                if (result != null)
                {
                    model.ConsumptionValue = result.ConsumptionValue;
                    model.ReallowanceStartTime = result.ReallowanceStartTime;
                }
            }
            return model;
        }
    }
}