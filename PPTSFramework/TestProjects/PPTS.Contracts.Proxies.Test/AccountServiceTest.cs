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
            AccountCollectionQueryResult queryresult = PPTSAccountQueryServiceProxy.Instance.QueryCustomerTeacherRelationByCustomerID("1008915");
            Assert.IsNotNull(queryresult);
            Assert.IsNotNull(queryresult.AccountCollection);
            Assert.IsTrue(queryresult.AccountCollection.Count > 0);
        }
    }
}
