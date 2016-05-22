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
    public class CustomerClassesDataSourceTests
    {
        [TestMethod()]
        public void CustomerClassesDataSourceTest()
        {
            CustomerClassQueryCriteriaModel criteria = new CustomerClassQueryCriteriaModel() { CustomerID= "446",
                OrderBy = new OrderByRequestItem[] { new OrderByRequestItem() { DataField = "c.createTime", SortDirection = FieldSortDirection.Descending } },
                PageParams = new PageRequestParams() { PageIndex = 0, PageSize=10, TotalCount=0 }
            };
            var result = CustomerClassesDataSource.Instance.Load(criteria.PageParams, criteria, criteria.OrderBy);
        }
    }
}