using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PPTS.Data.Common.Security;

namespace PPTS.WebAPI.Orders.Common
{
    public static class Extensions
    {
        public static string[] PermisstionFilter(this MCS.Library.OGUPermission.IUser currentUser, string[] campusIDs)
        {
            var campusRange = new List<string>();
            var functions = currentUser.GetCurrentJob().Functions;

            //if (functions.Any(s => s.Contains("全国")))
            //{

            //}else
            if (functions.Any(s => s.Contains("本分公司")))
            {
                campusRange.AddRange(currentUser.GetCurrentJob().Organization().GetChildOrganizationsByPPTSType(DepartmentType.Branch).Select(m => m.ID));
            }
            else if (functions.Any(s => s.Contains("本校区")))
            {
                campusRange.Add(currentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Campus).ID);
            }
            else if (functions.Any(s => s.Contains("本部门")))
            {
                campusRange.Add(currentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Department).ID);
            }
            

            if (campusIDs == null || campusIDs.Length < 1)
            {
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

    }
}