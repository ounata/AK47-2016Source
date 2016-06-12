using MCS.Library.Core;
using PPTS.Data.Customers.Entities;
using System;
using System.Linq;

namespace PPTS.Data.Customers.Adapters
{
    public class CustomerParentPhoneAdapter : CustomerAdapterBase<CustomerParentPhone, CustomerParentPhoneCollection>
    {
        public static readonly CustomerParentPhoneAdapter Instance = new CustomerParentPhoneAdapter();

        private CustomerParentPhoneAdapter()
        {
        }
        
        public CustomerParentPhone Load(string customerID)
        {
            customerID.CheckStringIsNullOrEmpty("customerID");

            return this.Load(builder => builder.AppendItem("CustomerID", customerID)).FirstOrDefault();
        }       
    }
}
