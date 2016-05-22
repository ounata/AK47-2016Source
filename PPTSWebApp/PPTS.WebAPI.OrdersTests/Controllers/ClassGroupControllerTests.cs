using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Orders.Adapters;
using PPTS.Data.Orders.DataSources;
using PPTS.WebAPI.Orders.Controllers;
using PPTS.WebAPI.Orders.ViewModels.ClassGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Orders.Controllers.Tests
{
    [TestClass()]
    public class ClassGroupControllerTests
    {
        [TestMethod()]
        public void getClassDetailTest()
        {
            ClassLessonQueryCriteriaModel criteria = new ClassLessonQueryCriteriaModel() { ClassID = "238116af-3c2c-b088-43b9-fcc012c1b02f" };

            ClassDetailModel result =  new ClassDetailModel()
            {
                Class = GenericClassGroupAdapter<ClassModel, ClassModelCollection>.Instance.Load(builder => builder.AppendItem("ClassID", criteria.ClassID)).SingleOrDefault(),                
                ClassLessones = new ClassLessonQueryResultModel()
                {
                    QueryResult = GenericOrderDataSource<ClassLessonModel, ClassLessonModelCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy)
                }
            };
        }
    }
}