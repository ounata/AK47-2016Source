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
            TeacherRelationByCustomerQueryResult queryResult = PPTSCustomerQueryServiceProxy.Instance.QueryCustomerTeacherRelationByCustomerID("605929");
            Assert.IsNotNull(queryResult);
            Assert.IsNotNull(queryResult.Customer);
            Assert.IsNotNull(queryResult.TeacherJobCollection);
        }

        [TestMethod]
        public void QueryCustomerTeacherRelationByTeacherJobIDTest()
        {
            CustomerRelationByTeacherQueryResult queryResult = PPTSCustomerQueryServiceProxy.Instance.QueryCustomerTeacherRelationByTeacherJobID("83179-Group");
            Assert.IsNotNull(queryResult);
            Assert.IsNotNull(queryResult.TeacherJob);
            Assert.IsNotNull(queryResult.CustomerCollection);
        }

        [TestMethod]
        public void QueryCustomerCollectionByCustomerIDsTest()
        {
            CustomerCollectionQueryResult queryResult = PPTSCustomerQueryServiceProxy.Instance.QueryCustomerCollectionByCustomerIDs(new string[] { "3990054" });
            Assert.IsNotNull(queryResult);
            Assert.IsNotNull(queryResult.CustomerCollection);
            Assert.IsTrue(queryResult.CustomerCollection.Count>0);
        }

        [TestMethod]
        public void QueryCustomerExpenseByCustomerIDTest()
        {
            CustomerExpenseCollectionQueryResult queryResult = PPTSCustomerQueryServiceProxy.Instance.QueryCustomerExpenseByCustomerID("1");
            Assert.IsNotNull(queryResult);
            Assert.IsNotNull(queryResult.CustomerExpenseRelationCollection);
            Assert.IsTrue(queryResult.CustomerExpenseRelationCollection.Count>0);
            foreach (CustomerExpenseRelation relation in queryResult.CustomerExpenseRelationCollection)
            {
                Console.WriteLine(relation.ExpenseMoney);
            }

        }

        //[TestMethod]
        //public void QueryCustomerExpenseByCustomerIDsTest()
        //{
        //    CustomerExpenseCollectionQueryResult queryResult = PPTSCustomerQueryServiceProxy.Instance.QueryCustomerExpenseByCustomerIDs("1","2");
        //    Assert.IsNotNull(queryResult);
        //    Assert.IsNotNull(queryResult.CustomerExpenseRelationCollection);
        //    Assert.IsTrue(queryResult.CustomerExpenseRelationCollection.Count > 0);
        //    foreach (CustomerExpenseRelation relation in queryResult.CustomerExpenseRelationCollection)
        //    {
        //        Console.WriteLine(relation.ExpenseMoney);
        //    }

        //}
    }
}