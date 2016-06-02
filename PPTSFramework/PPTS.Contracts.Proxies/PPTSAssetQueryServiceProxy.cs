using MCS.Library.Configuration;
using MCS.Library.WcfExtensions;
using PPTS.Contracts.Orders.Models;
using PPTS.Contracts.Orders.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Proxies
{
    public class PPTSAssetQueryServiceProxy : WfClientServiceProxyBase<IAssetQueryService>
    {
        public static readonly PPTSAssetQueryServiceProxy Instance = new PPTSAssetQueryServiceProxy();

        private PPTSAssetQueryServiceProxy()
        {
        }

        public AssetStatisticQueryResult QueryAssetStatisticByAccountID(string accountID)
        {
            return this.SingleCall(action => action.QueryAssetStatisticByAccountID(accountID));
        }

        public AssetStatisticQueryResult QueryAssetStatisticByCustomerID(string customerID)
        {
            return this.SingleCall(action => action.QueryAssetStatisticByCustomerID(customerID));
        }

        public RefundConsumptionValueQueryResult QueryConsumptionValue(RefundConsumptionValueQueryCriteriaModel criteria)
        {
            return this.SingleCall(action => action.QueryConsumptionValue(criteria));
        }

        public RefundReallowanceMoneyQueryResult QueryReallowanceMoney(RefundReallowanceMoneyQueryCriteriaModel criteria)
        {
            return this.SingleCall(action => action.QueryReallowanceMoney(criteria));
        }


        protected override WfClientChannelFactory<IAssetQueryService> GetService()
        {
            EndpointAddress endPoint = new EndpointAddress(UriSettings.GetConfig().GetUrl("pptsServices", "assetQueryService"));

            return new WfClientChannelFactory<IAssetQueryService>(endPoint);
        }
    }
}
