using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    public class CustomerServiceItemsAdapter : CustomerAdapterBase<CustomerServiceItem, CustomerServiceItemCollection>
    {
        public new static CustomerServiceItemsAdapter Instance = new CustomerServiceItemsAdapter();

        private CustomerServiceItemsAdapter()
        {
        }

        public CustomerServiceItem Load(string ItemID)
        {
            return this.Load(builder => builder.AppendItem("ItemID", ItemID)).SingleOrDefault();
        }


        public void LoadInContext(string ItemID, Action<CustomerServiceItemCollection> action)
        {
            this.LoadInContext(new WhereLoadingCondition(builder => builder.AppendItem("ItemID", ItemID)), action);
        }
    }
}
