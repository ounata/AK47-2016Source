using MCS.Library.Core;
using PPTS.Data.Customers.Entities;
using System;
using System.Linq;

namespace PPTS.Data.Customers.Adapters
{
    public class PotentialCustomerParentPhoneAdapter : CustomerAdapterBase<PotentialCustomerParentPhone, PotentialCustomerParentPhoneCollection>
    {
        public static readonly PotentialCustomerParentPhoneAdapter Instance = new PotentialCustomerParentPhoneAdapter();

        private PotentialCustomerParentPhoneAdapter()
        {
        }
        
        public PotentialCustomerParentPhone Load(string customerID)
        {
            customerID.CheckStringIsNullOrEmpty("customerID");

            return this.Load(builder => builder.AppendItem("CustomerID", customerID)).FirstOrDefault();
        }       
    }
}
