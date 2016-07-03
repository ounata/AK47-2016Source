using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    public class AccountChargeInvoicesAdapter : CustomerAdapterBase<AccountChargeInvoice, AccountChargeInvoiceCollection>
    {
        public static readonly AccountChargeInvoicesAdapter Instance = new AccountChargeInvoicesAdapter();
        private AccountChargeInvoicesAdapter()
        {
        }

        public AccountChargeInvoice LoadByInvoiceID(string invoiceID)
        {
            return this.Load(builder => builder.AppendItem("InvoiceID", invoiceID)).SingleOrDefault();
        }

    }
}
