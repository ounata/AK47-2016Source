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
    public class CustomerStaffRelationAdapter : CustomerAdapterBase<CustomerStaffRelation, CustomerStaffRelationCollection>
    {
        public static CustomerStaffRelationAdapter Instance = new CustomerStaffRelationAdapter();

        private CustomerStaffRelationAdapter()
        {
        }

        public CustomerStaffRelation Load(string id)
        {
            return this.Load(builder => builder.AppendItem("ID", id)).SingleOrDefault();
        }
    }
}
