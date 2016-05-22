using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Products.Tests
{
    [TestClass()]
    public class HelperTests
    {
        [TestMethod()]
        public void ArabToChnTest()
        {
            Assert.AreEqual("一", Helper.ArabToChn(1));
            Assert.AreEqual("十", Helper.ArabToChn(10));
            Assert.AreEqual("十一", Helper.ArabToChn(11));
            Assert.AreEqual("二十", Helper.ArabToChn(20));
            Assert.AreEqual("二十三", Helper.ArabToChn(23));
        }
    }
}