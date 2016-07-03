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
    public class AccountReturnApplyAdapter : CustomerAdapterBase<AccountReturnApply, AccountReturnApplyCollection>
	{
		public static readonly AccountReturnApplyAdapter Instance = new AccountReturnApplyAdapter();

		private AccountReturnApplyAdapter()
		{
		}

        /// <summary>
        /// 根据服务费返还单ID获取服务费返还申请信息。
        /// </summary>
        /// <param name="accountID"></param>
        /// <returns></returns>
        public AccountReturnApply LoadByApplyID(string applyID)
		{
            return this.Load(builder => builder.AppendItem("ApplyID", applyID)).SingleOrDefault();
		}

        protected override void BeforeInnerUpdateInContext(AccountReturnApply data, SqlContextItem sqlContext, Dictionary<string, object> context)
        {
            base.BeforeInnerUpdateInContext(data, sqlContext, context);
            this.InitData(data);
        }

        protected override void BeforeInnerUpdate(AccountReturnApply data, Dictionary<string, object> context)
        {
            base.BeforeInnerUpdate(data, context);
            this.InitData(data);
        }

        private void InitData(AccountReturnApply data)
        {
            if (data.ApplyID.IsNullOrEmpty())
                data.ApplyID = UuidHelper.NewUuidString();
            if (data.ApplyNo.IsNullOrEmpty())
                data.ApplyNo = Helper.GetApplyNo("FH");
        }
    }
}