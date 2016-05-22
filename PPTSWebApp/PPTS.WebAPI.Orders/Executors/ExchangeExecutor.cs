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
            base.PrepareData(context);

            var order = Model.ToOrder();
            var orderItem = Model.ToOrderItem();

            Data.Orders.Adapters.OrdersAdapter.Instance.UpdateInContext(order);
            Data.Orders.Adapters.OrderItemAdapter.Instance.UpdateInContext(orderItem);
            Data.Orders.Adapters.GenericAssetAdapter<Asset,AssetCollection>.Instance.ExchangeInContext(Model.ToExchangeAsset(), Model.ToAsset());
            

        }
        


    }
}