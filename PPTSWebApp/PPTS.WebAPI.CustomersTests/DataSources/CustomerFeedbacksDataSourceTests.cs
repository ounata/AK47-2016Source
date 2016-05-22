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
    public class CustomerFeedbacksDataSourceTests
    {

        CustomerFeedbacksDataSource pDataSource;

        [TestInitialize]
        public void Init()
        {
            pDataSource = CustomerFeedbacksDataSource.Instance;
        }
        [TestMethod()]
        public void GetCustomerFeedbackListTest()
        {
            string condition = null;
            var orderByBuilder = new OrderByRequestItem[] { new OrderByRequestItem() { DataField = "ReplyTime" } };
            var param = new PageRequestParams(1, 20);
            var result = pDataSource.GetCustomerFeedbackList(param, condition, orderByBuilder);
            Console.WriteLine(result.TotalCount);
        }
    }
}