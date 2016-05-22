using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.AsyncTransactional;
using MCS.Library.Test.Data.Adapters;
using MCS.Library.Test.Data.Entities;
using MCS.Library.WcfExtensions;
using MCS.Services.Test.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Transactions;

namespace MCS.Services.Test.Services
{
    public class OrderTransactionService : IOrderTransactionService
    {
        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void SetOrderStatus(string processID, string orderID, OrderStatus status)
        {
            OrderAdapter.GetInstance(Common.OrderDB).Load(orderID).IsNotNull(order =>
            {
                TxProcessExecutor.GetExecutor(processID).
                        PrepareData(process => process.CurrentActivity.Context["OrderStatus"] = order.Status).
                        ExecuteMoveTo(process => OrderAdapter.GetInstance(Common.OrderDB).UpdateStatus(orderID, status));
            });
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void ResetOrderStatus(string orderID, OrderStatus status)
        {
            OrderAdapter.GetInstance(Common.OrderDB).Load(orderID).IsNotNull(order =>
            {
                OrderAdapter.GetInstance(Common.OrderDB).UpdateStatus(orderID, status);
            });
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void RollbackOrderStatus(string processID, string orderID)
        {
            OrderAdapter.GetInstance(Common.OrderDB).Load(orderID).IsNotNull(order =>
            {
                TxProcessExecutor.GetExecutor(processID).ExecuteRollback(process =>
                {
                    OrderStatus oldStatus = process.CurrentActivity.Context.GetValue("OrderStatus", OrderStatus.Normal);

                    OrderAdapter.GetInstance(Common.OrderDB).UpdateStatus(orderID, oldStatus);
                });
            });
        }
    }
}
