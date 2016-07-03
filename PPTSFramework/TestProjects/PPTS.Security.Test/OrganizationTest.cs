using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MCS.Library.SOA.DataObjects.Security;
using MCS.Library.SOA.DataObjects.Security.Adapters;
using MCS.Library.Core;

namespace PPTS.Security.Test
{
    [TestClass]
    public class OrganizationTest
    {
        [TestMethod]
        public void RecursiveTest()
        {
            DataHelper.ResetData();
            DataHelper.InitOrganizations();

            SchemaObjectAdapter.Instance.UpdateChildrenFullPath();
            SCOrganization.GetRoot().OutputOrgRecursively();
        }
    }
}
