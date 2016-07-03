using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Contracts.Customers.Models;
using PPTS.Data.Customers;

namespace PPTS.Contracts.Proxies.Test
{
    [TestClass]
    public class CustomerWorkflowServiceTest
    {
        [TestMethod]
        public void ThawProcessCancellingTest()
        {
            PPTSCustomerWorkflowServiceProxy.Instance.ThawProcessCancelling("605917");
        }

        [TestMethod]
        public void ThawProcessCompletingTest()
        {
            PPTSCustomerWorkflowServiceProxy.Instance.ThawProcessCompleting("605917",ThawReasonType.Other);
        }
    }
}
