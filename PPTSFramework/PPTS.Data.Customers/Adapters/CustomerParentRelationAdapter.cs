using MCS.Library.Core;
using PPTS.Data.Customers.Entities;
using System;
using System.Linq;

namespace PPTS.Data.Customers.Adapters
{
    public class CustomerParentRelationAdapter : VersionedCustomerAdapterBase<CustomerParentRelation, CustomerParentRelationCollection>
    {
        public static readonly CustomerParentRelationAdapter Instance = new CustomerParentRelationAdapter();

        private CustomerParentRelationAdapter()
        {
        }

        public CustomerParentRelation Load(string customerID, string parentID)
        {
            customerID.CheckStringIsNullOrEmpty("customerID");
            parentID.CheckStringIsNullOrEmpty("parentID");

            return this.Load(builder => builder.AppendItem("CustomerID", customerID).AppendItem("ParentID", parentID), DateTime.MinValue).FirstOrDefault();
        }
        public CustomerParentRelation LoadPrimary(string customerID)
        {
            customerID.CheckStringIsNullOrEmpty("customerID");

            return this.Load(builder => builder.AppendItem("CustomerID", customerID).AppendItem("IsPrimary", 1), DateTime.MinValue).FirstOrDefault();
        }
        public CustomerParentRelationCollection Load(string customerID)
        {
            customerID.CheckStringIsNullOrEmpty("customerID");

            return this.Load(builder => builder.AppendItem("CustomerID", customerID), DateTime.MinValue);
        }
       
    }
}
