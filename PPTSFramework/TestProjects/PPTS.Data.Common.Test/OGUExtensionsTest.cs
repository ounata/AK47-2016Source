using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MCS.Library.OGUPermission;
using PPTS.Data.Common.Security;
using System.Diagnostics;
using MCS.Library.Core;

namespace PPTS.Data.Common.Test
{
    /// <summary>
    /// OGUExtensionsTest 的摘要说明
    /// </summary>
    [TestClass]
    public class OGUExtensionsTest
    {
        [TestMethod]
        public void GetOrganizationByIDTest()
        {
            IOrganization result = PPTS.Data.Common.OGUExtensions.GetOrganizationByID("2100-Org");//机构ID
            Assert.IsNotNull(result);
            Console.WriteLine(result.Name);
            Console.WriteLine(result.DisplayName);
        }

        [TestMethod]
        public void GetOrganizationByIDsTest()
        {
            OguObjectCollection<IOrganization> result = PPTS.Data.Common.OGUExtensions.GetOrganizationByIDs("1004-Org", "1007-Org");//机构ID
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
            foreach (IOrganization i in result)
            {
                Console.WriteLine(i.Name);
            }
        }

        [TestMethod]
        public void GetOrganizationByNameTest()
        {
            IOrganization result = PPTS.Data.Common.OGUExtensions.GetOrganizationByName("通锡苏分公司");//机构信息
            Assert.IsNotNull(result);
            Console.WriteLine(result.ID);
            DepartmentType type = result.PPTSDepartmentType();//组织机构类型
            Console.WriteLine(type.ToString());
        }

        [TestMethod]
        public void GetOrganizationByShortNameTest()
        {
            IOrganization result = PPTS.Data.Common.OGUExtensions.GetOrganizationByShortName("北京");//机构信息
            Assert.IsNotNull(result);
            Console.WriteLine(result.ID);
            DepartmentType type = result.PPTSDepartmentType();//组织机构类型
            IList<IOrganization> orgList = result.GetAllDataScopeParents();
            foreach (var org in orgList)
            {
                Console.WriteLine(org.PPTSDepartmentType().ToString());
                Console.WriteLine(org.GetOfficeAddress());
            }
            Console.WriteLine(type.ToString());
        }

        [TestMethod]
        public void GetUserByOANameTest()
        {
            IUser user = PPTS.Data.Common.OGUExtensions.GetUserByOAName("zhangxiaoyan_2");//人员信息
            PPTSJobCollection jobs = user.Jobs();//岗位信息
            Assert.IsNotNull(jobs);
            Assert.IsTrue(jobs.Count > 0);
            foreach (PPTSJob job in jobs)
            {
                Console.WriteLine(job.Name);
                Console.WriteLine(job.IsPrimary);
                Console.WriteLine(job.JobName);
                Console.WriteLine(job.JobType.ToString());
                IOrganization org = job.GetParentOrganizationByType(DepartmentType.HQ);//分公司信息
                if (org != null)
                {
                    Console.WriteLine(org.Name);
                    Console.WriteLine(org.GetFirstInitial());
                }
            }
        }

        [TestMethod]
        public void GetUserByIDTest()
        {
            IUser user = PPTS.Data.Common.OGUExtensions.GetUserByID("10285");//人员信息
            user.IsNotNull(action =>
            {
                Console.WriteLine(user.Email);
                Console.WriteLine(user.GetUserMobile());
            });
        }

        [TestMethod]
        public void GetUserByIDsTest()
        {
            OguObjectCollection<IUser> users = PPTS.Data.Common.OGUExtensions.GetUserByIDs("10285");//人员信息
            foreach (IUser user in users)
            {
                user.IsNotNull(action =>
                {
                    Console.WriteLine(user.Email);
                    Console.WriteLine(user.GetUserMobile());
                });
            }
        }


        [TestMethod]
        public void GetNullWhereBuilderTest()
        {
            MCS.Library.Data.Builder.WhereSqlClauseBuilder builder = new MCS.Library.Data.Builder.WhereSqlClauseBuilder();
            builder.AppendItem("name", (string)null, "IS");
            Console.WriteLine(builder.ToSqlString(MCS.Library.Data.Builder.TSqlBuilder.Instance));
        }

        [TestMethod]
        public void GetFirstEnglishCodeTest()
        {
            IOrganization result = PPTS.Data.Common.OGUExtensions.GetOrganizationByID("1004-Org");//机构ID

            Console.Write(result.GetFirstInitial());
            Console.WriteLine(result.GetShowShortName());
        }

        [TestMethod]
        public void TestEnum()
        {

        }
    }
}
