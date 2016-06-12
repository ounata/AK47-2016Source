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

        private WfServiceOperationDefinition PrepareCustomerWfServiceOperation(string customerID)
        {
            WfServiceOperationParameterCollection parameters = new WfServiceOperationParameterCollection();
            parameters.Add(new WfServiceOperationParameter("customerID", customerID));
            WfServiceOperationDefinition serviceDefine = new WfServiceOperationDefinition(
                new WfServiceAddressDefinition(WfServiceRequestMethod.Post,
                this.GetService().Endpoint.Address.Uri.ToString(),
                WfServiceContentType.Json)
                , "UpdateByCustomerTextInfo"
                , parameters
                , "");
            return serviceDefine;
        }

        public void UpdateByCustomerInfoByTask(CustomerSearchUpdateModel model)
        {
            InvokeServiceTask task = new InvokeServiceTask()
            {
                TaskID = UuidHelper.NewUuidString(),
                TaskTitle = "CustomerSearch更新客户信息任务",
                ResourceID = UuidHelper.NewUuidString()
            };
            task.SvcOperationDefs.Add(PrepareCustomerWfServiceOperation(model));
            task.FillData();
            InvokeServiceTaskAdapter.Instance.Push(task);
        }

        public WfServiceOperationDefinition PrepareCustomerWfServiceOperation(CustomerSearchUpdateModel model)
        {
            WfServiceOperationParameterCollection parameters = new WfServiceOperationParameterCollection();
            parameters.Add(new WfServiceOperationParameter("model", model));
            WfServiceOperationDefinition serviceDefine = new WfServiceOperationDefinition(
                new WfServiceAddressDefinition(WfServiceRequestMethod.Post,
                this.GetService().Endpoint.Address.Uri.ToString(),
                WfServiceContentType.Json)
                , "UpdateByCustomerInfo"
                , parameters
                , "");
            return serviceDefine;
        }
        #endregion

        protected override WfClientChannelFactory<ICustomerSearchUpdateService> GetService()
        {
            EndpointAddress endPoint = new EndpointAddress(UriSettings.GetConfig().GetUrl("pptsServices", "customerSearchUpdateService"));

            return new WfClientChannelFactory<ICustomerSearchUpdateService>(endPoint);
        }


    }
}
