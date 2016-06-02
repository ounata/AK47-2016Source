using MCS.Library.Configuration;
using MCS.Library.WcfExtensions;
using PPTS.Contracts.Customers.Models;
using PPTS.Contracts.Customers.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Proxies
{
    public class PPTSAccountQueryServiceProxy : WfClientServiceProxyBase<IAccountQueryService>
    {
        public static readonly PPTSAccountQueryServiceProxy Instance = new PPTSAccountQueryServiceProxy();

        private PPTSAccountQueryServiceProxy()
        {
        }

        public AccountCollectionQueryResult QueryAccountCollectionByCustomerID(string customerID)
        {
            return this.SingleCall(action => action.QueryAccountCollectionByCustomerID(customerID));
        }

        /// <summary>
        /// 查看所有有账户余额的付款记录集合
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <returns></returns>
        public AccountChargeCollectionQueryResult QueryAccountChargeCollectionByCustomerID(string customerID)
        {
            return this.SingleCall(action => action.QueryAccountChargeCollectionByCustomerID(customerID));
        }

        protected override WfClientChannelFactory<IAccountQueryService> GetService()
        {
            EndpointAddress endPoint = new EndpointAddress(UriSettings.GetConfig().GetUrl("pptsServices", "accountQueryService"));

            return new WfClientChannelFactory<IAccountQueryService>(endPoint);
        }
    }
}
