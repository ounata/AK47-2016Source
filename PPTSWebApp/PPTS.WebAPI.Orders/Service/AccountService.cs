using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.Service
{
    public class AccountService
    {
        /// <summary>
        /// 获取用户帐户
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public static List<Account> GetAccountbyCustomerId(string customerId)
        {
            var list = new List<Account>();

            var mapper = new AutoMapper.MapperConfiguration(c => c.CreateMap<Data.Customers.Entities.Account, Account>()).CreateMapper();
            Data.Customers.Adapters.AccountAdapter.Instance.LoadCollectionByCustomerID(customerId)
                .ForEach(m =>
                {
                    var n = mapper.Map<Account>(m);
                    list.Add(n);
                });
            return list;
        }

        /// <summary>
        /// 获取用户要扣取的服务费用
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>-1:未创建综合服务费</returns>
        public static decimal GetServiceMoneyByCustomerId(string customerId)
        {
            return 0;
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

    }

    public class Account : Data.Customers.Entities.Account { }

    public class AccountChargePayment :Data.Customers.Entities.AccountChargePayment { }

}