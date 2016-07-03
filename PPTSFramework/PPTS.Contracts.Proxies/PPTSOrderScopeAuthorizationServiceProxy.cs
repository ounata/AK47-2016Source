using MCS.Library.Configuration;
using MCS.Library.Core;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.Workflow;
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
    public class PPTSOrderScopeAuthorizationServiceProxy : WfClientServiceProxyBase<IOrderScopeAuthorizationService>
    {
        public static readonly PPTSOrderScopeAuthorizationServiceProxy Instance = new PPTSOrderScopeAuthorizationServiceProxy();

        private PPTSOrderScopeAuthorizationServiceProxy()
        {

        }

        public void CourseOrgAuthorizationToSearch(OrderScopeAuthorizationModel model)
        {
            this.SingleCall(action => action.CourseOrgAuthorizationToSearch(model));
        }
        
        public void CourseRelationAuthorizationToSearch(OrderScopeAuthorizationModel model)
        {
            this.SingleCall(action => action.CourseRelationAuthorizationToSearch(model));
        }
        
        public void OwnerRelationAuthorizationToSearch(OrderScopeAuthorizationModel model)
        {
            this.SingleCall(action => action.OwnerRelationAuthorizationToSearch(model));
        }

        public void RecordOrgAuthorizationToSearch(OrderScopeAuthorizationModel model)
        {
            this.SingleCall(action => action.RecordOrgAuthorizationToSearch(model));
        }

        protected override WfClientChannelFactory<IOrderScopeAuthorizationService> GetService()
        {
            EndpointAddress endPoint = new EndpointAddress(UriSettings.GetConfig().GetUrl("pptsServices", "orderScopeAuthorizationService"));

            return new WfClientChannelFactory<IOrderScopeAuthorizationService>(endPoint);
        }
    }
}
