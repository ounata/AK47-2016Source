using System.Linq;
using PPTS.Data.Customers.Entities;

namespace PPTS.Data.Customers.Adapters
{
    public class AccountAdapter : AccountAdapterBase<Account, AccountCollection>
    {
        public static readonly AccountAdapter Instance = new AccountAdapter();

        private AccountAdapter()
        {
        }

        /// <summary>
        /// 根据账户ID获取账户信息
        /// </summary>
        /// <param name="accountID"></param>
        /// <returns></returns>
        public Account LoadByAccountID(string accountID)
        {
            return this.Load(builder => builder.AppendItem("AccountID", accountID)).SingleOrDefault();
        }

        /// <summary>
        /// 根据学员ID获取账户列表
        /// </summary>
        /// <param name="accountID"></param>
        /// <returns></returns>
        public AccountCollection LoadCollectionByCustomerID(string customerID)
        {
            return this.Load(builder => builder.AppendItem("CustomerID", customerID));
        }
    }
}