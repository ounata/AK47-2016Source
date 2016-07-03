using MCS.Library.Configuration;
using MCS.Library.WcfExtensions;
using PPTS.Contracts.Customers.Operations;
using PPTS.Data.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Proxies
{
    public class PPTSCustomerWorkflowServiceProxy : WfClientServiceProxyBase<IWorkflowService>
    {
        public static readonly PPTSCustomerWorkflowServiceProxy Instance = new PPTSCustomerWorkflowServiceProxy();

        public void ThawProcessCancelling(string CustomerID)
        {
            this.SingleCall(action => action.ThawProcessCancelling(CustomerID));
        }

        public void ThawProcessCompleting(string CustomerID, ThawReasonType ThawReasonType)
        {
            this.SingleCall(action => action.ThawProcessCompleting(CustomerID, ThawReasonType));
        }

        protected override WfClientChannelFactory<IWorkflowService> GetService()
        {
            EndpointAddress endPoint = new EndpointAddress(UriSettings.GetConfig().GetUrl("pptsServices", "customerWorkflowService"));

            return new WfClientChannelFactory<IWorkflowService>(endPoint);
        }
    }
}
