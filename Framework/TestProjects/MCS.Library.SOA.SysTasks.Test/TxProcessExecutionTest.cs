using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Services;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.AsyncTransactional;
using MCS.Library.Test.Data.Adapters;
using MCS.Library.Test.Data.Entities;
using MCS.Library.Test.Data.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Threading;
using System.Transactions;

namespace MCS.Library.SOA.SysTasks.Test
{
    [TestClass]
    public class TxProcessExecutionTest
    {
        [TestMethod]
        public void OrderProcessTest()
        {
            ClearData();

            Repertory repertory = OrderDataHelper.PrepareRepertory();
            Order order = OrderDataHelper.PrepareOrder(repertory.ProductID);

            TxProcess process = DataHelper.PrepareOrderProcess(order, repertory);

            RepertoryAdapter.GetInstance(DataHelper.RepertoryDB).Update(repertory);
            OrderAdapter.GetInstance(DataHelper.OrderDB).Update(order);

            StartProcessOrder(order, process);

            ServiceContainer container = new ServiceContainer().StarService();

            try
            {
                CheckProcessCompleted(process.ProcessID, order, TimeSpan.FromSeconds(15));

                Repertory repertoryLoaded = RepertoryAdapter.GetInstance(DataHelper.RepertoryDB).Load(repertory.ProductID);
                Order orderLoaded = OrderAdapter.GetInstance(DataHelper.OrderDB).Load(order.OrderID);

                Assert.IsNotNull(repertoryLoaded);
                Assert.AreEqual(order.Quantity, repertoryLoaded.UsedQuantity);
            }
            finally
            {
                container.StopService();
            }
        }

        [TestMethod]
        public void OrderProcessWithInvalidActivityUrlRollbackTest()
        {
            ClearData();

            Repertory repertory = OrderDataHelper.PrepareRepertory();
            Order order = OrderDataHelper.PrepareOrder(repertory.ProductID);

            TxProcess process = DataHelper.PrepareOrderProcess(order, repertory);
            DataHelper.AddErrorActivity(process);

            RepertoryAdapter.GetInstance(DataHelper.RepertoryDB).Update(repertory);
            OrderAdapter.GetInstance(DataHelper.OrderDB).Update(order);

            StartProcessOrder(order, process);

            ServiceContainer container = new ServiceContainer().StarService();

            try
            {
                CheckProcessRolledBack(process.ProcessID, order, TimeSpan.FromSeconds(15));

                Repertory repertoryLoaded = RepertoryAdapter.GetInstance(DataHelper.RepertoryDB).Load(repertory.ProductID);
                Order orderLoaded = OrderAdapter.GetInstance(DataHelper.OrderDB).Load(order.OrderID);
                Assert.AreEqual(OrderStatus.RolledBack, orderLoaded.Status);

                Assert.IsNotNull(repertoryLoaded);
                Assert.AreEqual(0, repertoryLoaded.UsedQuantity);
            }
            finally
            {
                container.StopService();
            }
        }

        private TxProcess CheckProcessCompleted(string processID, Order order, TimeSpan timeout)
        {
            TxProcess process = null;

            Stopwatch sw = new Stopwatch();

            sw.Start();
            try
            {
                while (sw.Elapsed < timeout)
                {
                    process = TxProcessAdapter.DefaultInstance.Load(processID);

                    if (process != null && process.Status == TxProcessStatus.Completed)
                        break;

                    Thread.Sleep(400);
                }
            }
            finally
            {
                sw.Stop();
            }

            (process != null && process.Status == TxProcessStatus.Completed).FalseThrow("流程{0}在超时时间内没有被执行完成", processID);
            CheckOrderStatus(order, OrderStatus.Normal);

            return process;
        }

        private TxProcess CheckProcessRolledBack(string processID, Order order, TimeSpan timeout)
        {
            TxProcess process = null;

            Stopwatch sw = new Stopwatch();

            sw.Start();
            try
            {
                while (sw.Elapsed < timeout)
                {
                    process = TxProcessAdapter.DefaultInstance.Load(processID);

                    if (process != null && process.Status == TxProcessStatus.RolledBack)
                        break;

                    Thread.Sleep(400);
                }
            }
            finally
            {
                sw.Stop();
            }

            (process != null && process.Status == TxProcessStatus.RolledBack).FalseThrow("流程{0}在超时时间内没有执行回滚完成", processID);
            CheckOrderStatus(order, OrderStatus.RolledBack);

            return process;
        }

        private static void StartProcessOrder(Order order, TxProcess process)
        {
            using (TransactionScope scope = TransactionScopeFactory.Create())
            {
                OrderAdapter.GetInstance(DataHelper.OrderDB).UpdateStatus(order.OrderID, OrderStatus.Processing);

                TxProcessAdapter.GetInstance(DataHelper.OrderDB).Update(process);

                InvokeServiceTaskAdapter.Instance.Push(process.ToStartWorkflowTask());

                scope.Complete();
            }
        }

        private static void CheckOrderStatus(Order order, OrderStatus expectedStatus)
        {
            Stopwatch sw = new Stopwatch();
            Order orderLoaded = null;

            sw.Start();

            while (sw.Elapsed < TimeSpan.FromSeconds(5))
            {
                orderLoaded = OrderAdapter.GetInstance(DataHelper.OrderDB).Load(order.OrderID);

                if (orderLoaded.Status == expectedStatus)
                    break;

                Thread.Sleep(400);
            }

            (orderLoaded != null && orderLoaded.Status == expectedStatus).FalseThrow("Order {0}在超时时间内没有完成状态切换", order.OrderID);
        }

        private static void ClearData()
        {
            TxProcessAdapter.DefaultInstance.ClearAll();
            TxProcessAdapter.GetInstance(DataHelper.OrderDB).ClearAll();
            TxProcessAdapter.GetInstance(DataHelper.RepertoryDB).ClearAll();
            OrderAdapter.GetInstance(DataHelper.OrderDB).ClearAll();
            RepertoryAdapter.GetInstance(DataHelper.RepertoryDB).ClearAll();
            SysAccomplishedTaskAdapter.Instance.ClearAll();
            SysTaskAdapter.Instance.ClearAll();
        }
    }
}
