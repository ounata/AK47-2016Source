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

namespace PPTS.Data.Orders.Adapters
{
    public class OrdersAdapter : OrderAdapterBase<Order,OrderCollection>
    {
        public static readonly OrdersAdapter Instance = new OrdersAdapter();

        private OrdersAdapter()
        {
        }

        /// <summary>
        /// 插入操作
        /// </summary>
        /// <param name="orders"></param>
        /*
		public void Insert(Order orders)
		{
			this.InnerInsert(order, new Dictionary<string, object>());
		}
		*/

        /// <summary>
        /// 加载操作
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public Order Load(string orderid)
        {
            return this.Load(builder => builder.AppendItem("OrderID", orderid)).SingleOrDefault();
        }

        protected override string GetConnectionName()
        {
            throw new NotImplementedException();
        }
    }
}