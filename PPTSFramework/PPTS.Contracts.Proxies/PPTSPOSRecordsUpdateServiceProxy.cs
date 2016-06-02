using MCS.Library.Configuration;
using MCS.Library.WcfExtensions;
using PPTS.Contracts.UnionPay.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Proxies
{
    public class PPTSPOSRecordsUpdateServiceProxy : WfClientServiceProxyBase<IPOSRecordsUdateService>
    {
        public static readonly PPTSPOSRecordsUpdateServiceProxy Instanse = new PPTSPOSRecordsUpdateServiceProxy();

        public void UpdatePOSRecords(string strConfigPath)
        {
            this.SingleCall(action => action.UpdatePOSRecords(strConfigPath));
        }

        protected override WfClientChannelFactory<IPOSRecordsUdateService> GetService()
        {
            EndpointAddress endPoint = new EndpointAddress(UriSettings.GetConfig().GetUrl("pptsServices", "POSRecordsUpdateService"));

            return new WfClientChannelFactory<IPOSRecordsUdateService>(endPoint);
        }
    }
}
