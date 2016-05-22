using MCS.Library.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Orders.Entities;
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
    public class ClassCustomerDataSourcesTests
    {
        [TestMethod()]
        public void LoadTest()
        {
            ClassCustomerQueryCriteriaModel criteria = new ClassCustomerQueryCriteriaModel()
            {
                PageParams = new PageRequestParams() { PageIndex = 1, PageSize = 10, TotalCount = -1 },
                OrderBy = new OrderByRequestItem[] { new OrderByRequestItem() { DataField = "CustomerID", SortDirection = FieldSortDirection.Descending } }
            };

            criteria.ClassID = "238116af-3c2c-b088-43b9-fcc012c1b02f";

            PagedQueryResult<ClassLessonItem, ClassLessonItemCollection> result = ClassCustomerDataSource.Instance.Load(criteria.PageParams, criteria, criteria.OrderBy);

            Assert.IsTrue(result.TotalCount > 0);
        }
    }
}