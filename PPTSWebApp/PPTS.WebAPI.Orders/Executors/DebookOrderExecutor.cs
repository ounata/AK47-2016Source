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
                 .FillUser();

            var debookorder = Model.Order;
            var debookorderitem = Model.Item;

            debookorder.DebookID = debookorderitem.DebookID = UuidHelper.NewUuidString();

            OrdersAdapter.Instance.ExistsPendingApprovalInContext(debookorder.CustomerID);
            DebookOrderAdapter.Instance.UpdateInContext(debookorder);
            DebookOrderItemAdapter.Instance.AddOrderItemInContext(debookorder.DebookID,debookorderitem);
            
            GenericAssetAdapter<Asset, AssetCollection>.Instance.UpdateInContext(Model.ToAsset());

            base.PrepareData(context);

        }

        protected override void DoValidate(ValidationResults validationResults)
        {
            Model.Validate();
            base.DoValidate(validationResults);
        }



    }
}