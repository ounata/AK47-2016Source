using MCS.Library.OGUPermission;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects;
using MCS.Web.MVC.Library.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Common;
using PPTS.Data.Common.Security;
using PPTS.WebAPI.Common.Controllers;
using PPTS.WebAPI.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading;

namespace PPTS.WebAPI.Common.Test
{
    [TestClass]
    public class OrganizationControllerTest
    {
        [TestMethod]
        public void GetChildrenByTypeTest()
        {
            IUser user = OguObjectSettings.GetConfig().Objects["hq"].User;

            PPTSJob educatorJob = user.Jobs().Find(job => job.JobType == JobTypeDefine.Educator);

            Assert.IsNotNull(educatorJob);

            IOrganization root = educatorJob.Organization().GetUpperDataScope().Parent;

            Assert.IsNotNull(root);

            OrganizationController controller = PrepareController();

            QueryChildrenParams qp = new QueryChildrenParams("200-Org", DepartmentType.Campus);

            SelectionItemCollection items = controller.GetChildrenByType(qp);

            Console.WriteLine(items.Count);

            Assert.IsTrue(items.Count > 0);

            items.Output();
        }

        [TestMethod]
        public void GetDataScopeRootTest()
        {
            OrganizationController controller = PrepareController();

            UserGraphTreeParams requestParams = new UserGraphTreeParams();
            UserGraphTreeNode treeRootNode = controller.GetDataScopeRoot(requestParams);

            Console.WriteLine(treeRootNode.Name);
            Console.WriteLine(treeRootNode.Children.Count);

            treeRootNode.Output();
            treeRootNode.Children.Output();
        }

        [TestMethod]
        public void GetDataScopeChildrenTest()
        {
            IUser user = OguObjectSettings.GetConfig().Objects["hq"].User;

            PPTSJob educatorJob = user.Jobs().Find(job => job.JobType == JobTypeDefine.Educator);

            Assert.IsNotNull(educatorJob);

            IOrganization root = educatorJob.Organization().GetUpperDataScope().Parent;

            Assert.IsNotNull(root);

            UserGraphTreeParams requestParams = new UserGraphTreeParams() { ID = root.ID };

            OrganizationController controller = PrepareController();

            List<UserGraphTreeNode> childrenNodes = controller.GetDataScopeChildren(requestParams);

            Console.WriteLine(childrenNodes.Count);
            childrenNodes.Output();
        }

        private static OrganizationController PrepareController()
        {
            IUser user = OguObjectSettings.GetConfig().Objects["hq"].User;

            Thread.CurrentPrincipal = new DeluxePrincipal(new DeluxeIdentity(user));

            return new OrganizationController();
        }
    }
}