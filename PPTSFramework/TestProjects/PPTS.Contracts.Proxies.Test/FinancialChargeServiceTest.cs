using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Proxies.Test
{
    [TestClass]
    public class FinancialChargeServiceTest
    {
        [TestMethod]
        public void SendFinancialIncomeTest()
        {
            PPTSFinancialChargeServiceProxy.Instance.SendFinancialIncome();
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void SendFinancialRefoundTest()
        {
            PPTSFinancialChargeServiceProxy.Instance.SendFinancialRefound();
            Assert.IsTrue(true);
        }
    }
}
