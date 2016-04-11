﻿using MCS.Library.Data.Adapters;
using MCS.Library.Data.Test.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data.Test.Adapters
{
    public class OrderAdapter : VersionedObjectAdapterBase<VersionedOrder, VersionedOrderCollection>
    {
        public static readonly OrderAdapter Instance = new OrderAdapter();

        private OrderAdapter()
        {
        }

        public VersionedOrder LoadByID(string orderID)
        {
            return OrderAdapter.Instance.LoadByInBuilder(
                new InLoadingCondition(builder => builder.AppendItem(orderID), "OrderID"), DateTime.MinValue).SingleOrDefault();
        }

        public void LoadByIDInContext(string orderID, DateTime timePoint, Action<VersionedOrder> action)
        {
            this.LoadByInBuilderInContext(new InLoadingCondition(builder => builder.AppendItem(orderID), "OrderID"), orders => action(orders.SingleOrDefault()), timePoint);
        }

        protected override string GetConnectionName()
        {
            return Common.ConnectionName;
        }
    }
}
