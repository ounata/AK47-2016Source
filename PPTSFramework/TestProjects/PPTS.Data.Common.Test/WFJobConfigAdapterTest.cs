using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Core;

namespace PPTS.Data.Common.Test
{
    [TestClass]
    public class WFJobConfigAdapterTest
    {
        [TestMethod]
        public void LoadJobConfigTest()
        {
            WFJobConfig wjc = WFJobConfigAdapter.Instance.LoadJobConfig("103", "8-Org", "咨询师");
            wjc.IsNotNull(action => Console.Write(action.JobConfigID));
        }
    }
}
