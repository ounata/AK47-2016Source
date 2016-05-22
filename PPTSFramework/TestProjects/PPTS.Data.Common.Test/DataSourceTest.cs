using MCS.Library.OGUPermission;
using MCS.Library.SOA.DataObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Common.DataSources;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Security;
using System;

namespace PPTS.Data.Common.Test
{
    [TestClass]
    public class DataSourceTest
    {
        [TestMethod]
        public void UserAndJobDataSourceTest()
        {
            IUser user = OguObjectSettings.GetConfig().Objects["hq"].User;

            PPTSJob educatorJob = user.Jobs().Find(job => job.JobType == JobTypeDefine.Educator);

            Assert.IsNotNull(educatorJob);

            UserAndJobDataSource dataSource = new UserAndJobDataSource(educatorJob.Organization(), JobTypeDefine.Educator);

            int totalCount = -1;

            UserAndJobCollection result = dataSource.Query(0, int.MaxValue, ref totalCount);

            Console.WriteLine(result.Count);

            Assert.IsTrue(result.ContainsKey(educatorJob.ID));
        }
    }
}
