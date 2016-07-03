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
    public class AccountDeductApplyAdapter : CustomerAdapterBase<AccountDeductApply, AccountDeductApplyCollection>
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

        protected override void BeforeInnerUpdateInContext(AccountDeductApply data, SqlContextItem sqlContext, Dictionary<string, object> context)
        {
            base.BeforeInnerUpdateInContext(data, sqlContext, context);
            this.InitData(data);
        }

        protected override void BeforeInnerUpdate(AccountDeductApply data, Dictionary<string, object> context)
        {
            base.BeforeInnerUpdate(data, context);
            this.InitData(data);
        }

        private void InitData(AccountDeductApply data)
        {
            if (data.ApplyID.IsNullOrEmpty())
                data.ApplyID = UuidHelper.NewUuidString();
            if (data.ApplyNo.IsNullOrEmpty())
                data.ApplyNo = Helper.GetApplyNo("KJ");
        }
    }
}