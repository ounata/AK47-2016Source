using MCS.Library.Configuration;
using MCS.Library.WcfExtensions;
using PPTS.Contracts.Orders.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Proxies
{
    public class PPTSFinancialAssignServiceProxy : WfClientServiceProxyBase<IFinancialAssignService>
    {
        public static readonly PPTSFinancialAssignServiceProxy Instance = new PPTSFinancialAssignServiceProxy();

        public void SaveFinancialAssignInfo()
        {
            this.SingleCall(action => action.SaveFinancialAssignInfo());
        }
        public void SaveFinancialAssignInfoByMonth(int statisticalYear,int statisticalMonth)
        {
            this.SingleCall(action => action.SaveFinancialAssignInfo(statisticalYear, statisticalMonth));
        }

        protected override WfClientChannelFactory<IFinancialAssignService> GetService()
        {
            EndpointAddress endPoint = new EndpointAddress(UriSettings.GetConfig().GetUrl("pptsServices", "financialAssignService"));

            return new WfClientChannelFactory<IFinancialAssignService>(endPoint);
        }
    }
}
