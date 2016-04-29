using System.Linq;
using PPTS.Data.Customers.Entities;
using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.DataObjects;
using System.Collections.Generic;
using System;

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
        public void LoadByApplyIDInContext(string applyID, Action<AccountChargeApply> action)
        {
            this.LoadByInBuilderInContext(new InLoadingCondition(builder => builder.AppendItem(applyID), "ApplyID")
                , collection => action(collection.SingleOrDefault()));
        }

        protected override void BeforeInnerUpdateInContext(AccountChargeApply data, SqlContextItem sqlContext, Dictionary<string, object> context)
        {
            base.BeforeInnerUpdateInContext(data, sqlContext, context);
            this.InitData(data);
        }
        
        protected override void BeforeInnerUpdate(AccountChargeApply data, Dictionary<string, object> context)
        {
            base.BeforeInnerUpdate(data, context);
            this.InitData(data);
        }

        private void InitData(AccountChargeApply data)
        {
            if (data.ApplyID.IsNullOrEmpty())
                data.ApplyID = System.Guid.NewGuid().ToString();
            if (data.ApplyNo.IsNullOrEmpty())
                data.ApplyNo = Helper.GetApplyNo("NC");
            if (data.AccountID.IsNullOrEmpty())
                data.AccountID = data.ApplyID;
            if (data.AccountCode.IsNullOrEmpty())
                data.AccountCode = data.ApplyNo;
        }
    }
}