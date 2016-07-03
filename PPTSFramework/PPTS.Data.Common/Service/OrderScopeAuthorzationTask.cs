using MCS.Library.Configuration;
using MCS.Library.Core;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.Workflow;
using PPTS.Contracts.Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Service
{
    public class OrderScopeAuthorzationTask
    {
        public static OrderScopeAuthorzationTask Instance = new OrderScopeAuthorzationTask();

        public void CourseOrgAuthorizationToSearchByTask(OrderScopeAuthorizationModel model)
        {
            InvokeServiceTask task = new InvokeServiceTask()
            {
                TaskID = UuidHelper.NewUuidString(),
                TaskTitle = "Search复制课时机构授权任务",
                ResourceID = UuidHelper.NewUuidString()
            };
            task.SvcOperationDefs.Add(PrepareCustomerWfServiceOperation("CourseOrgAuthorizationToSearch", model));
            task.FillData();
            InvokeServiceTaskAdapter.Instance.Push(task);
        }

        public void CourseRelationAuthorizationToSearchByTask(OrderScopeAuthorizationModel model)
        {
            InvokeServiceTask task = new InvokeServiceTask()
            {
                TaskID = UuidHelper.NewUuidString(),
                TaskTitle = "Search复制课时关系授权任务",
                ResourceID = UuidHelper.NewUuidString()
            };
            task.SvcOperationDefs.Add(PrepareCustomerWfServiceOperation("CourseRelationAuthorizationToSearch", model));
            task.FillData();
            InvokeServiceTaskAdapter.Instance.Push(task);
        }

        public void OwnerRelationAuthorizationToSearchByTask(OrderScopeAuthorizationModel model)
        {
            InvokeServiceTask task = new InvokeServiceTask()
            {
                TaskID = UuidHelper.NewUuidString(),
                TaskTitle = "Search复制记录关系授权任务",
                ResourceID = UuidHelper.NewUuidString()
            };
            task.SvcOperationDefs.Add(PrepareCustomerWfServiceOperation("OwnerRelationAuthorizationToSearch", model));
            task.FillData();
            InvokeServiceTaskAdapter.Instance.Push(task);
        }


        public void RecordOrgAuthorizationToSearchByTask(OrderScopeAuthorizationModel model)
        {
            InvokeServiceTask task = new InvokeServiceTask()
            {
                TaskID = UuidHelper.NewUuidString(),
                TaskTitle = "Search复制记录机构授权任务",
                ResourceID = UuidHelper.NewUuidString()
            };
            task.SvcOperationDefs.Add(PrepareCustomerWfServiceOperation("RecordOrgAuthorizationToSearch", model));
            task.FillData();
            InvokeServiceTaskAdapter.Instance.Push(task);
        }

        public WfServiceOperationDefinition PrepareCustomerWfServiceOperation(string activeName, OrderScopeAuthorizationModel model)
        {
            WfServiceOperationParameterCollection parameters = new WfServiceOperationParameterCollection();
            parameters.Add(new WfServiceOperationParameter("model", model));
            WfServiceOperationDefinition serviceDefine = new WfServiceOperationDefinition(
                new WfServiceAddressDefinition(WfServiceRequestMethod.Post,
                UriSettings.GetConfig().GetUrl("pptsServices", "orderScopeAuthorizationService").ToString(),
                WfServiceContentType.Json)
                , activeName
                , parameters
                , "");
            return serviceDefine;
        }
    }
}
