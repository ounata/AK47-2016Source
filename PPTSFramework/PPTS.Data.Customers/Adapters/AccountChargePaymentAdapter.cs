using System.Linq;
using PPTS.Data.Customers.Entities;

namespace PPTS.Data.Customers.Adapters
{
    public class AccountChargePaymentAdapter : AccountAdapterBase<AccountChargePayment, AccountChargePaymentCollection>
	{
		public static readonly AccountChargePaymentAdapter Instance = new AccountChargePaymentAdapter();

		private AccountChargePaymentAdapter()
		{
		}

        /// <summary>
        /// 根据缴费申请单ID获取支付单列表。
        /// </summary>
        /// <param name="accountID"></param>
        /// <returns></returns>
        public AccountChargePaymentCollection LoadCollectionByApplyID(string applyID)
		{
            return this.Load(builder => builder.AppendItem("ApplyID", applyID));
		}
	}
}