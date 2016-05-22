using MCS.Library.OGUPermission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MCS.Library.Core;
using MCS.Library.SOA.DataObjects;

namespace MCS.Web.MVC.Library.Models
{
    public static class ConverterExtentions
    {
        public static void FillChildNodes(this IEnumerable<IOguObject> children, List<UserGraphTreeNode> childNodes, UserGraphControlObjectMask listMask,
            Func<IOguObject, bool> filter = null, Action<IOguObject, UserGraphTreeNode> addedAction = null)
        {
            children.ForEach(child =>
                {
                    if (((int)child.ObjectType & (int)listMask) != 0)
                    {
                        bool canAdd = true;

                        if (filter != null)
                            canAdd = filter(child);

                        if (canAdd)
                        {
                            UserGraphTreeNode treeNode = child.ToTreeNode();

                            if (addedAction != null)
                                addedAction(child, treeNode);

                            childNodes.Add(treeNode);
                        }
                    }
                }
            );
        }

        public static void FillResult(this IEnumerable<IOguObject> objs, List<IOguObject> queryResult, UserGraphControlObjectMask listMask,
            Func<IOguObject, bool> filter = null, Action<IOguObject, IOguObject> addedAction = null)
        {
            objs.ForEach(obj =>
                {
                    if (((int)obj.ObjectType & (int)listMask) != 0)
                    {
                        bool canAdd = true;

                        if (filter != null)
                            canAdd = filter(obj);

                        if (canAdd)
                        {
                            IOguObject newObj = OguBase.CreateWrapperObject(obj);

                            if (addedAction != null)
                                addedAction(obj, newObj);

                            queryResult.Add(newObj);
                        }
                    }
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

                //result.Data = obj.ToWrappedObject();
                result.Data = obj;

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
                treeNode.Open = true;
                treeNode.IconSkin = "user";
            });
        }

        private static void FillOrganizationInfo(this IOrganization obj, UserGraphTreeNode treeNode)
        {
            obj.IsNotNull(org =>
            {
                treeNode.Open = false;
                treeNode.IsParent = true;
                treeNode.IconSkin = treeNode.IsParent ? "depart" : "person";
            });
        }

        private static void FillGroupInfo(this IGroup obj, UserGraphTreeNode treeNode)
        {
            obj.IsNotNull(group =>
            {
                treeNode.Open = true;
                treeNode.IsParent = false;
                treeNode.IconSkin = "user";
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
    }
}