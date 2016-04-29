using MCS.Library.Core;
using MCS.Library.OGUPermission;
using MCS.Web.MVC.Library.ApiCore;
using MCS.Web.MVC.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MCS.Web.API.Controllers
{
    public class UserGraphController : ApiController
    {
        public UserGraphTreeNode GetRoot()
        {
            return this.GetRoot(string.Empty, new UserGraphTreeParams());
        }

        [HttpPost]
        public UserGraphTreeNode GetRoot(string fullPath, UserGraphTreeParams requestParams)
        {
            return UserGraphCore.GetRoot(fullPath, requestParams);
        }

        [HttpPost]
        public List<UserGraphTreeNode> GetChildren(UserGraphTreeParams requestParams)
        {
            return UserGraphCore.GetChildren(requestParams);
        }
    }
}