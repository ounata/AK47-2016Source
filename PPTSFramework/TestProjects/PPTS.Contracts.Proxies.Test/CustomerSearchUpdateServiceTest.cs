using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Contracts.Search.Models;
using MCS.Library.SOA.DataObjects;
using MCS.Library.Core;
using MCS.Library.SOA.DataObjects.Workflow;

namespace PPTS.Contracts.Proxies.Test
{
    [TestClass]
    public class CustomerSearchUpdateServiceTest
    {
        [TestMethod]
        public void UpdateByCustomerInfoTest()
        {
            CustomerSearchUpdateModel model = new CustomerSearchUpdateModel()
            {
                CustomerID = "605917",
                ObjectID = "605917",
                Type = CustomerSearchUpdateType.Customer
            };
            PPTSCustomerSearchUpdateServiceProxy.Instance.UpdateByCustomerInfo(model);
        }

        [TestMethod]
        public void UpdateByCustomerInfoByTaskTest()
        {
            CustomerSearchUpdateModel model = new CustomerSearchUpdateModel()
            {
                CustomerID = "605917",
                ObjectID = "605917",
                Type = CustomerSearchUpdateType.Customer
            };
            PPTSCustomerSearchUpdateServiceProxy.Instance.UpdateByCustomerInfoByTask(model);
        }

        [TestMethod]
        public void UpdateByCustomerInfoByTaskTest2()
        {
            SysAccomplishedTaskAdapter.Instance.ClearAll();
            SysTaskAdapter.Instance.ClearAll();
            CustomerSearchUpdateModel model = new CustomerSearchUpdateModel()
            {
                CustomerID = "605917",
                ObjectID = "605917",
                Type = CustomerSearchUpdateType.Customer
            };
            InvokeServiceTask task = new InvokeServiceTask()
            {
                TaskID = UuidHelper.NewUuidString(),
                TaskTitle = "CustomerSearch更新客户信息任务",
                ResourceID = UuidHelper.NewUuidString()
            };
            task.SvcOperationDefs.Add(PPTSCustomerSearchUpdateServiceProxy.Instance.PrepareCustomerWfServiceOperation(model));
            task.FillData();
            InvokeServiceTaskAdapter.Instance.Push(task);

            InvokeServiceTaskExecutor executor = new InvokeServiceTaskExecutor();

            executor.Execute(task);

            //return WfServiceInvoker.InvokeContext["ReturnValue"] as UserData;
        }
    }
}
