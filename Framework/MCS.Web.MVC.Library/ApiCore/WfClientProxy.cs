using MCS.Library.OGUPermission;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.Workflow;
using MCS.Web.MVC.Library.Models.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Web.MVC.Library.ApiCore
{
    /// <summary>
    /// 工作流引擎客户端调用代理类
    /// </summary>
    public class WfClientProxy
    {
        /// <summary>
        /// 启动工作流
        /// </summary>
        /// <param name="userLogonName"></param>
        /// <param name="processKey"></param>
        /// <param name="processParameters"></param>
        public static WFClientProcess Startup(WfClientStartupParameters parameters)
        {
            //获取启动工作流的用户
            IUser user = GetUser(parameters.UserLogonName);

            //设置启动参数
            WfProcessStartupParams startupParams = new WfProcessStartupParams();
            startupParams.ProcessDescriptor = WorkflowSettings.GetConfig().GetDescriptorManager().GetDescriptor(parameters.ProcessKey);
            startupParams.Creator = user;
            startupParams.Assignees.Add(user);
            startupParams.ResourceID = parameters.ResourceID.ToString();
            startupParams.AutoCommit = true;

            //设置工作流运行时参数
            SetWorkflowParameters(parameters.ProcessParameters, startupParams.ApplicationRuntimeParameters);

            //准备执行器
            WfStartWorkflowExecutor executor = new WfStartWorkflowExecutor(null, startupParams, null, true);

            //调整待办的url和标题
            executor.PrepareMoveToTasks += (dataContext, tasks) =>
            {
                PrepareUserTasks(tasks, parameters.TaskUrl, parameters.TaskTitle, startupParams.ProcessDescriptor.InitialActivity.Name);
            };

            //添加审批意见
            executor.AfterSaveApplicationData += (dataContext) =>
            {
                PrepareGenericOpinions(dataContext, user, "请领导审批");
            };

            IWfProcess process = executor.Execute();

            return GetClientProcess(process);
        }
        public static WFClientProcess GetClientProcess(IWfProcess process)
        {
            WFClientProcess clientProcess = new WFClientProcess()
            {
                ProcessID = process.ID,
                Activities = new List<WfClientActivity>()
            };

            WfMainStreamActivityDescriptorCollection activityCollection = process.GetMainStreamActivities(false);
            foreach (var item in activityCollection)
            {
                IWfActivity activity = item.Activity.Instance;

                WfClientActivity clientActivity = new WfClientActivity()
                {
                    ActivityID = item.Activity.Instance.ID,
                    ActivityName = item.Activity.Name,
                    IsActive = (item.Activity.Instance.Status == WfActivityStatus.Running),
                    Approvers = (item.Activity.Instance.Candidates.Count > 0 ? item.Activity.Instance.Candidates[0].User.Name : ""),
                    ApproverCount = item.Activity.Instance.Candidates.Count,
                    ActivityScene = item.Activity.Scene
                };
                if (item.Activity.Instance.Candidates.Count > 1) clientActivity.Approvers += "...";
                clientActivity.ApproverList = string.Join(",", item.Activity.Instance.Candidates.Select(a => a.User.Name + "(" + a.User.LogOnName + ")").ToArray());

                clientProcess.Activities.Add(clientActivity);
            }

            return clientProcess;
        }

        public static IUser GetUser(string userLogonName)
        {
            IUser user = OguMechanismFactory.GetMechanism().GetObjects<IUser>(SearchOUIDType.LogOnName, new string[] { userLogonName }).FirstOrDefault();
            return user;
        }

        /// <summary>
        /// 设置工作流运行时参数
        /// </summary>
        /// <param name="processParameters"></param>
        /// <param name="applicationRuntimeParameters"></param>
        private static void SetWorkflowParameters(Dictionary<string, object> processParameters, Dictionary<string, object> applicationRuntimeParameters)
        {
            if (processParameters != null && processParameters.Count > 0)
            {
                foreach (string key in processParameters.Keys)
                {
                    applicationRuntimeParameters[key] = processParameters[key];
                }
            }
        }

        private static void PrepareUserTasks(UserTaskCollection tasks, string taskUrl, string taskTitle, string taskBody)
        {
            tasks.ForEach(task =>
            {
                task.Url = taskUrl;
                task.TaskTitle = taskTitle;
                task.Body = taskBody;
            });
        }

        private static void PrepareGenericOpinions(WfExecutorDataContext dataContext, IUser user, string content)
        {
            GenericOpinion opinion = new GenericOpinion();
            opinion.ID = Guid.NewGuid().ToString();
            opinion.ProcessID = dataContext.CurrentProcess.ID;
            opinion.ActivityID = dataContext.CurrentProcess.Activities[0].ID;
            opinion.ResourceID = dataContext.CurrentProcess.ResourceID;
            opinion.Content = content;
            opinion.IssueDatetime = DateTime.Now;
            opinion.IssuePerson = user;
            opinion.OpinionType = "提交";
            GenericOpinionAdapter.Instance.Update(opinion);
        }
    }
}
