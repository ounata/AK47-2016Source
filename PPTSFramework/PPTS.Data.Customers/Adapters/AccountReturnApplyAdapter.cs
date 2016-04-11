using System.Linq;
using PPTS.Data.Customers.Entities;

namespace PPTS.Data.Customers.Adapters
{
    public class AccountReturnApplyAdapter : AccountAdapterBase<AccountReturnApply, AccountReturnApplyCollection>
	{
		public static readonly AccountReturnApplyAdapter Instance = new AccountReturnApplyAdapter();

		private AccountReturnApplyAdapter()
		{
		}

        /// <summary>
        /// 根据服务费返还单ID获取服务费返还申请信息。
        /// </summary>
        /// <param name="accountID"></param>
        /// <returns></returns>
        public AccountReturnApply LoadByApplyID(string applyID)
		{
            return this.Load(builder => builder.AppendItem("ApplyID", applyID)).SingleOrDefault();
		}
	}
}