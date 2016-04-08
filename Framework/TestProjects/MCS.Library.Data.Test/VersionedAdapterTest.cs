using MCS.Library.Core;
using MCS.Library.Data.Test.Adapters;
using MCS.Library.Data.Test.DataObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace MCS.Library.Data.Test
{
    [TestClass]
    public class VersionedAdapterTest
    {
        [TestMethod]
        public void UpdateOrderTest()
        {
            OrderAdapter.Instance.ClearAll();
            DBTimePointActionContext.Current.TimePoint = DateTime.MinValue;

            VersionedOrder order = PrepareData();

            OrderAdapter.Instance.Update(order);

            VersionedOrder loaded = OrderAdapter.Instance.LoadByID(order.OrderID);

            Assert.IsNotNull(loaded);

            AreEqual(order, loaded);
        }

        [TestMethod]
        public void UpdateOrderTwiceTest()
        {
            OrderAdapter.Instance.ClearAll();
            DBTimePointActionContext.Current.TimePoint = DateTime.MinValue;

            VersionedOrder order = PrepareData();

            OrderAdapter.Instance.Update(order);

            DBTimePointActionContext.Current.TimePoint = DateTime.MinValue;

            VersionedOrder loaded = OrderAdapter.Instance.LoadByID(order.OrderID);

            Assert.IsNotNull(loaded);

            loaded.OrderName = "Surface Pro 4";
            loaded.Amount = 7360;

            OrderAdapter.Instance.Update(loaded);

            VersionedOrderCollection orders = OrderAdapter.Instance.LoadByInBuilder(new Data.Adapters.InLoadingCondition(builder => builder.AppendItem("OrderID", order.OrderID), "OrderID"), DateTime.MinValue);

            Assert.AreEqual(1, orders.Count);

            AreEqual(loaded, orders.SingleOrDefault());
        }

        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void UpdateExpiredOrderTest()
        {
            OrderAdapter.Instance.ClearAll();
            DBTimePointActionContext.Current.TimePoint = DateTime.MinValue;

            VersionedOrder order = PrepareData();

            OrderAdapter.Instance.Update(order);

            VersionedOrder loaded = OrderAdapter.Instance.LoadByID(order.OrderID);

            Assert.IsNotNull(loaded);

            VersionedOrder loadedExpired = OrderAdapter.Instance.LoadByID(order.OrderID);

            Assert.IsNotNull(loadedExpired);

            loaded.OrderName = "Surface Pro 4";
            loaded.Amount = 7360;

            DBTimePointActionContext.Current.TimePoint = DateTime.MinValue;
            OrderAdapter.Instance.Update(loaded);

            loadedExpired.OrderName = "Surface Pro 2";
            loadedExpired.Amount = 4100;

            DBTimePointActionContext.Current.TimePoint = DateTime.MinValue;
            OrderAdapter.Instance.Update(loadedExpired);
        }

        private static VersionedOrder PrepareData()
        {
            VersionedOrder order = new VersionedOrder();

            order.OrderID = UuidHelper.NewUuidString();
            order.OrderName = "Surface Pro 3";
            order.Amount = 6388;

            return order;
        }

        private static void AreEqual(VersionedOrder expected, VersionedOrder actual)
        {
            Assert.AreEqual(expected.OrderID, actual.OrderID);
            Assert.AreEqual(expected.OrderName, actual.OrderName);
        }
    }
}
