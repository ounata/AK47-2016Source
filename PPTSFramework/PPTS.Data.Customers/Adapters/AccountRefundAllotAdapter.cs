using System.Linq;
using PPTS.Data.Customers.Entities;

namespace PPTS.Data.Customers.Adapters
{
    public class AccountRefundAllotAdapter : AccountAdapterBase<AccountRefundAllot, AccountRefundAllotCollection>
	{
		public static readonly AccountRefundAllotAdapter Instance = new AccountRefundAllotAdapter();

		private AccountRefundAllotAdapter()
		{
		}

        /// <summary>
        /// 根据退费申请单ID获取费用分摊列表。
        /// </summary>
        /// <param name="accountID"></param>
        /// <returns></returns>
        public AccountRefundAllotCollection LoadCollectionByApplyID(string applyID)
		{
            return this.Load(builder => builder.AppendItem("ApplyID", applyID));
		}
	}
}