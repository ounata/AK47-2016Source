using MCS.Library.OGUPermission;
using MCS.Library.SOA.DataObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PPTS.Data.Common.Test
{
    [TestClass]
    public class UserInOrganizationAndJobTest
    {
        [TestMethod]
        public void LoadUsersInJobsByOrganizationID()
        {
            IUser user = OguObjectSettings.GetConfig().Objects["hq"].User;

            PPTSJob job = GetCampusJob(user);

            Assert.IsNotNull(job);
            Console.WriteLine(job.JobName);

            IOrganization org = job.Organization().GetParentOrganizationByType(DepartmentType.Campus);

            Assert.IsNotNull(org);

            OguDataCollection<IUser> users = PPTSOrganizationAdapter.Instance.LoadUsersInJobsByOrganizationID(org.ID, job.JobName);

            Console.WriteLine(users.Count());

            Assert.IsNotNull(users.FindSingleObjectByID(user.ID));
        }

        [TestMethod]
        public void GetUsersInJobsByOrganizationID()
        {
            IUser user = OguObjectSettings.GetConfig().Objects["hq"].User;

            PPTSJob job = GetCampusJob(user);

            Assert.IsNotNull(job);
            Console.WriteLine(job.JobName);

            IOrganization org = job.Organization().GetParentOrganizationByType(DepartmentType.Campus);

            Assert.IsNotNull(org);

            PPTSOrgAndJobCacheQueue.Instance.Clear();

            IEnumerable<IUser> users = PPTSOrganizationAdapter.Instance.GetUsersInJobsByOrganizationID(org.ID, job.JobName);

            Console.WriteLine(users.Count());

            Assert.IsNotNull(users.SingleOrDefault(u => u.ID == user.ID));
            Assert.AreEqual(1, PPTSOrgAndJobCacheQueue.Instance.Count);
        }

        private static PPTSJob GetCampusJob(IUser user)
        {
            PPTSJob result = null;

            foreach (PPTSJob job in user.Jobs())
            {
                if (job.GetParentOrganizationByType(DepartmentType.Campus) != null)
                {
                    result = job;
                    break;
                }
            }

            return result;
        }
    }
}
