using System;
using System.Linq;
using System.Collections.Generic;
using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.DataObjects;
using PPTS.Data.Customers.Entities;

namespace PPTS.Data.Customers.Adapters
{
    public class AccountChargePaymentAdapter : CustomerAdapterBase<AccountChargePayment, AccountChargePaymentCollection>
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

        protected override void BeforeInnerUpdateInContext(AccountChargePayment data, SqlContextItem sqlContext, Dictionary<string, object> context)
        {
            base.BeforeInnerUpdateInContext(data, sqlContext, context);
            this.InitData(data);
        }

        protected override void BeforeInnerUpdate(AccountChargePayment data, Dictionary<string, object> context)
        {
            base.BeforeInnerUpdate(data, context);
            this.InitData(data);
        }

        private void InitData(AccountChargePayment data)
        {
            if (data.PayID.IsNullOrEmpty())
                data.PayID = System.Guid.NewGuid().ToString();
            if (data.PayNo.IsNullOrEmpty())
                data.PayNo = Helper.GetApplyNo("SK");
        }
    }
}