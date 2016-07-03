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
    public class AccountChargeAllotAdapter : CustomerAdapterBase<AccountChargeAllot, AccountChargeAllotCollection>
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
        public void LoadCollectionByApplyIDInContext(string applyID, Action<AccountChargeAllotCollection> action)
        {
            this.LoadByInBuilderInContext(new InLoadingCondition(builder => builder.AppendItem(applyID), "ApplyID"), collection => action(collection));
        }

        protected override void BeforeInnerUpdateInContext(AccountChargeAllot data, SqlContextItem sqlContext, Dictionary<string, object> context)
        {
            base.BeforeInnerUpdateInContext(data, sqlContext, context);
            this.InitData(data);
        }

        protected override void BeforeInnerUpdate(AccountChargeAllot data, Dictionary<string, object> context)
        {
            base.BeforeInnerUpdate(data, context);
            this.InitData(data);
        }

        private void InitData(AccountChargeAllot data)
        {
            if (data.AllotID.IsNullOrEmpty())
                data.AllotID = UuidHelper.NewUuidString();
        }
    }
}