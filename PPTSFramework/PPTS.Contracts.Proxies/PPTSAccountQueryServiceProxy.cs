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
    public class PPTSAccountQueryServiceProxy : PPTSClientServiceProxyBase<IAccountQueryService>
    {
        public static readonly PPTSAccountQueryServiceProxy Instance = new PPTSAccountQueryServiceProxy();

        private PPTSAccountQueryServiceProxy()
        {
        }

        public AccountCollectionQueryResult QueryCustomerTeacherRelationByCustomerID(string CustomerID)
        {
            return this.SingleCall(action => action.QueryAccountCollectionByCustomerID(CustomerID));
        }

        protected override WfClientChannelFactory<IAccountQueryService> GetService()
        {
            EndpointAddress endPoint = new EndpointAddress(UriSettings.GetConfig().GetUrl("pptsServices", "accountQueryService"));

            return new WfClientChannelFactory<IAccountQueryService>(endPoint);
        }
    }
}
