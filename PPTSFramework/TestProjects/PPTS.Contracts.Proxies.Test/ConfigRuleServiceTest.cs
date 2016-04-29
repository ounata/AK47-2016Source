using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PPTS.Contracts.Proxies.Test
{
    [TestClass]
    public class ConfigRuleServiceTest
    {
        [TestMethod]
        public void QueryDiscountByCampusIDTest()
        {
            PPTS.Contracts.Products.Models.DiscountQueryResult dqr = PPTSConfigRuleQueryServiceProxy.Instance.QueryDiscountByCampusID("2121");
            Assert.IsNotNull(dqr);
            Assert.IsNotNull(dqr.Discount);
        }
        [TestMethod]
        public void QueryExpenseByCampusIDTest()
        {
            PPTS.Contracts.Products.Models.ExpenseQueryResult queryresult = PPTSConfigRuleQueryServiceProxy.Instance.QueryExpenseByCampusID("2121");
            Assert.IsNotNull(queryresult);
            Assert.IsNotNull(queryresult.Expense);

        }
    }
}
