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
    public class WFRelationConfigAdapterTest
    {
        [TestMethod]
        public void LoadWFRelationConfigTest()
        {
            WFRelationConfig wrc = WFRelationConfigAdapter.Instance.LoadWFRelationConfig("101", "102", OGUExtensions.GetOrganizationByID("8-Org"), "咨询师");
            wrc.IsNotNull(action => Console.WriteLine(action.ProcessKey));
            wrc = WFRelationConfigAdapter.Instance.LoadWFRelationConfig("101", OGUExtensions.GetOrganizationByID("18-Org"), "咨询师");
            wrc.IsNotNull(action => Console.WriteLine(action.ProcessKey));

        }
    }
}
