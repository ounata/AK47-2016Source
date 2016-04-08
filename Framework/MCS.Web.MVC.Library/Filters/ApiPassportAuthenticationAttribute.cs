using MCS.Library.Core;
using MCS.Library.Principal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Threading;
using System.Web;

namespace MCS.Web.MVC.Library.Filters
{
    /// <summary>
    /// 为Web API进行认证的Filter
    /// </summary>
    public class ApiPassportAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        public ApiPassportAuthenticationAttribute()
        {
            this.Enabled = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enabled"></param>
        public ApiPassportAuthenticationAttribute(bool enabled)
        {

            this.Enabled = enabled;
        }

        public bool AllowMultiple
        {
            get
            {
                return false;
            }
        }

        public bool Enabled
        {
            get;
            set;
        }

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            if (this.Enabled)
            {
                IPrincipal principal = DeluxePrincipal.CreateByRequest(false);

                if (principal != null)
                    context.Principal = principal;
                else
                    context.ErrorResult = new PassportAuthenticationFailureResult("Need Passport Authentication", context.Request);
            }

            return Task.FromResult(0);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        //public override void OnAuthorization(HttpActionContext actionContext)
        //{
        //    if (this.Enabled)
        //    {
        //        IPrincipal principal = DeluxePrincipal.CreateByRequest();

        //        if (principal == null)
        //        {
        //            actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
        //            {
        //                Content = new StringContent(JsonConvert.SerializeObject(new { number = -1, description = "需要认证", stackTrace = string.Empty })),
        //            };
        //        }
        //    }
        //}
    }
}
