using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Products.Entities;

namespace PPTS.Data.Products.Test
{
    [TestClass]
    public class DiscountAdapterTest
    {
        [TestMethod]
        public void LoadByCampusID()
        {
            Discount discount = PPTS.Data.Products.Adapters.DiscountAdapter.Instance.LoadByCampusID("2121");
            Assert.IsTrue(discount != null);
        }
    }
}
