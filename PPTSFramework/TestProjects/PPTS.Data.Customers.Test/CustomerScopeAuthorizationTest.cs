using MCS.Library.OGUPermission;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Common;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Authorization;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Test
{
    [TestClass]
    public class CustomerScopeAuthorizationTest
    {
        [TestMethod]
        public void ReadAuthorizationExistBuiderSQLTest()
        {
            IUser user = OGUExtensions.GetUserByOAName("zhangxiaoyan_2");
            PPTSJobCollection jobs = user.Jobs();
            if (jobs.Count > 0)
            {
                jobs[0].Functions.Clear();
                jobs[0].Functions.Add("a");
                jobs[0].Functions.Add("b");
                jobs[0].Functions.Add("h");
                jobs[0].Functions.Add("ewww");
                string sql = ScopeAuthorization<Customer>.Instance.ReadAuthorizationExistBuiderSQL(jobs[0], "CustomerID", "CustomerID", jobs[0].Functions.ToList());
                Console.Write(sql);
            }
            //Assert.IsTrue(result);
        }

        [TestMethod]
        public void ReadAuthorizationExistBuiderSQLTest2()
        {
            IUser user = OGUExtensions.GetUserByOAName("zhangxiaoyan_2");
            PPTSJobCollection jobs = user.Jobs();
            if (jobs.Count > 0)
            {
                jobs[0].Functions.Clear();
                jobs[0].Functions.Add("a");
                jobs[0].Functions.Add("b");
                jobs[0].Functions.Add("h");
                jobs[0].Functions.Add("ewww");
                string sql = ScopeAuthorization<Customer>.Instance.ReadAuthorizationExistBuiderSQL(jobs[0], jobs[0].Functions.ToList());
                Console.Write(sql);
            }

        }

        [TestMethod]
        public void GetFirstAuthorizationOrgTest()
        {
            IUser user = OGUExtensions.GetUserByOAName("zhangxiaoyan_2");
            PPTSJobCollection jobs = user.Jobs();
            if (jobs.Count > 0)
            {
                jobs[7].Functions.Clear();
                jobs[7].Functions.Add("a");
                jobs[7].Functions.Add("b");
                jobs[7].Functions.Add("h");
                jobs[7].Functions.Add("ewww");
                IOrganization org = ScopeAuthorization<Customer>.Instance.GetFirstAuthorizationOrg(jobs[7], jobs[7].Functions.ToList());
                Assert.IsNotNull(org);
                Console.Write(org.Name);
            }


        }

        [TestMethod]
        public void ReadAuthorizationWhereBuiderSQLTest()
        {
            IUser user = OGUExtensions.GetUserByOAName("zhangxiaoyan_2");
            PPTSJobCollection jobs = user.Jobs();
            if (jobs.Count > 0)
            {
                jobs[7].Functions.Clear();
                jobs[7].Functions.Add("a");
                jobs[7].Functions.Add("b");
                jobs[7].Functions.Add("h");
                jobs[7].Functions.Add("ewww");
                string sql = ScopeAuthorization<Customer>.Instance.ReadAuthorizationWhereBuiderSQL(jobs[7], "CustomerID", "CustomerID", jobs[7].Functions.ToList());
                Console.Write(sql);
            }
        }

        [TestMethod]
        public void HasReadAuthorizationTest()
        {
            IUser user = OGUExtensions.GetUserByOAName("zhangxiaoyan_2");
            PPTSJobCollection jobs = user.Jobs();
            if (jobs.Count > 0)
            {
                jobs[0].Functions.Clear();
                jobs[0].Functions.Add("a");
                jobs[0].Functions.Add("b");
                jobs[0].Functions.Add("h");
                jobs[0].Functions.Add("ewww");
                bool result = ScopeAuthorization<Customer>.Instance.HasReadAuthorization(jobs[0], "CustomerID", "CustomerID", jobs[0].Functions.ToList());
                Console.Write(result);
            }
        }

        [TestMethod]
        public void ExistReadRelationAuthorizationSQLTest()
        {
            List<string> jobFunctions = new List<string>();
            jobFunctions.Add("a");
            jobFunctions.Add("am");
            jobFunctions.Add("yy");
            //string sql= CustomerScopeAuthorization<Customer>.Instance.ExistReadRelationAuthorizationSQL("jobID", jobFunctions);
            //Console.WriteLine(sql);
        }
    }
}
