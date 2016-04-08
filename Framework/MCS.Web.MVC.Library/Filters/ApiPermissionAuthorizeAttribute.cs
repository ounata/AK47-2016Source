using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Principal;

namespace MCS.Web.MVC.Library.Filters
{
    public class ApiPermissionAuthorizeAttribute : ApiAuthorizeAttributeBase
    {
        public ApiPermissionAuthorizeAttribute() : base()
        {
        }

        public ApiPermissionAuthorizeAttribute(string permissions, bool enabled = true) :
            base(permissions, enabled)
        {
        }

        protected override bool IsAuthorized(DeluxePrincipal principal, string permissions)
        {
            throw new NotImplementedException();
        }
    }
}
