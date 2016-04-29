using System.Linq;
using PPTS.Data.Customers.Entities;
using MCS.Library.Data.Adapters;
using System;

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
            AccountCollection accounts = new AccountCollection();
            foreach (Account account in this.Load(builder => builder.AppendItem("CustomerID", customerID), DateTime.MinValue).OrderByDescending(x => x.CreateTime))
            {
                if (account.AccountStatus != AccountStatusDefine.Disabled)
                    accounts.Add(account);
            }
            if (accounts.Count != 0)
                accounts[0].IsLatest = true;
            return accounts;
        }
    }
}