using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.WebAPI.Orders.Executors;
using PPTS.WebAPI.Orders.ViewModels.Purchase;
using PPTS.Data.Orders.Adapters;
using PPTS.Data.Orders.Entities;
using MCS.Library.Core;
using PPTS.Data.Common;
using System.Threading;
using MCS.Library.SOA.DataObjects.AsyncTransactional;

namespace PPTS.WebAPI.OrdersTests.Executors
{
    [TestClass]
    public class AddOrderExecutorTest
    {
        [TestMethod]
        public void AddOrderExecutorTestMethod()
        {


            var productid = "67acf6fe-d77f-4d30-99e3-fcfd6eaa3bbe";
            var cartId = Guid.NewGuid().ToString();
            var customerId = "3990034";

            ShoppingCartAdapter.Instance.Delete(builder => builder.AppendItem("CustomerID", customerId));

            var prepareShoppingCart = new ShoppingCart() { CartID = cartId, Amount = 1, CustomerID = customerId, ProductID = productid, OrderType = 1, ProductCampusID = "8" };
            ShoppingCartAdapter.Instance.Update(prepareShoppingCart);

            var model = new SubmitOrderModel() { CustomerCampusID = "18-Org", AccountID = "241444", CustomerID = customerId, item = new System.Collections.Generic.List<OrderItemViewModel>() { new OrderItemViewModel() { CartID = cartId, ProductID = productid, OrderAmount = 1 } } };
            new AddOrderExecutor(model) { NeedValidation = true }.Execute();
            

            while (true)
            {
                var status = TxProcessAdapter.DefaultInstance.Load(model.TxProcess.ProcessID).Status;
                if (status == TxProcessStatus.Completed) { break; }
                Thread.Sleep(1000 * 5);
            }
              
            var order = OrdersAdapter.Instance.Load(model.Order.OrderID);
            Assert.AreEqual(order.ProcessStatus, (int)ProcessStatusDefine.Processed);

            var assetIds = string.Join(",", model.Assets.Select(m => "'" + m.AssetID + "'"));
            var assetCollection = GenericAssetAdapter<Asset, AssetCollection>.Instance.Load(builder => { builder.AppendItem("AssetID", "(" + assetIds + ")", "in", true); }, DateTime.MinValue);

            Assert.AreEqual(model.Assets.Count, assetCollection.Count);

        }
    }
}
