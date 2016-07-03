using MCS.Library.Data;
using MCS.Library.Data.Builder;
using MCS.Library.OGUPermission;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Common;
using PPTS.Data.Common.Authorization;
using PPTS.Data.Common.Security;
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
        #region 验证操作
        [TestMethod]
        public void ReadAuthorizationExistBuilderSQLTest()
        {
            IUser user = GetUserInfo();
            PPTSJobCollection jobs = user.Jobs();
            foreach(PPTSJob job in jobs)
            {   //18-Org
                string sql = ScopeAuthorization<PotentialCustomer>.GetInstance(ConnectionDefine.PPTSCustomerConnectionName).ReadAuthExistsBuilder(job, "CustomerID", "CustomerID", job.Functions.ToList()).ToSqlString(TSqlBuilder.Instance);
                Console.WriteLine(sql);
            }
        }

        [TestMethod]
        public void ReadStaffAuthorizationExistBuilderSQLTest()
        {
            IUser user = GetUserInfo();
            PPTSJobCollection jobs = user.Jobs();
            foreach (PPTSJob job in jobs)
            {
                //18-Org
                string sql = ScopeAuthorization<PotentialCustomer>.GetInstance(ConnectionDefine.PPTSCustomerConnectionName).ReadAuthExistsBuilder(user, job.Organization(), "CustomerID", "CustomerID", job.Functions.ToList()).ToSqlString(TSqlBuilder.Instance);
                Console.WriteLine(sql);
            }
        }

        [TestMethod]
        public void HasReadAuthorizationTest()
        {
            IUser user = GetUserInfo();
            PPTSJobCollection jobs = user.Jobs();
            foreach (PPTSJob job in jobs)
            {
                bool result = ScopeAuthorization<PotentialCustomer>.GetInstance(ConnectionDefine.PPTSCustomerConnectionName).HasReadAuth(job, "CustomerID", "CustomerID", job.Functions.ToList());
                Console.WriteLine(result);
            }
        }

        [TestMethod]
        public void HasReadStaffAuthorizationTest()
        {
            IUser user = GetUserInfo();
            PPTSJobCollection jobs = user.Jobs();
            foreach (PPTSJob job in jobs)
            {
                bool result = ScopeAuthorization<PotentialCustomer>.GetInstance(ConnectionDefine.PPTSCustomerConnectionName).HasReadAuth(user, job.Organization(), "CustomerID", "CustomerID", job.Functions.ToList());
                Console.Write(result);
            }
        }

        [TestMethod]
        public void HasEditStaffAuthorizationTest()
        {
            IUser user = GetUserInfo();
            PPTSJobCollection jobs = user.Jobs();
            foreach (PPTSJob job in jobs)
            {
                bool result = ScopeAuthorization<PotentialCustomer>.GetInstance(ConnectionDefine.PPTSCustomerConnectionName).HasEditAuth(user, job.Organization(), "CustomerID", "CustomerID", job.Functions.ToList());
                Console.Write(result);
            }
        }
        #endregion 验证操作

        #region 更新操作
        [TestMethod]
        public void UpdateAuthTest()
        {
            IUser user = GetUserInfo();
            PPTSJobCollection jobs = user.Jobs();
            RecordInfo ri = GetRecordCunstomerInfo();
            if (jobs.Count > 0)
            {
                ScopeAuthorization<Customer>.GetInstance(ConnectionDefine.PPTSCustomerConnectionName).UpdateAuth(jobs[0], jobs[0].Organization(), ri.RecordID, ri.RecordType);
                Common.Entities.CustomerRelationAuthorizationCollection ras = CustomerRelationAuthorizationAdaper.GetInstance(ConnectionDefine.PPTSCustomerConnectionName).Load(builder => builder.AppendItem("OwnerID", ri.RecordID));
                ras.ForEach(modle => { Console.WriteLine(modle.ObjectType); Console.WriteLine(modle.OwnerType); Console.WriteLine(modle.CreateTime); Console.WriteLine(modle.ModifyTime); });
                Console.WriteLine();
                Common.Entities.CustomerOrgAuthorizationCollection oas = CustomerOrgAuthorizationAdapter.GetInstance(ConnectionDefine.PPTSCustomerConnectionName).Load(builder => builder.AppendItem("OwnerID", ri.RecordID));
                oas.ForEach(modle => { Console.WriteLine(modle.ObjectType); Console.WriteLine(modle.RelationType); Console.WriteLine(modle.OwnerType); Console.WriteLine(modle.CreateTime); Console.WriteLine(modle.ModifyTime); });
            }
        }

        [TestMethod]
        public void UpdateAuthInContextTest()
        {
            IUser user = GetUserInfo();
            PPTSJobCollection jobs = user.Jobs();
            RecordInfo ri = GetRecordCunstomerInfo();
            if (jobs.Count > 0)
            {
                ScopeAuthorization<Customer>.GetInstance(ConnectionDefine.PPTSCustomerConnectionName).UpdateAuthInContext(jobs[0], jobs[0].Organization(), ri.RecordID, ri.RecordType);
                DbContext.GetContext(ConnectionDefine.PPTSCustomerConnectionName).ExecuteNonQuerySqlInContext();

                Common.Entities.CustomerRelationAuthorizationCollection ras = CustomerRelationAuthorizationAdaper.GetInstance(ConnectionDefine.PPTSCustomerConnectionName).Load(builder => builder.AppendItem("OwnerID", ri.RecordID));
                ras.ForEach(modle => { Console.WriteLine(modle.ObjectType); Console.WriteLine(modle.OwnerType); Console.WriteLine(modle.CreateTime); Console.WriteLine(modle.ModifyTime); });
                Console.WriteLine();
                Common.Entities.CustomerOrgAuthorizationCollection oas = CustomerOrgAuthorizationAdapter.GetInstance(ConnectionDefine.PPTSCustomerConnectionName).Load(builder => builder.AppendItem("OwnerID", ri.RecordID));
                oas.ForEach(modle => { Console.WriteLine(modle.ObjectType); Console.WriteLine(modle.RelationType); Console.WriteLine(modle.OwnerType); Console.WriteLine(modle.CreateTime); Console.WriteLine(modle.ModifyTime); });
            }
        }

        [TestMethod]
        public void UpdateRecordAuthTest()
        {
            IUser user = GetUserInfo();
            PPTSJobCollection jobs = user.Jobs();
            RecordInfo ri = GetRecordAccountInfo();
            if (jobs.Count > 0)
            {
                ScopeAuthorization<Account>.GetInstance(ConnectionDefine.PPTSCustomerConnectionName).UpdateAuth(jobs[0], jobs[0].Organization(), ri.RecordID, ri.RecordType);
                Common.Entities.OwnerRelationAuthorizationCollection ras = OwnerRelationAuthorizationAdapter.GetInstance(ConnectionDefine.PPTSCustomerConnectionName).Load(builder => builder.AppendItem("OwnerID", ri.RecordID));
                ras.ForEach(modle => { Console.WriteLine(modle.ObjectType); Console.WriteLine(modle.OwnerType); Console.WriteLine(modle.CreateTime); Console.WriteLine(modle.ModifyTime); });
                Console.WriteLine();
                Common.Entities.RecordOrgAuthorizationCollection oas = RecordOrgAuthorizationAdapter.GetInstance(ConnectionDefine.PPTSCustomerConnectionName).Load(builder => builder.AppendItem("OwnerID", ri.RecordID));
                oas.ForEach(modle => { Console.WriteLine(modle.ObjectType); Console.WriteLine(modle.RelationType); Console.WriteLine(modle.OwnerType); Console.WriteLine(modle.CreateTime); Console.WriteLine(modle.ModifyTime); });
            }
        }

        [TestMethod]
        public void UpdateRecordAuthInContextTest()
        {
            IUser user = GetUserInfo();
            PPTSJobCollection jobs = user.Jobs();
            RecordInfo ri = GetRecordAccountInfo();
            if (jobs.Count > 0)
            {
                ScopeAuthorization<Account>.GetInstance(ConnectionDefine.PPTSCustomerConnectionName).UpdateAuthInContext(jobs[0], jobs[0].Organization(), ri.RecordID, ri.RecordType);
                DbContext.GetContext(ConnectionDefine.PPTSCustomerConnectionName).ExecuteNonQuerySqlInContext();

                Common.Entities.OwnerRelationAuthorizationCollection ras = OwnerRelationAuthorizationAdapter.GetInstance(ConnectionDefine.PPTSCustomerConnectionName).Load(builder => builder.AppendItem("OwnerID", "1"));
                ras.ForEach(modle => { Console.WriteLine(modle.ObjectType); Console.WriteLine(modle.OwnerType); Console.WriteLine(modle.CreateTime); Console.WriteLine(modle.ModifyTime); });
                Console.WriteLine();
                Common.Entities.RecordOrgAuthorizationCollection oas = RecordOrgAuthorizationAdapter.GetInstance(ConnectionDefine.PPTSCustomerConnectionName).Load(builder => builder.AppendItem("OwnerID", "1"));
                oas.ForEach(modle => { Console.WriteLine(modle.ObjectType); Console.WriteLine(modle.RelationType); Console.WriteLine(modle.OwnerType); Console.WriteLine(modle.CreateTime); Console.WriteLine(modle.ModifyTime); });
            }
        }

        [TestMethod]
        public void UpdateRecordStaffAuthTest()
        {
            IUser user = GetUserInfo();
            PPTSJobCollection jobs = user.Jobs();

            RecordInfo ri = GetRecordCunstomerInfo();
            if (jobs.Count > 0)
            {
                ScopeAuthorization<Account>.GetInstance(ConnectionDefine.PPTSCustomerConnectionName).UpdateAuth(user, jobs[0].Organization(), ri.RecordID, ri.RecordType);
                Common.Entities.OwnerRelationAuthorizationCollection ras = OwnerRelationAuthorizationAdapter.GetInstance(ConnectionDefine.PPTSCustomerConnectionName).Load(builder => builder.AppendItem("OwnerID", "1"));
                ras.ForEach(modle => { Console.WriteLine(modle.ObjectType); Console.WriteLine(modle.OwnerType); Console.WriteLine(modle.CreateTime); Console.WriteLine(modle.ModifyTime); });
                Console.WriteLine();
                Common.Entities.RecordOrgAuthorizationCollection oas = RecordOrgAuthorizationAdapter.GetInstance(ConnectionDefine.PPTSCustomerConnectionName).Load(builder => builder.AppendItem("OwnerID", "1"));
                oas.ForEach(modle => { Console.WriteLine(modle.ObjectType); Console.WriteLine(modle.RelationType); Console.WriteLine(modle.OwnerType); Console.WriteLine(modle.CreateTime); Console.WriteLine(modle.ModifyTime); });
            }
        }

        [TestMethod]
        public void UpdateRecordStaffAuthInContextTest()
        {
            IUser user = GetUserInfo();

            PPTSJobCollection jobs = user.Jobs();
            RecordInfo ri = GetRecordCunstomerInfo();
            if (jobs.Count > 0)
            {
                ScopeAuthorization<Account>.GetInstance(ConnectionDefine.PPTSCustomerConnectionName).UpdateAuthInContext(user, jobs[0].Organization(), ri.RecordID, ri.RecordType);
                DbContext.GetContext(ConnectionDefine.PPTSCustomerConnectionName).ExecuteNonQuerySqlInContext();

                Common.Entities.OwnerRelationAuthorizationCollection ras = OwnerRelationAuthorizationAdapter.GetInstance(ConnectionDefine.PPTSCustomerConnectionName).Load(builder => builder.AppendItem("OwnerID", ri.RecordID));
                ras.ForEach(modle => { Console.WriteLine(modle.ObjectType); Console.WriteLine(modle.OwnerType); Console.WriteLine(modle.CreateTime); Console.WriteLine(modle.ModifyTime); });
                Console.WriteLine();
                Common.Entities.RecordOrgAuthorizationCollection oas = RecordOrgAuthorizationAdapter.GetInstance(ConnectionDefine.PPTSCustomerConnectionName).Load(builder => builder.AppendItem("OwnerID", ri.RecordID));
                oas.ForEach(modle => { Console.WriteLine(modle.ObjectType); Console.WriteLine(modle.RelationType); Console.WriteLine(modle.OwnerType); Console.WriteLine(modle.CreateTime); Console.WriteLine(modle.ModifyTime); });
            }
        }

        [TestMethod]
        public void UpdateUpdateJobCollectionAuthTest()
        {
            IUser user = GetTeacherUserInfo();

            PPTSJobCollection jobs = user.Jobs();
            RecordInfo ri = GetRecordCunstomerInfo();
            if (jobs.Count > 0)
            {
                ScopeAuthorization<Customer>.GetInstance(ConnectionDefine.PPTSCustomerConnectionName).UpdateAuthByJobCollection(jobs.Select(job=>job.ID).ToList(), ri.RecordID, ri.RecordType,RelationType.Teacher);
                DbContext.GetContext(ConnectionDefine.PPTSCustomerConnectionName).ExecuteNonQuerySqlInContext();

                Common.Entities.CustomerRelationAuthorizationCollection ras = CustomerRelationAuthorizationAdaper.GetInstance(ConnectionDefine.PPTSCustomerConnectionName).Load(builder => builder.AppendItem("OwnerID", ri.RecordID));
                ras.ForEach(modle => { Console.WriteLine(modle.ObjectType); Console.WriteLine(modle.OwnerType); Console.WriteLine(modle.CreateTime); Console.WriteLine(modle.ModifyTime); });
                Console.WriteLine();
                Common.Entities.CustomerOrgAuthorizationCollection oas = CustomerOrgAuthorizationAdapter.GetInstance(ConnectionDefine.PPTSCustomerConnectionName).Load(builder => builder.AppendItem("OwnerID", ri.RecordID));
                oas.ForEach(modle => { Console.WriteLine(modle.ObjectType); Console.WriteLine(modle.RelationType); Console.WriteLine(modle.OwnerType); Console.WriteLine(modle.CreateTime); Console.WriteLine(modle.ModifyTime); });
            }
        }

        [TestMethod]
        public void InitAuthTest()
        {
            IUser user = GetUserInfo();
                        
            PPTSJobCollection jobs = user.Jobs();
            RecordInfo ri = GetRecordCunstomerInfo();

            if (jobs.Count > 0)
            {
                ScopeAuthorization<Customer>.GetInstance(ConnectionDefine.PPTSCustomerConnectionName).InitAuth(jobs[0].Organization(), ri.RecordID, ri.RecordType);
                Common.Entities.CustomerRelationAuthorizationCollection ras = CustomerRelationAuthorizationAdaper.GetInstance(ConnectionDefine.PPTSCustomerConnectionName).Load(builder => builder.AppendItem("OwnerID", ri.RecordID));
                ras.ForEach(modle => { Console.WriteLine(modle.ObjectType); Console.WriteLine(modle.OwnerType); Console.WriteLine(modle.CreateTime); Console.WriteLine(modle.ModifyTime); });
                Console.WriteLine();
                Common.Entities.CustomerOrgAuthorizationCollection oas = CustomerOrgAuthorizationAdapter.GetInstance(ConnectionDefine.PPTSCustomerConnectionName).Load(builder => builder.AppendItem("OwnerID", ri.RecordID));
                oas.ForEach(modle => { Console.WriteLine(modle.ObjectType); Console.WriteLine(modle.RelationType); Console.WriteLine(modle.OwnerType); Console.WriteLine(modle.CreateTime); Console.WriteLine(modle.ModifyTime); });
            }
        }

        [TestMethod]
        public void InitAuthInContextTest()
        {
            IUser user = GetUserInfo();
            PPTSJobCollection jobs = user.Jobs();
            RecordInfo ri = GetRecordCunstomerInfo();
            if (jobs.Count > 0)
            {
                ScopeAuthorization<Customer>.GetInstance(ConnectionDefine.PPTSCustomerConnectionName).InitAuth(jobs[0].Organization(), ri.RecordID, ri.RecordType);
                DbContext.GetContext(ConnectionDefine.PPTSCustomerConnectionName).ExecuteNonQuerySqlInContext();

                Common.Entities.CustomerRelationAuthorizationCollection ras = CustomerRelationAuthorizationAdaper.GetInstance(ConnectionDefine.PPTSCustomerConnectionName).Load(builder => builder.AppendItem("OwnerID", "1"));
                ras.ForEach(modle => { Console.WriteLine(modle.ObjectType); Console.WriteLine(modle.OwnerType); Console.WriteLine(modle.CreateTime); Console.WriteLine(modle.ModifyTime); });
                Console.WriteLine();
                Common.Entities.CustomerOrgAuthorizationCollection oas = CustomerOrgAuthorizationAdapter.GetInstance(ConnectionDefine.PPTSCustomerConnectionName).Load(builder => builder.AppendItem("OwnerID", "1"));
                oas.ForEach(modle => { Console.WriteLine(modle.ObjectType); Console.WriteLine(modle.RelationType); Console.WriteLine(modle.OwnerType); Console.WriteLine(modle.CreateTime); Console.WriteLine(modle.ModifyTime); });
            }
        }

        [TestMethod]
        public void Test()
        {
            InSqlClauseBuilder insql = new InSqlClauseBuilder();
            insql.DataField = "aaa";
            insql.AppendItem(new string[] { "111", "222" });

            WhereSqlClauseBuilder whereBuilder = new WhereSqlClauseBuilder();
            whereBuilder.AppendItem("ObjectID", "")
               .AppendItem("OwnerID", "")
                .AppendItem("OwnerType", "")
                .AppendItem("OwnerType","(1,2,3)","IN",true)
                .AppendItem("", insql.ToSqlString(TSqlBuilder.Instance), "",true);
            //SqlCaluseBuilderItemInOperator inss = new SqlCaluseBuilderItemInOperator();
            //inss.Data = new string[] { "111" };
            //whereBuilder.Add(inss);
            Console.Write(whereBuilder.ToSqlString(TSqlBuilder.Instance));
        }
        #endregion 更新操作

        private IUser GetUserInfo()
        {
            return OGUExtensions.GetUserByOAName("zhangxiaoyan_2");
        }

        private IUser GetTeacherUserInfo()
        {
            return OGUExtensions.GetUserByOAName("jiakun");
        }

        private RecordInfo GetRecordAccountInfo()
        {
            return new RecordInfo()
            {
                RecordID = "1",
                RecordType = RecordType.Account
            };
        }

        private RecordInfo GetRecordCunstomerInfo()
        {
            return new RecordInfo()
            {
                RecordID = "1",
                RecordType = RecordType.Customer
            };
        }
    }

    public class RecordInfo
    {
        public string RecordID { get; set; }

        public RecordType RecordType { get; set; }
    }
}
