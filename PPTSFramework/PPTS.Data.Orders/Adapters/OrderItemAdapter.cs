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
		public OrderItem Load(string itemid)
		{
            return this.Load(builder => builder.AppendItem("ItemID", itemid)).SingleOrDefault();
		}
        
        protected override string GetConnectionName()
        {
            throw new NotImplementedException();
        }
	}
}