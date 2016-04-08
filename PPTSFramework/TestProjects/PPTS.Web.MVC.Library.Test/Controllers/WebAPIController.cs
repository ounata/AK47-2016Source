using MCS.Library.Core;
using MCS.Library.Principal;
using MCS.Web.MVC.Library.Filters;
using PPTS.Data.Common.Security;
using PPTS.Web.MVC.Library.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PPTS.Web.MVC.Library.Test.Controllers
{
    public class WebAPIController : ApiController
    {
        [HttpGet]
        [PPTSFunctionAuthorize("PPTS:查看客户（不含联系方式）,学员列表查看")]
        [ApiPassportAuthentication]
        public string ActionNeedFunctions()
        {
            return "I have functions";
        }

        [HttpGet]
        [PPTSJobFunctionAuthorize("PPTS:查看班组产品,学员列表查看")]
        [ApiPassportAuthentication]
        public string ActionNeedJobFunctions()
        {
            return "I have job functions";
        }

        [HttpGet]
        [ApiPassportAuthentication]
        public string GetDataScopeID()
        {
            return DeluxeIdentity.CurrentUser.GetCurrentJob().GetDataScopeID();
        }
    }
}
