using MCS.Library.Configuration;
using MCS.Library.WcfExtensions;
using PPTS.Contracts.Orders.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Proxies
{
    public class PPTSClassServiceProxy : WfClientServiceProxyBase<IClassService>
    {
        public static readonly PPTSClassServiceProxy Instance = new PPTSClassServiceProxy();

        public void SyncClassCountToProduct(string productID)
        {

            this.SingleCall(action => action.SyncClassCountToProduct(productID));
        }

        public void ConfirmClassLesson(DateTime ConfirmTime)
        {

            this.SingleCall(action => action.ConfirmClassLesson(ConfirmTime));
        }

        public void Job_ConfirmClassLesson() {
            this.SingleCall(action => action.Job_ConfirmClassLesson());
        }

        public void Job_InitClassCountToProduct()
        {
            this.SingleCall(action => action.Job_InitClassCountToProduct());
        }

        protected override WfClientChannelFactory<IClassService> GetService()
        {
            EndpointAddress endPoint = new EndpointAddress(UriSettings.GetConfig().GetUrl("pptsServices", "classService"));

            return new WfClientChannelFactory<IClassService>(endPoint);
        }
    }
}
