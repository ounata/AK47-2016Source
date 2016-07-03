using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Common.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Test
{
    [TestClass]
    public class ScopeAuthSettingsSectionConfigSetting
    {
        [TestMethod]
        public void ScopeAuthSettingsSectionTest()
        {
            ScopeAuthSettingsSection config = ScopeAuthSettingsSection.GetConfig();
            Console.Write(config.Eabled);
        }
    }
}
