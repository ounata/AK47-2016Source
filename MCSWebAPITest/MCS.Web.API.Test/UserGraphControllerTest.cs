using MCS.Library.OGUPermission;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects;
using MCS.Web.API.Controllers;
using MCS.Web.MVC.Library.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MCS.Web.API.Test
{
    [TestClass]
    public class UserGraphControllerTest
    {
        [TestMethod]
        public void GetRootTest()
        {
            UserGraphController controller = PrepareController();

            UserGraphTreeNode rootNode = controller.GetRoot();

            rootNode.Data.Output();
        }

        [TestMethod]
        public void GetChildrenTest()
        {
            IUser user = OguObjectSettings.GetConfig().Objects["hq"].User;
            UserGraphController controller = PrepareController();

            UserGraphTreeParams requestParams = new UserGraphTreeParams();

            requestParams.ID = user.Parent.ID;
            requestParams.ListMask = UserGraphControlObjectMask.All;

            List<UserGraphTreeNode> childNodes = controller.GetChildren(requestParams);

            childNodes.Select(node => node.Data).Output();
        }

        [TestMethod]
        public void QueryTest()
        {
            IUser user = OguObjectSettings.GetConfig().Objects["hq"].User;
            UserGraphController controller = PrepareController();

            UserGraphSearchParams requestParams = new UserGraphSearchParams();

            requestParams.ListMask = UserGraphControlObjectMask.All;
            requestParams.SearchTerm = user.Name;

            //controller.Query(requestParams).Output();
        }

        private static UserGraphController PrepareController()
        {
            IUser user = OguObjectSettings.GetConfig().Objects["hq"].User;

            Thread.CurrentPrincipal = new DeluxePrincipal(new DeluxeIdentity(user));

            return new UserGraphController();
        }
    }
}
