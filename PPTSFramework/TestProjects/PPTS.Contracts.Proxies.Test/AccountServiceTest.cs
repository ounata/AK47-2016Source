using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Customers.Entities;
using PPTS.Contracts.Customers.Models;

namespace PPTS.Contracts.Proxies.Test
{
    [TestClass]
    public class AccountServiceTest
    {
        [TestMethod]
        public void QueryAccountCollectionByCustomerIDTest()
        {
            AccountCollectionQueryResult queryResult = PPTSAccountQueryServiceProxy.Instance.QueryAccountCollectionByCustomerID("1531137");
            Assert.IsNotNull(queryResult);
            Assert.IsNotNull(queryResult.AccountCollection);
            Assert.IsTrue(queryResult.AccountCollection.Count > 0);
        }

        [TestMethod]
        public void QueryAccountChargeCollectionByCustomerIDTest()
        {
            AccountChargeCollectionQueryResult  queryResult = PPTSAccountQueryServiceProxy.Instance.QueryAccountChargeCollectionByCustomerID("3785659");
            Assert.IsNotNull(queryResult);
            Assert.IsNotNull(queryResult.AccountChargeCollection);
            Assert.IsTrue(queryResult.AccountChargeCollection.Count > 0);
            foreach (var value in queryResult.AccountChargeCollection)
            {
                Console.WriteLine(value.AccountCode);
                Console.WriteLine(value.ApplyNo);
            }
        }
    }
}
