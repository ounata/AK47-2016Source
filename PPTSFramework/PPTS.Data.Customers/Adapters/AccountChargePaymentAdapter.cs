using System.Linq;
using PPTS.Data.Customers.Entities;
using System;
using MCS.Library.Data.Adapters;

namespace PPTS.Data.Customers.Adapters
{
    public class AccountChargePaymentAdapter : AccountAdapterBase<AccountChargePayment, AccountChargePaymentCollection>
    {
        public static readonly AccountChargePaymentAdapter Instance = new AccountChargePaymentAdapter();

        private AccountChargePaymentAdapter()
        {
        }

        public AccountChargePayment LoadByPayID(string payID)
        {
            return this.Load(builder => builder.AppendItem("PayID", payID)).SingleOrDefault();
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

        public void LoadCollectionByApplyIDInContext(string applyID, Action<AccountChargePaymentCollection> action)
        {
            this.LoadByInBuilderInContext(new InLoadingCondition(builder => builder.AppendItem(applyID), "ApplyID"), collection => action(collection));
        }
    }
}