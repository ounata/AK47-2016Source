using MCS.Library.Data.Adapters;
using MCS.Library.Test.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Core;
using MCS.Library.Data.Builder;

namespace MCS.Library.Test.Data.Adapters
{
    public class OrderAdapter : UpdatableAndLoadableAdapterBase<Order, OrderCollection>
    {
        private string _ConnectionName = string.Empty;

        public static readonly OrderAdapter DefaultInstance = new OrderAdapter();

        public static OrderAdapter GetInstance(string connectionName)
        {
            return new OrderAdapter(connectionName);
        }

        private OrderAdapter()
        {
        }

        public OrderAdapter(string connectionName)
        {
            this._ConnectionName = connectionName;
        }

        public Order Load(string orderID)
        {
            orderID.CheckStringIsNullOrEmpty("orderID");

            return this.LoadByInBuilder(new InLoadingCondition(builder => builder.AppendItem(orderID), "OrderID")).SingleOrDefault();
        }

        public void UpdateStatus(string orderID, OrderStatus status)
        {
            orderID.CheckStringIsNullOrEmpty("orderID");

            UpdateSqlClauseBuilder uBuilder = new UpdateSqlClauseBuilder();

            uBuilder.AppendItem("Status", status.ToString());

            WhereSqlClauseBuilder wBuilder = new WhereSqlClauseBuilder();

            wBuilder.AppendItem("OrderID", orderID);

            string sql = string.Format("UPDATE {0} SET {1} WHERE {2}",
                this.GetTableName(),
                uBuilder.ToSqlString(TSqlBuilder.Instance),
                wBuilder.ToSqlString(TSqlBuilder.Instance));

            DbHelper.RunSql(sql, this.GetConnectionName());
        }

        protected override string GetConnectionName()
        {
            string result = "DataAccessTest";

            if (this._ConnectionName.IsNotEmpty())
                result = this._ConnectionName;

            return result;
        }
    }
}
