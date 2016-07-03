using MCS.Library.Configuration;
using MCS.Library.WcfExtensions;
using PPTS.Contracts.Customers.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Proxies
{
    public class PPTSFinancialChargeServiceProxy : WfClientServiceProxyBase<IFinancialChargeService>
    {
        public static readonly PPTSFinancialChargeServiceProxy Instance = new PPTSFinancialChargeServiceProxy();
        public void SendFinancialIncome()
        {
            this.SingleCall(action => action.SendFinancialIncome());
        }
        public void SendFinancialRefound()
        {
            this.SingleCall(action => action.SendFinancialRefound());
        }
        protected override WfClientChannelFactory<IFinancialChargeService> GetService()
        {
            EndpointAddress endPoint = new EndpointAddress(UriSettings.GetConfig().GetUrl("pptsServices", "financialChargeService"));
            return new WfClientChannelFactory<IFinancialChargeService>(endPoint);
        }
    }
}
