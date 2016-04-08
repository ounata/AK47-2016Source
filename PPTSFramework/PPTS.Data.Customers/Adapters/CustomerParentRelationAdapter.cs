using System.Linq;
using MCS.Library.Core;
using PPTS.Data.Customers.Entities;

namespace PPTS.Data.Customers.Adapters
{
    public class CustomerParentRelationAdapter : CustomerAdapterBase<CustomerParentRelation, CustomerParentRelationCollection>
    {
        public static readonly CustomerParentRelationAdapter Instance = new CustomerParentRelationAdapter();

        private CustomerParentRelationAdapter()
        {
        }

        public CustomerParentRelation Load(string customerID, string parentID)
        {
            customerID.CheckStringIsNullOrEmpty("customerID");
            parentID.CheckStringIsNullOrEmpty("parentID");

            return this.Load(builder => builder.AppendItem("CustomerID", customerID).AppendItem("ParentID", parentID)).SingleOrDefault();
        }
    }
}
