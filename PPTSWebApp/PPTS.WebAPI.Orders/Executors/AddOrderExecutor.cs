using MCS.Library.Core;
using MCS.Library.Data.Executors;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Orders.Executors;
using System;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Orders.Adapters;
using PPTS.WebAPI.Orders.ViewModels.Purchase;

namespace PPTS.WebAPI.Orders.Executors
{
    [DataExecutorDescription("提交订单")]
    public class AddOrderExecutor : PPTSEditShoppingCartExecutorBase<SubmitOrderModel>
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

            OrdersAdapter.Instance.UpdateInContext(order);
            OrderItemAdapter.Instance.UpdateByOrderInContext(order, orderItems);
            Model.item.ForEach(m => ShoppingCartAdapter.Instance.DeleteInContext(builder => builder.AppendItem("CartID", m.CartID)));
            
        }

    }
}