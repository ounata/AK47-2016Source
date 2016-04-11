using System.Linq;
using PPTS.Data.Customers.Entities;

namespace PPTS.Data.Customers.Adapters
{
    public class AccountTransferApplyAdapter : AccountAdapterBase<AccountTransferApply, AccountTransferApplyCollection>
	{
		public static readonly AccountTransferApplyAdapter Instance = new AccountTransferApplyAdapter();

		private AccountTransferApplyAdapter()
		{
		}

        /// <summary>
        /// 根据服务费返还单ID获取服务费返还申请信息。
        /// </summary>
        /// <param name="accountID"></param>
        /// <returns></returns>
        public AccountTransferApply LoadByApplyID(string applyID)
		{
            return this.Load(builder => builder.AppendItem("ApplyID", applyID)).SingleOrDefault();
		}
	}
}