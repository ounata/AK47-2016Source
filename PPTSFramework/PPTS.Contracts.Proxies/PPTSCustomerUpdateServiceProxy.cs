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
    public class PPTSCustomerUpdateServiceProxy : WfClientServiceProxyBase<ICustomerUpdateService>
    {
        public static readonly PPTSCustomerUpdateServiceProxy Instance = new PPTSCustomerUpdateServiceProxy();

        private PPTSCustomerUpdateServiceProxy()
        {
        }

        public void DeductExpenses(List<CustomerExpenseRelation> expenses)
        {
            this.SingleCall(action => action.DeductExpenses(expenses));
        }

        protected override WfClientChannelFactory<ICustomerUpdateService> GetService()
        {
            EndpointAddress endPoint = new EndpointAddress(UriSettings.GetConfig().GetUrl("pptsServices", "customerQueryService"));

            return new WfClientChannelFactory<ICustomerUpdateService>(endPoint);
        }
    }
}
