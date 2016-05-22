using MCS.Library.Core;
using MCS.Library.Data.Executors;
using PPTS.Data.Orders.Executors;
using System;
using System.Linq;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Orders.Adapters;
using PPTS.WebAPI.Orders.ViewModels.Purchase;
using MCS.Library.Data;

namespace PPTS.WebAPI.Orders.Executors
{
    [DataExecutorDescription("提交订单")]
    public class AddOrderExecutor : PPTSEditPurchaseExecutorBase<SubmitOrderModel>
    {


        public AddOrderExecutor(SubmitOrderModel model) : base(model, null)
        {
            model.NullCheck("model");
            model.Account.NullCheck("Account");
            model.ProductViews.NullCheck("ProductViews");
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);

            var orderItems = Model.ToOrderItemCollection();
            var order = Model.ToOrder();

            if (new int[] { 1, 2 }.Contains(Model.ListType))
            {
                Data.Orders.Adapters.DebookOrderAdapter.Instance.ExistsPendingApprovalInContext(Model.CustomerID);
            }

            OrdersAdapter.Instance.UpdateInContext(order);
            OrderItemAdapter.Instance.UpdateByOrderInContext(order, orderItems);
            Model.item.ForEach(m => ShoppingCartAdapter.Instance.DeleteInContext(builder => builder.AppendItem("CartID", m.CartID)));

            OrdersAdapter.Instance.ExecSuccessInContext();
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