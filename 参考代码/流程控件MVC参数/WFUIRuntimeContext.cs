using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using CIIC.HSR.TSP.IoC;
using CIIC.HSR.TSP.WF.UI.Control.Controls;
using MCS.Library.Core;
using MCS.Library.WF.Contracts.Ogu;
using MCS.Library.WF.Contracts.Proxies;
using MCS.Library.WF.Contracts.Workflow.Runtime;
using MCS.Library.WcfExtensions;
using CIIC.HSR.TSP.WF.Bizlet.Common;
using System.Threading;
using MCS.Library.Passport;
using System.Collections.Specialized;

namespace CIIC.HSR.TSP.WF.UI.Control.Interfaces
{
    /// <summary>
    /// 流程上下文
    /// </summary>
    public class WFUIRuntimeContext
    {
        private WFUIRuntimeContext()
        {
        }

        private WFUIRuntimeContext(string activityID)
        {
            this.ActivityID = activityID;
        }

        internal static WFUIRuntimeContext InitByHttpRequest(HttpRequestBase request)
        {
            return InitByHttpRequest(request.QueryString);
        }

        internal static WFUIRuntimeContext InitByHttpRequest(NameValueCollection queryString)
        {
            string tenantCode = GetCurrentTenantCode();

            if (tenantCode.IsNullOrEmpty())
                tenantCode = TenantContext.Current.TenantCode;

            TenantContext.Current.TenantCode = tenantCode;

            WfClientServiceBrokerContext.Current.Context[Consts.Culture] = Thread.CurrentThread.CurrentCulture.Name;

            if (PrincipaContextAccessor.GetPrincipalInContext<IGenericTokenPrincipal, WfClientServiceBrokerContext>(WfClientServiceBrokerContext.Current) == null)
                PrincipaContextAccessor.SetPrincipalInContext(WfClientServiceBrokerContext.Current, new GenericTicketPrincipal(GetCurrentUser()));

            WFUIRuntimeContext result = null;

            if (queryString["activityID"] != null)
                result = InitByActivityID(queryString["activityID"]);
            else
                if (queryString["processID"] != null)
                    result = InitByProcessID(queryString["processID"]);
                else
                    if (queryString["resourceID"] != null)
                        result = InitByResourceID(queryString["resourceID"]);
                    else
                        result = InitWithoutProcessInfo();

            return result;
        }

        internal static WFUIRuntimeContext InitByActivityID(string activityID)
        {
            WFUIRuntimeContext result = new WFUIRuntimeContext(activityID);

            result.CurrentUser = GetCurrentUser();
            result.TenantCode = GetCurrentTenantCode();
            result.Process = WfClientProcessRuntimeServiceProxy.Instance.GetProcessInfoByActivityID(activityID, result.CurrentUser);

            return result;
        }

        internal static WFUIRuntimeContext InitByProcessID(string processID)
        {
            WfClientUser user = GetCurrentUser();

            WfClientProcessInfo processInfo = WfClientProcessRuntimeServiceProxy.Instance.GetProcessInfoByID(processID, user);

            WFUIRuntimeContext result = new WFUIRuntimeContext(processInfo.CurrentActivity.ID);

            result.TenantCode = GetCurrentTenantCode();
            result.Process = processInfo;
            result.CurrentUser = user;

            return result;
        }

        private static WFUIRuntimeContext InitByResourceID(string resourceID)
        {
            WfClientUser user = GetCurrentUser();

            WfClientProcessInfoCollection processesInfo = WfClientProcessRuntimeServiceProxy.Instance.GetProcessInfoByResourceID(resourceID, user);

            (processesInfo.Count > 0).FalseThrow("不能根据'{0}'找到ResourceID对应的流程", resourceID);

            WfClientProcessInfo processInfo = processesInfo.Find(p =>
            {
                return p.HasParentProcess == false;
            });

            if (processInfo == null)
                processInfo = processesInfo[0];

            WFUIRuntimeContext result = new WFUIRuntimeContext(processInfo.CurrentActivity.ID);

            result.TenantCode = GetCurrentTenantCode();
            result.Process = processInfo;
            result.CurrentUser = user;

            return result;
        }

        private static WFUIRuntimeContext InitWithoutProcessInfo()
        {
            WFUIRuntimeContext result = new WFUIRuntimeContext();

            result.CurrentUser = GetCurrentUser();
            result.TenantCode = GetCurrentTenantCode();

            return result;
        }

        private static WfClientUser GetCurrentUser()
        {
            WfClientUser user = null;

            IWFUserContext userContext = Containers.Global.Singleton.Resolve<IWFUserContext>();

            if (null != userContext)
                user = userContext.GetUser().WfClientUser;

            return user;
        }

        private static string GetCurrentTenantCode()
        {
            string tenantCode = null;

            IWFUserContext userContext = Containers.Global.Singleton.Resolve<IWFUserContext>();

            if (null != userContext)
                tenantCode = userContext.GetUser().TenantCode;

            return tenantCode;
        }

        public WfClientUser CurrentUser
        {
            get;
            private set;
        }

        public WfClientProcessInfo Process
        {
            get;
            private set;
        }

        public string TenantCode
        {
            get;
            private set;
        }

        /// <summary>
        /// 节点ID
        /// </summary>
        public string ActivityID
        {
            get;
            set;
        }
    }
}
