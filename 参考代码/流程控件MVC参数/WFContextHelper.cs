using MCS.Library.Core;
using MCS.Library.WF.Contracts.Ogu;
using MCS.Library.WF.Contracts.Workflow.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CIIC.HSR.TSP.WF.UI.Control.Interfaces
{
    /// <summary>
    /// 流程上下文创建帮助
    /// </summary>
    public static class WFContextHelper
    {
        private const string KeyInWFContext = "WFContext";

        /// <summary>
        /// 根据请求上下文创建流程上下文
        /// </summary>
        /// <param name="request">请求</param>
        /// <returns>流程上下文</returns>
        public static WFUIRuntimeContext GetWFContext(this HttpRequestBase request)
        {
            WFUIRuntimeContext result = null;

            if (request.RequestContext.HttpContext.Items.Contains(KeyInWFContext) == false)
            {
                result = WFUIRuntimeContext.InitByHttpRequest(request);

                request.RequestContext.HttpContext.Items[KeyInWFContext] = result;
            }
            else
                result = (WFUIRuntimeContext)request.RequestContext.HttpContext.Items[KeyInWFContext];

            return result;
        }

        /// <summary>
        /// 根据请求上下文创建流程上下文
        /// </summary>
        /// <param name="request">请求</param>
        /// <returns>流程上下文</returns>
        public static WFUIRuntimeContext GetWFContext(this HttpRequest request)
        {
            WFUIRuntimeContext result = null;

            if (request.RequestContext.HttpContext.Items.Contains(KeyInWFContext) == false)
            {
                result = WFUIRuntimeContext.InitByHttpRequest(request.QueryString);

                request.RequestContext.HttpContext.Items[KeyInWFContext] = result;
            }
            else
                result = (WFUIRuntimeContext)request.RequestContext.HttpContext.Items[KeyInWFContext];

            return result;
        }

        /// <summary>
        /// 如果已经存在流程上下文，则重新刷新流程上下文
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static WFUIRuntimeContext ReloadWFContext(this HttpRequestBase request)
        {
            WFUIRuntimeContext currentContext = GetWFContext(request);

            if (currentContext.Process != null)
            {
                if (currentContext.Process.CurrentActivity != null)
                    currentContext = WFUIRuntimeContext.InitByActivityID(currentContext.Process.CurrentActivity.ID);
                else
                    currentContext = WFUIRuntimeContext.InitByProcessID(currentContext.Process.ID);

                request.RequestContext.HttpContext.Items[KeyInWFContext] = currentContext;
            }

            return currentContext;
        }

        /// <summary>
        /// 是否能够撤回
        /// </summary>
        /// <param name="runtime"></param>
        /// <returns></returns>
        public static bool CanWithdraw(this WFUIRuntimeContext runtime)
        {
            bool result = false;

            if (runtime != null && runtime.Process != null && runtime.Process.CurrentActivity != null)
            {
                result = runtime.Process.CanWithdraw;

                if (result)
                {
                    if (runtime.Process.AuthorizationInfo.IsProcessAdmin == false)
                    {
                        result = runtime.Process.CurrentActivity.Descriptor.Properties.GetValue("AllowToBeWithdrawn", true) &&
                           runtime.Process.PreviousActivity.Descriptor.Properties.GetValue("AllowWithdraw", true);

                        if (result)
                        {
                            //不是管理员，进行更严格的权限判断(前一个点的操作人是我)
                            result = runtime.Process.PreviousActivity.Operator.IsNotNullOrEmpty() &&
                                string.Compare(runtime.Process.PreviousActivity.Operator.ID, runtime.CurrentUser.ID, true) == 0;
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 是否能够作废
        /// </summary>
        /// <param name="runtime"></param>
        /// <returns></returns>
        public static bool CanCancel(this WFUIRuntimeContext runtime)
        {
            bool result = false;

            if (runtime != null && runtime.Process != null)
            {
                result = runtime.Process.CanCancel;

                if (result)
                {
                    //当前环节允许的话，要看是否是待办人或者流程已经办结
                    result = ((runtime.Process.AuthorizationInfo.InMoveToMode || runtime.Process.Status == WfClientProcessStatus.Completed) &&
                        runtime.Process.CurrentActivity.Descriptor.Properties.GetValue("AllowAbortProcess", true));

                    if (result == false)
                        result = runtime.Process.AuthorizationInfo.IsProcessAdmin;
                }
            }

            return result;
        }

        /// <summary>
        /// 是否可以暂停
        /// </summary>
        /// <param name="runtime"></param>
        /// <returns></returns>
        public static bool CanPause(this WFUIRuntimeContext runtime)
        {
            bool result = false;

            if (runtime != null && runtime.Process != null)
            {
                result = (runtime.Process.CanPause && runtime.Process.AuthorizationInfo.IsProcessAdmin);
            }

            return result;
        }

        /// <summary>
        /// 是否可以继续（取消暂停）
        /// </summary>
        /// <param name="runtime"></param>
        /// <returns></returns>
        public static bool CanResume(this WFUIRuntimeContext runtime)
        {
            bool result = false;

            if (runtime != null && runtime.Process != null)
            {
                result = (runtime.Process.CanResume && runtime.Process.AuthorizationInfo.IsProcessAdmin);
            }

            return result;
        }

        /// <summary>
        /// 是否可以还原流程
        /// </summary>
        /// <param name="runtime"></param>
        /// <returns></returns>
        public static bool CanRestore(this WFUIRuntimeContext runtime)
        {
            bool result = false;

            if (runtime != null && runtime.Process != null)
            {
                result = runtime.Process.CanRestore;

                if (result)
                {
                    //当前环节允许的话，要看是否是待办人或者流程已经办结
                    result = ((runtime.Process.AuthorizationInfo.InMoveToMode || runtime.Process.AuthorizationInfo.IsProcessAdmin));
                }
            }

            return result;
        }

        /// <summary>
        /// 是否可以流转
        /// </summary>
        /// <param name="runtime"></param>
        /// <returns></returns>
        public static bool CanMoveTo(this WFUIRuntimeContext runtime)
        {
            bool result = false;

            if (runtime != null && runtime.Process != null)
            {
                result = runtime.Process.AuthorizationInfo.InMoveToMode ||
                    (runtime.Process.AuthorizationInfo.InMoveToStatus && runtime.Process.AuthorizationInfo.IsProcessAdmin);
            }

            return result;
        }

        /// <summary>
        /// 是否可以保存
        /// </summary>
        /// <param name="runtime"></param>
        /// <returns></returns>
        public static bool CanSave(this WFUIRuntimeContext runtime)
        {
            bool result = false;

            if (runtime != null && runtime.Process != null)
                result = ((runtime.Process.AuthorizationInfo.InMoveToMode &&
                       runtime.Process.CurrentActivity.Descriptor.Properties.GetValue("AllowSave", true)) ||
                       (runtime.Process.AuthorizationInfo.InMoveToStatus && runtime.Process.AuthorizationInfo.IsProcessAdmin));

            return result;
        }
    }
}
