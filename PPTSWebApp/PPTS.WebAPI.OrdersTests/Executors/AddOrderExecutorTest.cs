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
using MCS.Library.SOA.DataObjects;
using MCS.Library.Principal;

namespace PPTS.WebAPI.OrdersTests.Executors
{
    [TestClass]
    public class AddOrderExecutorTest
    {
        public AddOrderExecutorTest()
        {
            var config = OguObjectSettings.GetConfig();
            var user = config.Objects["campus"].User;
            Thread.CurrentPrincipal = new DeluxePrincipal(new DeluxeIdentity(user));
        }

        [TestMethod]
        public void AddOrderExecutorTestMethod()
        {

            
            var productid = "1115558";
            var cartId = Guid.NewGuid().ToString();
            var customerId = "3990022";

            ShoppingCartAdapter.Instance.Delete(builder => builder.AppendItem("CustomerID", customerId));

            var prepareShoppingCart = new ShoppingCart() { CartID = cartId, Amount = 10, CustomerID = customerId, ProductID = productid, OrderType = 1, ProductCampusID = "18-Org" };
            ShoppingCartAdapter.Instance.Update(prepareShoppingCart);

            var model = new SubmitOrderModel()
            {
                AccountID = "56749601-29ab-a5cf-45ef-57e0d2ca573a",
                ChargeApplyID = "56749601-29ab-a5cf-45ef-57e0d2ca573a",
                CustomerID = customerId,
                ListType = Data.Orders.OrderType.Ordinary,
                SpecialType = "5",
                SpecialMemo="123123123123",
                item = new System.Collections.Generic.List<OrderItemViewModel>() { new OrderItemViewModel() {
                    CartID = cartId, ProductID = productid, OrderAmount = 2, SpecialRate = (decimal)0 } }
            };
            new AddOrderExecutor(model) { NeedValidation = true }.Execute();


            //while (true)
            //{
            //    var status = TxProcessAdapter.DefaultInstance.Load(model.TxProcess.ProcessID).Status;
            //    if (status == TxProcessStatus.Completed) { break; }
            //    Thread.Sleep(1000 * 5);
            //}

            //var order = OrdersAdapter.Instance.Load(model.Order.OrderID);
            //Assert.AreEqual(order.ProcessStatus, (int)ProcessStatusDefine.Processed);

            //var assetIds = string.Join(",", model.Assets.Select(m => "'" + m.AssetID + "'"));
            //var assetCollection = GenericAssetAdapter<Asset, AssetCollection>.Instance.Load(builder => { builder.AppendItem("AssetID", "(" + assetIds + ")", "in", true); }, DateTime.MinValue);

            //Assert.AreEqual(model.Assets.Count, assetCollection.Count);

        }

        [TestMethod]
        public void AddOrderExecutorTestMethod1()
        {
            var productid = "1115466";
            var cartId = Guid.NewGuid().ToString();
            var customerId = "3990022";

            ShoppingCartAdapter.Instance.Delete(builder => builder.AppendItem("CustomerID", customerId));

            var prepareShoppingCart = new ShoppingCart() { CartID = cartId, CustomerID = customerId, ProductID = productid, OrderType =2, ProductCampusID = "18-Org" };
            ShoppingCartAdapter.Instance.Update(prepareShoppingCart);

            var model = new SubmitOrderModel()
            { 
                AccountID = "56749601-29ab-a5cf-45ef-57e0d2ca573a",
                CustomerID = customerId,
                item = new System.Collections.Generic.List<OrderItemViewModel>() {
                    new OrderItemViewModel() { CartID = cartId, ProductID = productid, OrderAmount = 1 }
                },
                ListType = Data.Orders.OrderType.Freebie,
                ChargeApplyID = "56749601-29ab-a5cf-45ef-57e0d2ca573a",
            };

            new AddOrderExecutor(model) { NeedValidation = true }.Execute();


        }

        [TestMethod]
        public void GenericAssetAdapterTest()
        {

            var Assets = new AssetCollection();

            Assets.Add(new Asset() { AssetID = UuidHelper.NewUuidString() });
            Assets.Add(new Asset() { AssetID = UuidHelper.NewUuidString() });

            var whereSqlBuilder = new MCS.Library.Data.Builder.InSqlClauseBuilder("AssetID");
            whereSqlBuilder.AppendItem(Assets.Select(i => i.AssetID).ToArray());
            GenericAssetAdapter<Asset, AssetCollection>.Instance.UpdateCollectionInContext(whereSqlBuilder, Assets);
        }

    }
}
