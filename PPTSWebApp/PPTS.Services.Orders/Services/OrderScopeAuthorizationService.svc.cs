using PPTS.Contracts.Orders.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PPTS.Contracts.Orders.Models;
using MCS.Library.WcfExtensions;
using System.ServiceModel.Web;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Authorization;
using MCS.Library.Core;
using PPTS.Data.Orders;

namespace PPTS.Services.Orders.Services
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“OrderScopeAuthorizationService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 OrderScopeAuthorizationService.svc 或 OrderScopeAuthorizationService.svc.cs，然后开始调试。
    public class OrderScopeAuthorizationService : IOrderScopeAuthorizationService
    {
        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void CourseOrgAuthorizationToSearch(OrderScopeAuthorizationModel model)
        {
            model.NullCheck("model");
            model.OwnerID.NullCheck("OwnerID");
            CourseOrgAuthorizationCollection collection = CourseOrgAuthorizationAdapter.GetInstance(ConnectionDefine.PPTSOrderConnectionName)
                .Load(builder => builder.AppendItem("OwnerID", model.OwnerID).AppendItem("OwnerType", (int)model.OwnerType).AppendItem("RelationType", (int)model.RelationType));
            CourseOrgAuthorizationAdapter adapter = CourseOrgAuthorizationAdapter.GetInstance(PPTS.Data.Orders.ConnectionDefine.PPTSSearchConnectionName);
            adapter.DeleteInContext(builder => builder.AppendItem("OwnerID", model.OwnerID).AppendItem("OwnerType", (int)model.OwnerType).AppendItem("RelationType", (int)model.RelationType));
            collection.ForEach(auth => adapter.UpdateInContext(auth));
            adapter.GetDbContext().ExecuteNonQuerySqlInContext();
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void CourseRelationAuthorizationToSearch(OrderScopeAuthorizationModel model)
        {
            model.NullCheck("model");
            model.OwnerID.NullCheck("OwnerID");
            CourseRelationAuthorizationCollection collection = CourseRelationAuthorizationAdpter.GetInstance(ConnectionDefine.PPTSOrderConnectionName)
                .Load(builder => builder.AppendItem("OwnerID", model.OwnerID).AppendItem("OwnerType", (int)model.OwnerType).AppendItem("ObjectType", (int)model.RelationType));
            CourseRelationAuthorizationAdpter adapter = CourseRelationAuthorizationAdpter.GetInstance(PPTS.Data.Orders.ConnectionDefine.PPTSSearchConnectionName);
            adapter.DeleteInContext(builder => builder.AppendItem("OwnerID", model.OwnerID).AppendItem("OwnerType", (int)model.OwnerType).AppendItem("ObjectType", (int)model.RelationType));
            collection.ForEach(auth => adapter.UpdateInContext(auth));
            adapter.GetDbContext().ExecuteNonQuerySqlInContext();
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void OwnerRelationAuthorizationToSearch(OrderScopeAuthorizationModel model)
        {
            model.NullCheck("model");
            model.OwnerID.NullCheck("OwnerID");
            OwnerRelationAuthorizationCollection collection = OwnerRelationAuthorizationAdapter.GetInstance(ConnectionDefine.PPTSOrderConnectionName)
                .Load(builder => builder.AppendItem("OwnerID", model.OwnerID).AppendItem("OwnerType", (int)model.OwnerType).AppendItem("ObjectType", (int)model.RelationType));
            OwnerRelationAuthorizationAdapter adapter = OwnerRelationAuthorizationAdapter.GetInstance(PPTS.Data.Orders.ConnectionDefine.PPTSSearchConnectionName);
            adapter.DeleteInContext(builder => builder.AppendItem("OwnerID", model.OwnerID).AppendItem("OwnerType", (int)model.OwnerType).AppendItem("ObjectType", (int)model.RelationType));
            collection.ForEach(auth => adapter.UpdateInContext(auth));
            adapter.GetDbContext().ExecuteNonQuerySqlInContext();
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void RecordOrgAuthorizationToSearch(OrderScopeAuthorizationModel model)
        {
            model.NullCheck("model");
            model.OwnerID.NullCheck("OwnerID");
            RecordOrgAuthorizationCollection collection = RecordOrgAuthorizationAdapter.GetInstance(ConnectionDefine.PPTSOrderConnectionName)
                .Load(builder => builder.AppendItem("OwnerID", model.OwnerID).AppendItem("OwnerType", (int)model.OwnerType).AppendItem("RelationType", (int)model.RelationType));
            RecordOrgAuthorizationAdapter adapter = RecordOrgAuthorizationAdapter.GetInstance(PPTS.Data.Orders.ConnectionDefine.PPTSSearchConnectionName);
            adapter.DeleteInContext(builder => builder.AppendItem("OwnerID", model.OwnerID).AppendItem("OwnerType", (int)model.OwnerType).AppendItem("RelationType", (int)model.RelationType));
            collection.ForEach(auth => adapter.UpdateInContext(auth));
            adapter.GetDbContext().ExecuteNonQuerySqlInContext();
        }
    }
}
