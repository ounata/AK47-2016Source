using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Orders.Adapters;

namespace PPTS.Data.Orders.Test
{
    [TestClass]
    public class AssetAdapterTest
    {
        [TestMethod]
        public void LoadAssetsValueByAccountIDTest()
        {
            decimal des = AssetAdapter.Instance.LoadAssetsValueByAccountID("145291");
            Assert.IsNotNull(des);
            Console.WriteLine("des:{0}", des);
        }
    }
}
