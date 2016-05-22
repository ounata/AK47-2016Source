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

        /// <summary>
        /// 通过账户获得资产对应价值信息
        /// </summary>
        /// <param name="accountID">账户ID</param>
        /// <returns></returns>
        public AssetStatisticQueryResult QueryAssetStatisticByAccountID(string accountID)
        {
            return this.SingleCall(action => action.QueryAssetStatisticByAccountID(accountID));
        }


        protected override WfClientChannelFactory<IAssetQueryService> GetService()
        {
            EndpointAddress endPoint = new EndpointAddress(UriSettings.GetConfig().GetUrl("pptsServices", "assetQueryService"));

            return new WfClientChannelFactory<IAssetQueryService>(endPoint);
        }
    }
}
