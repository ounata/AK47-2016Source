using System.Linq;
using PPTS.Data.Customers.Entities;
using MCS.Library.Data.Adapters;
using System;
using MCS.Library.Data.Builder;
using MCS.Library.Core;

namespace PPTS.Data.Customers.Adapters
{
    public class AccountAdapter : GenericAccountAdapter<Account, AccountCollection>
    {
        public new static readonly AccountAdapter Instance = new AccountAdapter();

        /// <summary>
        /// 根据学员ID获取账户列表
        /// </summary>
        /// <param name="accountID"></param>
        /// <returns></returns>
        public AccountCollection LoadCollectionByCustomerID(string customerID)
        {
            return this.Load(builder => builder.AppendItem("CustomerID", customerID), DateTime.MinValue);
        }

        /// <summary>
        /// 根据学员ID获取当前可充值账号。
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public Account LoadChargableByCustomerID(string customerID)
        {
            return this.Load(builder => builder
            .AppendItem("CustomerID", customerID)
            .AppendItem("AccountStatus",(int)AccountStatusDefine.Chargable), DateTime.MinValue).FirstOrDefault();
        }

        public Account Load(string accountID)
        {
            //.AppendItem("AccountMoney", money, ">=")
            return Load(builder => builder.AppendItem("AccountID", accountID), DateTime.MinValue).SingleOrDefault();
        }
        
    }
}