using MCS.Library.Passport;
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
    public abstract class ApiAuthorizeAttributeBase : AuthorizeAttribute
    {
        public ApiAuthorizeAttributeBase()
        {
            this.Enabled = true;
        }

        public ApiAuthorizeAttributeBase(string roles, bool enabled = true)
        {
            this.Roles = roles;
            this.Enabled = enabled;
        }

        public bool Enabled
        {
            get;
            set;
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            bool result = (this.Enabled == false || RolesDefineConfig.GetConfig().Enabled == false);

            if (result == false)
            {
                DeluxePrincipal pricipal = actionContext.RequestContext.Principal as DeluxePrincipal;

                if (pricipal != null)
                    result = this.IsAuthorized(pricipal, this.Roles);
            }

            return result;
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            DeluxePrincipal pricipal = actionContext.RequestContext.Principal as DeluxePrincipal;

            string message = string.Empty;
            if (pricipal == null)
                message = "用户需要认证后才能够判断权限";
            else
                message = "用户没有权限";

            actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                Content = new StringContent(JsonConvert.SerializeObject(new { number = -1, description = message, stackTrace = string.Empty })),
            };
        }

        protected abstract bool IsAuthorized(DeluxePrincipal principal, string roles);
    }
}
