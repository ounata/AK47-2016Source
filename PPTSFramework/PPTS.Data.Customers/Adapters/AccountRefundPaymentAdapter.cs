using System.Linq;
using PPTS.Data.Customers.Entities;

namespace PPTS.Data.Customers.Adapters
{
    public class AccountRefundPaymentAdapter : AccountAdapterBase<AccountRefundPayment, AccountRefundPaymentCollection>
	{
		public static readonly AccountRefundPaymentAdapter Instance = new AccountRefundPaymentAdapter();

		private AccountRefundPaymentAdapter()
		{
		}

        /// <summary>
        /// 根据缴费申请单ID获取支付单列表。
        /// </summary>
        /// <param name="accountID"></param>
        /// <returns></returns>
        public AccountRefundPaymentCollection LoadCollectionByApplyID(string applyID)
		{
            return this.Load(builder => builder.AppendItem("ApplyID", applyID));
		}
	}
}