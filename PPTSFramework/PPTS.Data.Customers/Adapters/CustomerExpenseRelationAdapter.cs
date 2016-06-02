using MCS.Library.Core;
using MCS.Library.Data.Adapters;
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

        public void DeleteInContext(string customerID, string expenseID)
        {
            DeleteInContext(builder => builder.AppendItem("CustomerID", customerID).AppendItem("ExpenseID", expenseID));
        }
        public void LoadInContext(string customerID, string expenseID,Action<CustomerExpenseRelationCollection> action)
        {
            LoadInContext(new WhereLoadingCondition(builder => builder.AppendItem("CustomerID", customerID).AppendItem("ExpenseID", expenseID)), action);
        }

        public void UpdateCollection(CustomerExpenseRelationCollection collection) {
            collection.ForEach(m => UpdateInContext(m));
            GetDbContext().DoAction(dbContext => dbContext.ExecuteNonQuerySqlInContext());
        }

    }
}
