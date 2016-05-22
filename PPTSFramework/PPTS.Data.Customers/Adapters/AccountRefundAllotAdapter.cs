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
    public class AccountRefundAllotAdapter : CustomerAdapterBase<AccountRefundAllot, AccountRefundAllotCollection>
	{
		public static readonly AccountRefundAllotAdapter Instance = new AccountRefundAllotAdapter();

		private AccountRefundAllotAdapter()
		{
		}

        /// <summary>
        /// 根据退费申请单ID获取费用分摊列表。
        /// </summary>
        /// <param name="accountID"></param>
        /// <returns></returns>
        public AccountRefundAllotCollection LoadCollectionByApplyID(string applyID)
		{
            return this.Load(builder => builder.AppendItem("ApplyID", applyID));
		}

        protected override void BeforeInnerUpdateInContext(AccountRefundAllot data, SqlContextItem sqlContext, Dictionary<string, object> context)
        {
            base.BeforeInnerUpdateInContext(data, sqlContext, context);
            this.InitData(data);
        }

        protected override void BeforeInnerUpdate(AccountRefundAllot data, Dictionary<string, object> context)
        {
            base.BeforeInnerUpdate(data, context);
            this.InitData(data);
        }

        private void InitData(AccountRefundAllot data)
        {
            if (data.AllotID.IsNullOrEmpty())
                data.AllotID = System.Guid.NewGuid().ToString();
        }
    }
}