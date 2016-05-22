using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Test.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data.Test.Adapters
{
    public class VersionedOrderItemAdapter : VersionedObjectAdapterBase<VersionedOrderItem, VersionedOrderItemCollection>
    {
        public static readonly VersionedOrderItemAdapter Instance = new VersionedOrderItemAdapter();

        private VersionedOrderItemAdapter()
        {
        }

        public VersionedOrderItemCollection LoadByOrderID(string orderID, DateTime timePoint)
        {
            return this.LoadByInBuilder(new InLoadingCondition(builder => builder.AppendItem(orderID),
                builder => builder.AppendItem("ItemID", FieldSortDirection.Ascending), "OrderID"), timePoint);
        }

        public void LoadByOrderIDInContext(string orderID, DateTime timePoint, Action<VersionedOrderItemCollection> action)
        {
            this.LoadByInBuilderInContext(new InLoadingCondition(builder => builder.AppendItem(orderID),
                builder => builder.AppendItem("ItemID", FieldSortDirection.Ascending), "OrderID"), (items) => action(items), timePoint);
        }

        public void UpdateCollection(string orderID, IList<VersionedOrderItem> newItems)
        {
            WhereSqlClauseBuilder keyBuilder = new WhereSqlClauseBuilder();

            keyBuilder.AppendItem("OrderID", orderID);

            base.UpdateCollection(keyBuilder, newItems);
        }

        public void UpdateCollectionInContext(string orderID, IList<VersionedOrderItem> newItems)
        {
            WhereSqlClauseBuilder keyBuilder = new WhereSqlClauseBuilder();

            keyBuilder.AppendItem("OrderID", orderID);

            base.UpdateCollectionInContext(keyBuilder, newItems);
        }

        protected override string GetConnectionName()
        {
            return Common.ConnectionName;
        }
    }
}
