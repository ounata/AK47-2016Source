using System;
using System.Web.Http;
using MCS.Web.MVC.Library.ApiCore;
using MCS.Web.MVC.Library.Filters;
using MCS.Web.MVC.Library.Models.Workflow;
using MCS.Web.API.Models;
using System.Collections.Generic;
using MCS.Library.Principal;

namespace MCS.Web.API.Controllers
{
    /// <summary>
    /// 工作流相关的Controller必须加认证
    /// </summary>
    [ApiPassportAuthentication]
    public class WorkflowController : ApiController
    {
        [HttpGet]
        public string Test()
        {
            return DateTime.Now.ToString();
        }

        [HttpPost]
        public WfClientProcess GetClientProcess(WfClientSearchParameters searchParams)
        {
            WfClientProcess process = WfClientProxy.GetClientProcess(searchParams);
            return process;
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
        public void DynamicProcessSample1()
        {
            //todo 1. 此处可进行业务表单保存，如客服表单，状态为初始的草稿状态

            Dictionary<string, object> p = new Dictionary<string, object>();

            //todo 2. 此处可根据需要存业务表单数据到流程上下文(字典p中)

            //启动工作流
            WfClientProxy.DynamicProcessStartup(
                new WfClientDynamicProcessStartupParameters
                {
                    ProcessParameters = p,
                    ResourceID = Guid.NewGuid().ToString(), //这里用真实的resource id代替
                    TaskTitle = string.Format("客服(咨询)：{0}", "张三"),  //这里根据实际需要调整
                    TaskUrl = "这里用真实的表单view页面地址代替", //例如："/PPTSWebApp/PPTS.Portal/#/ppts/student/student-thaw-view"
                    RuntimeProcessName = "客服(咨询)",
                    InitialActivityDescriptor = new WfClientActivityDescriptorParameters()
                    {
                        ActivityName = "总客服专员", //根据实际需要调整
                        UserResourceList = new List<WfClientUserResourceDescriptorParameters>()
                        {
                            new WfClientUserResourceDescriptorParameters()
                            {
                                User = DeluxeIdentity.CurrentUser   //根据实际需要调整，可以是任何人
                            }
                        }
                    }
                }
            );
        }

        [HttpPost]
        public void DynamicProcessSample2()
        {
            //todo 1. 此处可进行业务表单保存，如客服表单，状态有可能需要更新

            Dictionary<string, object> p = new Dictionary<string, object>();

            //todo 2. 此处可根据需要存业务表单数据到流程上下文(字典p中)

            //流转到下一个人
            WfClientProcess process = WfClientProxy.DynamicProcessMoveto(
                new WfClientDynamicProcessMovetoParameters
                {
                    ProcessParameters = p,
                    ResourceID = "这里用真实的resource id代替",
                    ActivityID = "这里用客户端传上来的真实activityID代替",
                    ProcessID = "这里用客户端传上来的真实processID代替",
                    Comment = "这里用客户端穿上来的真实客服输入的详细文字代替",
                    MovetoActivityDescriptor = new WfClientActivityDescriptorParameters()
                    {
                        ActivityName = "分客服专员", //下一处理人的岗位，根据实际需要调整
                        UserResourceList = new List<WfClientUserResourceDescriptorParameters>()
                        {
                            new WfClientUserResourceDescriptorParameters()
                            {
                                User = new DeluxeIdentity("下一处理人的账号").User   //下一处理人，可以多个
                            }
                        }
                    }
                }
            );
        }

        [HttpPost]
        public void Startup(WfClientStartupParameters startupParames)
        {
            startupParames.ResourceID = Guid.NewGuid().ToString();

            WfClientProxy.Startup(startupParames);
        }
    }
}