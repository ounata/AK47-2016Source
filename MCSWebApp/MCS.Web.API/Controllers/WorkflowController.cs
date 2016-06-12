using MCS.Library.Core;
using MCS.Library.OGUPermission;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects;
using MCS.Web.MVC.Library.ApiCore;
using MCS.Web.MVC.Library.Filters;
using MCS.Web.MVC.Library.Models;
using MCS.Web.MVC.Library.Models.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using MCS.Web.MVC.Library.ModelBinder;
using MCS.Web.MVC.Library.Models.UserTask;

namespace MCS.Web.API.Controllers
{
    public class WorkflowController : ApiController
    {
        [HttpPost]
        public WFClientProcess GetClientProcess(WfClientSearchParameters searchParams)
        {
            WFClientProcess process = WfClientProxy.GetClientProcess(searchParams.ProcessID);
            return process;
        }

        [HttpPost]
        public WFClientProcess Moveto(WfClientMovetoParameters movetoParams)
        {
            movetoParams.TaskTitle = "请审批" + DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            movetoParams.TaskUrl = "http://localhost/MCSWebApp/MCS.Web.Component/samples/index.html#/workflow-form?pid={0}&aid={1}&rid={2}";
            return WfClientProxy.Moveto(movetoParams);
        }

        [HttpPost]
        public WFClientProcess Cancel(WfClientCancelParameters cancelParams)
        {
            return WfClientProxy.Cancel(cancelParams);
        }

        [HttpPost]
        public void Startup(WfClientStartupParameters startupParames)
        {
            startupParames.ResourceID = Guid.NewGuid().ToString();
            startupParames.TaskTitle = "请审批" + DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            startupParames.TaskUrl = "http://localhost/MCSWebApp/MCS.Web.Component/samples/index.html#/workflow-form?pid={0}&aid={1}&rid={2}";
            WfClientProxy.Startup(startupParames);
        }

        [HttpPost]
        public UserTaskData QueryUsertask(UserTaskSearchParams searchParams)
        {
            //获取启动工作流的用户
            IUser user = GetUser(searchParams.UserLogonName);

            UserTaskCollection usertasks = UserTaskAdapter.Instance.GetUserTasks(UserTaskIDType.SendToUserID, UserTaskFieldDefine.All, new string[] { user.ID });

            return new UserTaskData()
            {
                Data = usertasks.OrderByDescending(a => a.TaskStartTime).Take(20).ToList()
            };
        }

        private IUser GetUser(string userLogonName)
        {
            IUser user = OguMechanismFactory.GetMechanism().GetObjects<IUser>(SearchOUIDType.LogOnName, new string[] { userLogonName }).FirstOrDefault();
            return user;
        }
    }
}