using System.Linq;
using PPTS.Data.Customers.Entities;

namespace PPTS.Data.Customers.Adapters
{
    public class AccountChargeApplyAdapter : AccountAdapterBase<AccountChargeApply, AccountChargeApplyCollection>
	{
		public static readonly AccountChargeApplyAdapter Instance = new AccountChargeApplyAdapter();

		private AccountChargeApplyAdapter()
		{
		}

        /// <summary>
        /// 根据缴费申请单ID获取缴费单申请信息。
        /// </summary>
        /// <param name="accountID"></param>
        /// <returns></returns>
        public AccountChargeApply LoadByApplyID(string applyID)
		{
            return this.Load(builder => builder.AppendItem("ApplyID", applyID)).SingleOrDefault();
		}
	}
}