using MCS.Library.Data.Executors;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Orders.Executors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MCS.Library.SOA.DataObjects;
using PPTS.WebAPI.Orders.ViewModels.Unsubscribe;
using MCS.Library.Data;
using PPTS.Data.Orders.Adapters;

namespace PPTS.WebAPI.Orders.Executors
{
    [DataExecutorDescription("退订单")]
    public class DebookOrderExecutor : PPTSEditPurchaseExecutorBase<DebookOrderModel>
    {
        

        public DebookOrderExecutor(DebookOrderModel model) : base(model, null)
        {
        }
        

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);

            Model.FillOrder()
                 .FillOrderItem();

            var debookorder = Model.Order;
            var debookorderitem = Model.Item;
            debookorder.DebookID = debookorderitem.DebookID = Guid.NewGuid().ToString();

            //OrdersAdapter.Instance.ExistsPendingApprovalInContext(debookorder.CustomerID);
            DebookOrderAdapter.Instance.UpdateInContext(debookorder);
            DebookOrderItemAdapter.Instance.AddOrderItemInContext(debookorder.DebookID,debookorderitem);
            DebookOrderAdapter.Instance.ReturnSuccessResultInContext();
        }

        protected override object DoOperation(DataExecutionContext<UserOperationLogCollection> context)
        {
            using (DbContext dbContext = PPTS.Data.Orders.ConnectionDefine.GetDbContext())
            {
                return dbContext.ExecuteScalarSqlInContext();
            }
        }


    }
}