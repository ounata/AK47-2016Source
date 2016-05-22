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
    public class DeleteClassExecutorTests
    {
        [TestMethod()]
        public void DeleteClassExecutorTest()
        {
            DeleteClassExecutor executor = new DeleteClassExecutor("cd9c0e12-1922-8a3a-4e1e-71dd62d571b5");
            executor.Execute();
        }
    }
}