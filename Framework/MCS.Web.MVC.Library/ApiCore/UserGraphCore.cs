using MCS.Library.Core;
using MCS.Library.OGUPermission;
using MCS.Web.MVC.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Web.MVC.Library.ApiCore
{
    /// <summary>
    /// 用户和组织架构控件的API的内核
    /// </summary>
    public static class UserGraphCore
    {
        /// <summary>
        /// 得到根组织，如果传入的根组织路径为空，则返回系统组织架构树的根
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="requestParams"></param>
        /// <returns></returns>
        public static UserGraphTreeNode GetRoot(string fullPath, UserGraphTreeParams requestParams)
        {
            UserGraphTreeNode rootNode = null;

            requestParams.DoServiceCall((rp) =>
            {
                IOrganization parent = null;

                if (fullPath.IsNotEmpty())
                {
                    parent = OguMechanismFactory.GetMechanism().GetObjects<IOrganization>(SearchOUIDType.FullPath, fullPath).SingleOrDefault();

                    (parent != null).FalseThrow("不能找到路径为\"{0}\"的组织", fullPath);
                }
                else
                    parent = OguMechanismFactory.GetMechanism().GetRoot();

                rootNode = parent.ToTreeNode();

                parent.Children.FillChildNodes(rootNode.Children, rp.ListMask);
                rootNode.Open = true;
            }
            );

            return rootNode;
        }

        /// <summary>
        /// 得到某一级组织的子对象
        /// </summary>
        /// <param name="parentID"></param>
        /// <param name="requestParams"></param>
        /// <returns></returns>
        public static List<UserGraphTreeNode> GetChildren(UserGraphTreeParams requestParams)
        {
            List<UserGraphTreeNode> childNodes = new List<UserGraphTreeNode>();

            requestParams.DoServiceCall(rp =>
            {
                IOrganization parent = OguMechanismFactory.GetMechanism().GetObjects<IOrganization>(SearchOUIDType.Guid, requestParams.Id).SingleOrDefault();

                (parent != null).FalseThrow("不能找到ID为\"{0}\"的组织", requestParams.Id);

                parent.Children.FillChildNodes(childNodes, requestParams.ListMask);
            });

            return childNodes;
        }
    }
}
