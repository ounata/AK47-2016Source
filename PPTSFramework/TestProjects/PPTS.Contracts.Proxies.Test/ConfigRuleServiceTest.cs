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
            PPTS.Contracts.Products.Models.DiscountQueryResult discountQueryResult = PPTSConfigRuleQueryServiceProxy.Instance.QueryDiscountByCampusID("2121-Org");
            Assert.IsNotNull(discountQueryResult);
            Assert.IsNotNull(discountQueryResult.Discount);
        }
        [TestMethod]
        public void QueryExpenseByCampusIDTest()
        {
            PPTS.Contracts.Products.Models.ExpenseQueryResult queryResult = PPTSConfigRuleQueryServiceProxy.Instance.QueryExpenseByCampusID("2121-Org");
            Assert.IsNotNull(queryResult);
            Assert.IsNotNull(queryResult.Expense);

        }

        [TestMethod]
        public void QueryPresentByCampusIDTest()
        {
            PPTS.Contracts.Products.Models.PresentQueryResult queryResult = PPTSConfigRuleQueryServiceProxy.Instance.QueryPresentByCampusID("18-Org");
            Assert.IsNotNull(queryResult);
            Assert.IsNotNull(queryResult.Present);
            Assert.IsNotNull(queryResult.PresentItemCollection);

        }
    }
}
