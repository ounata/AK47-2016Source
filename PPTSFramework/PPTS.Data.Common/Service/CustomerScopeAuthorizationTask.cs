using MCS.Library.Configuration;
using MCS.Library.Core;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.Workflow;
using PPTS.Contracts.Customers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Service
{
    public class CustomerScopeAuthorizationTask
    {
        public static CustomerScopeAuthorizationTask Instance = new CustomerScopeAuthorizationTask();

        public void CustomerOrgAuthorizationToOrderByTask(CustomerScopeAuthorizationModel model)
        {
            InvokeServiceTask task = new InvokeServiceTask()
            {
                TaskID = UuidHelper.NewUuidString(),
                TaskTitle = "Order复制学员(潜客)机构授权任务",
                ResourceID = UuidHelper.NewUuidString()
            };
            task.SvcOperationDefs.Add(PrepareCustomerWfServiceOperation("CustomerOrgAuthorizationToOrder", model));
            task.FillData();
            InvokeServiceTaskAdapter.Instance.Push(task);
        }

        public void CustomerOrgAuthorizationToSearchByTask(CustomerScopeAuthorizationModel model)
        {
            InvokeServiceTask task = new InvokeServiceTask()
            {
                TaskID = UuidHelper.NewUuidString(),
                TaskTitle = "Search复制学员(潜客)机构授权任务",
                ResourceID = UuidHelper.NewUuidString()
            };
            task.SvcOperationDefs.Add(PrepareCustomerWfServiceOperation("CustomerOrgAuthorizationToSearch", model));
            task.FillData();
            InvokeServiceTaskAdapter.Instance.Push(task);
        }

        public void CustomerRelationAuthorizationToOrderByTask(CustomerScopeAuthorizationModel model)
        {
            InvokeServiceTask task = new InvokeServiceTask()
            {
                TaskID = UuidHelper.NewUuidString(),
                TaskTitle = "Order复制学员(潜客)关系授权任务",
                ResourceID = UuidHelper.NewUuidString()
            };
            task.SvcOperationDefs.Add(PrepareCustomerWfServiceOperation("CustomerRelationAuthorizationToOrder", model));
            task.FillData();
            InvokeServiceTaskAdapter.Instance.Push(task);
        }

        public void CustomerRelationAuthorizationToSearchByTask(CustomerScopeAuthorizationModel model)
        {
            InvokeServiceTask task = new InvokeServiceTask()
            {
                TaskID = UuidHelper.NewUuidString(),
                TaskTitle = "Search复制学员(潜客)关系授权任务",
                ResourceID = UuidHelper.NewUuidString()
            };
            task.SvcOperationDefs.Add(PrepareCustomerWfServiceOperation("CustomerRelationAuthorizationToSearch", model));
            task.FillData();
            InvokeServiceTaskAdapter.Instance.Push(task);
        }

        public void OwnerRelationAuthorizationToSearchByTask(CustomerScopeAuthorizationModel model)
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

        public void RecordOrgAuthorizationToSearchByTask(CustomerScopeAuthorizationModel model)
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


        private WfServiceOperationDefinition PrepareCustomerWfServiceOperation(string activeName, CustomerScopeAuthorizationModel model)
        {
            WfServiceOperationParameterCollection parameters = new WfServiceOperationParameterCollection();
            parameters.Add(new WfServiceOperationParameter("model", model));
            WfServiceOperationDefinition serviceDefine = new WfServiceOperationDefinition(
                new WfServiceAddressDefinition(WfServiceRequestMethod.Post,
               UriSettings.GetConfig().GetUrl("pptsServices", "customerScopeAuthorizationService").ToString(),
                WfServiceContentType.Json)
                , activeName
                , parameters
                , "");
            return serviceDefine;
        }

    }
}
