using MCS.Library.OGUPermission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MCS.Library.Core;

namespace MCS.Web.MVC.Library.Models
{
    public static class ConverterExtentions
    {
        public static void FillChildNodes(this IEnumerable<IOguObject> children, List<UserGraphTreeNode> childNodes, UserGraphControlObjectMask listMask)
        {
            children.ForEach(child =>
                {
                    if (((int)child.ObjectType & (int)listMask) != 0)
                        childNodes.Add(child.ToTreeNode());
                }
            );
        }

        public static UserGraphTreeNode ToTreeNode(this IOguObject obj)
        {
            UserGraphTreeNode result = null;

            if (obj != null)
            {
                result = new UserGraphTreeNode();

                result.ID = obj.ID;
                result.Name = obj.DisplayName.IsNotEmpty() ? obj.DisplayName : obj.Name;
                result.FullPath = obj.FullPath;
                result.ObjectType = obj.ObjectType;

                (obj as IUser).FillUserInfo(result);
                (obj as IOrganization).FillOrganizationInfo(result);
                (obj as IGroup).FillGroupInfo(result);
            }

            return result;
        }

        private static void FillUserInfo(this IUser obj, UserGraphTreeNode treeNode)
        {
            obj.IsNotNull(user =>
            {
                treeNode.CodeName = user.LogOnName;
                treeNode.Open = true;
                treeNode.Icon = "user";
            });
        }

        private static void FillOrganizationInfo(this IOrganization obj, UserGraphTreeNode treeNode)
        {
            obj.IsNotNull(org =>
            {
                treeNode.CodeName = obj.Properties.GetValue("CodeName", string.Empty);
                treeNode.Open = false;
                treeNode.IsParent = true;
                treeNode.Icon = "folder";
                treeNode.IconOpen = "folder-open";
                treeNode.IconClose = "folder";
            });
        }

        public static void DoServiceCall(this UserGraphTreeParams requestParams, Action<UserGraphTreeParams> action)
        {
            ServiceBrokerContext.Current.SaveContextStates();
            try
            {
                if (requestParams.ShowDeletedObjects)
                {
                    ServiceBrokerContext.Current.UseLocalCache = false;
                    ServiceBrokerContext.Current.ListObjectCondition = ListObjectMask.All;
                }
                else
                    ServiceBrokerContext.Current.ListObjectCondition = ListObjectMask.Common;

                action(requestParams);
            }
            finally
            {
                ServiceBrokerContext.Current.RestoreSavedStates();
            }
        }

        public static T DoServiceCall<T>(this UserGraphTreeParams requestParams, Func<UserGraphTreeParams, T> func)
        {
            ServiceBrokerContext.Current.SaveContextStates();
            try
            {
                if (requestParams.ShowDeletedObjects)
                {
                    ServiceBrokerContext.Current.UseLocalCache = false;
                    ServiceBrokerContext.Current.ListObjectCondition = ListObjectMask.All;
                }
                else
                    ServiceBrokerContext.Current.ListObjectCondition = ListObjectMask.Common;

                return func(requestParams);
            }
            finally
            {
                ServiceBrokerContext.Current.RestoreSavedStates();
            }
        }

        private static void FillGroupInfo(this IGroup obj, UserGraphTreeNode treeNode)
        {
            obj.IsNotNull(group =>
            {
                treeNode.CodeName = group.Properties.GetValue("CodeName", string.Empty);
                treeNode.Open = true;
                treeNode.IsParent = false;
                treeNode.Icon = "users";
            });
        }
    }
}