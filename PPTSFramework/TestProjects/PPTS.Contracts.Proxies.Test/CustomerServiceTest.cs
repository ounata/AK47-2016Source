using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Customers.Entities;
using PPTS.Contracts.Customers.Models;

namespace PPTS.Contracts.Proxies.Test
{
    [TestClass]
    public class CustomerServiceTest
    {
        [TestMethod]
        public void QueryCustomerByIDTest()
        {
            Customer customer = PPTSCustomerQueryServiceProxy.Instance.QueryCustomerByID("id1");

            Console.WriteLine("ID: {0}, Name: {1}", customer.CustomerID, customer.CustomerName);
        }

        [TestMethod]
        public void QueryCustomerTeacherRelationByCustomerIDTest()
        {
            CustomerTearcherRelationQueryModel querymodel = new CustomerTearcherRelationQueryModel()
            {
                CustomerID = "1358641"
            };
            CustomerTeacherRelationQueryResult queryresult = PPTSCustomerQueryServiceProxy.Instance.QueryCustomerTeacherRelationByCustomerID(querymodel);
            Assert.IsNotNull(queryresult);
            Assert.IsNotNull(queryresult.CustomerTeacherRelationCollection);

        }

        [TestMethod]
        public void QueryCustomerTeacherRelationByCustomerID2Test()
        {
            CustomerTearcherRelationQueryModel querymodel = new CustomerTearcherRelationQueryModel()
            {
                CustomerID = "1358641",
                IsContainCustomerInfo = true
            };
            CustomerTeacherRelationQueryResult queryresult = PPTSCustomerQueryServiceProxy.Instance.QueryCustomerTeacherRelationByCustomerID(querymodel);
            Assert.IsNotNull(queryresult);
            Assert.IsNotNull(queryresult.CustomerTeacherRelationCollection);
            Assert.IsNotNull(queryresult.Customer);
        }
        [TestMethod]
        public void QueryCustomerCollectionByCustomerIDsTest()
        {
            CustomerCollectionQueryResult queryresult = PPTSCustomerQueryServiceProxy.Instance.QueryCustomerCollectionByCustomerIDs(new string[] { "1000029", "1000030", "1000196" });
            Assert.IsNotNull(queryresult);
            Assert.IsNotNull(queryresult.CustomerCollection);
            Assert.IsTrue(queryresult.CustomerCollection.Count>0);
        }
    }
}