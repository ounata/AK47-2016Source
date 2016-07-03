using MCS.Library.Data.Executors;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Orders.Executors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MCS.Library.SOA.DataObjects;
using PPTS.WebAPI.Orders.ViewModels.Unsubscribe;
using MCS.Library.Core;
using MCS.Library.Data;
using PPTS.Data.Orders.Adapters;
using MCS.Library.Validation;
using System.Transactions;
using MCS.Library.SOA.DataObjects.AsyncTransactional;
using PPTS.Data.Common;
using MCS.Library.Principal;

namespace PPTS.WebAPI.Orders.Executors
{
    [DataExecutorDescription("退订单")]
    public class DebookOrderExecutor : PPTSEditPurchaseExecutorBase<DebookOrderModel>
    {


        public DebookOrderExecutor(DebookOrderModel model) : base(model, null)
        {
            model.NullCheck("model");
        }


        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {

            Model.FillOrder()
                 .FillOrderItem()
                 .FillAsset()
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
                            AccountID = this.Model.Item.AccountID,
                            Action = MutexAction.Debook,
                            Description = string.Format("{0}({1}) ，账号 {2} 退订中 单号{3}",Model.Order.CustomerName, this.Model.Order.CustomerCode, this.Model.Item.AccountCode, Model.Order.DebookNo),
                            BillID = this.Model.Order.DebookID
                        });

            if (Model.IsApprove)
            {

                string wfName = WorkflowNames.Debook_Youxue;
                WorkflowHelper wfHelper = new WorkflowHelper(wfName, DeluxeIdentity.CurrentUser);
                if (wfHelper.CheckWorkflow(true))
                {

                    mutex.Lock(
                        delegate ()
                        {

                            //OrdersAdapter.Instance.ExistsPendingApprovalInContext(debookorder.CustomerID);
                            DebookOrderAdapter.Instance.UpdateInContext(Model.Order);
                            DebookOrderItemAdapter.Instance.AddOrderItemInContext(Model.Order.DebookID, Model.Item);
                            GenericAssetAdapter<Asset, AssetCollection>.Instance.UpdateInContext(Model.Asset);
                            Data.Orders.ConnectionDefine.GetDbContext().DoAction(d => d.ExecuteTimePointSqlInContext());

                        },
                        delegate ()
                        {

                            var param = new WorkflowStartupParameter()
                            {
                                ResourceID = this.Model.Order.DebookID,
                                TaskTitle = string.Format("{0}({1}) 退订订购单", this.Model.Order.CustomerName, this.Model.Order.CustomerCode),
                                TaskUrl = "/PPTSWebApp/PPTS.Portal/#/ppts/order/unsubscribe/approve"//?processID&activityID&resourceID
                            };

                            wfHelper.StartupWorkflow(param);
                        });
                }

            }
            else
            {
                mutex.LockAndRelease(() =>
                {
                    //OrdersAdapter.Instance.ExistsPendingApprovalInContext(debookorder.CustomerID);
                    DebookOrderAdapter.Instance.UpdateInContext(Model.Order);
                    DebookOrderItemAdapter.Instance.AddOrderItemInContext(Model.Order.DebookID, Model.Item);
                    GenericAssetAdapter<Asset, AssetCollection>.Instance.UpdateInContext(Model.Asset);
                    Data.Orders.ConnectionDefine.GetDbContext().DoAction(d => d.ExecuteTimePointSqlInContext());

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