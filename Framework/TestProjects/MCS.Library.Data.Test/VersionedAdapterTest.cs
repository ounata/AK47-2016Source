using MCS.Library.Core;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Test.Adapters;
using MCS.Library.Data.Test.DataObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;

namespace MCS.Library.Data.Test
{
    [TestClass]
    public class VersionedAdapterTest
    {
        [TestMethod]
        public void UpdateOrderTest()
        {
            VersionedOrderAdapter.Instance.ClearAll();
            DBTimePointActionContext.Current.TimePoint = DateTime.MinValue;

            VersionedOrder order = PrepareOrderData();

            VersionedOrderAdapter.Instance.Update(order);

            VersionedOrder loaded = VersionedOrderAdapter.Instance.LoadByID(order.OrderID);

            Assert.IsNotNull(loaded);

            AreEqual(order, loaded);
        }

        [TestMethod]
        public void DeleteOrderTest()
        {
            VersionedOrderAdapter.Instance.ClearAll();
            DBTimePointActionContext.Current.TimePoint = DateTime.MinValue;

            VersionedOrder order = PrepareOrderData();

            VersionedOrderAdapter.Instance.Update(order);

            VersionedOrder loaded = VersionedOrderAdapter.Instance.LoadByID(order.OrderID);

            Assert.IsNotNull(loaded);

            VersionedOrderAdapter.Instance.Delete(loaded);

            VersionedOrder loadedAgain = VersionedOrderAdapter.Instance.LoadByID(order.OrderID);

            Assert.IsNull(loadedAgain);
        }

        [TestMethod]
        public void DeleteOrderInContextTest()
        {
            VersionedOrderAdapter.Instance.ClearAll();
            DBTimePointActionContext.Current.TimePoint = DateTime.MinValue;

            VersionedOrder order = PrepareOrderData();

            VersionedOrderAdapter.Instance.Update(order);

            VersionedOrder loaded = VersionedOrderAdapter.Instance.LoadByID(order.OrderID);

            Assert.IsNotNull(loaded);

            VersionedOrderAdapter.Instance.DeleteInContext(loaded);

            VersionedOrderAdapter.Instance.GetDbContext().DoAction(context => context.ExecuteTimePointSqlInContext());

            VersionedOrder loadedAgain = VersionedOrderAdapter.Instance.LoadByID(order.OrderID);

            Assert.IsNull(loadedAgain);
        }

        [TestMethod]
        public void UpdateOrderTwiceTest()
        {
            VersionedOrderAdapter.Instance.ClearAll();
            DBTimePointActionContext.Current.TimePoint = DateTime.MinValue;

            VersionedOrder order = PrepareOrderData();

            VersionedOrderAdapter.Instance.Update(order);

            DBTimePointActionContext.Current.TimePoint = DateTime.MinValue;

            VersionedOrder loaded = VersionedOrderAdapter.Instance.LoadByID(order.OrderID);

            Assert.IsNotNull(loaded);

            loaded.OrderName = "Surface Pro 4";
            loaded.Amount = 7360;

            VersionedOrderAdapter.Instance.Update(loaded);

            VersionedOrderCollection orders = VersionedOrderAdapter.Instance.LoadByInBuilder(new Data.Adapters.InLoadingCondition(builder => builder.AppendItem("OrderID", order.OrderID), "OrderID"), DateTime.MinValue);

            Assert.AreEqual(1, orders.Count);

            AreEqual(loaded, orders.SingleOrDefault());
        }

        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void UpdateExpiredOrderTest()
        {
            VersionedOrderAdapter.Instance.ClearAll();
            DBTimePointActionContext.Current.TimePoint = DateTime.MinValue;

            VersionedOrder order = PrepareOrderData();

            VersionedOrderAdapter.Instance.Update(order);

            VersionedOrder loaded = VersionedOrderAdapter.Instance.LoadByID(order.OrderID);

            Assert.IsNotNull(loaded);

            VersionedOrder loadedExpired = VersionedOrderAdapter.Instance.LoadByID(order.OrderID);

            Assert.IsNotNull(loadedExpired);

            loaded.OrderName = "Surface Pro 4";
            loaded.Amount = 7360;

            DBTimePointActionContext.Current.TimePoint = DateTime.MinValue;
            VersionedOrderAdapter.Instance.Update(loaded);

            loadedExpired.OrderName = "Surface Pro 2";
            loadedExpired.Amount = 4100;

            DBTimePointActionContext.Current.TimePoint = DateTime.MinValue;
            VersionedOrderAdapter.Instance.Update(loadedExpired);
        }

        [TestMethod]
        public void UpdateOrderItemsTest()
        {
            VersionedOrderItemAdapter.Instance.ClearAll();

            string orderID = UuidHelper.NewUuidString();

            VersionedOrderItemCollection items = PrepareOrderItems(orderID);

            VersionedOrderItemCollection loaded = UpdateOrderItems(orderID, items);

            AreEqual(items, loaded);
        }

        [TestMethod]
        public void UpdateOrderItemsThenAddTest()
        {
            VersionedOrderItemAdapter.Instance.ClearAll();

            string orderID = UuidHelper.NewUuidString();

            VersionedOrderItemCollection items = PrepareOrderItems(orderID);

            VersionedOrderItemCollection loaded = UpdateOrderItems(orderID, items);

            loaded.Add(new VersionedOrderItem()
            {
                OrderID = orderID,
                ItemID = 3,
                ItemName = "三星Galaxy"
            });

            VersionedOrderItemCollection loadedAgain = UpdateOrderItems(orderID, loaded);

            loadedAgain.Output();

            AreEqual(loaded, loadedAgain);
        }

        [TestMethod]
        public void UpdateOrderItemsThenUpdateTest()
        {
            VersionedOrderItemAdapter.Instance.ClearAll();

            string orderID = UuidHelper.NewUuidString();

            VersionedOrderItemCollection items = PrepareOrderItems(orderID);

            VersionedOrderItemCollection loaded = UpdateOrderItems(orderID, items);

            VersionedOrderItemCollection originalLoaded = VersionedOrderItemAdapter.Instance.LoadByOrderID(orderID, DateTime.MinValue);

            DateTime firstTimePoint = DBTimePointActionContext.Current.TimePoint;

            Console.WriteLine("{0:yyyy-MM-dd HH:mm:ss.fff}", firstTimePoint);

            loaded[1].ItemName = "三星Galaxy";
            loaded[1].ModifierID = UuidHelper.NewUuidString();

            VersionedOrderItemCollection loadedAgain = UpdateOrderItems(orderID, loaded);

            Assert.AreEqual(loaded[1].ModifierID, loadedAgain[1].ModifierID);
            AreEqual(loaded, loadedAgain);

            VersionedOrderItemCollection oldItems = VersionedOrderItemAdapter.Instance.LoadByOrderID(orderID, firstTimePoint);

            AreEqual(originalLoaded, oldItems);
        }

        [TestMethod]
        public void UpdateOrderItemsThenNullUpdateTest()
        {
            VersionedOrderItemAdapter.Instance.ClearAll();

            string orderID = UuidHelper.NewUuidString();

            VersionedOrderItemCollection items = PrepareOrderItems(orderID);

            VersionedOrderItemCollection loaded = UpdateOrderItems(orderID, items);

            VersionedOrderItemCollection originalLoaded = VersionedOrderItemAdapter.Instance.LoadByOrderID(orderID, DateTime.MinValue);

            DateTime firstTimePoint = DBTimePointActionContext.Current.TimePoint;

            Console.WriteLine("{0:yyyy-MM-dd HH:mm:ss.fff}", firstTimePoint);

            //没有更新，仅仅修改了修改人的ID
            loaded[1].ModifierID = UuidHelper.NewUuidString();

            //实际上没有更新
            VersionedOrderItemCollection loadedAgain = UpdateOrderItems(orderID, loaded);

            AreEqual(originalLoaded, loadedAgain);
        }

        [TestMethod]
        public void UpdateOrderItemsThenDeleteTest()
        {
            VersionedOrderItemAdapter.Instance.ClearAll();

            string orderID = UuidHelper.NewUuidString();

            VersionedOrderItemCollection items = PrepareOrderItems(orderID);

            VersionedOrderItemCollection loaded = UpdateOrderItems(orderID, items);

            loaded.RemoveAt(1);

            VersionedOrderItemCollection loadedAgain = UpdateOrderItems(orderID, loaded);

            loadedAgain.Output();

            AreEqual(loaded, loadedAgain);
        }

        [TestMethod]
        public void UpdateTwoOrderWithItemsThenDeleteTest()
        {
            VersionedOrderItemAdapter.Instance.ClearAll();

            string orderID1 = UuidHelper.NewUuidString();

            VersionedOrderItemCollection items1 = PrepareOrderItems(orderID1);

            VersionedOrderItemCollection loaded1 = UpdateOrderItems(orderID1, items1);

            loaded1.RemoveAt(1);

            VersionedOrderItemCollection loadedAgain1 = UpdateOrderItems(orderID1, loaded1);

            AreEqual(loaded1, loadedAgain1);

            string orderID2 = UuidHelper.NewUuidString();

            VersionedOrderItemCollection items2 = PrepareOrderItems(orderID2);

            VersionedOrderItemCollection loaded2 = UpdateOrderItems(orderID2, items2);

            loaded2.RemoveAt(1);

            VersionedOrderItemCollection loadedAgain2 = UpdateOrderItems(orderID2, loaded2);

            AreEqual(loaded2, loadedAgain2);
        }

        [TestMethod]
        public void UpdateOrderAndItemsInContext()
        {
            VersionedOrderAdapter.Instance.ClearAll();
            VersionedOrderItemAdapter.Instance.ClearAll();

            VersionedOrder order = PrepareOrderData();
            VersionedOrderItemCollection items = PrepareOrderItems(order.OrderID);

            VersionedOrderAdapter.Instance.UpdateInContext(order);
            VersionedOrderItemAdapter.Instance.UpdateCollectionInContext(order.OrderID, items);

            Console.WriteLine(VersionedOrderAdapter.Instance.GetSqlContext().GetSqlInContext());

            using (DbContext context = VersionedOrderAdapter.Instance.GetDbContext())
            {
                context.ExecuteTimePointSqlInContext();
            }

            VersionedOrderAdapter.Instance.LoadByIDInContext(order.OrderID, DateTime.MinValue,
                orderLoaded => AreEqual(order, orderLoaded));

            VersionedOrderItemAdapter.Instance.LoadByOrderIDInContext(order.OrderID, DateTime.MinValue,
                itemsLoaded => AreEqual(items, itemsLoaded));

            using (DbContext context = VersionedOrderAdapter.Instance.GetDbContext())
            {
                context.ExecuteDataSetSqlInContext();
            }
        }

        [TestMethod]
        public void UpdateOrderAndItemsThenUpdateInContext()
        {
            VersionedOrderAdapter.Instance.ClearAll();
            VersionedOrderItemAdapter.Instance.ClearAll();

            VersionedOrder order = PrepareOrderData();
            VersionedOrderItemCollection items = PrepareOrderItems(order.OrderID);

            VersionedOrderAdapter.Instance.UpdateInContext(order);
            VersionedOrderItemAdapter.Instance.UpdateCollectionInContext(order.OrderID, items);

            Console.WriteLine(VersionedOrderAdapter.Instance.GetSqlContext().GetSqlInContext());

            using (DbContext context = VersionedOrderAdapter.Instance.GetDbContext())
            {
                context.ExecuteTimePointSqlInContext();
            }

            DateTime firstTimePoint = DBTimePointActionContext.Current.TimePoint;

            Console.WriteLine("{0:yyyy-MM-dd HH:mm:ss.fff}", firstTimePoint);

            VersionedOrderAdapter.Instance.LoadByIDInContext(order.OrderID, DateTime.MinValue,
                orderLoaded => AreEqual(order, orderLoaded));

            VersionedOrderItemAdapter.Instance.LoadByOrderIDInContext(order.OrderID, DateTime.MinValue,
                itemsLoaded =>
                {
                    AreEqual(items, itemsLoaded);

                    VersionedOrderItemCollection itemsLoaded1 = VersionedOrderItemAdapter.Instance.LoadByOrderID(order.OrderID, DateTime.MinValue);

                    DBTimePointActionContext.Current.TimePoint = DateTime.MinValue;

                    string originalItemName = itemsLoaded[1].ItemName;
                    itemsLoaded[1].ItemName = "三星Galaxy";

                    VersionedOrderItemAdapter.Instance.UpdateCollection(order.OrderID, itemsLoaded);

                    VersionedOrderItemCollection itemsLoaded2 = VersionedOrderItemAdapter.Instance.LoadByOrderID(order.OrderID, DateTime.MinValue);

                    AreEqual(itemsLoaded, itemsLoaded2);

                    VersionedOrderItemCollection itemsLoaded3 = VersionedOrderItemAdapter.Instance.LoadByOrderID(order.OrderID, firstTimePoint);

                    AreEqual(itemsLoaded1, itemsLoaded3);
                });

            using (DbContext context = VersionedOrderAdapter.Instance.GetDbContext())
            {
                context.ExecuteDataSetSqlInContext();
            }
        }

        private static VersionedOrder PrepareOrderData()
        {
            VersionedOrder order = new VersionedOrder();

            order.OrderID = UuidHelper.NewUuidString();
            order.OrderName = "Surface Pro 3";
            order.Amount = 6388;

            return order;
        }

        private static VersionedOrderItemCollection UpdateOrderItems(string orderID, VersionedOrderItemCollection items)
        {
            DBTimePointActionContext.Current.TimePoint = DateTime.MinValue;

            VersionedOrderItemAdapter.Instance.UpdateCollection(orderID, items);

            return VersionedOrderItemAdapter.Instance.LoadByOrderID(orderID, DateTime.MinValue);
        }

        private static void AreEqual(VersionedOrder expected, VersionedOrder actual)
        {
            Assert.AreEqual(expected.OrderID, actual.OrderID);
            Assert.AreEqual(expected.OrderName, actual.OrderName);
        }

        private static void AreEqual(VersionedOrderItem expected, VersionedOrderItem actual)
        {
            Assert.AreEqual(expected.OrderID, actual.OrderID);
            Assert.AreEqual(expected.ItemID, actual.ItemID);
            Assert.AreEqual(expected.ItemName, actual.ItemName);
        }

        private static void AreEqual(IList<VersionedOrderItem> expected, IList<VersionedOrderItem> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
                AreEqual(expected[i], actual[i]);
        }

        private static VersionedOrderItemCollection PrepareOrderItems(string oederID)
        {
            VersionedOrderItemCollection items = new VersionedOrderItemCollection();

            items.Add(new VersionedOrderItem()
            {
                OrderID = oederID,
                ItemID = 0,
                ItemName = "IPhone 6"
            });

            items.Add(new VersionedOrderItem()
            {
                OrderID = oederID,
                ItemID = 1,
                ItemName = "华为荣耀6"
            });

            items.Add(new VersionedOrderItem()
            {
                OrderID = oederID,
                ItemID = 2,
                ItemName = "Surface Phone"
            });

            return items;
        }
    }
}
