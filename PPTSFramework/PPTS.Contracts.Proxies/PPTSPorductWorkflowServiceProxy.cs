using MCS.Library.Configuration;
using MCS.Library.WcfExtensions;
using PPTS.Contracts.Products.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Proxies
{
    public class PPTSPorductWorkflowServiceProxy : WfClientServiceProxyBase<IWorkflowService>
    {
        public static readonly PPTSPorductWorkflowServiceProxy Instance = new PPTSPorductWorkflowServiceProxy();

        protected override WfClientChannelFactory<IWorkflowService> GetService()
        {
            EndpointAddress endPoint = new EndpointAddress(UriSettings.GetConfig().GetUrl("pptsServices", "customerWorkflowService"));

            return new WfClientChannelFactory<IWorkflowService>(endPoint);
        }

        //public void DiscountProcessCancelling(string DiscountID)
        //{
        //    this.SingleCall(action => action.DiscountProcessCancelling(DiscountID));
        //}

        //public void DiscountProcessCompleting(string DiscountID)
        //{
        //    this.SingleCall(action => action.DiscountProcessCompleting(DiscountID));
        //}

        //public void PresentProcessCancelling(string PresentID)
        //{
        //    this.SingleCall(action => action.PresentProcessCancelling(PresentID));
        //}

        //public void PresentProcessCompleting(string PresentID)
        //{
        //    this.SingleCall(action => action.PresentProcessCompleting(PresentID));
        //}
    }
}
