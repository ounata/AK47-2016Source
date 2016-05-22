using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Adapters;
using System.Collections;
using PPTS.Data.Common.Security;
using MCS.Library.OGUPermission;

namespace PPTS.Data.Common.Test
{
    /// <summary>
    /// OrganizationAdapterTest 的摘要说明
    /// </summary>
    [TestClass]
    public class PPTSOrganizationAdapterTest
    {

        [TestMethod]
        public void LoadOrganizationCollectionTest()
        {
            PPTSOrganizationCollection organizationCollection = PPTSOrganizationAdapter.Instance.Load(builder => builder.AppendItem("Name", "加盟版分公司"));
            Assert.IsNotNull(organizationCollection);
            Assert.IsTrue(organizationCollection.Count > 0);
            Assert.IsNotNull(organizationCollection[0]);
        }

        [TestMethod]
        public void LoadOrganizationByNameTest()
        {
            PPTSOrganization organization = PPTSOrganizationAdapter.Instance.LoadByName("加盟版分公司");
            Assert.IsNotNull(organization);
            IOrganization result = organization.ToBaseOrganization();
            Console.WriteLine(organization.ID);
            Assert.IsNotNull(result);
        }

       
    }
}
