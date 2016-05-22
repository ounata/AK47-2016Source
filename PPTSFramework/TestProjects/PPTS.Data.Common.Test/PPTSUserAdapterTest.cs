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
    [TestClass]
    public class PPTSUserAdapterTest
    {
        [TestMethod]
        public void LoadUserByOANameTest()
        {
            PPTSUser user = PPTSUserAdpter.Instance.LoadByOAName("zhangxiaoyan_2");
            PPTSJobCollection jobs = user.Jobs();
            Assert.IsNotNull(jobs);
            Assert.IsTrue(jobs.Count > 0);
            foreach (PPTSJob job in jobs)
            {
                Console.WriteLine(job.Name);
                IOrganization org = job.GetParentOrganizationByType(DepartmentType.Branch);
                if (org != null)
                    Console.WriteLine(org.Name);
            }

        }
    }
}
