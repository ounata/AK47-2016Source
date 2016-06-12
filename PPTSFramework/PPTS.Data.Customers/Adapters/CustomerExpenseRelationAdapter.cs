using MCS.Library.Core;
using MCS.Library.Expression;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using PPTS.Data.Customers.Entities;
using System;
using System.Linq;
using System.Text;

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

        public CustomerExpenseRelation Load(string orderID)
        {
            return Load(w => w.AppendItem("OrderID", orderID)).FirstOrDefault();
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

        /// <summary>
        /// 同步扣除服务费订单id
        /// </summary>
        /// <param name="collection"></param>
        public void SyncOrderExpenseRelation(CustomerExpenseRelationCollection collection)
        {
            if (collection.Count < 1) return;

            var sbSql = new StringBuilder();
            foreach (var item in collection)
            {
                var whereSqlBuilder = new WhereSqlClauseBuilder();
                whereSqlBuilder.AppendItem("CustomerID", item.CustomerID)
                    .AppendItem("ExpenseType", item.ExpenseType)
                    .AppendItem("AccountID",item.AccountID)
                    .AppendItem("OrderID","null","is",true);

                var updateSqlBuilder = new UpdateSqlClauseBuilder();
                updateSqlBuilder.AppendItem("OrderID", item.OrderID);


                sbSql.AppendFormat("update {0} set {1} where {2}",
                    GetTableName(),
                    updateSqlBuilder.ToSqlString(TSqlBuilder.Instance),
                    whereSqlBuilder.ToSqlString(TSqlBuilder.Instance)
                    );

                sbSql.Append(TSqlBuilder.Instance.DBStatementSeperator);
            }

            GetSqlContext().AppendSqlWithSperatorInContext(TSqlBuilder.Instance, sbSql.ToString());
            GetDbContext().DoAction(context => context.ExecuteNonQuerySqlInContext());

        }

    }
}
