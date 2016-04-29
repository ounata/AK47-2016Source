using MCS.Library.Caching;
using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPTS.Data.Orders.Entities;
using MCS.Library.Data;


namespace PPTS.Data.Orders.Adapters
{
    public class OrderItemAdapter : OrderAdapterBase<OrderItem, OrderItemCollection>
    {
        public static readonly OrderItemAdapter Instance = new OrderItemAdapter();

        private OrderItemAdapter()
        {
        }

        /// <summary>
        /// 插入操作
        /// </summary>
        /// <param name="orderItem"></param>
        /*
		public void Insert(OrderItem orderItem)
		{
			this.InnerInsert(orderItem, new Dictionary<string, object>());
		}
		*/

        /// <summary>
        /// 加载操作
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        public OrderItem Load(string itemID)
        {
            return this.Load(builder => builder.AppendItem("ItemID", itemID)).SingleOrDefault();
        }

        public OrderItemCollection LoadCollection(IList<string> itemID)
        {
            return this.LoadByInBuilder(new InLoadingCondition(builder => builder.AppendItem(itemID.ToArray<string>()), dataField: "ItemID"));
        }
        public void UpdateByOrderInContext(Order order, OrderItemCollection collection)
        {
            order.NullCheck("order");
            collection.NullCheck("collection");

            Dictionary<string, object> context = new Dictionary<string, object>();
            SqlContextItem sqlContext = this.GetSqlContext();

            for (int i = 0; i < collection.Count; i++)
            {
                var item = collection[i];
                item.OrderID = order.OrderID;
                item.ItemNo = order.OrderNo + (i + 1);
                sqlContext.AppendSqlInContext(TSqlBuilder.Instance, TSqlBuilder.Instance.DBStatementSeperator);

                this.InnerInsertInContext(item, sqlContext, context, StringExtension.EmptyStringArray);
            }
        }
    }
}