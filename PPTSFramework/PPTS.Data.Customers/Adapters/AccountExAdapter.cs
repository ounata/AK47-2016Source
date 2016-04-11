using System.Linq;
using PPTS.Data.Customers.Entities;

namespace PPTS.Data.Customers.Adapters
{
    public class AccountExAdapter : AccountAdapterBase<AccountEx, AccountExCollection>
    {
        public static readonly AccountExAdapter Instance = new AccountExAdapter();

        private AccountExAdapter()
        {
        }

        /// <summary>
        /// 根据账户ID获取账户扩展信息。
        /// </summary>
        /// <param name="accountID">账户ID</param>
        /// <returns></returns>
        public AccountEx LoadByAccountID(string accountID)
        {
            return this.Load(builder => builder.AppendItem("AccountID", accountID)).SingleOrDefault();
        }

        /// <summary>
        /// 根据学员ID获取账户扩展信息列表。
        /// </summary>
        /// <param name="customerID">学员ID</param>
        /// <returns></returns>
        public AccountExCollection LoadCollectionByCustomerID(string customerID)
        {
            return this.Load(builder => builder.AppendItem("CustomerID", customerID));
        }
    }
}