using MCS.Library.OGUPermission;
using MCS.Library.SOA.DataObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Common.Security;
using System;

namespace PPTS.Data.Common.Test
{
    [TestClass]
    public class SecurityTest
    {
        [TestMethod]
        public void UserJobTest()
        {
            IUser user = OguObjectSettings.GetConfig().Objects["hq"].User;

            user.Jobs().Output();

            Assert.IsTrue(user.Jobs().Count > 0);
        }
        [TestMethod]
        public void JobAbbrTest()
        {
            IUser user = OguObjectSettings.GetConfig().Objects["hq"].User;

            user.Jobs().ForEach(job => Console.WriteLine(job.GetDataScopeAbbr()));
        }

        [TestMethod]
        public void UserFunctionTest()
        {
            IUser user = OguObjectSettings.GetConfig().Objects["hq"].User;

            user.AllFunctions().Output();

            Assert.IsTrue(user.AllFunctions().Count > 0);
        }

        [TestMethod]
        public void UserDepartmentTest()
        {
            IUser user = OguObjectSettings.GetConfig().Objects["hq"].User;

            foreach (IGroup group in user.MemberOf)
            {
                Console.WriteLine(group.Parent.PPTSDepartmentType());
            }
        }

        [TestMethod]
        public void JobDepartmentTypeTest()
        {
            IUser user = OguObjectSettings.GetConfig().Objects["hq"].User;

            foreach (PPTSJob job in user.Jobs())
            {
                IGroup group = job.ToGroup();

                DepartmentType deptType = group.Parent.PPTSDepartmentType();

                IOrganization org =  job.GetParentOrganizationByType(deptType);

                Assert.AreEqual(group.Parent.ID, org.ID);
            }
        }

        [TestMethod]
        public void UserRolesTest()
        {
            IUser user = OguObjectSettings.GetConfig().Objects["hq"].User;

            user.PPTSRoles().Output();
        }
    }
}
