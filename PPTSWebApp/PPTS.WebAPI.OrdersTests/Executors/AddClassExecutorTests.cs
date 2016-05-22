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
    public class AddClassExecutorTests
    {
        [TestMethod()]
        public void AddClassExecutorTest()
        {
            CreatableClassModel model = new CreatableClassModel() {
                StartTime = new DateTime(2016,5,9,10,0,0),
                DayOfWeeks =new List<int>() {1,3,5 },
                ProductID = "1115618"
            };

           
            AddClassExecutor executor = new AddClassExecutor(model);
            executor.Execute();
        }
    }
}