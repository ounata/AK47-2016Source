using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Contracts.Orders.Models;
using PPTS.Data.Common.Authorization;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Proxies.Test
{
    [TestClass]
    public class OrderScopeAuthorizationServiceTest
    {
        [TestMethod]
        public void CourseOrgAuthorizationToSearchTest()
        {
            OrderScopeAuthorizationModel model = CreateCourseModel();
            PPTSOrderScopeAuthorizationServiceProxy.Instance.CourseOrgAuthorizationToSearch(model);
            CourseOrgAuthorizationAdapter adapter = CourseOrgAuthorizationAdapter.GetInstance(PPTS.Data.Orders.ConnectionDefine.PPTSSearchConnectionName);
            CourseOrgAuthorizationCollection collection = adapter.Load(builder => builder.AppendItem("OwnerID", model.OwnerID).AppendItem("OwnerType", (int)model.OwnerType));
            collection.ForEach(auth => Console.WriteLine(auth.ObjectID));
        }

        [TestMethod]
        public void CourseRelationAuthorizationToSearchTest()
        {
            OrderScopeAuthorizationModel model = CreateCourseModel();
            PPTSOrderScopeAuthorizationServiceProxy.Instance.CourseRelationAuthorizationToSearch(model);
            CourseRelationAuthorizationAdpter adapter = CourseRelationAuthorizationAdpter.GetInstance(PPTS.Data.Orders.ConnectionDefine.PPTSSearchConnectionName);
            CourseRelationAuthorizationCollection collection = adapter.Load(builder => builder.AppendItem("OwnerID", model.OwnerID).AppendItem("OwnerType", (int)model.OwnerType));
            collection.ForEach(auth => Console.WriteLine(auth.ObjectID));
        }

        [TestMethod]
        public void OwnerRelationAuthorizationToSearchTest()
        {
            OrderScopeAuthorizationModel model = CreateRecordModel();
            PPTSOrderScopeAuthorizationServiceProxy.Instance.OwnerRelationAuthorizationToSearch(model);
            OwnerRelationAuthorizationAdapter adapter = OwnerRelationAuthorizationAdapter.GetInstance(PPTS.Data.Orders.ConnectionDefine.PPTSSearchConnectionName);
            OwnerRelationAuthorizationCollection collection = adapter.Load(builder => builder.AppendItem("OwnerID", model.OwnerID).AppendItem("OwnerType", (int)model.OwnerType));
            collection.ForEach(auth => Console.WriteLine(auth.ObjectID));
        }

        [TestMethod]
        public void RecordOrgAuthorizationToSearchTest()
        {
            OrderScopeAuthorizationModel model = CreateRecordModel();
            PPTSOrderScopeAuthorizationServiceProxy.Instance.RecordOrgAuthorizationToSearch(model);
            RecordOrgAuthorizationAdapter adapter = RecordOrgAuthorizationAdapter.GetInstance(PPTS.Data.Orders.ConnectionDefine.PPTSSearchConnectionName);
            RecordOrgAuthorizationCollection collection = adapter.Load(builder => builder.AppendItem("OwnerID", model.OwnerID).AppendItem("OwnerType", (int)model.OwnerType));
            collection.ForEach(auth => Console.WriteLine(auth.ObjectID));
        }


        private OrderScopeAuthorizationModel CreateCourseModel()
        {
            return new OrderScopeAuthorizationModel()
            {
                OwnerID = "1",
                RelationType = RelationType.Owner,
                OwnerType = Data.Common.Authorization.RecordType.Assign
            };
        }

        private OrderScopeAuthorizationModel CreateRecordModel()
        {
            return new OrderScopeAuthorizationModel()
            {
                OwnerID = "2",
                RelationType = RelationType.Owner,
                OwnerType = Data.Common.Authorization.RecordType.Asset
            };
        }
    }
}
