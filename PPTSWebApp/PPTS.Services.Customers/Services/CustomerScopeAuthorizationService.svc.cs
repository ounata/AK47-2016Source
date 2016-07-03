using PPTS.Contracts.Customers.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PPTS.Contracts.Customers.Models;
using MCS.Library.WcfExtensions;
using System.ServiceModel.Web;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Authorization;
using PPTS.Data.Customers;
using MCS.Library.Core;

namespace PPTS.Services.Customers.Services
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“ScopeAuthorizationService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 ScopeAuthorizationService.svc 或 ScopeAuthorizationService.svc.cs，然后开始调试。
    public class CustomerScopeAuthorizationService : ICustomerScopeAuthorizationService
    {
        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void CustomerOrgAuthorizationToOrder(CustomerScopeAuthorizationModel model)
        {
            model.NullCheck("model");
            model.OwnerID.NullCheck("OwnerID");
            CustomerOrgAuthorizationCollection collection = CustomerOrgAuthorizationAdapter.GetInstance(ConnectionDefine.PPTSCustomerConnectionName)
                .Load(builder => builder.AppendItem("OwnerID", model.OwnerID).AppendItem("OwnerType", (int)model.OwnerType).AppendItem("RelationType",(int)model.RelationType));

            CustomerOrgAuthorizationAdapter adapter = CustomerOrgAuthorizationAdapter.GetInstance(PPTS.Data.Orders.ConnectionDefine.PPTSOrderConnectionName);
            adapter.DeleteInContext(builder => builder.AppendItem("OwnerID", model.OwnerID).AppendItem("OwnerType", (int)model.OwnerType).AppendItem("RelationType", (int)model.RelationType));
            collection.ForEach(auth => adapter.UpdateInContext(auth));
            adapter.GetDbContext().ExecuteNonQuerySqlInContext();
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void CustomerOrgAuthorizationToSearch(CustomerScopeAuthorizationModel model)
        {
            model.NullCheck("model");
            model.OwnerID.NullCheck("OwnerID");
            CustomerOrgAuthorizationCollection collection = CustomerOrgAuthorizationAdapter.GetInstance(ConnectionDefine.PPTSCustomerConnectionName)
               .Load(builder => builder.AppendItem("OwnerID", model.OwnerID).AppendItem("OwnerType", (int)model.OwnerType).AppendItem("RelationType", (int)model.RelationType));
            CustomerOrgAuthorizationAdapter adapter = CustomerOrgAuthorizationAdapter.GetInstance(PPTS.Data.Orders.ConnectionDefine.PPTSSearchConnectionName);
            adapter.DeleteInContext(builder => builder.AppendItem("OwnerID", model.OwnerID).AppendItem("OwnerType", (int)model.OwnerType).AppendItem("RelationType", (int)model.RelationType));
            collection.ForEach(auth => adapter.UpdateInContext(auth));
            adapter.GetDbContext().ExecuteNonQuerySqlInContext();
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void CustomerRelationAuthorizationToOrder(CustomerScopeAuthorizationModel model)
        {
            model.NullCheck("model");
            model.OwnerID.NullCheck("OwnerID");
            CustomerRelationAuthorizationCollection collection = CustomerRelationAuthorizationAdaper.GetInstance(ConnectionDefine.PPTSCustomerConnectionName)
                .Load(builder => builder.AppendItem("OwnerID", model.OwnerID).AppendItem("OwnerType", (int)model.OwnerType).AppendItem("ObjectType", (int)model.RelationType));
            CustomerRelationAuthorizationAdaper adapter = CustomerRelationAuthorizationAdaper.GetInstance(PPTS.Data.Orders.ConnectionDefine.PPTSOrderConnectionName);
            adapter.DeleteInContext(builder => builder.AppendItem("OwnerID", model.OwnerID).AppendItem("OwnerType", (int)model.OwnerType).AppendItem("ObjectType", (int)model.RelationType));
            collection.ForEach(auth => adapter.UpdateInContext(auth));
            adapter.GetDbContext().ExecuteNonQuerySqlInContext();
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void CustomerRelationAuthorizationToSearch(CustomerScopeAuthorizationModel model)
        {
            model.NullCheck("model");
            model.OwnerID.NullCheck("OwnerID");
            CustomerRelationAuthorizationCollection collection = CustomerRelationAuthorizationAdaper.GetInstance(ConnectionDefine.PPTSCustomerConnectionName)
                .Load(builder => builder.AppendItem("OwnerID", model.OwnerID).AppendItem("OwnerType", (int)model.OwnerType).AppendItem("ObjectType", (int)model.RelationType));
            CustomerRelationAuthorizationAdaper adapter = CustomerRelationAuthorizationAdaper.GetInstance(PPTS.Data.Orders.ConnectionDefine.PPTSSearchConnectionName);
            adapter.DeleteInContext(builder => builder.AppendItem("OwnerID", model.OwnerID).AppendItem("OwnerType", (int)model.OwnerType).AppendItem("ObjectType", (int)model.RelationType));
            collection.ForEach(auth => adapter.UpdateInContext(auth));
            adapter.GetDbContext().ExecuteNonQuerySqlInContext();
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void OwnerRelationAuthorizationToSearch(CustomerScopeAuthorizationModel model)
        {
            model.NullCheck("model");
            model.OwnerID.NullCheck("OwnerID");
            OwnerRelationAuthorizationCollection collection = OwnerRelationAuthorizationAdapter.GetInstance(ConnectionDefine.PPTSCustomerConnectionName)
                .Load(builder => builder.AppendItem("OwnerID", model.OwnerID).AppendItem("OwnerType", (int)model.OwnerType).AppendItem("ObjectType", (int)model.RelationType));
            OwnerRelationAuthorizationAdapter adapter = OwnerRelationAuthorizationAdapter.GetInstance(PPTS.Data.Orders.ConnectionDefine.PPTSSearchConnectionName);
            adapter.DeleteInContext(builder => builder.AppendItem("OwnerID", model.OwnerID).AppendItem("OwnerType", (int)model.OwnerType).AppendItem("ObjectType", (int)model.RelationType));
            collection.ForEach(auth => adapter.UpdateInContext(auth));
            adapter.GetDbContext().ExecuteNonQuerySqlInContext();
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void RecordOrgAuthorizationToSearch(CustomerScopeAuthorizationModel model)
        {
            model.NullCheck("model");
            model.OwnerID.NullCheck("OwnerID");
            RecordOrgAuthorizationCollection collection = RecordOrgAuthorizationAdapter.GetInstance(ConnectionDefine.PPTSCustomerConnectionName)
                .Load(builder => builder.AppendItem("OwnerID", model.OwnerID).AppendItem("OwnerType", (int)model.OwnerType).AppendItem("RelationType", (int)model.RelationType));
            RecordOrgAuthorizationAdapter adapter = RecordOrgAuthorizationAdapter.GetInstance(PPTS.Data.Orders.ConnectionDefine.PPTSSearchConnectionName);
            adapter.DeleteInContext(builder => builder.AppendItem("OwnerID", model.OwnerID).AppendItem("OwnerType", (int)model.OwnerType).AppendItem("RelationType", (int)model.RelationType));
            collection.ForEach(auth => adapter.UpdateInContext(auth));
            adapter.GetDbContext().ExecuteNonQuerySqlInContext();
        }
    }
}
