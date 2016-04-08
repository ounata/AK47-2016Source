using MCS.Library.Principal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace MCS.Web.MVC.Library.Filters
{
    public class ApiRoleAuthorizeAttribute : ApiAuthorizeAttributeBase
    {
        public ApiRoleAuthorizeAttribute() : base()
        {
        }

        public ApiRoleAuthorizeAttribute(string roles, bool enabled = true) :
            base(roles, enabled)
        {
        }

        protected override bool IsAuthorized(DeluxePrincipal principal, string roles)
        {
            return principal.IsInRole(roles);
        }
    }
}
