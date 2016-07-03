using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Proxies.Test
{
    [TestClass]
    public class FinancialAssignServiceTest
    {
        [TestMethod]
        public void SaveFinancialAssignTest()
        {
            PPTSFinancialAssignServiceProxy.Instance.SaveFinancialAssignInfo();
        }

        [TestMethod]
        public void SaveFinancialAssignByMonthTest()
        {
            PPTSFinancialAssignServiceProxy.Instance.SaveFinancialAssignInfoByMonth(2015, 6);
        }
    }
}
