using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Customers.ViewModels.Accounts
{
    public class AccountChargeInvoiceModel
    {
        public AccountChargeInvoice Invoice { get; set; }

        public Dictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; private set; }

        public void InitModel() {
            this.Invoice = new AccountChargeInvoice();
            this.Invoice.InvoiceStatus = Data.Customers.InvoiceStatusDefine.Normal;
            this.Invoice.IsDiscarded = Data.Customers.InvoiceRecordStatusDefine.Normal;
            this.Invoice.FillCreator();
            this.Invoice.FillModifier();
            this.Invoice.InvoiceTime = DateTime.Now;
            Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Data.Customers.Entities.AccountChargeInvoice));
        }

        public void LoadData(string invoiceID)
        {
            this.Invoice = AccountChargeInvoicesAdapter.Instance.LoadByInvoiceID(invoiceID);
            Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Data.Customers.Entities.AccountChargeInvoice));
        }

    }
}