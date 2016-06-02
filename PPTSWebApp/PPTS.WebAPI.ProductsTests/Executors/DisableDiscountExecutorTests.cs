using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.WebAPI.Products.Executors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Products.Executors.Tests
{
    [TestClass()]
    public class DisableDiscountExecutorTests
    {
        [TestMethod()]
        public void DisableDiscountExecutorTest()
        {
            string discountID = "6bd37843-617d-a46d-44c6-2e78da44e807";
            DisableDiscountExecutor executor = new DisableDiscountExecutor(discountID);
            executor.Execute();
        }
    }
}