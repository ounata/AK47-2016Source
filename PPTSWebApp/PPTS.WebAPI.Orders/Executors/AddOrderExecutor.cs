using MCS.Library.Core;
using MCS.Library.Data.Executors;
using PPTS.Data.Orders.Executors;
using System;
using System.Linq;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Orders.Adapters;
using PPTS.WebAPI.Orders.ViewModels.Purchase;
using MCS.Library.Data;
using MCS.Library.Validation;
using System.Transactions;
using MCS.Library.SOA.DataObjects.AsyncTransactional;
using PPTS.Data.Common.Security;
using MCS.Library.Principal;

namespace PPTS.WebAPI.Orders.Executors
{
    [DataExecutorDescription("提交订单")]
    public class AddOrderExecutor : PPTSEditPurchaseExecutorBase<SubmitOrderModel>
    {


        public AddOrderExecutor(SubmitOrderModel model) : base(model, null)
        {
            model.NullCheck("model");
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            Model.FillOrder()
                 .FillOrderItemCollection()
                 .FillAssets();
            
            if (new int[] { 1, 2 }.Contains(Model.ListType))
            {
                DebookOrderAdapter.Instance.ExistsPendingApprovalInContext(Model.CustomerID);
            }
            
            OrdersAdapter.Instance.UpdateInContext(Model.Order);
            OrderItemAdapter.Instance.UpdateByOrderInContext(Model.Order, Model.OrderItems);
            Model.item.ForEach(m => ShoppingCartAdapter.Instance.DeleteInContext(builder => builder.AppendItem("CartID", m.CartID)));

            base.PrepareData(context);
        }

        protected override void DoValidate(ValidationResults validationResults)
        {
            Model.Validate();
            base.DoValidate(validationResults);
        }

        protected override object DoOperation(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.DoOperation(context);

            using (TransactionScope scope = TransactionScopeFactory.Create())
            {

                TxProcess process = Model.PrepareProcess();
                TxProcessAdapter.GetInstance(MCS.Library.SOA.DataObjects.ConnectionDefine.DBConnectionName).Update(process);
                InvokeServiceTaskAdapter.Instance.Push(process.ToStartWorkflowTask());
                
                scope.Complete();
            }

            return null;



        }

    }
}