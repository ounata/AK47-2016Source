using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.WebAPI.Orders.Executors;
using PPTS.WebAPI.Orders.ViewModels.Purchase;
using PPTS.Data.Orders.Adapters;

namespace PPTS.WebAPI.OrdersTests.Executors
{
    /// <summary>
    /// AddAssetConfirmExecutorTest 的摘要说明
    /// </summary>
    [TestClass]
    public class AddAssetConfirmExecutorTests
    {


        [TestMethod]
        public void AddAssetConfirmExecutor()
        {
            var itemid = "c57c735b-69ba-b637-41e6-3c6ecd12f7a2";
            AssetConfirmAdapter.Instance.Delete(b => b.AppendItem("AssetRefID", itemid));
            var model = new AssetConfirmModel() { CustomerID= "3990022", ItemID= itemid, OrderID= "00c2c030-c483-aa60-42c3-05f85eccdb9c", ConfirmedMoney=1 };
            new AddAssetConfirmExecutor(model) { NeedValidation = true }.Execute();
            Assert.IsTrue(AssetConfirmAdapter.Instance.Load(itemid).Count == 1);
        }



    }
}
