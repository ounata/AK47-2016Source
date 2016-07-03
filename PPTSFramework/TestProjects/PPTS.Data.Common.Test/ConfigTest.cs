using MCS.Library.OGUPermission;
using MCS.Library.SOA.DataObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.DataSources;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Security;
using System;

namespace PPTS.Data.Common.Test
{
    [TestClass]
    public class ConfigTest
    {
        [TestMethod]
        public void TestConfig()
        {
            ConfigValue v = new ConfigValue();
            v.AccountChargeEarlyMinDaysValue = 15;
            v.AccountFirstChargeMinMoneyValue = 500;
            v.AccountRefundTypeJudgeDaysValue = 7;
            v.EndingClassMinAccountValueValue = 200;
            v.IsTulandDiscountSchema2Value = true;

            ConfigAdapter.Instance.SetConfigValue("8-Org",v);
        }
    }
}
