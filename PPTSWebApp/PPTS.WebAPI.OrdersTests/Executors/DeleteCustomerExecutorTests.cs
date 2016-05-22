using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.WebAPI.Orders.Executors;
using PPTS.WebAPI.Orders.ViewModels.ClassGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Orders.Executors.Tests
{
    [TestClass()]
    public class DeleteCustomerExecutorTests
    {
        [TestMethod()]
        public void DeleteCustomerExecutorTest()
        {
            DeleteCustomerModel model = new DeleteCustomerModel()
            {
                CustomerIDs = new string[] { "3886647" },
                ClassID = "238116af-3c2c-b088-43b9-fcc012c1b02f"
            };
            DeleteCustomerExecutor executor = new DeleteCustomerExecutor(model);
            executor.Execute();
        }
    }
}