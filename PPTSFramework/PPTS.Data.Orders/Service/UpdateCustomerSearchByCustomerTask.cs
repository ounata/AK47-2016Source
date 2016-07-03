using MCS.Library.Configuration;
using MCS.Library.Core;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.Workflow;
using PPTS.Contracts.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Orders
{
    public class UpdateCustomerSearchByCustomerTask
    {
        public static UpdateCustomerSearchByCustomerTask Instance = new UpdateCustomerSearchByCustomerTask();

        public void UpdateByCustomerInfoByTask(CustomerSearchUpdateModel model)
        {
            InvokeServiceTask task = new InvokeServiceTask()
            {
                TaskID = UuidHelper.NewUuidString(),
                TaskTitle = "CustomerSearch更新客户信息任务",
                ResourceID = UuidHelper.NewUuidString()
            };
            task.SvcOperationDefs.Add(PrepareCustomerWfServiceOperation("UpdateByCustomerInfo", "model", model));
            task.FillData();
            InvokeServiceTaskAdapter.Instance.Push(task);
        }

        public void UpdateByCustomerCollectionInfoByTask(List<CustomerSearchUpdateModel> modelCollection)
        {
            InvokeServiceTask task = new InvokeServiceTask()
            {
                TaskID = UuidHelper.NewUuidString(),
                TaskTitle = "CustomerSearch批量更新客户信息任务",
                ResourceID = UuidHelper.NewUuidString()
            };
            task.SvcOperationDefs.Add(PrepareCustomerWfServiceOperation("UpdateByCustomerCollectionInfo", "modelCollection", modelCollection));
            task.FillData();
            InvokeServiceTaskAdapter.Instance.Push(task);
        }

        public void InitCustomerSearchByTask(List<string> customerIDs)
        {
            InvokeServiceTask task = new InvokeServiceTask()
            {
                TaskID = UuidHelper.NewUuidString(),
                TaskTitle = "CustomerSearch初始化客户信息任务",
                ResourceID = UuidHelper.NewUuidString()
            };
            task.SvcOperationDefs.Add(PrepareCustomerWfServiceOperation("InitCustomerSearch", "customerIDs", customerIDs));
            task.FillData();
            InvokeServiceTaskAdapter.Instance.Push(task);
        }

        public WfServiceOperationDefinition PrepareCustomerWfServiceOperation(string methodName, string paramName, object model)
        {
            WfServiceOperationParameterCollection parameters = new WfServiceOperationParameterCollection();
            parameters.Add(new WfServiceOperationParameter(paramName, model));
            WfServiceOperationDefinition serviceDefine = new WfServiceOperationDefinition(
                new WfServiceAddressDefinition(WfServiceRequestMethod.Post,
                UriSettings.GetConfig().GetUrl("pptsServices", "customerSearchUpdateService").ToString(),
                WfServiceContentType.Json)
                , methodName
                , parameters
                , "");
            return serviceDefine;
        }
    }
}
