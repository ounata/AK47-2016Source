using MCS.Library.Configuration;
using MCS.Library.Core;
using MCS.Library.WcfExtensions;
using PPTS.Contracts.Customers.Models;
using PPTS.Contracts.Customers.Operations;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Proxies
{
    public class PPTSCustomerQueryServiceProxy : PPTSClientServiceProxyBase<ICustomerQueryService>
    {
        public static readonly PPTSCustomerQueryServiceProxy Instance = new PPTSCustomerQueryServiceProxy();

        private PPTSCustomerQueryServiceProxy()
        {
        }

        public Customer QueryCustomerByID(string customerID)
        {
            return this.SingleCall(action => action.QueryCustomerByID(customerID));
        }

        public CustomerTeacherRelationQueryResult QueryCustomerTeacherRelationByCustomerID(CustomerTearcherRelationQueryModel QueryModel)
        {
            return this.SingleCall(action => action.QueryCustomerTeacherRelationByCustomerID(QueryModel));
        }

        public CustomerCollectionQueryResult QueryCustomerCollectionByCustomerIDs(string[] CustomerIDs)
        {
            return this.SingleCall(action => action.QueryCustomerCollectionByCustomerIDs(CustomerIDs));
        }

        protected override WfClientChannelFactory<ICustomerQueryService> GetService()
        {
            EndpointAddress endPoint = new EndpointAddress(UriSettings.GetConfig().GetUrl("pptsServices", "customerQueryService"));

            return new WfClientChannelFactory<ICustomerQueryService>(endPoint);
        }
    }
}
