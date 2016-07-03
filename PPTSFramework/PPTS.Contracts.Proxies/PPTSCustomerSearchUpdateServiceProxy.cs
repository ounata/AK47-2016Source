using MCS.Library.Configuration;
using MCS.Library.Core;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.Workflow;
using MCS.Library.WcfExtensions;
using PPTS.Contracts.Search.Models;
using PPTS.Contracts.Search.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Proxies
{
    public class PPTSCustomerSearchUpdateServiceProxy : WfClientServiceProxyBase<ICustomerSearchUpdateService>
    {
        public static readonly PPTSCustomerSearchUpdateServiceProxy Instance = new PPTSCustomerSearchUpdateServiceProxy();

        private PPTSCustomerSearchUpdateServiceProxy()
        {
        }

        #region 更新客户信息部分
        public void UpdateByCustomerInfo(CustomerSearchUpdateModel model)
        {
            this.SingleCall(action => action.UpdateByCustomerInfo(model));
        }

        public void UpdateByCustomerCollectionInfo(List<CustomerSearchUpdateModel> modelCollection)
        {
            this.SingleCall(action => action.UpdateByCustomerCollectionInfo(modelCollection));
        }

        #endregion

        #region 初始化客户信息部分
        public void InitCustomerSearch(List<string> customerIDs)
        {
            this.SingleCall(action => action.InitCustomerSearch(customerIDs));
        }
        #endregion

        protected override WfClientChannelFactory<ICustomerSearchUpdateService> GetService()
        {
            EndpointAddress endPoint = new EndpointAddress(UriSettings.GetConfig().GetUrl("pptsServices", "customerSearchUpdateService"));

            return new WfClientChannelFactory<ICustomerSearchUpdateService>(endPoint);
        }


    }
}
