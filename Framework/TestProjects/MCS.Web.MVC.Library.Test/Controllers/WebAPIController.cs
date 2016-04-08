using MCS.Library.Principal;
using MCS.Web.MVC.Library.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MCS.Web.MVC.Library.Test.Controllers
{
    public class WebAPIController : ApiController
    {
        [HttpGet]
        [ApiExceptionFilter]
        public string ActionWithException()
        {
            throw new ApplicationException("This is a exception");
        }

        [HttpGet]
        [ApiPassportAuthentication]
        public string ActionNeedAuthenticate()
        {
            return DeluxeIdentity.CurrentUser.DisplayName;
        }

        [HttpGet]
        [ApiRoleAuthorize("PPTS:PPTSAdmin")]
        [ApiPassportAuthentication]
        public string ActionNeedRoles()
        {
            return "Hello world !";
        }
    }
}
