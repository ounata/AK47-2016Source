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
    public class NumberConventerTests
    {

        [TestMethod()]
        public void ArabToChnTest()
        {
            
            NumberConventer n = new NumberConventer();
            string test =  n.ArabToChn(113, out test);
            Assert.AreEqual("一百一十三", test);
        }

        [TestMethod()]
        public void ChnToArabTest()
        {
            NumberConventer n = new NumberConventer();
            decimal test = n.ChnToArab("二十三");
            Assert.AreEqual(23, test);
        }

        

    }
}