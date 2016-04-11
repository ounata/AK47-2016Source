using System.Linq;
using PPTS.Data.Customers.Entities;

namespace PPTS.Data.Customers.Adapters
{
    public class AccountChargeAllotAdapter : AccountAdapterBase<AccountChargeAllot, AccountChargeAllotCollection>
	{
		public static readonly AccountChargeAllotAdapter Instance = new AccountChargeAllotAdapter();

		private AccountChargeAllotAdapter()
		{
		}

        /// <summary>
        /// 根据缴费申请单ID获取费用分摊列表。
        /// </summary>
        /// <param name="accountID"></param>
        /// <returns></returns>
        public AccountChargeAllotCollection LoadCollectionByApplyID(string applyID)
		{
            return this.Load(builder => builder.AppendItem("ApplyID", applyID));
		}
	}
}