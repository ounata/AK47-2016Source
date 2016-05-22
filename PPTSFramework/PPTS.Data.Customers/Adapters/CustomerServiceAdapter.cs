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
    public class CustomerServiceAdapter : CustomerAdapterBase<CustomerService, CustomerServiceCollection>  
    {
        public new static CustomerServiceAdapter Instance = new CustomerServiceAdapter();

        private CustomerServiceAdapter()
        {
        }

        public CustomerService Load(string ServiceID)
        {
            return this.Load(builder => builder.AppendItem("ServiceID", ServiceID)).SingleOrDefault();
        }


        public void LoadInContext(string serviceID, Action<CustomerServiceCollection> action)
        {
            this.LoadInContext(new WhereLoadingCondition(builder => builder.AppendItem("ServiceID", serviceID)), action);
        }
        
    }
}
