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
using PPTS.Data.Common;
using System.Collections.Generic;

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
                 //.FillAssets()
                 //.FillAssignAndClassLessonItem()
                 .PrepareIsApprove();

            base.PrepareData(context);
        }

        protected override void DoValidate(ValidationResults validationResults)
        {
            Model.Validate();
            base.DoValidate(validationResults);
        }

        protected override object DoOperation(DataExecutionContext<UserOperationLogCollection> context)
        {
            var mutex = new MutexLocker(
                        new MutexLockParameter()
                        {
                            CustomerID = this.Model.Order.CustomerID,
                            AccountID = this.Model.Order.AccountID,
                            Action = MutexAction.Order,
                            Description = string.Format("{0}({1}) ，账号 {2} 订购中 单号{3} ",Model.Order.CustomerName, this.Model.Order.CustomerCode, this.Model.Order.AccountCode, Model.Order.OrderNo),
                            BillID = this.Model.Order.OrderID
                        });

            if (Model.IsApprove)
            {
                string wfName = WorkflowNames.OrderExtraDiscount;
                WorkflowHelper wfHelper = new WorkflowHelper(wfName, DeluxeIdentity.CurrentUser);
                if (wfHelper.CheckWorkflow(true))
                {

                    mutex.Lock(
                        delegate ()
                        {
                            //执行订购

                            OrdersAdapter.Instance.UpdateInContext(Model.Order);
                            OrderItemAdapter.Instance.UpdateByOrderInContext(Model.Order, Model.OrderItems);
                            Model.item.ForEach(m => ShoppingCartAdapter.Instance.DeleteInContext(builder => builder.AppendItem("CartID", m.CartID)));

                            base.DoOperation(context);
                        },
                        delegate ()
                        {

                            var param = new WorkflowStartupParameter()
                            {
                                ResourceID = this.Model.Order.OrderID,
                                TaskTitle = string.Format("{0}({1}) 申请特殊订购", this.Model.Order.CustomerName, this.Model.Order.CustomerCode),
                                TaskUrl = "/PPTSWebApp/PPTS.Portal/#/ppts/order/purchase/approve"//?processID&activityID&resourceID
                            };

                            var dictionary = new Dictionary<string, object>();
                            dictionary.Add("DiscountRate", Model.OrderItems.Min(m=>m.DiscountRate)*100);
                            dictionary.Add("IsYouxue", Model.OrderItems.Any(m=>m.CategoryType == ((int)CategoryType.YouXue).ToString()));

                            param.Parameters = dictionary;

                            wfHelper.StartupWorkflow(param);
                        });
                }
            }
            else
            {

                mutex.LockAndRelease(
                    delegate ()
                    {
                        OrdersAdapter.Instance.UpdateInContext(Model.Order);
                        OrderItemAdapter.Instance.UpdateByOrderInContext(Model.Order, Model.OrderItems);
                        Model.item.ForEach(m => ShoppingCartAdapter.Instance.DeleteInContext(builder => builder.AppendItem("CartID", m.CartID)));

                        base.DoOperation(context);

                        Model.FillAssets()
                         .FillAssignAndClassLessonItem();

                        using (TransactionScope scope = TransactionScopeFactory.Create())
                        {

                            TxProcess process = Model.PrepareProcess();
                            TxProcessAdapter.GetInstance(MCS.Library.SOA.DataObjects.ConnectionDefine.DBConnectionName).Update(process);
                            InvokeServiceTaskAdapter.Instance.Push(process.ToStartWorkflowTask());

                            scope.Complete();
                        }
                    });
            }

            return null;
        }


    }
}