using PPTS.Data.Common.Security;
using PPTS.WebAPI.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MCS.Library.Core;
using MCS.Library.OGUPermission;
using MCS.Web.MVC.Library.Models;
using MCS.Web.MVC.Library.ApiCore;

namespace PPTS.WebAPI.Common.Controllers
{
    public class OrganizationController : ApiController
    {
        [HttpPost]
        public SelectionItemCollection GetChildrenByType(QueryChildrenParams queryParams)
        {
            return QueryChildrenByType(queryParams);
        }

        /// <summary>
        /// 得到带数据范围的根组织。里面列出的组织都是数据范围对象，例如总部、大区、分公司，校区
        /// </summary>
        /// <param name="requestParams"></param>
        /// <returns></returns>
        [HttpPost]
        public UserGraphTreeNode GetDataScopeRoot(UserGraphTreeParams requestParams)
        {
            requestParams.ListMask = UserGraphControlObjectMask.Organization;

            UserGraphTreeNode root = UserGraphCore.GetRoot(requestParams,
                (org) => ((IOrganization)(org)).IsDataScope(),
                (org, treeNode) => (((IOrganization)org).PPTSDepartmentType() == DepartmentType.Campus).TrueFunc(() => treeNode.IsParent = false));

            return root;
        }

        [HttpPost]
        public List<UserGraphTreeNode> GetDataScopeChildren(UserGraphTreeParams requestParams)
        {
            requestParams.ListMask = UserGraphControlObjectMask.Organization;

            List<UserGraphTreeNode> children = UserGraphCore.GetChildren(requestParams, 
                (org) => ((IOrganization)(org)).IsDataScope(),
                (org, treeNode) => (((IOrganization)org).PPTSDepartmentType() == DepartmentType.Campus).TrueFunc(() => treeNode.IsParent = false));

            return children;
        }

        private static SelectionItemCollection QueryChildrenByType(QueryChildrenParams queryParams)
        {
            queryParams.NullCheck("queryParams");
            queryParams.ParentKey.CheckStringIsNullOrEmpty("ParentKey");

            IOrganization root = OguMechanismFactory.GetMechanism().GetObjects<IOrganization>(SearchOUIDType.Guid, queryParams.ParentKey).SingleOrDefault();

            List<IOrganization> orgs = root.GetChildOrganizationsByPPTSType(queryParams.DepartmentType);

            return orgs.ToSelectionItems();
        }
    }
}
