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

namespace PPTS.Services.Orders.Services
{
    public class OrderTransactionService : IOrderTransactionService
    {

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void ResetOrderStatus(string processID, string orderId,int status)
        {
            OrdersAdapter.Instance.Load(orderId).IsNotNull(order => {
                TxProcessExecutor.GetExecutor(processID).PrepareData(tp =>
                {
                    tp.CurrentActivity.Context["orderId"] = orderId;
                    tp.CurrentActivity.Context["status"] = status;
                }).ExecuteMoveTo(tp =>
                {
                    OrdersAdapter.Instance.Update(orderId, new Dictionary<string, object>() { { "ProcessStatus", status } });
                });
                
            });

        }


        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void SyncAsset(string processID, AssetCollection assets)
        {
            TxProcessExecutor.GetExecutor(processID).PrepareData(tp =>
            {
                tp.CurrentActivity.Context["assets"] = assets;
            }).ExecuteMoveTo(tp =>
            {
                var whereSqlBuilder = new MCS.Library.Data.Builder.InSqlClauseBuilder("AssetID");
                whereSqlBuilder.AppendItem(assets.Select(i => i.AssetID).ToArray());
                GenericAssetAdapter<Asset, AssetCollection>.Instance.UpdateCollection(whereSqlBuilder, assets);
            });
            
        }
    }
}
