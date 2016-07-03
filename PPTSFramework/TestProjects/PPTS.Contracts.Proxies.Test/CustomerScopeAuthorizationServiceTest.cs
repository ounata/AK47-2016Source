using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Contracts.Customers.Models;
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
    public class CustomerScopeAuthorizationServiceTest
    {
        [TestMethod]
        public void CustomerOrgAuthorizationToOrderTest()
        {
            CustomerScopeAuthorizationModel model = CreateCustomerModel();
            PPTSCustomerScopeAuthorizationServiceProxy.Instance.CustomerOrgAuthorizationToOrder(model);
            CustomerOrgAuthorizationAdapter adapter = CustomerOrgAuthorizationAdapter.GetInstance(PPTS.Data.Orders.ConnectionDefine.PPTSOrderConnectionName);
            CustomerOrgAuthorizationCollection collection = adapter.Load(builder => builder.AppendItem("OwnerID", model.OwnerID).AppendItem("OwnerType", (int)model.OwnerType));
            collection.ForEach(auth=>Console.WriteLine(auth.ObjectID));
        }

        [TestMethod]
        public void CustomerOrgAuthorizationToSearchTest()
        {
            CustomerScopeAuthorizationModel model = CreateCustomerModel();
            PPTSCustomerScopeAuthorizationServiceProxy.Instance.CustomerOrgAuthorizationToSearch(model);
            CustomerOrgAuthorizationAdapter adapter = CustomerOrgAuthorizationAdapter.GetInstance(PPTS.Data.Orders.ConnectionDefine.PPTSSearchConnectionName);
            CustomerOrgAuthorizationCollection collection = adapter.Load(builder => builder.AppendItem("OwnerID", model.OwnerID).AppendItem("OwnerType", (int)model.OwnerType));
            collection.ForEach(auth => Console.WriteLine(auth.ObjectID));

        }

        [TestMethod]
        public void CustomerRelationAuthorizationToOrderTest()
        {
            CustomerScopeAuthorizationModel model = CreateCustomerModel();
            PPTSCustomerScopeAuthorizationServiceProxy.Instance.CustomerRelationAuthorizationToOrder(model);
            CustomerOrgAuthorizationAdapter adapter = CustomerOrgAuthorizationAdapter.GetInstance(PPTS.Data.Orders.ConnectionDefine.PPTSOrderConnectionName);
            CustomerOrgAuthorizationCollection collection = adapter.Load(builder => builder.AppendItem("OwnerID", model.OwnerID).AppendItem("OwnerType", (int)model.OwnerType));
            collection.ForEach(auth => Console.WriteLine(auth.ObjectID));
        }

        [TestMethod]
        public void CustomerRelationAuthorizationToSearchTest()
        {
            CustomerScopeAuthorizationModel model = CreateCustomerModel();
            PPTSCustomerScopeAuthorizationServiceProxy.Instance.CustomerRelationAuthorizationToOrder(model);
            CustomerOrgAuthorizationAdapter adapter = CustomerOrgAuthorizationAdapter.GetInstance(PPTS.Data.Orders.ConnectionDefine.PPTSSearchConnectionName);
            CustomerOrgAuthorizationCollection collection = adapter.Load(builder => builder.AppendItem("OwnerID", model.OwnerID).AppendItem("OwnerType", (int)model.OwnerType));
            collection.ForEach(auth => Console.WriteLine(auth.ObjectID));
        }

        [TestMethod]
        public void OwnerRelationAuthorizationToSearchTest()
        {
            CustomerScopeAuthorizationModel model = CreateRecordModel();
            PPTSCustomerScopeAuthorizationServiceProxy.Instance.OwnerRelationAuthorizationToSearch(model);
            OwnerRelationAuthorizationAdapter adapter = OwnerRelationAuthorizationAdapter.GetInstance(PPTS.Data.Orders.ConnectionDefine.PPTSSearchConnectionName);
            OwnerRelationAuthorizationCollection collection = adapter.Load(builder => builder.AppendItem("OwnerID", model.OwnerID).AppendItem("OwnerType", (int)model.OwnerType));
            collection.ForEach(auth => Console.WriteLine(auth.ObjectID));
        }

        [TestMethod]
        public void RecordOrgAuthorizationToSearchTest()
        {
            CustomerScopeAuthorizationModel model = CreateRecordModel();
            PPTSCustomerScopeAuthorizationServiceProxy.Instance.RecordOrgAuthorizationToSearch(model);
            RecordOrgAuthorizationAdapter adapter = RecordOrgAuthorizationAdapter.GetInstance(PPTS.Data.Orders.ConnectionDefine.PPTSSearchConnectionName);
            RecordOrgAuthorizationCollection collection = adapter.Load(builder => builder.AppendItem("OwnerID", model.OwnerID).AppendItem("OwnerType", (int)model.OwnerType));
            collection.ForEach(auth => Console.WriteLine(auth.ObjectID));
        }


        private CustomerScopeAuthorizationModel CreateCustomerModel()
        {
            return new CustomerScopeAuthorizationModel()
            {
                OwnerID = "1",
                RelationType = RelationType.Owner,
                OwnerType = Data.Common.Authorization.RecordType.Customer
            };
        }

        private CustomerScopeAuthorizationModel CreateRecordModel()
        {
            return new CustomerScopeAuthorizationModel()
            {
                OwnerID = "1",
                RelationType=RelationType.Owner,
                OwnerType = Data.Common.Authorization.RecordType.Account
            };
        }
    }
}
