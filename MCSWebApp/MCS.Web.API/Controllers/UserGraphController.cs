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
        [HttpGet]
        public UserGraphTreeNode GetRoot()
        {
            return this.GetRoot(new UserGraphTreeParams() { FullPath = String.Empty });
        }

        [HttpPost]
        public UserGraphTreeNode GetRoot(UserGraphTreeParams requestParams)
        {
            return UserGraphCore.GetRoot(requestParams);
        }

        [HttpPost]
        public List<UserGraphTreeNode> GetChildren(UserGraphTreeParams requestParams)
        {
            return UserGraphCore.GetChildren(requestParams);
        }

        [HttpGet]
        public List<IOguObject> Query(string searchTerm)
        {
            UserGraphSearchParams requestParams = new UserGraphSearchParams();

            requestParams.ListMask = UserGraphControlObjectMask.All;
            requestParams.MaxCount = 15;
            requestParams.SearchTerm = searchTerm;

            return UserGraphCore.Query(requestParams);
        }

        [HttpPost]
        public List<IOguObject> Query(UserGraphSearchParams requestParams)
        {
            return UserGraphCore.Query(requestParams);
        }
    }
}