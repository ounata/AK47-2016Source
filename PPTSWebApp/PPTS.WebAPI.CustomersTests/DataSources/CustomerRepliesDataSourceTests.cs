using MCS.Library.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.WebAPI.Customers.DataSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Customers.DataSources.Tests
{
    [TestClass()]
    public class CustomerRepliesDataSourceTests
    {
        CustomerRepliesDataSource cDataSource;

        [TestInitialize]
        public void Init()
        {
            cDataSource = CustomerRepliesDataSource.Instance;
        }
        [TestMethod()]
        public void GetCustomerRepliesListTest()
        {
            string condition = null;
            var orderByBuilder = new OrderByRequestItem[] { new OrderByRequestItem() { DataField = "ReplyTime" } };
            var param = new PageRequestParams(1, 20);
            var result = cDataSource.GetCustomerRepliesList(param, condition, orderByBuilder);
            Console.WriteLine(result.TotalCount);
        }
    }
}