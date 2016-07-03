using MCS.Library.Core;
using MCS.Web.MVC.Library.ApiCore;
using MCS.Web.MVC.Library.Filters;
using MCS.Web.MVC.Library.Models.Workflow;
using PPTS.WebAPI.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PPTS.WebAPI.Common.Controllers
{
    /// <summary>
    /// 工作流相关的Controller必须加认证
    /// </summary>
    [ApiPassportAuthentication]
    public class WorkflowController : ApiController
    {
        [HttpPost]
        public WfClientProcess GetClientProcess(WfClientSearchParameters searchParams)
        {
            return WfClientProxy.GetClientProcess(searchParams);
        }

        [HttpPost]
        public WfClientProcess Moveto(WfClientMovetoParameters movetoParams)
        {
            return WfClientProxy.Moveto(movetoParams);
        }

        [HttpPost]
        public WfClientProcess Cancel(WfClientCancelParameters cancelParams)
        {
            return WfClientProxy.Cancel(cancelParams);
        }

        [HttpPost]
        public WfClientProcess Startup(WfClientStartupParameters startupParames)
        {
            startupParames.ResourceID = Guid.NewGuid().ToString();

            return WfClientProxy.Startup(startupParames);
        }

        [HttpPost]
        public WfClientProcess Withdraw(WfClientWithdrawParameters withdrawParams)
        {
            return WfClientProxy.Withdraw(withdrawParams);
        }

        [HttpPost]
        public WfClientProcess Save(WfClientSaveParameters saveParams)
        {
            return WfClientProxy.Save(saveParams);
        }

        [HttpPost]
        public dynamic GetForm(WfClientSearchParameters searchParams)
        {
            WfClientProcess clientProcess = WfClientProxy.GetClientProcess(searchParams);

            string subject = clientProcess.ProcessParameters.GetValue("Subject", string.Empty);

            return new
            {
                form = new { title = subject },
                clientProcess = clientProcess
            };
        }

        [HttpPost]
        public WfClientProcess StartupFreeSteps(WfClientStartupFreeStepsParameters startupParams)
        {
            return WfClientProxy.Startup(startupParams, "/PPTSWebApp/PPTS.Portal/#/ppts/workflow/approve");
        }
    }
}