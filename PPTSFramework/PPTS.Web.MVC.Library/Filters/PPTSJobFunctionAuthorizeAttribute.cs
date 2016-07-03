using MCS.Library.Core;
using MCS.Library.Principal;
using MCS.Web.MVC.Library.Filters;
using PPTS.Data.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PPTS.Web.MVC.Library.Filters
{
    /// <summary>
    /// 使用用户的当前岗位进行权限判断的Filter
    /// </summary>
    public class PPTSJobFunctionAuthorizeAttribute : ApiAuthorizeAttributeBase
    {
        private const string JobHeaderTag = "pptsCurrentJobID";

        public PPTSJobFunctionAuthorizeAttribute() : base()
        {
        }

        public PPTSJobFunctionAuthorizeAttribute(string permissions, bool enabled = true) :
            base(permissions, enabled)
        {
        }

        protected override bool IsAuthorized(DeluxePrincipal principal, string functions)
        {
            PPTSJob job = DeluxeIdentity.CurrentUser.GetCurrentJob();

            HashSet<string> matchedFunctions = principal.MatchedJobFunctions();

            if (job != null)
            {
                //将匹配到的功能记录下来
                DeluxePrincipal.ParseRoleDescription(functions, (appName, function) =>
                {
                    if (job.Functions.Contains(function))
                        matchedFunctions.Add(function);

                    return false;
                });
            }

            return matchedFunctions.Count > 0;
        }
    }
}
