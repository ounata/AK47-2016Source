using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    public class CustomerTransferResourcesAdapter: CustomerTransferResourcesAdapterBase<Entities.CustomerTransferResource, Entities.CustomerTransferResourceCollection>
    {
        public new static CustomerTransferResourcesAdapter Instance = new CustomerTransferResourcesAdapter();
    }
}
