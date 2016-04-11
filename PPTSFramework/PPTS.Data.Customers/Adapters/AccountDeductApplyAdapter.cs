using System.Linq;
using PPTS.Data.Customers.Entities;

namespace PPTS.Data.Customers.Adapters
{
    public class AccountDeductApplyAdapter : AccountAdapterBase<AccountDeductApply, AccountDeductApplyCollection>
	{
		public static readonly AccountDeductApplyAdapter Instance = new AccountDeductApplyAdapter();

		private AccountDeductApplyAdapter()
		{
		}

        /// <summary>
        /// 根据服务费扣除单ID获取服务费扣除申请信息。
        /// </summary>
        /// <param name="accountID"></param>
        /// <returns></returns>
        public AccountDeductApply LoadByApplyID(string applyID)
		{
            return this.Load(builder => builder.AppendItem("ApplyID", applyID)).SingleOrDefault();
		}
	}
}