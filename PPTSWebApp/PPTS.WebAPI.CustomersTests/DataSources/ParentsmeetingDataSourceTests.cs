using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.DataSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Customers.DataSources.Tests
{
    [TestClass()]
    public class ParentsmeetingDataSourceTests
    {
        CustomerMeetingDataSource pDataSource;

        [TestInitialize]
        public void Init()
        {
            pDataSource = CustomerMeetingDataSource.Instance;
        }
        [TestMethod()]
        public void GetCustomerMeetingsListTest()
        {
            string condition = null;
            var orderByBuilder = new OrderByRequestItem[] { new OrderByRequestItem() { DataField = "MeetingTime" } };
            var param = new PageRequestParams(1, 20);
            var result = pDataSource.GetCustomerMeetingsList(param, condition, orderByBuilder);
            Console.WriteLine(result.TotalCount);
        }
    }
}