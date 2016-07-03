using MCS.Library.Configuration;
using MCS.Library.Core;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.Workflow;
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
    public class PPTSCustomerScopeAuthorizationServiceProxy : WfClientServiceProxyBase<ICustomerScopeAuthorizationService>
    {
        public static readonly PPTSCustomerScopeAuthorizationServiceProxy Instance = new PPTSCustomerScopeAuthorizationServiceProxy();

        private PPTSCustomerScopeAuthorizationServiceProxy()
        {
        }

        public void CustomerOrgAuthorizationToOrder(CustomerScopeAuthorizationModel model)
        {
            this.SingleCall(action => action.CustomerOrgAuthorizationToOrder(model));
        }



        public void CustomerOrgAuthorizationToSearch(CustomerScopeAuthorizationModel model)
        {
            this.SingleCall(action => action.CustomerOrgAuthorizationToSearch(model));
        }


        public void CustomerRelationAuthorizationToOrder(CustomerScopeAuthorizationModel model)
        {
            this.SingleCall(action => action.CustomerRelationAuthorizationToOrder(model));
        }


        public void CustomerRelationAuthorizationToSearch(CustomerScopeAuthorizationModel model)
        {
            this.SingleCall(action => action.CustomerRelationAuthorizationToSearch(model));
        }


        public void OwnerRelationAuthorizationToSearch(CustomerScopeAuthorizationModel model)
        {
            this.SingleCall(action => action.OwnerRelationAuthorizationToSearch(model));
        }


        public void RecordOrgAuthorizationToSearch(CustomerScopeAuthorizationModel model)
        {
            this.SingleCall(action => action.RecordOrgAuthorizationToSearch(model));
        }

        protected override WfClientChannelFactory<ICustomerScopeAuthorizationService> GetService()
        {
            EndpointAddress endPoint = new EndpointAddress(UriSettings.GetConfig().GetUrl("pptsServices", "customerScopeAuthorizationService"));

            return new WfClientChannelFactory<ICustomerScopeAuthorizationService>(endPoint);
        }
    }
}
