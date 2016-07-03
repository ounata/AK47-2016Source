using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MCS.Library.SOA.DataObjects;
using MCS.Library.Principal;
using System.Threading;
using PPTS.WebAPI.Orders.Executors;
using PPTS.WebAPI.Orders.ViewModels.Unsubscribe;
using PPTS.Data.Common;

namespace PPTS.WebAPI.OrdersTests.Executors
{
    [TestClass]
    public class DebookOrderExecutorTest
    {
        public DebookOrderExecutorTest()
        {
            var config = OguObjectSettings.GetConfig();
            var user = config.Objects["campus"].User;
            Thread.CurrentPrincipal = new DeluxePrincipal(new DeluxeIdentity(user));
        }

        [TestMethod]
        public void DebookOrderExecutorTestMethod1()
        {
            var model = new DebookOrderModel()
            {
                Item = new Data.Orders.Entities.DebookOrderItem() { DebookAmount = 1, },
                Order = new Data.Orders.Entities.DebookOrder() { ContactTel = "123123123", DebookMemo = "123123", SubmitTime = DateTime.Now },
                OrderItemID = "1709bbfb-899b-8ca0-4dcd-863b3085f1c7"
            };
            new DebookOrderExecutor(model) { NeedValidation = true }.Execute();

            //return;

            //model.FillOrder()
            //     .FillOrderItem()
            //     .FillAsset()
            //     .PrepareIsApprove();
            //if(!model.IsApprove)
            //{
            //    new DebookOrderExecutor(model) { NeedValidation = true }.Execute();
            //    return;
            //}
            //string wfName = WorkflowNames.Debook_Youxue;
            //WorkflowHelper wfHelper = new WorkflowHelper(wfName, DeluxeIdentity.CurrentUser);
            //if (wfHelper.CheckWorkflow(true))
            //{
            //    MutexLocker mutex = new MutexLocker(
            //            new MutexLockParameter()
            //            {
            //                CustomerID = model.Order.CustomerID,
            //                CustomerCode = model.Order.CustomerCode,
            //                AccountID = model.Item.AccountID,
            //                AccountCode = model.Item.AccountCode,
            //                Action = MutexAction.Debook,
            //                Description = string.Format("学员 {0} ，账号 {1} 退订中", model.Order.CustomerCode, model.Item.AccountCode),
            //                BillID = model.Order.DebookID,
            //                BillNo = model.Order.DebookNo
            //            });
            //    mutex.Lock(
            //            delegate ()
            //            {
            //                new DebookOrderExecutor(model) { NeedValidation = true }.Execute();
            //            },
            //            delegate ()
            //            {

            //                var param = new WorkflowStartupParameter()
            //                {
            //                    ResourceID = model.Order.DebookID,
            //                    TaskTitle = string.Format("{0}({1})的订单 {2} 进行退订", model.Order.CustomerName, model.Order.CustomerCode, model.Order.DebookNo),
            //                    TaskUrl = "/PPTSWebApp/PPTS.Portal/#/ppts/order/unsubscribe/approve"//?processID&activityID&resourceID
            //                };

            //                wfHelper.StartupWorkflow(param);
            //            });
            //    }



            }
    }
}
