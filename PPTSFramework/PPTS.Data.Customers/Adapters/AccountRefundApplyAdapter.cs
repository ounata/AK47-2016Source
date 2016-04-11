using System.Linq;
using PPTS.Data.Customers.Entities;

namespace PPTS.Data.Customers.Adapters
{
    public class AccountRefundApplyAdapter : AccountAdapterBase<AccountRefundApply, AccountRefundApplyCollection>
	{
		public static readonly AccountRefundApplyAdapter Instance = new AccountRefundApplyAdapter();

		private AccountRefundApplyAdapter()
		{
		}

        /// <summary>
        /// 根据退费申请单ID获取退费单申请信息。
        /// </summary>
        /// <param name="accountID"></param>
        /// <returns></returns>
        public AccountRefundApply LoadByApplyID(string applyID)
		{
            return this.Load(builder => builder.AppendItem("ApplyID", applyID)).SingleOrDefault();
		}
	}
}