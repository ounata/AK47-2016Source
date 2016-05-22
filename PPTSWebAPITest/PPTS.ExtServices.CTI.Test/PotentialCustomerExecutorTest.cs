using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Common;
using MCS.Library.OGUPermission;

namespace PPTS.ExtServices.CTI.Test
{
    [TestClass]
    public class PotentialCustomerExecutorTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            IOrganization org = OGUExtensions.GetOrganizationByShortName("北京");
            Console.WriteLine("ID : " + org.ID + "\r\n" + "名称：" + org.Name);

            Assert.IsTrue(true);
        }
    }
}
