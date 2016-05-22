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
    public class EditClassLessonesExecutorTests
    {
        [TestMethod()]
        public void EditClassLessonesExecutorTest()
        {
            EditClassLessonesModel model = new EditClassLessonesModel() {
                ClassID = "7c9626e5-fc82-93c1-4f12-07d4864a8dcc",
                LessonID= "788b0e90-de66-9dca-480c-b6a7f2fd44e3",
                DayOfWeeks = new List<int>() { 2,4,6},
                StartTime = new DateTime(2016,5,15)
            };
            EditClassLessonesExecutor executor = new EditClassLessonesExecutor(model);
            executor.Execute();
        }
    }
}