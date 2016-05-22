using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.Service
{
    public class CustomerService
    {
        /// <summary>
        /// 获取用户帐户
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public static List<Account> GetAccountbyCustomerId(string customerId)
        {
            return PPTS.Contracts.Proxies.PPTSAccountQueryServiceProxy.Instance.QueryAccountCollectionByCustomerID(customerId).AccountCollection;
            
            //return Data.Customers.Adapters.AccountAdapter.Instance.LoadCollectionByCustomerID(customerId).ToList();
        }

        /// <summary>
        /// 获取用户是否扣除过综合服务费
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public static Dictionary<int,bool> GetWhetherToDeductServiceChargeByCustomerId(string customerId)
        {
            return new Dictionary<int, bool>() { {1,true } };
        }



        /// <summary>
        /// 获取缴费单信息
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public static List<AccountChargePayment> GetChargePaymentsByCustomerId(string customerId)
        {
            return new List<AccountChargePayment>() { new AccountChargePayment() { ApplyID="111", PayMemo="www" } };
        }

        /// <summary>
        /// 获取缴费单信息
        /// </summary>
        /// <param name="chargeApplyId"></param>
        /// <returns></returns>
        public static AccountChargePayment GetChargePaymentById(string chargeApplyId)
        {
            return new AccountChargePayment() { ApplyID = "111", PayMemo = "www" };
        }

        /// <summary>
        /// 获取学员信息
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public static Data.Customers.Entities.Customer GetCustomerByCustomerId(string customerId)
        {
            return Data.Customers.Adapters.CustomerAdapter.Instance.Load(customerId);
        }

        /// <summary>
        /// 获取学员父母信息
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public static Data.Customers.Entities.Parent GetPrimaryParentByCustomerId(string customerId)
        {
            return Data.Customers.Adapters.ParentAdapter.Instance.LoadPrimaryParentInContext(customerId);
        }

    }

    //public class Account : Data.Customers.Entities.Account { }

    //public class AccountChargePayment :Data.Customers.Entities.AccountChargePayment { }

}