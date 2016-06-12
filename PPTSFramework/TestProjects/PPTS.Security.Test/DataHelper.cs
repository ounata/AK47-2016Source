using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Office.OpenXml.Excel;
using MCS.Library.SOA.DataObjects.Security;
using MCS.Library.SOA.DataObjects.Security.Adapters;
using MCS.Library.SOA.DataObjects.Security.Executors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace PPTS.Security.Test
{
    public static class DataHelper
    {
        private const string ApplicationID = "68DB2697-59B2-414B-8591-58CE06C4B44F";

        public static void ResetData()
        {
            SchemaObjectAdapter.Instance.ClearAllData();

            DataHelper.InitSchemas();
            DataHelper.InitApplicationAndRoles();
        }

        public static SCApplication GetApplication()
        {
            return (SCApplication)SchemaObjectAdapter.Instance.Load(ApplicationID);
        }

        public static WorkSheet GetExcelSheet()
        {
            WorkBook workbook = WorkBook.Load(ResourceHelper.GetResourceStream(Assembly.GetExecutingAssembly(), "PPTS.Security.Test.Contents.系统权限汇总.xlsx"));

            return workbook.Sheets.Any;
        }

        public static WorkSheet GetMiniExcelSheet()
        {
            WorkBook workbook = WorkBook.Load(ResourceHelper.GetResourceStream(Assembly.GetExecutingAssembly(), "PPTS.Security.Test.Contents.系统权限汇总-mini.xlsx"));

            return workbook.Sheets.Any;
        }

        public static void InitSchemas()
        {
            SchemaDefineCollection schemas = SchemaExtensions.CreateSchemasDefineFromConfiguration();

            using (TransactionScope scope = TransactionScopeFactory.Create())
            {
                schemas.ForEach(schema => SchemaDefineAdapter.Instance.Update(schema));
                scope.Complete();
            }
        }

        public static void InitApplicationAndRoles()
        {
            SCApplication app = new SCApplication();

            app.ID = ApplicationID;
            app.Name = "PPTS管理系统";
            app.CodeName = "PPTS";
            app.DisplayName = "PPTS管理系统";

            SCObjectOperations.Instance.AddApplication(app);

            SCRole role = new SCRole();
            role.ID = "6BEA73AB-0924-483B-BEE0-55C0847CFDAB";
            role.DisplayName = role.Name = "PPTS系统管理员";
            role.CodeName = "PPTSAdmin";

            SCObjectOperations.Instance.AddRole(role, app);
        }

        public static void InitGroups(IEnumerable<SCRole> roles)
        {
            foreach(SCRole role in roles)
            {
                SCGroup group = new SCGroup();

                group.ID = UuidHelper.NewUuidString();
                group.Name = group.DisplayName = group.CodeName = role.CodeName;

                SCObjectOperations.Instance.AddGroup(group, SCOrganization.GetRoot());
            }
        }
    }
}
