using MCS.Library.Configuration;
using MCS.Library.Core;
using MCS.Library.SOA.DataObjects.AsyncTransactional;
using MCS.Library.Test.Data.Adapters;
using MCS.Library.Test.Data.Entities;
using MCS.Services.Test.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.SOA.SysTasks.Test
{
    internal static class DataHelper
    {
        public const string BizConnectionName = "DataAccessTest";
        public const string OrderDB = "OrderDB";
        public const string RepertoryDB = "RepertoryDB";

        public static TxProcess PrepareProcess()
        {
            TxProcess process = new TxProcess();

            process.Category = "Test";
            process.ConnectionName = TxProcessAdapter.DefaultInstance.ConnectionName;
            process.ResourceID = UuidHelper.NewUuidString();

            process.Activities.AddActivity("扣减库存");
            process.Activities.AddActivity("改变状态");

            return process;
        }

        public static TxProcess PrepareOrderProcess(Order order, Repertory repertory)
        {
            TxProcess process = new TxProcess();

            process.ProcessName = "从订单到库存";
            process.Category = "Test";
            process.ConnectionName = RepertoryDB;
            process.ResourceID = UuidHelper.NewUuidString();

            process.AddCompensationService<IOrderTransactionService>(UriSettings.GetConfig().CheckAndGet("mcs.soa.systask", "orderService").ToString(),
                proxy => proxy.ResetOrderStatus(order.OrderID, OrderStatus.RolledBack));

            TxActivity activity1 = process.Activities.AddActivity("扣减库存", DataHelper.RepertoryDB);

            activity1.AddActionService<IRepertoryTransactionService>(
                UriSettings.GetConfig().CheckAndGet("mcs.soa.systask", "repertoryService").ToString(),
                proxy => proxy.ChangeUsedQuantity(process.ProcessID, repertory.ProductID, order.Quantity));

            activity1.AddCompensationService<IRepertoryTransactionService>(
                UriSettings.GetConfig().CheckAndGet("mcs.soa.systask", "repertoryService").ToString(),
                proxy => proxy.RollbackUsedQuantity(process.ProcessID, repertory.ProductID));

            TxActivity activity2 = process.Activities.AddActivity("改变订单状态", DataHelper.OrderDB);

            activity2.AddActionService<IOrderTransactionService>(
                UriSettings.GetConfig().CheckAndGet("mcs.soa.systask", "orderService").ToString(),
                proxy => proxy.SetOrderStatus(process.ProcessID, order.OrderID, OrderStatus.Normal));

            activity2.AddCompensationService<IOrderTransactionService>(
                UriSettings.GetConfig().CheckAndGet("mcs.soa.systask", "orderService").ToString(),
                proxy => proxy.RollbackOrderStatus(process.ProcessID, order.OrderID));

            return process;
        }

        /// <summary>
        /// 增加一个异常的方法
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        public static TxProcess AddErrorActivity(TxProcess process)
        {
            process.Activities.AddActivity("异常活动").AddActionService(
                UriSettings.GetConfig().CheckAndGet("mcs.soa.systask", "repertoryService").ToString(),
                "InvalidAction");

            return process;
        }
    }
}
