using MCS.Library.Data;
using MCS.Library.Office.OpenXml.Excel;
using MCS.Library.SOA.DataObjects.Security;
using MCS.Library.SOA.DataObjects.Security.Adapters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.Linq;
using System.Transactions;

namespace PPTS.Security.Test
{
    [TestClass]
    public class ImportRolesTest
    {
        //[ClassInitialize]
        //public static void Cleanup(TestContext context)
        //{

        //}

        [TestMethod]
        public void SheetToDataTable()
        {
            DataTable table = PrepareDataTableFromSheet();

            table.OutputColumns();
            table.OutputRowsInfo();

            Assert.AreEqual("【客户管理】-潜客", table.Rows[0]["模块"].ToString());
            Assert.AreEqual("Y", table.Rows[201]["合规稽查总监"].ToString());
            Assert.AreEqual("缴费单管理列表", table.Rows[66]["权限点/数据权限"].ToString());
        }

        [TestMethod]
        public void GetRolesAndPermissions()
        {
            DataTable table = PrepareDataTableFromSheet();

            SCRolesAndPermissions rolesAndPermissions = table.GetRolesAndPermissions();

            SCRoleCollection roles = rolesAndPermissions.Roles;

            Assert.AreEqual(table.Columns.Count - 3, roles.Count);

            Console.WriteLine(roles.Find(role => role.CodeName == "校教育咨询师").CurrentPermissions.Count);
            Assert.IsTrue(roles.Find(role => role.CodeName == "校教育咨询师").CurrentPermissions.Count > 1);

            Console.WriteLine(roles.Find(role => role.CodeName == "校学习管理师").CurrentPermissions.Count);
            Assert.IsTrue(roles.Find(role => role.CodeName == "校学习管理师").CurrentPermissions.Count > 1);

            Console.WriteLine(roles.Find(role => role.CodeName == "审计总监").CurrentPermissions.Count);
            Assert.AreEqual(0, roles.Find(role => role.CodeName == "审计总监").CurrentPermissions.Count);
        }

        [TestMethod]
        public void GetImportRoles()
        {
            DataTable table = PrepareDataTableFromSheet();

            SCRoleCollection roles = RolesFunctionsImporter.GetRoles(table);

            roles.Output();
            Console.WriteLine(roles.Count);

            Assert.AreEqual(table.Columns.Count - 3, roles.Count);
        }

        [TestMethod]
        public void GetImportPermissions()
        {
            DataTable table = PrepareDataTableFromSheet();

            SCPermissionCollection permissions = RolesFunctionsImporter.GetPermissions(table);

            permissions.Output();
            Console.WriteLine(permissions.Count);

            Assert.AreEqual(table.Rows.Count, permissions.Count);
        }

        [TestMethod]
        public void GetInsertedDeltaRoles()
        {
            DataHelper.ResetData();

            DataTable table = PrepareDataTableFromSheet();

            SCRoleCollection roles = RolesFunctionsImporter.GetRoles(table);

            SCApplication app = DataHelper.GetApplication();

            DeltaSchemaObjectCollection delta = app.GetDeltaRoles(roles);

            Console.WriteLine(delta.Inserted.Count);

            Assert.AreEqual(roles.Count, delta.Inserted.Count);
        }

        [TestMethod]
        public void GetInsertedDeltaPermissions()
        {
            DataHelper.ResetData();

            DataTable table = PrepareDataTableFromSheet();

            SCPermissionCollection permissions = RolesFunctionsImporter.GetPermissions(table);

            SCApplication app = DataHelper.GetApplication();

            DeltaSchemaObjectCollection delta = app.GetDeltaPermissions(permissions);

            Console.WriteLine(delta.Inserted.Count);

            Assert.AreEqual(permissions.Count, delta.Inserted.Count);
        }

        [TestMethod]
        public void AddRoles()
        {
            DataHelper.ResetData();

            DataTable table = PrepareDataTableFromMiniSheet();

            SCRoleCollection roles = RolesFunctionsImporter.GetRoles(table);

            SCApplication app = DataHelper.GetApplication();
            int originalCount = app.CurrentRoles.Count;

            app.UpdateImportedRoles(roles);

            app = DataHelper.GetApplication();

            Assert.AreEqual(originalCount + roles.Count, app.CurrentRoles.Count);
        }

        [TestMethod]
        public void AddRolesAgain()
        {
            DataHelper.ResetData();

            DataTable table = PrepareDataTableFromMiniSheet();

            SCRoleCollection roles = RolesFunctionsImporter.GetRoles(table);

            SCApplication app = DataHelper.GetApplication();
            int originalCount = app.CurrentRoles.Count;

            app.UpdateImportedRoles(roles);

            app = DataHelper.GetApplication();

            DeltaSchemaObjectCollection delta = app.UpdateImportedRoles(roles);

            Assert.IsTrue(delta.IsEmpty());
            Assert.AreEqual(originalCount + roles.Count, app.CurrentRoles.Count);
        }

        [TestMethod]
        public void DeleteRoles()
        {
            DataHelper.ResetData();

            DataTable table = PrepareDataTableFromMiniSheet();

            SCRoleCollection roles = RolesFunctionsImporter.GetRoles(table);

            SCApplication app = DataHelper.GetApplication();
            int originalCount = app.CurrentRoles.Count;

            app.UpdateImportedRoles(roles);

            app = DataHelper.GetApplication();

            SCRole lastRole = roles.Last();

            roles.Remove(lastRole);

            DeltaSchemaObjectCollection delta = app.UpdateImportedRoles(roles);

            app = DataHelper.GetApplication();

            Assert.AreEqual(1, delta.Deleted.Count);
            Assert.AreEqual(originalCount + roles.Count, app.CurrentRoles.Count);
        }

        [TestMethod]
        public void AddPermissions()
        {
            DataHelper.ResetData();

            DataTable table = PrepareDataTableFromMiniSheet();

            SCPermissionCollection permissions = RolesFunctionsImporter.GetPermissions(table);

            SCApplication app = DataHelper.GetApplication();
            int originalCount = app.CurrentPermissions.Count;

            app.UpdateImportedPermissions(permissions);

            app = DataHelper.GetApplication();

            Assert.AreEqual(originalCount + permissions.Count, app.CurrentPermissions.Count);
        }

        [TestMethod]
        public void AddPermissionsAgain()
        {
            DataHelper.ResetData();

            DataTable table = PrepareDataTableFromMiniSheet();

            SCPermissionCollection permissions = RolesFunctionsImporter.GetPermissions(table);

            SCApplication app = DataHelper.GetApplication();
            int originalCount = app.CurrentPermissions.Count;

            app.UpdateImportedPermissions(permissions);

            app = DataHelper.GetApplication();

            DeltaSchemaObjectCollection delta = app.UpdateImportedPermissions(permissions);

            Assert.IsTrue(delta.IsEmpty());
            Assert.AreEqual(originalCount + permissions.Count, app.CurrentPermissions.Count);
        }

        [TestMethod]
        public void DeletePermissions()
        {
            DataHelper.ResetData();

            DataTable table = PrepareDataTableFromMiniSheet();

            SCPermissionCollection permissions = RolesFunctionsImporter.GetPermissions(table);

            SCApplication app = DataHelper.GetApplication();
            int originalCount = app.CurrentPermissions.Count;

            app.UpdateImportedPermissions(permissions);

            app = DataHelper.GetApplication();

            SCPermission lastPermission = permissions.Last();

            permissions.Remove(lastPermission);

            DeltaSchemaObjectCollection delta = app.UpdateImportedPermissions(permissions);

            app = DataHelper.GetApplication();

            Assert.AreEqual(1, delta.Deleted.Count);
            Assert.AreEqual(originalCount + permissions.Count, app.CurrentPermissions.Count);
        }

        [TestMethod]
        public void AddRolesAndPermissions()
        {
            DataHelper.ResetData();

            DataTable table = PrepareDataTableFromMiniSheet();

            SCRolesAndPermissions rolesAndPermissions = table.GetRolesAndPermissions();

            SCRoleCollection roles = rolesAndPermissions.Roles;
            SCPermissionCollection permissions = rolesAndPermissions.Permissions;

            SCApplication app = DataHelper.GetApplication();

            app.UpdateImportedRoles(roles);
            app.UpdateImportedPermissions(permissions);

            app = DataHelper.GetApplication();

            app.UpdateImportedPermissionsInRoles(roles);

            app = DataHelper.GetApplication();

            Console.WriteLine(app.CurrentRoles.Find(role => role.CodeName == "校教育咨询师").CurrentPermissions.Count);
            Assert.IsTrue(app.CurrentRoles.Find(role => role.CodeName == "校教育咨询师").CurrentPermissions.Count > 1);

            Console.WriteLine(app.CurrentRoles.Find(role => role.CodeName == "校学习管理师").CurrentPermissions.Count);
            Assert.IsTrue(app.CurrentRoles.Find(role => role.CodeName == "校学习管理师").CurrentPermissions.Count > 1);

            Console.WriteLine(app.CurrentRoles.Find(role => role.CodeName == "审计总监").CurrentPermissions.Count);
            Assert.AreEqual(0, app.CurrentRoles.Find(role => role.CodeName == "审计总监").CurrentPermissions.Count);
        }

        [TestMethod]
        public void AddRolesAndPermissionsAgain()
        {
            DataHelper.ResetData();

            DataTable table = PrepareDataTableFromMiniSheet();

            SCRolesAndPermissions rolesAndPermissions = table.GetRolesAndPermissions();

            SCRoleCollection roles = rolesAndPermissions.Roles;
            SCPermissionCollection permissions = rolesAndPermissions.Permissions;

            SCApplication app = DataHelper.GetApplication();

            app.UpdateImportedRoles(roles);
            app.UpdateImportedPermissions(permissions);

            app = DataHelper.GetApplication();

            app.UpdateImportedPermissionsInRoles(roles);

            app = DataHelper.GetApplication();

            app.UpdateImportedPermissionsInRoles(roles);

            app = DataHelper.GetApplication();

            Console.WriteLine(app.CurrentRoles.Find(role => role.CodeName == "校教育咨询师").CurrentPermissions.Count);
            Assert.IsTrue(app.CurrentRoles.Find(role => role.CodeName == "校教育咨询师").CurrentPermissions.Count > 1);

            Console.WriteLine(app.CurrentRoles.Find(role => role.CodeName == "校学习管理师").CurrentPermissions.Count);
            Assert.IsTrue(app.CurrentRoles.Find(role => role.CodeName == "校学习管理师").CurrentPermissions.Count > 1);

            Console.WriteLine(app.CurrentRoles.Find(role => role.CodeName == "审计总监").CurrentPermissions.Count);
            Assert.AreEqual(0, app.CurrentRoles.Find(role => role.CodeName == "审计总监").CurrentPermissions.Count);
        }

        [TestMethod]
        public void DeleteRolesAndPermissions()
        {
            DataHelper.ResetData();

            DataTable table = PrepareDataTableFromMiniSheet();

            SCRolesAndPermissions rolesAndPermissions = table.GetRolesAndPermissions();

            SCRoleCollection roles = rolesAndPermissions.Roles;
            SCPermissionCollection permissions = rolesAndPermissions.Permissions;

            SCApplication app = DataHelper.GetApplication();

            app.UpdateImportedRoles(roles);
            app.UpdateImportedPermissions(permissions);

            app = DataHelper.GetApplication();

            app.UpdateImportedPermissionsInRoles(roles);

            app = DataHelper.GetApplication();

            roles.Find(role => role.CodeName == "校教育咨询师").CurrentPermissions.Clear();

            app.UpdateImportedPermissionsInRoles(roles);

            app = DataHelper.GetApplication();

            Console.WriteLine(app.CurrentRoles.Find(role => role.CodeName == "校教育咨询师").CurrentPermissions.Count);
            Assert.AreEqual(0, app.CurrentRoles.Find(role => role.CodeName == "校教育咨询师").CurrentPermissions.Count);

            Console.WriteLine(app.CurrentRoles.Find(role => role.CodeName == "校学习管理师").CurrentPermissions.Count);
            Assert.IsTrue(app.CurrentRoles.Find(role => role.CodeName == "校学习管理师").CurrentPermissions.Count > 1);

            Console.WriteLine(app.CurrentRoles.Find(role => role.CodeName == "审计总监").CurrentPermissions.Count);
            Assert.AreEqual(0, app.CurrentRoles.Find(role => role.CodeName == "审计总监").CurrentPermissions.Count);
        }

        [TestMethod]
        public void AddRolesAndPermissionsAndGroups()
        {
            DataHelper.ResetData();

            DataTable table = PrepareDataTableFromMiniSheet();

            SCRolesAndPermissions rolesAndPermissions = table.GetRolesAndPermissions();

            SCRoleCollection roles = rolesAndPermissions.Roles;
            SCPermissionCollection permissions = rolesAndPermissions.Permissions;

            DataHelper.InitGroups(roles);

            SCApplication app = DataHelper.GetApplication();

            app.UpdateImportedRoles(roles);
            app.UpdateImportedPermissions(permissions);

            app = DataHelper.GetApplication();

            app.UpdateImportedPermissionsInRoles(roles);

            app = DataHelper.GetApplication();
            app.SyncGroupsInRoles();

            app = DataHelper.GetApplication();

            foreach (SCRole role in app.GetExistsImportedRoles())
            {
                Assert.IsTrue(role.CurrentMembers.Count > 0);
                Assert.AreEqual(role.CodeName, role.CurrentMembers[0].Properties.GetValue("CodeName", string.Empty));
            }
        }

        [TestMethod]
        public void ImportRolesAndPermissions()
        {
            DataHelper.ResetData();

            DataTable table = PrepareDataTableFromMiniSheet();

            SCRolesAndPermissions rolesAndPermissions = table.GetRolesAndPermissions();

            SCRoleCollection roles = rolesAndPermissions.Roles;
            SCPermissionCollection permissions = rolesAndPermissions.Permissions;

            DataHelper.InitGroups(roles);

            SCApplication app = DataHelper.GetApplication();

            WorkSheet sheet = DataHelper.GetMiniExcelSheet();

            app.ImportRolesAndPermissions(sheet);

            app = DataHelper.GetApplication();

            foreach (SCRole role in app.GetExistsImportedRoles())
            {
                Assert.IsTrue(role.CurrentMembers.Count > 0);
                Assert.AreEqual(role.CodeName, role.CurrentMembers[0].Properties.GetValue("CodeName", string.Empty));
            }

            Console.WriteLine(app.CurrentRoles.Find(role => role.CodeName == "校教育咨询师").CurrentPermissions.Count);
            Assert.IsTrue(app.CurrentRoles.Find(role => role.CodeName == "校教育咨询师").CurrentPermissions.Count > 1);

            Console.WriteLine(app.CurrentRoles.Find(role => role.CodeName == "校学习管理师").CurrentPermissions.Count);
            Assert.IsTrue(app.CurrentRoles.Find(role => role.CodeName == "校学习管理师").CurrentPermissions.Count > 1);

            Console.WriteLine(app.CurrentRoles.Find(role => role.CodeName == "审计总监").CurrentPermissions.Count);
            Assert.AreEqual(0, app.CurrentRoles.Find(role => role.CodeName == "审计总监").CurrentPermissions.Count);
        }

        private static DataTable PrepareDataTableFromSheet()
        {
            WorkSheet sheet = DataHelper.GetExcelSheet();

            DataTable table = RolesFunctionsImporter.BuildDataTableFromSheet(sheet);

            return table;
        }

        private static DataTable PrepareDataTableFromMiniSheet()
        {
            WorkSheet sheet = DataHelper.GetMiniExcelSheet();

            DataTable table = RolesFunctionsImporter.BuildDataTableFromSheet(sheet);

            return table;
        }
    }
}
