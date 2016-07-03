using PPTS.Contracts.Orders.Operations;
using PPTS.Data.Orders.Adapters;
using MCS.Library.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PPTS.Data.Common;
using MCS.Library.WcfExtensions;
using System.ServiceModel.Web;
using PPTS.Data.Orders.Entities;
using MCS.Library.SOA.DataObjects.AsyncTransactional;
using PPTS.Data.Orders;

namespace PPTS.Services.Orders.Services
{
    public class OrderTransactionService : IOrderTransactionService
    {

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void ModifyDebookOrderStatus(string processID, string debookId, OrderStatus orderStatus, ProcessStatusDefine processStatus, string currentUserId, string currentUserName)
        {
            DebookOrderAdapter.Instance.Load(debookId).IsNotNull((model) =>
            {
                TxProcessExecutor.GetExecutor(processID).PrepareData(tp =>
                {
                    tp.CurrentActivity.Context["debookId"] = debookId;
                }).ExecuteMoveTo(tp =>
                {
                    model.DebookStatus = ((int)orderStatus).ToString();
                    model.ProcessStatus = ((int)processStatus).ToString();
                    model.ModifierID = currentUserId;
                    model.ModifierName = currentUserName;

                    DebookOrderAdapter.Instance.Update(model);
                });

            });
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void OrderProcess(string processID, string orderId, ProcessStatusDefine processStatus, Order order, List<OrderItem> items, List<Asset> assets, List<Assign> assigns, List<ClassLessonItem> classLessonItems)
        {
            OrdersAdapter.Instance.Load(order.OrderID).IsNotNull(model =>
            {
                TxProcessExecutor.GetExecutor(processID).PrepareData(tp =>
                {
                    tp.CurrentActivity.Context["order"] = model;
                    tp.CurrentActivity.Context["items"] = items;
                    tp.CurrentActivity.Context["assets"] = assets;
                }).ExecuteMoveTo(tp =>
                {
                    var OrderItems = new OrderItemCollection();
                    items.ForEach(m => { OrderItems.Add(m); });
                    var Assets = new AssetCollection();
                    assets.ForEach(m => { Assets.Add(m); });

                    AssignCollection Assigns = null;
                    assigns.IsNotNull((l) =>
                    {
                        Assigns = new AssignCollection();
                        assigns.ForEach(m => { Assigns.Add(m); });
                    });
                    ClassLessonItemCollection ClassLessonItems = null;
                    classLessonItems.IsNotNull((l) =>
                    {
                        ClassLessonItems = new ClassLessonItemCollection();
                        classLessonItems.ForEach(m => { ClassLessonItems.Add(m); });
                    });


                    new PPTS.Data.Orders.Executors.PPTSyncOrderExecutor("OrderProcess")
                    {
                        Order = order,
                        Items = OrderItems,
                        Assets = Assets,
                        Assigns = Assigns,
                        ClassLessonItems = ClassLessonItems,
                        ProcessStatus = processStatus,
                        OrderID = orderId
                    }.Execute();
                });

            });
        }

    }
}
