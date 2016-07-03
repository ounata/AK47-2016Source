using MCS.Library.Core;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Executors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("登记发票")]
    public class AccountEditChargeInvoiceExecutor : PPTSEditCustomerExecutorBase<AccountChargeInvoice>
    {
        public string Msg { get; private set; }
        public AccountEditChargeInvoiceExecutor(AccountChargeInvoice model)
            : base(model, null)
        {
            model.NullCheck("model");
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            this.Msg = "ok";
            base.PrepareData(context);

            if (string.IsNullOrEmpty(this.Model.InvoiceID))
                this.Model.InvoiceID = UuidHelper.NewUuidString();

            AccountChargeInvoicesAdapter.Instance.UpdateInContext(this.Model);
        }

        protected override void PrepareOperationLog(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareOperationLog(context);
            context.Logs.ForEach(log => log.ResourceID = this.Model.ApplyID);
        }

    }
}