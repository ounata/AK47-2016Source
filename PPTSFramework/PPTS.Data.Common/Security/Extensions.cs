using MCS.Library.Core;
using MCS.Library.OGUPermission;
using MCS.Library.Principal;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PPTS.Data.Common.Security
{
    /// <summary>
    /// PPTS相关的扩展类
    /// </summary>
    public static class Extensions
    {
        #region Fill User系列
        /// <summary>
        /// 从user填充具有IEntityWithCreator的对象
        /// </summary>
        /// <param name="creatorInfo"></param>
        /// <param name="user"></param>
        public static void FillFromUser(this IEntityWithCreator creatorInfo, IUser user)
        {
            user.FillCreatorInfo(creatorInfo);
        }

        /// <summary>
        /// 根据User填充具有IEntityWithCreator的对象
        /// </summary>
        /// <param name="user"></param>
        /// <param name="creatorInfo"></param>
        public static void FillCreatorInfo(this IUser user, IEntityWithCreator creatorInfo)
        {
            if (user != null && creatorInfo != null)
            {
                creatorInfo.CreatorID = user.ID;
                creatorInfo.CreatorName = user.DisplayName;
            }
        }

        /// <summary>
        /// 根据User填充具有IEntityWithModifier的对象
        /// </summary>
        /// <param name="user"></param>
        /// <param name="modifierInfo"></param>
        public static void FillModifierInfo(this IUser user, IEntityWithModifier modifierInfo)
        {
            if (user != null && modifierInfo != null)
            {
                modifierInfo.ModifierID = user.ID;
                modifierInfo.ModifierName = user.DisplayName;
            }
        }

        /// <summary>
        /// 根据User填充具有IEntityWithModifier的对象
        /// </summary>
        /// <param name="modifierInfo"></param>
        /// <param name="user"></param>
        public static void FillFromUser(this IEntityWithModifier modifierInfo, IUser user)
        {
            user.FillModifierInfo(modifierInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="creatorInfoList"></param>
        public static IEnumerable<T> FillCreatorList<T>(this IEnumerable<T> creatorInfoList) where T : IEntityWithCreator
        {
            if (DeluxePrincipal.IsAuthenticated)
                creatorInfoList.ForEach(ci => ci.FillCreator());

            return creatorInfoList;
        }

        /// <summary>
        /// 根据DeluxeIdentity填充具有IEntityWithCreator的对象。如果没有登录过，则填充无效
        /// </summary>
        /// <param name="creatorInfo"></param>
        public static T FillCreator<T>(this T creatorInfo) where T : IEntityWithCreator
        {
            if (DeluxePrincipal.IsAuthenticated)
                DeluxeIdentity.Current.FillCreatorInfo(creatorInfo);

            return creatorInfo;
        }

        /// <summary>
        /// 根据Identity填充具有IEntityWithModifier的对象
        /// </summary>
        /// <param name="modifierInfo"></param>
        public static T FillModifier<T>(this T modifierInfo) where T : IEntityWithModifier
        {
            if (DeluxePrincipal.IsAuthenticated)
                DeluxeIdentity.Current.FillModifierInfo(modifierInfo);

            return modifierInfo;
        }

        /// <summary>
        /// 填充一个集合，该集合包含的元素包含IEntityWithModifier
        /// </summary>
        /// <param name="modifierInfoList"></param>
        public static void FillModifierList<T>(this IEnumerable<T> modifierInfoList) where T : IEntityWithModifier
        {
            if (DeluxePrincipal.IsAuthenticated)
                modifierInfoList.ForEach(mi => mi.FillModifier());
        }

        /// <summary>
        /// 根据DeluxeIdentity填充具有IEntityWithCreator的对象。如果没有登录过，则填充无效
        /// </summary>
        /// <param name="user"></param>
        /// <param name="creatorInfo"></param>
        public static void FillCreatorInfo(this DeluxeIdentity identity, IEntityWithCreator creatorInfo)
        {
            if (identity != null)
                DeluxeIdentity.CurrentUser.FillCreatorInfo(creatorInfo);
        }

        /// <summary>
        /// 根据User填充具有IEntityWithModifier的对象
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="modifierInfo"></param>
        public static void FillModifierInfo(this DeluxeIdentity identity, IEntityWithModifier modifierInfo)
        {
            if (identity != null)
                DeluxeIdentity.CurrentUser.FillModifierInfo(modifierInfo);
        }
        #endregion Fill User系列

        #region 功能和岗位
        /// <summary>
        /// 得到用户所有的功能
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static HashSet<string> AllFunctions(this IUser user)
        {
            HashSet<string> result = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            lock (user.Properties)
            {
                result = (HashSet<string>)user.Properties["PPTSFunctions"];

                if (result == null)
                {
                    result = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                    PPTSJobCollection jobs = user.Jobs();

                    foreach (PPTSJob job in jobs)
                    {
                        foreach (string function in job.Functions)
                        {
                            if (result.Contains(function) == false)
                                result.Add(function);
                        }
                    }

                    user.Properties.Add("PPTSFunctions", result);
                }
            }

            return result;
        }

        /// <summary>
        /// 获取user的岗位。会缓存在user的Properties中。
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static PPTSJobCollection Jobs(this IUser user)
        {
            user.NullCheck("user");

            PPTSJobCollection result = null;

            lock (user.Properties)
            {
                result = (PPTSJobCollection)user.Properties["PPTSJobs"];

                if (result == null)
                {
                    result = user.MemberOf.ToJobs();
                    user.Properties.Add("PPTSJobs", result);
                }
            }

            return result;
        }

        /// <summary>
        /// 从当前的HttpHeader中当前的岗位
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static PPTSJob GetCurrentJob(this IUser user)
        {
            user.NullCheck("user");

            PPTSJob result = null;

            if (EnvironmentHelper.Mode == InstanceMode.Web)
            {
                string jobID = HttpContext.Current.Request.Headers.GetValue(PPTSJob.JobHeaderTag, string.Empty);

                if (jobID.IsNotEmpty())
                    result = user.Jobs()[jobID];
            }

            return result;
        }

        /// <summary>
        /// 岗位转换为组
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public static IGroup ToGroup(this PPTSJob job)
        {
            IGroup result = null;

            if (job != null)
                result = OguMechanismFactory.GetMechanism().GetObjects<IGroup>(SearchOUIDType.Guid, job.ID).SingleOrDefault();

            return result;
        }

        /// <summary>
        /// 人员上的PPTS的角色
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static PPTSRoleCollection PPTSRoles(this IUser user)
        {
            PPTSRoleCollection pptsRoles = null;

            if (user != null)
            {
                RoleCollection roles = user.Roles[ConnectionDefine.PPTSApplicationName];

                pptsRoles = roles.ToPPTSRoles();
            }
            else
                pptsRoles = new PPTSRoleCollection();

            return pptsRoles;
        }

        /// <summary>
        /// 群组转换为岗位
        /// </summary>
        /// <param name="groups"></param>
        /// <returns></returns>
        public static PPTSJobCollection ToJobs(this IEnumerable<IGroup> groups)
        {
            PPTSJobCollection jobs = new PPTSJobCollection();

            foreach (IGroup group in groups)
            {
                if (group.Properties.GetValue("GroupType", GroupType.Normal) == GroupType.Job)
                    jobs.Add(group.ToJob());
            }

            return jobs;
        }

        /// <summary>
        /// 组转换为岗位
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public static PPTSJob ToJob(this IGroup group)
        {
            group.NullCheck("group");

            PPTSJob job = new PPTSJob()
            {
                ID = group.ID,
                Name = group.Name,
                JobType = group.GetJobType(),
                IsPrimary= group.Properties.GetValue("IsPrimary", false),
                JobName= group.Name,
                ParentOrganizationID = group.Properties.GetValue("PARENT_GUID", string.Empty)
            };

            string abbr = GetDataScopeUltraAbbr(group.Parent);

            if (abbr.IsNotEmpty())
                job.Name = string.Format("{0} - {1}", job.Name, abbr);

            string[] functions = group.Properties.GetValue("GroupFunctions", string.Empty).Split(',');

            job.Functions = new HashSet<string>(functions, StringComparer.OrdinalIgnoreCase);

            return job;
        }

        private static JobTypeDefine GetJobType(this IGroup group)
        {
            JobTypeDefine result = JobTypeDefine.Unknown;

            EnumItemDescriptionList desps = EnumItemDescriptionAttribute.GetDescriptionList(typeof(JobTypeDefine));

            foreach (EnumItemDescription item in desps)
            {
                if (item.Filter.IsNotEmpty())
                {
                    if (group.Name.IndexOf(item.Filter, 0, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        result = (JobTypeDefine)Enum.Parse(typeof(JobTypeDefine), item.EnumValue.ToString());
                        break;
                    }
                }
            }

            return result;
        }

        private static string GetShortNameLastPart(string shortName)
        {
            string result = shortName;

            string[] parts = shortName.Split('-');

            if (parts.Length > 1)
                result = parts[1];

            return result;
        }

        /// <summary>
        /// 权限中心的角色集合转换为PPTS的角色
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public static PPTSRoleCollection ToPPTSRoles(this IEnumerable<IRole> roles)
        {
            PPTSRoleCollection pptsRoles = new PPTSRoleCollection();

            roles.ForEach(role => pptsRoles.Add(role.ToPPTSRole()));

            return pptsRoles;
        }

        /// <summary>
        /// 转换为PPTS的角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static PPTSRole ToPPTSRole(this IRole role)
        {
            role.NullCheck("role");

            return new PPTSRole()
            {
                ID = role.ID,
                CodeName = role.CodeName,
                Name = role.Name
            };
        }

        /// <summary>
        /// 得到岗位所属数据范围的缩写
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public static string GetDataScopeUltraAbbr(this PPTSJob job)
        {
            return job.Organization().GetDataScopeUltraAbbr();
        }

        /// <summary>
        /// 得到岗位所属数据范围的缩写
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public static string GetDataScopeAbbr(this PPTSJob job)
        {
            return job.Organization().GetDataScopeAbbr();
        }

        /// <summary>
        /// 得到组织的缩写
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        public static string GetDataScopeUltraAbbr(this IOrganization org)
        {
            string result = string.Empty;
            string shortName = string.Empty;

            EnumItemDescriptionList desps = EnumItemDescriptionAttribute.GetDescriptionList(typeof(DepartmentType));

            while (org != null)
            {
                if (org.IsAbbrOrg(desps))
                {
                    shortName = org.Properties.GetValue("ShortName", string.Empty);

                    if (shortName.IsNotEmpty())
                        break;
                }

                org = org.Parent.GetUpperDataScope();
            }

            if (shortName.IsNotEmpty())
                result = GetShortNameLastPart(shortName);

            return result;
        }

        /// <summary>
        /// 得到数据范围组织的缩写
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        public static string GetDataScopeAbbr(this IOrganization org)
        {
            string shortName = string.Empty;

            EnumItemDescriptionList desps = EnumItemDescriptionAttribute.GetDescriptionList(typeof(DepartmentType));

            while (org != null)
            {
                if (org.IsDataScope(desps))
                {
                    shortName = org.Properties.GetValue("ShortName", string.Empty);

                    if (shortName.IsNotEmpty())
                        break;
                }

                org = org.Parent.GetUpperDataScope();
            }

            return shortName;
        }
        #endregion 功能和岗位

        #region 部门类型和数据范围
        /// <summary>
        /// 根据PPTS的组织类型得到下一级的组织
        /// </summary>
        /// <param name="root"></param>
        /// <param name="deptType"></param>
        /// <returns></returns>
        public static List<IOrganization> GetChildOrganizationsByPPTSType(this IOrganization root, DepartmentType deptType)
        {
            List<IOrganization> children = new List<IOrganization>();

            foreach (IOrganization org in root.QueryChildren<IOrganization>("@SearchAll@", false, SearchLevel.OneLevel, int.MaxValue))
            {
                if (org.PPTSDepartmentType() == deptType)
                    children.Add(org);
            }

            return children;
        }

        /// <summary>
        /// 组织上的PPTS的部门类型
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        public static DepartmentType PPTSDepartmentType(this IOrganization org)
        {
            DepartmentType deptType = DepartmentType.None;

            if (org != null)
                deptType = org.Properties.GetValue("DepartmentType", DepartmentType.None);

            return deptType;
        }

        /// <summary>
        /// 得到岗位所属的组织
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public static IOrganization Organization(this PPTSJob job)
        {
            IOrganization org = null;

            if (job != null && job.ParentOrganizationID.IsNotEmpty())
                org = OguMechanismFactory.GetMechanism().GetObjects<IOrganization>(SearchOUIDType.Guid, job.ParentOrganizationID).SingleOrDefault();

            return org;
        }

        /// <summary>
        /// 得到岗位涉及到的数据范围ID（向上遍历部门，找到是数据范围的第一个部门）
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public static string GetDataScopeID(this PPTSJob job)
        {
            string result = string.Empty;

            if (job != null)
            {
                EnumItemDescriptionList desps = EnumItemDescriptionAttribute.GetDescriptionList(typeof(DepartmentType));

                job.Organization().ProbeParents(org =>
                {
                    if (org.IsDataScope(desps))
                        result = org.ID;

                    return result.IsNullOrEmpty();
                });
            }

            return result;
        }

        /// <summary>
        /// 得到上层组织中的第一个DataScope
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static IOrganization GetUpperDataScope(this IOrganization root)
        {
            IOrganization result = null;

            EnumItemDescriptionList desps = EnumItemDescriptionAttribute.GetDescriptionList(typeof(DepartmentType));

            root.ProbeParents(org =>
            {
                if (org.IsDataScope(desps))
                    result = org;

                return result == null;
            });

            return result;
        }

        /// <summary>
        /// 根据岗位以及部门的类型得到部门。如果岗位为空，或者找不到指定类型的部门，则返回null
        /// </summary>
        /// <param name="job"></param>
        /// <param name="targetDeptType"></param>
        /// <returns></returns>
        public static IOrganization GetParentOrganizationByType(this PPTSJob job, DepartmentType targetDeptType)
        {
            IOrganization result = null;

            if (job != null)
            {
                job.Organization().ProbeParents(org =>
                {
                    DepartmentType deptType = org.Properties.GetValue("DepartmentType", DepartmentType.None);

                    if (deptType == targetDeptType)
                        result = org;

                    return result == null;
                });
            }

            return result;
        }

        /// <summary>
        /// 从某一级组织开始向上爬，得到所有的涉及到数据范围的组织，包括校区、分公司、校区。返回集合的次序从最深层次的组织开始。
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static IList<IOrganization> GetAllDataScopeParents(this IOrganization current)
        {
            List<IOrganization> result = new List<IOrganization>();

            EnumItemDescriptionList desps = EnumItemDescriptionAttribute.GetDescriptionList(typeof(DepartmentType));

            current.ProbeParents(org =>
            {
                if (org.IsDataScope(desps))
                    result.Add(org);
            });

            return result;
        }

        /// <summary>
        /// 该部门是不是数据范围
        /// </summary>
        /// <param name="org"></param>
        /// <param name="desps"></param>
        /// <returns></returns>
        public static bool IsDataScope(this IOrganization org, EnumItemDescriptionList desps = null)
        {
            if (desps == null)
                desps = EnumItemDescriptionAttribute.GetDescriptionList(typeof(DepartmentType));

            DepartmentType deptType = org.Properties.GetValue("DepartmentType", DepartmentType.None);

            return desps.IsDataScope(deptType);
        }

        public static bool IsAbbrOrg(this IOrganization org, EnumItemDescriptionList desps = null)
        {
            if (desps == null)
                desps = EnumItemDescriptionAttribute.GetDescriptionList(typeof(DepartmentType));

            DepartmentType deptType = org.Properties.GetValue("DepartmentType", DepartmentType.None);

            return desps.IsAbbrOrg(deptType);
        }
        #endregion 部门类型和数据范围

        #region 组织机构扩展
        public static IOrganization ToBaseOrganization(this PPTSOrganization org)
        {
            IOrganization result = null;
            if (org != null)
                result = OguMechanismFactory.GetMechanism().GetObjects<IOrganization>(SearchOUIDType.Guid, org.ID).SingleOrDefault();
            return result;
        }
        #endregion 组织机构扩展

        #region 人员扩展
        //public static IUser ToBaseUser(this PPTSUser user)
        //{
        //    IUser result = null;
        //    if (user != null)
        //    {
        //        OguObjectCollection<IUser> users = OguMechanismFactory.GetMechanism().GetObjects<IUser>(SearchOUIDType.Guid, user.ID);
        //        result = OguMechanismFactory.GetMechanism().GetObjects<IUser>(SearchOUIDType.Guid, user.ID).FirstOrDefault();
        //    }
        //    return result;
        //}

        //public static PPTSJobCollection Jobs(this PPTSUser user)
        //{
        //    IUser userBase = user.ToBaseUser();
        //    return userBase.Jobs();
        //}
        #endregion 人员扩展

        private static IOrganization ProbeParents(this IOrganization org, Action<IOrganization> action)
        {
            IOrganization originalOrg = org;

            while (org != null && action != null)
            {
                action(org);

                org = org.Parent;
            }

            return originalOrg;
        }

        private static IOrganization ProbeParents(this IOrganization org, Func<IOrganization, bool> func)
        {
            IOrganization originalOrg = org;

            while (org != null && func != null)
            {
                if (func(org) == false)
                    break;

                org = org.Parent;
            }

            return originalOrg;
        }

        private static bool IsDataScope(this EnumItemDescriptionList desps, DepartmentType deptType)
        {
            EnumItemDescription desp = desps.FirstOrDefault(d => d.EnumValue == (int)deptType);

            bool result = false;

            if (desp != null)
                result = desp.Category == "DataScope";

            return result;
        }

        private static bool IsAbbrOrg(this EnumItemDescriptionList desps, DepartmentType deptType)
        {
            EnumItemDescription desp = desps.FirstOrDefault(d => d.EnumValue == (int)deptType);

            bool result = false;

            if (desp != null)
                result = deptType == DepartmentType.Branch || deptType == DepartmentType.Campus || deptType == DepartmentType.Region;

            return result;
        }
    }
}
