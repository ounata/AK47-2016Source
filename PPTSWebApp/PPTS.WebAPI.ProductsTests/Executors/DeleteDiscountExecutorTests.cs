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
    public class DeleteDiscountExecutorTests
    {
        [TestMethod()]
        public void DeleteDiscountExecutorTest()
        {
            string discountID = "40CDB8BD-6777-4CC8-96B3-336516E35852";
            DeleteDiscountExecutor executor = new DeleteDiscountExecutor(discountID);
            executor.Execute();
        }
    }
}