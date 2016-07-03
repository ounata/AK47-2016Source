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
    public class AccountRefundVerifyingAdapter : CustomerAdapterBase<AccountRefundVerifying, AccountRefundVerifyingCollection>
    {
        public static readonly AccountRefundVerifyingAdapter Instance = new AccountRefundVerifyingAdapter();

        private AccountRefundVerifyingAdapter()
        {
        }

        /// <summary>
        /// 根据缴费申请单ID获取支付单列表。
        /// </summary>
        /// <param name="accountID"></param>
        /// <returns></returns>
        public AccountRefundVerifyingCollection LoadCollectionByApplyID(string applyID)
        {
            return this.Load(builder => builder.AppendItem("ApplyID", applyID));
        }

        protected override void BeforeInnerUpdateInContext(AccountRefundVerifying data, SqlContextItem sqlContext, Dictionary<string, object> context)
        {
            base.BeforeInnerUpdateInContext(data, sqlContext, context);
            this.InitData(data);
        }

        protected override void BeforeInnerUpdate(AccountRefundVerifying data, Dictionary<string, object> context)
        {
            base.BeforeInnerUpdate(data, context);
            this.InitData(data);
        }

        private void InitData(AccountRefundVerifying data)
        {
            if (data.VerifyID.IsNullOrEmpty())
                data.VerifyID = UuidHelper.NewUuidString();
        }
    }
}