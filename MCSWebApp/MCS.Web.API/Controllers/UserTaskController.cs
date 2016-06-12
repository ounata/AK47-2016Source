using MCS.Library.Core;
using MCS.Library.OGUPermission;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects;
using MCS.Web.MVC.Library.ApiCore;
using MCS.Web.MVC.Library.Filters;
using MCS.Web.MVC.Library.Models;
using MCS.Web.MVC.Library.Models.UserTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MCS.Web.API.Controllers
{
    [ApiPassportAuthentication]
    public class UserTaskController : ApiController
    {
        
    }
}