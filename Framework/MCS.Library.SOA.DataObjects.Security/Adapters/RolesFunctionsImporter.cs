using MCS.Library.Core;
using MCS.Library.Office.OpenXml.Excel;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;
using MCS.Library.SOA.DataObjects.Security;
using MCS.Library.SOA.DataObjects.Security.Adapters;
using MCS.Library.SOA.DataObjects.Security.Executors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.SOA.DataObjects.Security.Adapters
{
    /// <summary>
    /// 导入角色和功能的上下文参数
    /// </summary>
    public class RolesFunctionsImportContext
    {
        public RolesFunctionsImportContext()
        {
            this.Continue = true;
        }

        public SchemaObjectBase RelativeObject
        {
            get;
            internal set;
        }

        /// <summary>
        /// 操作描述
        /// </summary>
        public string Description
        {
            get;
            internal set;
        }

        public string ResponseMessage
        {
            get;
            set;
        }

        public Exception Exception
        {
            get;
            internal set;
        }

        public bool Continue
        {
            get;
            set;
        }
    }

    public static class RolesFunctionsImporter
    {
        private const string ImportedTag = "Imported";

        public static void ImportRolesAndPermissions(this SCApplication app, WorkSheet sheet, Action<RolesFunctionsImportContext> callerAction = null)
        {
            DataTable table = BuildDataTableFromSheet(sheet);

            app.ImportRolesAndPermissions(table);
        }

        public static void ImportRolesAndPermissions(this SCApplication app, DataTable table, Action<RolesFunctionsImportContext> callerAction = null)
        {
            if (table.Columns.Count > 3)
            {
                ProcessProgress.Current.MinStep = 0;
                ProcessProgress.Current.MaxStep = 1;
                ProcessProgress.Current.CurrentStep = 0;
                ProcessProgress.Current.Response();

                SCRolesAndPermissions rolesAndPermissions = table.GetRolesAndPermissions();

                SCRoleCollection roles = rolesAndPermissions.Roles;
                SCPermissionCollection permissions = rolesAndPermissions.Permissions;

                app.UpdateImportedRoles(roles, callerAction);
                app.UpdateImportedPermissions(permissions, callerAction);

                app = app.Refresh();

                app.UpdateImportedPermissionsInRoles(roles, callerAction);

                app = app.Refresh();
                app.SyncGroupsInRoles(callerAction);
            }
        }

        private static SCApplication Refresh(this SCApplication app)
        {
            return (SCApplication)SchemaObjectAdapter.Instance.Load(app.ID);
        }

        public static DeltaSchemaObjectCollection UpdateImportedRoles(this SCApplication app, IEnumerable<SCRole> rolesNeedToImported, Action<RolesFunctionsImportContext> callerAction = null)
        {
            DeltaSchemaObjectCollection delta = app.GetDeltaRoles(rolesNeedToImported);
            ProcessProgress.Current.MaxStep += delta.TotalChanges;
            ProcessProgress.Current.Response();

            foreach (SCRole role in delta.Inserted)
            {
                DoOperation(role, string.Format("添加角色\"{0}\"", role.CodeName), callerAction,
                    () => SCObjectOperations.Instance.AddRole((SCRole)role.CloneObject(), app));
            }

            foreach (SCRole role in delta.Deleted)
            {
                DoOperation(role, string.Format("删除角色\"{0}\"", role.CodeName), callerAction,
                    () => SCObjectOperations.Instance.DeleteRole((SCRole)role.CloneObject()));
            }

            return delta;
        }

        public static DeltaSchemaObjectCollection UpdateImportedPermissions(this SCApplication app, IEnumerable<SCPermission> permissionsNeedToImported, Action<RolesFunctionsImportContext> callerAction = null)
        {
            DeltaSchemaObjectCollection delta = app.GetDeltaPermissions(permissionsNeedToImported);
            ProcessProgress.Current.MaxStep += delta.TotalChanges;
            ProcessProgress.Current.Response();

            foreach (SCPermission permission in delta.Inserted)
            {
                DoOperation(permission, string.Format("添加权限\"{0}\"", permission.CodeName), callerAction,
                    () => SCObjectOperations.Instance.AddPermission((SCPermission)permission.CloneObject(), app));
            }

            foreach (SCPermission permission in delta.Deleted)
            {
                DoOperation(permission, string.Format("删除权限\"{0}\"", permission.CodeName), callerAction,
                    () => SCObjectOperations.Instance.DeletePermission((SCPermission)permission.CloneObject()));
            }

            return delta;
        }

        public static List<SCRoleAndDeltaPermission> UpdateImportedPermissionsInRoles(this SCApplication app, IEnumerable<SCRole> rolesNeedToImported, Action<RolesFunctionsImportContext> callerAction = null)
        {
            List<SCRoleAndDeltaPermission> result = GetDeltaPermissionsInRoles(app, rolesNeedToImported);

            ProcessProgress.Current.MaxStep += result.Sum(rp => rp.Delta.TotalChanges);
            ProcessProgress.Current.Response();

            foreach (SCRoleAndDeltaPermission roleAndPermissions in result)
            {
                foreach (SCPermission permission in roleAndPermissions.Delta.Inserted)
                {
                    DoOperation(permission, string.Format("在角色\"{0}\"中添加权限{1}", roleAndPermissions.Container.CodeName, permission.CodeName), callerAction,
                        () => SCObjectOperations.InstanceWithoutValidationAndStatusCheck.JoinRoleAndPermission((SCRole)roleAndPermissions.Container.CloneObject(), (SCPermission)permission.CloneObject()));
                }

                foreach (SCPermission permission in roleAndPermissions.Delta.Deleted)
                {
                    DoOperation(permission, string.Format("在角色\"{0}\"中删除权限{1}", roleAndPermissions.Container.CodeName, permission.CodeName), callerAction,
                        () => SCObjectOperations.InstanceWithoutValidationAndStatusCheck.DisjoinRoleAndPermission((SCRole)roleAndPermissions.Container.CloneObject(), (SCPermission)permission.CloneObject()));
                }
            }

            return result;
        }

        public static void SyncGroupsInRoles(this SCApplication app, Action<RolesFunctionsImportContext> callerAction = null)
        {
            List<SCRoleAndDeltaGroup> rolesAndGroups = app.GetDeltaGroupsInRoles();

            ProcessProgress.Current.MaxStep += rolesAndGroups.Sum(rg => rg.Delta.TotalChanges);
            ProcessProgress.Current.Response();

            foreach (SCRoleAndDeltaGroup roleAndGroups in rolesAndGroups)
            {
                roleAndGroups.Delta.Inserted.ForEach((group, index) =>
                    DoOperation(group, string.Format("在角色\"{0}\"增加群组({1}/{2})", roleAndGroups.Container.CodeName, index + 1, roleAndGroups.Delta.Inserted.Count),
                    callerAction,
                    () => SCObjectOperations.InstanceWithoutValidationAndStatusCheck.AddMemberToRole((SCBase)group, roleAndGroups.Container))
                );

                roleAndGroups.Delta.Deleted.ForEach((group, index) =>
                    DoOperation(group, string.Format("在角色\"{0}\"删除群组({1}/{2})", roleAndGroups.Container.CodeName, index + 1, roleAndGroups.Delta.Inserted.Count),
                    callerAction,
                    () => SCObjectOperations.InstanceWithoutValidationAndStatusCheck.RemoveMemberFromRole((SCBase)group, roleAndGroups.Container))
                );
            }
        }

        /// <summary>
        /// 得到变化的角色
        /// </summary>
        /// <param name="app"></param>
        /// <param name="rolesNeedToImported"></param>
        /// <returns></returns>
        public static DeltaSchemaObjectCollection GetDeltaRoles(this SCApplication app, IEnumerable<SCRole> rolesNeedToImported)
        {
            ProcessProgress.Current.Response("获取需要变化的角色");

            SchemaObjectCollection roles = app.GetExistsImportedRoles();

            return GetDeltaObjects(app.GetExistsImportedRoles(), rolesNeedToImported);
        }

        /// <summary>
        /// 得到变化的权限
        /// </summary>
        /// <param name="app"></param>
        /// <param name="permissionsNeedToImported"></param>
        /// <returns></returns>
        public static DeltaSchemaObjectCollection GetDeltaPermissions(this SCApplication app, IEnumerable<SCPermission> permissionsNeedToImported)
        {
            ProcessProgress.Current.Response("获取需要变化的权限");

            return GetDeltaObjects(app.GetExistsImportedPermissions(), permissionsNeedToImported);
        }

        public static List<SCRoleAndDeltaPermission> GetDeltaPermissionsInRoles(this SCApplication app, IEnumerable<SCRole> rolesNeedToImported)
        {
            ProcessProgress.Current.MaxStep += rolesNeedToImported.Count();
            ProcessProgress.Current.Response("获取需要变化的角色中的权限");

            List<SCRoleAndDeltaPermission> result = new List<SCRoleAndDeltaPermission>();

            foreach (SCRole role in rolesNeedToImported)
            {
                SCRoleAndDeltaPermission roleAndDelta = app.GetDeltaPermissionsInRole(role);
                result.Add(roleAndDelta);

                ProcessProgress.Current.Increment();
                ProcessProgress.Current.Response();
            }

            return result;
        }

        /// <summary>
        /// 得到角色中权限的变化列表
        /// </summary>
        /// <param name="app"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public static SCRoleAndDeltaPermission GetDeltaPermissionsInRole(this SCApplication app, SCRole role)
        {
            SCRole existedRole = app.CurrentRoles.Find(existed => AreEqual(existed, role));

            DeltaSchemaObjectCollection delta = null;

            if (existedRole != null)
            {
                delta = GetDeltaObjects(existedRole.CurrentPermissions, role.CurrentPermissions);
            }
            else
            {
                delta = new DeltaSchemaObjectCollection();
                existedRole = role;
            }

            return new SCRoleAndDeltaPermission(existedRole, delta);
        }

        private static List<SCRoleAndDeltaGroup> GetDeltaGroupsInRoles(this SCApplication app)
        {
            List<SCRoleAndDeltaGroup> result = new List<SCRoleAndDeltaGroup>();
            SchemaObjectCollection roles = app.GetExistsImportedRoles();

            ProcessProgress.Current.MaxStep += roles.Count;

            foreach (SCRole role in roles)
            {
                ProcessProgress.Current.Response(string.Format("获取需要变化的角色\"{0}\"中的群组", role.CodeName));

                SchemaObjectCollection groupsInOrgs = SchemaObjectAdapter.Instance.LoadByName("Groups", role.CodeName, SchemaObjectStatus.Normal, DateTime.MinValue);

                SCRoleAndDeltaGroup roleAndDelta = role.GetDeltaGroupsInRole(groupsInOrgs.FilterByType<SCGroup>());
                result.Add(roleAndDelta);

                ProcessProgress.Current.Increment();
                ProcessProgress.Current.Response(string.Format("获取需要变化的角色\"{0}\"中的群组完成", role.CodeName));
            }

            return result;
        }

        /// <summary>
        /// 得到角色中权限的变化列表
        /// </summary>
        /// <param name="app"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        private static SCRoleAndDeltaGroup GetDeltaGroupsInRole(this SCRole role, IEnumerable<SCGroup> groupsInOrgs)
        {
            List<SCGroup> existedGroups = new List<SCGroup>();

            role.CurrentMembers.ForEach(m => (m is SCGroup).TrueAction(() => existedGroups.Add((SCGroup)m)));

            DeltaSchemaObjectCollection delta = GetDeltaObjects(existedGroups, groupsInOrgs);

            return new SCRoleAndDeltaGroup(role, delta);
        }

        private static DeltaSchemaObjectCollection GetDeltaObjects<T>(this IEnumerable<T> existed, IEnumerable<T> needToImported) where T : SchemaObjectBase
        {
            DeltaSchemaObjectCollection delta = new DeltaSchemaObjectCollection();

            Dictionary<string, T> existedDict = existed.ToDictionaryByCodeName();
            Dictionary<string, T> needToImportedDict = needToImported.ToDictionaryByCodeName();

            needToImported.ForEach(obj =>
            {
                string codeName = obj.Properties.GetValue("CodeName", string.Empty);

                if (existedDict.ContainsKey(codeName) == false)
                    delta.Inserted.Add(obj);
            });

            existed.ForEach(objExisted =>
            {
                string codeName = objExisted.Properties.GetValue("CodeName", string.Empty);

                if (needToImportedDict.ContainsKey(codeName) == false)
                    delta.Deleted.Add(objExisted);
            });

            return delta;
        }

        private static bool AreEqual<T>(T obj1, T obj2) where T : SchemaObjectBase
        {
            return string.Compare(obj1.Properties.GetValue("CodeName", string.Empty), obj2.Properties.GetValue("CodeName", string.Empty), true) == 0;
        }

        private static Dictionary<string, T> ToDictionaryByCodeName<T>(this IEnumerable<T> objs) where T : SchemaObjectBase
        {
            Dictionary<string, T> result = new Dictionary<string, T>();

            foreach (T obj in objs)
            {
                string codeName = obj.Properties.GetValue("CodeName", string.Empty);

                if (result.ContainsKey(codeName) == false)
                    result.Add(codeName, obj);
            }

            return result;
        }

        public static SCRolesAndPermissions GetRolesAndPermissions(this DataTable table)
        {
            table.NullCheck("table");

            SCRoleCollection roles = new SCRoleCollection();
            SCPermissionCollection permissions = table.GetPermissions();

            if (table.Columns.Contains("权限点/数据权限") && table.Columns.Count > 3)
            {
                for (int i = 3; i < table.Columns.Count; i++)
                {
                    string roleName = table.Columns[i].ColumnName;

                    SCRole role = roles.Append(InitProperties(new SCRole(), roleName));

                    foreach (DataRow row in table.Rows)
                    {
                        string permissionName = row["权限点/数据权限"].ToString();

                        if (permissionName.IsNotEmpty())
                        {
                            SCPermission permission = permissions.Find(p => string.Compare(permissionName, p.CodeName, true) == 0);

                            if (permission != null)
                            {
                                string cellValue = row[roleName].ToString();

                                if (cellValue.IsNullOrEmpty() || cellValue.Trim().ToUpper() == "N")
                                    continue;

                                role.CurrentPermissions.Append(permission);
                            }
                        }
                    }
                }
            }

            return new SCRolesAndPermissions(roles, permissions);
        }

        public static SCRoleCollection GetRoles(this DataTable table)
        {
            SCRoleCollection roles = new SCRoleCollection();

            if (table != null && table.Columns.Count > 3)
            {
                for (int i = 3; i < table.Columns.Count; i++)
                {
                    string roleName = table.Columns[i].ColumnName;

                    SCRole role = new SCRole();

                    roles.Add(InitProperties(role, roleName));
                }
            }

            return roles;
        }

        public static SCPermissionCollection GetPermissions(this DataTable table)
        {
            SCPermissionCollection permissions = new SCPermissionCollection();

            if (table != null && table.Columns.Contains("权限点/数据权限"))
            {
                foreach (DataRow row in table.Rows)
                {
                    string permissionName = row["权限点/数据权限"].ToString();

                    if (permissionName.IsNotEmpty())
                    {
                        SCPermission permission = new SCPermission();

                        permissions.Add(InitProperties(permission, permissionName));
                    }
                }
            }

            return permissions;
        }

        public static void ImportRoles(this DataTable table, SCApplication app)
        {
            app.NullCheck("app");

            if (table != null && table.Columns.Count > 2 && table.Rows.Count > 0)
            {
                for (int i = 2; i < table.Columns.Count; i++)
                {
                    string roleName = table.Columns[i].ColumnName;

                    if (SchemaObjectAdapter.Instance.LoadByCodeName("Roles", roleName, SchemaObjectStatus.Normal, DateTime.MinValue) == null)
                    {
                        SCRole role = new SCRole();
                        role.ID = UuidHelper.NewUuidString();
                        role.DisplayName = role.Name = roleName;
                        role.CodeName = roleName;

                        SCObjectOperations.Instance.AddRole(role, app);
                    }
                }
            }
        }

        public static DataTable BuildDataTableFromSheet(this WorkSheet sheet)
        {
            DataTable table = new DataTable();

            BuildColumns(sheet, table);
            BuildRows(sheet, table);

            return table;
        }

        public static SchemaObjectCollection GetExistsImportedRoles(this SCApplication app)
        {
            SchemaObjectCollection result = new SchemaObjectCollection();

            app.CurrentRoles.ForEach(r => r.IsImported().TrueAction(() => result.Add(r)));

            return result;
        }

        private static SchemaObjectCollection GetExistsImportedPermissions(this SCApplication app)
        {
            SchemaObjectCollection result = new SchemaObjectCollection();

            app.CurrentPermissions.ForEach(p => p.IsImported().TrueAction(() => result.Add(p)));

            return result;
        }

        private static bool IsImported(this SchemaObjectBase obj)
        {
            return obj.Properties.GetValue("Comment", string.Empty) == ImportedTag;
        }

        private static void BuildColumns(WorkSheet sheet, DataTable table)
        {
            for (int i = 1; i < sheet.Columns.Count; i++)
            {
                object cellValue = sheet.Cells[1, i].Value;

                if (cellValue != null)
                    table.Columns.Add(cellValue.ToString());
                else
                    break;
            }
        }

        private static T InitProperties<T>(T obj, string objName) where T : SCBase
        {
            obj.ID = UuidHelper.NewUuidString();
            obj.DisplayName = obj.Name = objName;
            obj.CodeName = objName;
            obj.Properties.TrySetValue("Comment", "Imported");

            return obj;
        }

        private static void BuildRows(WorkSheet sheet, DataTable table)
        {
            for (int rowIndex = 2; rowIndex < sheet.Rows.Count; rowIndex++)
            {
                DataRow row = null;

                for (int columnIndex = 1; columnIndex <= table.Columns.Count; columnIndex++)
                {
                    object cellValue = sheet.Cells[rowIndex, columnIndex].Value;

                    if (cellValue != null)
                    {
                        if (row == null)
                        {
                            row = table.NewRow();
                            table.Rows.Add(row);
                        }

                        row[table.Columns[columnIndex - 1].ColumnName] = cellValue;
                    }
                    else
                    {
                        if (columnIndex == 1)
                            break;
                    }
                }
            }
        }

        private static void DoOperation(SchemaObjectBase relativeObject, string description, Action<RolesFunctionsImportContext> callerAction, Action innerAction)
        {
            innerAction.NullCheck("innerAction");

            RolesFunctionsImportContext context = new RolesFunctionsImportContext()
            {
                RelativeObject = relativeObject,
                Description = description
            };

            ProcessProgress.Current.Response(context.Description);

            try
            {
                innerAction();
            }
            catch (System.Exception ex)
            {
                context.Exception = ex;
            }
            finally
            {
                if (context.Exception == null)
                    context.ResponseMessage = string.Format("{0}完成", context.Description);
                else
                    context.ResponseMessage = string.Format("{0}异常: {1}", context.Description, context.Exception.Message);

                if (callerAction != null)
                    callerAction(context);

                ProcessProgress.Current.Increment();
                ProcessProgress.Current.Response(context.ResponseMessage);
            }

            context.Continue.FalseThrow("导入被终止");
        }
    }
}
