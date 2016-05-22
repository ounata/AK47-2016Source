using MCS.Library.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.WebAPI.Orders.DataSources;
using PPTS.WebAPI.Orders.ViewModels.ClassGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Orders.DataSources.Tests
{
    [TestClass()]
    public class ClassGroupDataSourceTests
    {
        [TestMethod()]
        public void LoadClassesTest()
        {
            ClassesQueryCriteriaModel criteria = new ClassesQueryCriteriaModel() {
                PageParams = new PageRequestParams() { PageIndex = 1,PageSize=10,TotalCount=-1 },
                OrderBy= new OrderByRequestItem[] { new OrderByRequestItem() { DataField = "ClassID", SortDirection= FieldSortDirection.Descending } }
            };

            //criteria.ClassName = "PT131228000091-6-北京分公司方庄校区6";//Classes 条件
            //criteria.TeacherName = "测试五";//ClassLessons 条件
            //criteria.CustomerCode = "S131229000129";//ClassLessonItems条件
            criteria.CustomerID = "3886647";

            PagedQueryResult<ClassSearchModel, ClassSearchModelCollection> result = ClassGroupDataSource.Instance.LoadClasses(criteria.PageParams, criteria, criteria.OrderBy);

            Assert.IsTrue(result.TotalCount>0);

        }
    }
}