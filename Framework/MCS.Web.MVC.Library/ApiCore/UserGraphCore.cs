using MCS.Library.Core;
using MCS.Library.Office.OpenXml.Excel;
using MCS.Library.OGUPermission;
using MCS.Web.MVC.Library.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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
        /// <param name="filter">子对象的过滤器</param>
        /// <param name="addedAction">添加了树节点以后的回调</param>
        /// <returns></returns>
        public static UserGraphTreeNode GetRoot(UserGraphTreeParams requestParams, Func<IOguObject, bool> filter = null, Action<IOguObject, UserGraphTreeNode> addedAction = null)
        {
            UserGraphTreeNode rootNode = null;

            requestParams.DoServiceCall((rp) =>
            {
                IOrganization parent = GetOrganization(requestParams);

                rootNode = parent.ToTreeNode();

                parent.Children.FillChildNodes(rootNode.Children, rp.ListMask, filter, addedAction);
                rootNode.Open = true;
            });

            return rootNode;
        }

        /// <summary>
        /// 得到某一级组织的子对象
        /// </summary>
        /// <param name="requestParams"></param>
        /// <param name="addedAction">添加了树节点以后的回调</param>
        /// <returns></returns>
        public static List<UserGraphTreeNode> GetChildren(UserGraphTreeParams requestParams, Func<IOguObject, bool> filter = null, Action<IOguObject, UserGraphTreeNode> addedAction = null)
        {
            List<UserGraphTreeNode> childNodes = new List<UserGraphTreeNode>();

            requestParams.DoServiceCall(rp =>
            {
                IOrganization parent = GetOrganization(requestParams);

                parent.Children.FillChildNodes(childNodes, requestParams.ListMask, filter, addedAction);
            });

            return childNodes;
        }

        public static List<IOguObject> Query(UserGraphSearchParams requestParams, Func<IOguObject, bool> filter = null, Action<IOguObject, IOguObject> addedAction = null)
        {
            List<IOguObject> childNodes = new List<IOguObject>();

            requestParams.DoServiceCall(rp =>
            {
                IOrganization parent = GetOrganization(requestParams);

                parent.QueryChildren<IOguObject>(
                    requestParams.SearchTerm,
                    true,
                    SearchLevel.SubTree,
                    requestParams.MaxCount).FillResult(childNodes, requestParams.ListMask, filter, addedAction);
            });

            return childNodes;
        }

        /// <summary>
        /// 根据浏览器类型决定是否encode文件名
        /// </summary>
        /// <param name="request"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string EncodeFileNameByBrowser(this HttpRequest request, string fileName)
        {
            string result = fileName;

            if (request != null)
            {
                if (request.Browser.IsBrowser("IE") || request.Browser.IsBrowser("internetexplorer") || request.UserAgent.IndexOf(" edge/", StringComparison.OrdinalIgnoreCase) >= 0)
                    result = HttpUtility.UrlEncode(fileName);
            }
            else
                result = HttpUtility.UrlEncode(fileName);

            return result;
        }

        /// <summary>
        /// WorkBook输出到Excel
        /// </summary>
        /// <param name="wb"></param>
        /// <param name="fileName">文件名称</param>
        /// <returns></returns>
        public static HttpResponseMessage ToResponseMessage(this WorkBook wb, string fileName = "")
        {
            wb.NullCheck("wb");

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);

            MemoryStream stream = new MemoryStream();

            wb.Save(stream);

            result.Content = new StreamContent(stream);

            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/vnd.ms-excel");

            if (fileName.IsNullOrEmpty())
                fileName = "export.xlsx";

            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("Attachment")
            {
                FileName = HttpContext.Current.Request.EncodeFileNameByBrowser(fileName)
            };

            return result;
        }

        private static IOrganization GetOrganization(UserGraphTreeParams requestParams)
        {
            IOrganization org = null;

            if (requestParams.ID.IsNotEmpty())
            {
                org = OguMechanismFactory.GetMechanism().GetObjects<IOrganization>(SearchOUIDType.Guid, requestParams.ID).SingleOrDefault();

                (org != null).FalseThrow("不能找到ID为\"{0}\"的组织", requestParams.ID);
            }
            else
            {
                if (requestParams.FullPath.IsNotEmpty())
                {
                    org = OguMechanismFactory.GetMechanism().GetObjects<IOrganization>(SearchOUIDType.FullPath, requestParams.FullPath).SingleOrDefault();

                    (org != null).FalseThrow("不能找到路径为\"{0}\"的组织", requestParams.FullPath);
                }
                else
                    org = OguMechanismFactory.GetMechanism().GetRoot();
            }

            return org;
        }
    }
}
