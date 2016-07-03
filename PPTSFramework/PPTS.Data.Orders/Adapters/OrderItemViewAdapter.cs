using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using PPTS.Data.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PPTS.Data.Orders.Adapters
{
    public class OrderItemViewAdapter : OrderAdapterBase<OrderItemView, OrderItemViewCollection>
    {
        public static readonly OrderItemViewAdapter Instance = new OrderItemViewAdapter();

        private OrderItemViewAdapter()
        {
        }

        public OrderItemView Load(string itemId)
        {
            return Load(new WhereLoadingCondition(builder => builder.AppendItem("ItemID", itemId))).SingleOrDefault();
        }

        public OrderItemViewCollection LoadCollection(string OperaterCampusID, string CustomerID)
        {
            WhereLoadingCondition wLC = new WhereLoadingCondition(builder => builder
            .AppendItem("Amount", 0, ">")
            .AppendItem("CustomerID", CustomerID));
            //.AppendItem("CustomerCampusID", OperaterCampusID));
            return this.Load(wLC);
        }

        public void LoadInContext(string orderId, string[] campusIds, Action<OrderItemViewCollection> action)
        {

            var where = new WhereSqlClauseBuilder(LogicOperatorDefine.And);
            where.AppendItem("OrderID", orderId);
            if (campusIds != null)
            {
                where.AppendItem("exists", OrdersAdapter.Instance.IsExistsCampusIDSQL(orderId, campusIds), "", true);
            }
            LoadByBuilderInContext(new ConnectiveLoadingCondition(where), action);
        }
    }
}
