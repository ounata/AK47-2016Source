using MCS.Library.Core;
using MCS.Library.OGUPermission;
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

        public static string GetDataScopeID(this PPTSJob job)
        {
            string result = string.Empty;

            if (job != null)
            {
                IGroup group = OguMechanismFactory.GetMechanism().GetObjects<IGroup>(SearchOUIDType.Guid, job.ID).SingleOrDefault();

                if (group != null)
                {
                    IOrganization org = group.Parent;
                    EnumItemDescriptionList desps = EnumItemDescriptionAttribute.GetDescriptionList(typeof(DepartmentType));

                    while (org != null)
                    {
                        DepartmentType deptType = org.Properties.GetValue("DepartmentType", DepartmentType.None);

                        if (desps.IsDataScope(deptType))
                        {
                            result = org.ID;
                            break;
                        }

                        org = org.Parent;
                    }
                }
            }

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
                Name = group.Name
            };

            string[] functions = group.Properties.GetValue("GroupFunctions", string.Empty).Split(',');

            job.Functions = new HashSet<string>(functions, StringComparer.OrdinalIgnoreCase);

            return job;
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

        private static bool IsDataScope(this EnumItemDescriptionList desps, DepartmentType deptType)
        {
            EnumItemDescription desp = desps.FirstOrDefault(d => d.EnumValue == (int)deptType);

            bool result = false;

            if (desp != null)
                result = desp.Category == "DataScope";

            return result;
        }
    }
}
