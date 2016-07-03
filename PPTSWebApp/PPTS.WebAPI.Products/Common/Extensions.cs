using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PPTS.Data.Common.Security;
using MCS.Library.Passport;

namespace PPTS.WebAPI.Products.Common
{
    public static class Extensions
    {
        public static string[] PermisstionFilter(this MCS.Library.OGUPermission.IUser currentUser, string[] campusIDs)
        {
            //权限 开关
            if (!RolesDefineConfig.GetConfig().Enabled) {
                if (campusIDs == null || campusIDs.Length < 1) { return null; }
                return campusIDs;
            }

            var campusRange = new List<string>();

            var currentJob = currentUser.GetCurrentJob();
            var io = PPTS.Data.Common.OGUExtensions.GetOrganizationByID(currentJob.GetDataScopeID());
            if(io.PPTSDepartmentType() == DepartmentType.HQ)
            {
                if (campusIDs==null || campusIDs.Length < 1) { return null; }
                return campusIDs;
            }
            else if(io.PPTSDepartmentType() == DepartmentType.Region)
            {
                //大区岗位
                campusRange.AddRange(currentJob.Organization().GetChildOrganizationsByPPTSType(DepartmentType.Region).Select(m => m.ID));
            }
            else
            {
                var functions = currentUser.GetCurrentJob().Functions;

                if (functions.Any(s => s.Contains("本分公司")))
                {
                    campusRange.AddRange(currentJob.Organization().GetChildOrganizationsByPPTSType(DepartmentType.Branch).Select(m => m.ID));
                }
                else if (functions.Any(s => s.Contains("本校区")))
                {
                    campusRange.Add(currentJob.GetParentOrganizationByType(DepartmentType.Campus).ID);
                }
                //else if (functions.Any(s => s.Contains("全国")))
                 //{

                //}
            }
            
            if (campusIDs ==null || campusIDs.Length < 1) {
                if (campusRange.ToArray().Length > 0)
                {
                    return campusRange.ToArray();
                }
            }
            else if (campusRange.Any(s => campusIDs.Contains(s)))
            {
                return campusRange.Where(s => campusIDs.Contains(s)).ToArray();
            }
            return new string[] { "-1" };
        }

        public static string[] PermisstionFilter(this MCS.Library.OGUPermission.IUser currentUser)
        {
            return PermisstionFilter(currentUser, null);
        }

        /// <summary>
        /// 是否 可以国际游学
        /// </summary>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        public static bool HasInternationalYouXue(this MCS.Library.OGUPermission.IUser currentUser)
        {
            //权限 开关
            if (!RolesDefineConfig.GetConfig().Enabled)
            {
                return true;
            }

            var currentJob = currentUser.GetCurrentJob();
            var io = PPTS.Data.Common.OGUExtensions.GetOrganizationByID(currentJob.GetDataScopeID());
            return io.PPTSDepartmentType() == DepartmentType.HQ;

        }

    }
}