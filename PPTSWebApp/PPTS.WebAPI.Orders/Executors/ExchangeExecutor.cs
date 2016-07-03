using MCS.Library.Data.Executors;
using PPTS.Data.Orders.Executors;
using PPTS.WebAPI.Orders.ViewModels.Exchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MCS.Library.Core;
using MCS.Library.SOA.DataObjects;
using MCS.Library.Validation;
using PPTS.Data.Orders.Entities;
using MCS.Library.Data;

namespace PPTS.WebAPI.Orders.Executors
{
    /// <summary>
    /// 资产兑换
    /// </summary>
    [DataExecutorDescription("资产兑换")]
    public class ExchangeExecutor : PPTSEditPurchaseExecutorBase<ExchangeOrderModel>
    {
        public ExchangeExecutor(ExchangeOrderModel model) : base(model, null)
        {
            model.NullCheck("model");
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            Model.FillOrder()
                .FillOrderItem();

            base.PrepareData(context);
            
        }

        protected override void DoValidate(ValidationResults validationResults)
        {
            Model.Validate();
            base.DoValidate(validationResults);
        }

        protected override object DoOperation(DataExecutionContext<UserOperationLogCollection> context)
        {

            Data.Orders.Adapters.OrdersAdapter.Instance.UpdateInContext(Model.Order);
            Data.Orders.Adapters.OrderItemAdapter.Instance.UpdateInContext(Model.Item);
            Data.Orders.Adapters.GenericAssetAdapter<Asset, AssetCollection>.Instance.UpdateInContext(Model.ToExchangeAsset());
            Data.Orders.Adapters.GenericAssetAdapter<Asset, AssetCollection>.Instance.UpdateInContext(Model.ToAsset());

            Data.Orders.ConnectionDefine.GetDbContext().DoAction(db => db.ExecuteTimePointSqlInContext());

            return null;
        }

    }
}