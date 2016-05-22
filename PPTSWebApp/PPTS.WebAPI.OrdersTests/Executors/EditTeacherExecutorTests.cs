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
    public class EditTeacherExecutorTests
    {
        [TestMethod()]
        public void EditTeacherExecutorTest()
        {
            EditTeacherModel model = new EditTeacherModel() {
                ClassID = "7c9626e5-fc82-93c1-4f12-07d4864a8dcc",
                StartLessonNum=3,
                EndLessonNum=7,
                TeacherID="测试",
                TeacherCode="测试",
                TeacherName="修改教师测试"

            };
            EditTeacherExecutor executor = new EditTeacherExecutor(model);
            executor.Execute();
        }
    }
}