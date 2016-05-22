using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MCS.Library.Test.Data.Helpers;
using MCS.Library.Test.Data.Entities;
using MCS.Library.Test.Data.Adapters;

namespace MCS.Library.Data.Test
{
    [TestClass]
    public class OrderAndRepertoryTest
    {
        [TestMethod]
        public void UpdateOrderTest()
        {
            Repertory repertory = OrderDataHelper.PrepareRepertory();
            Order order = OrderDataHelper.PrepareOrder(repertory.ProductID);

            OrderAdapter.DefaultInstance.Update(order);
            RepertoryAdapter.DefaultInstance.Update(repertory);

            Order orderLoaded = OrderAdapter.DefaultInstance.Load(order.OrderID);
            order.AreEqual(orderLoaded);

            Repertory repertoryLoaded = RepertoryAdapter.DefaultInstance.Load(repertory.ProductID);
            repertory.AreEqual(repertoryLoaded);
        }

        [TestMethod]
        public void UpdateOrderStatusTest()
        {
            Repertory repertory = OrderDataHelper.PrepareRepertory();
            Order order = OrderDataHelper.PrepareOrder(repertory.ProductID);

            OrderAdapter.DefaultInstance.Update(order);
            RepertoryAdapter.DefaultInstance.Update(repertory);

            OrderAdapter.DefaultInstance.UpdateStatus(order.OrderID, OrderStatus.Processing);

            Order orderLoaded = OrderAdapter.DefaultInstance.Load(order.OrderID);

            Assert.AreEqual(OrderStatus.Processing, orderLoaded.Status);
        }

        [TestMethod]
        public void UpdateRepertoryUsedCountTest()
        {
            Repertory repertory = OrderDataHelper.PrepareRepertory();
            Order order = OrderDataHelper.PrepareOrder(repertory.ProductID);

            OrderAdapter.DefaultInstance.Update(order);
            RepertoryAdapter.DefaultInstance.Update(repertory);

            RepertoryAdapter.DefaultInstance.ChangeUsedQuantity(repertory.ProductID, 25);

            Repertory repertoryLoaded = RepertoryAdapter.DefaultInstance.Load(repertory.ProductID);

            Assert.AreEqual(25, repertoryLoaded.UsedQuantity);
        }
    }
}
