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
    public class AccountRefundApplyAdapter : CustomerAdapterBase<AccountRefundApply, AccountRefundApplyCollection>
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

        public AccountRefundApplyCollection LoadVerifiedCollectionByAccountID(string accountID)
        {
            return this.Load(builder => builder
                                        .AppendItem("AccountID", accountID)
                                        .AppendItem("VerifyStatus", (int)RefundVerifyStatus.Refunded));
        }

        protected override void BeforeInnerUpdateInContext(AccountRefundApply data, SqlContextItem sqlContext, Dictionary<string, object> context)
        {
            base.BeforeInnerUpdateInContext(data, sqlContext, context);
            this.InitData(data);
        }

        protected override void BeforeInnerUpdate(AccountRefundApply data, Dictionary<string, object> context)
        {
            base.BeforeInnerUpdate(data, context);
            this.InitData(data);
        }

        private void InitData(AccountRefundApply data)
        {
            if (data.ApplyID.IsNullOrEmpty())
                data.ApplyID = System.Guid.NewGuid().ToString();
            if (data.ApplyNo.IsNullOrEmpty())
                data.ApplyNo = Helper.GetApplyNo("TF");
        }
    }
}