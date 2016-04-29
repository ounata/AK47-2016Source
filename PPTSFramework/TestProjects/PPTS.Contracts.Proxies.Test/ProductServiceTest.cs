using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Products.Entities;

namespace PPTS.Contracts.Proxies.Test
{
    [TestClass]
    public class ProductServiceTest
    {
        [TestMethod]
        public void QueryProductViewsByIDsTest()
        {
            string[] productids = new string[] { "1104150", "1104152", "1104154" };
            ProductViewCollection _pvc =PPTSProductQueryServiceProxy.Instance.QueryProductViewsByIDs(productids);
            Assert.IsTrue(_pvc.Count > 0);
        }
    }
}
