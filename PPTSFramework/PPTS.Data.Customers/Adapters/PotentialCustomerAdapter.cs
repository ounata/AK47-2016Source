using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    public class PotentialCustomerAdapter : GenericPotentialCustomerAdapter<PotentialCustomer, PotentialCustomerCollection>
    {
        public new static PotentialCustomerAdapter Instance = new PotentialCustomerAdapter();

        private PotentialCustomerAdapter()
        {
        }
    }
}
