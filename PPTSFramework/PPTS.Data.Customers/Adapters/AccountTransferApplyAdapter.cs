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
    public class AccountTransferApplyAdapter : CustomerAdapterBase<AccountTransferApply, AccountTransferApplyCollection>
    {
        public static readonly AccountTransferApplyAdapter Instance = new AccountTransferApplyAdapter();

        private AccountTransferApplyAdapter()
        {
        }

        /// <summary>
        /// 根据服务费返还单ID获取服务费返还申请信息。
        /// </summary>
        /// <param name="applyID"></param>
        /// <returns></returns>
        public AccountTransferApply LoadByApplyID(string applyID)
        {
            return this.Load(builder => builder.AppendItem("ApplyID", applyID)).SingleOrDefault();
        }


        protected override void BeforeInnerUpdateInContext(AccountTransferApply data, SqlContextItem sqlContext, Dictionary<string, object> context)
        {
            base.BeforeInnerUpdateInContext(data, sqlContext, context);
            this.InitData(data);
        }

        protected override void BeforeInnerUpdate(AccountTransferApply data, Dictionary<string, object> context)
        {
            base.BeforeInnerUpdate(data, context);
            this.InitData(data);
        }

        private void InitData(AccountTransferApply data)
        {
            if (data.ApplyID.IsNullOrEmpty())
                data.ApplyID = System.Guid.NewGuid().ToString();
            if (data.ApplyNo.IsNullOrEmpty())
                data.ApplyNo = Helper.GetApplyNo("ZR");
            if (data.BizAccountID.IsNullOrEmpty())
                data.BizAccountID = data.ApplyID;
            if (data.BizAccountCode.IsNullOrEmpty())
                data.BizAccountCode = data.ApplyNo;
        }
    }

    public class AccountTransferApplyViewAdapter : CustomerAdapterBase<AccountTransferApplyView, AccountTransferApplyViewCollection>
    {
        public static readonly AccountTransferApplyViewAdapter Instance = new AccountTransferApplyViewAdapter();

        private AccountTransferApplyViewAdapter()
        {
        }

        /// <summary>
        /// 根据服务费返还单ID获取服务费返还申请信息。
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public AccountTransferApplyViewCollection LoadCollectionByCustomerID(string customerID)
        {
            return this.Load(builder => builder.AppendItem("CustomerID", customerID));
        }
    }
}