using MCS.Library.Principal;
using MCS.Web.MVC.Library.Filters;
using PPTS.Data.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Web.MVC.Library.Filters
{
    /// <summary>
    /// 判断用户是否具有某个功能的Filter
    /// </summary>
    public class PPTSFunctionAuthorizeAttribute : ApiAuthorizeAttributeBase
    {
        public PPTSFunctionAuthorizeAttribute() : base()
        {
        }

        public PPTSFunctionAuthorizeAttribute(string permissions, bool enabled = true) :
            base(permissions, enabled)
        {
        }

        protected override bool IsAuthorized(DeluxePrincipal principal, string functions)
        {
            HashSet<string> allFunctions = DeluxeIdentity.CurrentUser.AllFunctions();

            HashSet<string> matchedFunctions = principal.MatchedAllFunctions();

            //将匹配到的功能记录下来
            DeluxePrincipal.ParseRoleDescription(functions, (appName, function) =>
            {
                if (allFunctions.Contains(function))
                    matchedFunctions.Add(function);

                return false;
            });

            return matchedFunctions.Count > 0;
        }
    }
}
