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
    public class CustomerVisitAdapter : CustomerAdapterBase<CustomerVisit, CustomerVisitCollection>
    {
        public new static CustomerVisitAdapter Instance = new CustomerVisitAdapter();

        private CustomerVisitAdapter()
        {
        }

        public CustomerVisit Load(string ServiceID)
        {
            return this.Load(builder => builder.AppendItem("VisitID", ServiceID)).SingleOrDefault();
        }


        public void LoadInContext(string serviceID, Action<CustomerVisitCollection> action)
        {
            this.LoadInContext(new WhereLoadingCondition(builder => builder.AppendItem("VisitID", serviceID)), action);
        }
    }
}
