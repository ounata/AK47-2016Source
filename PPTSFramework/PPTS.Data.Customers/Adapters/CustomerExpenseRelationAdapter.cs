using MCS.Library.Core;
using PPTS.Data.Customers.Entities;
using System;
using System.Linq;

namespace PPTS.Data.Customers.Adapters
{
    public class CustomerExpenseRelationAdapter : CustomerAdapterBase<CustomerExpenseRelation, CustomerExpenseRelationCollection>
    {
        public static readonly CustomerExpenseRelationAdapter Instance = new CustomerExpenseRelationAdapter();

        private CustomerExpenseRelationAdapter()
        {
        }
        public CustomerExpenseRelation Load(string customerID, string expenseID)
        {
            return this.Load(builder => builder.AppendItem("CustomerID", customerID).AppendItem("ExpenseID", expenseID)).SingleOrDefault();
        }

        public CustomerExpenseRelationCollection LoadCollection(string customerID)
        {
            return this.Load(builder => builder.AppendItem("CustomerID", customerID));
        }       
    }
}
