using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace PPTS.ExtServices.UnionPay.Filters
{
    public class POSAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        public bool AllowMultiple
        {
            get
            {
                return false;
            }
        }
        
        public string AuthenPassport
        {
            get;
            set;
        }

        public bool Enable
        {
            get {  return HttpContext.Current.Request.Headers["token"] == ConfigurationManager.AppSettings[this.AuthenPassport].ToString(); }
        }

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            
            if(!Enable)
            {
                context.ErrorResult = new PassportAuthenticationFailureResult();
            }
            return Task.FromResult(0);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}