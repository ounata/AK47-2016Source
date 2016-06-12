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
        /// <param name="parameters"></param>
        /// <returns></returns>
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
            if (process == null) return null;

            WFClientProcess clientProcess = new WFClientProcess()
            {
                ProcessID = process.ID,
                ProcessStatus = process.Status.ToString(),
                Activities = new List<WfClientActivity>(),
                ResourceID = process.ResourceID
            };
            if (process.Status == WfProcessStatus.Completed) clientProcess.ProcessStatusString = "已完成";
            else if (process.Status == WfProcessStatus.Aborted) clientProcess.ProcessStatusString = "已驳回";

            GenericOpinionCollection opinionCollection = GenericOpinionAdapter.Instance.Load(builder =>
            {
                builder.AppendItem("PROCESS_ID", process.ID);
            });

            WfMainStreamActivityDescriptorCollection activityCollection = process.GetMainStreamActivities(false);
            foreach (var item in activityCollection)
            {
                IWfActivity activity = item.Activity.Instance;

                WfClientActivity clientActivity = new WfClientActivity()
                {
                    ActivityID = item.Activity.Instance.ID,
                    ActivityName = item.Activity.Name,
                    ActivityStatus = item.Activity.Instance.Status.ToString(),
                    IsActive = (item.Activity.Instance.Status == WfActivityStatus.Running),
                    Approvers = (item.Activity.Instance.Candidates.Count > 0 ? item.Activity.Instance.Candidates[0].User.Name : ""),
                    ApproverCount = item.Activity.Instance.Candidates.Count,
                    ActivityScene = item.Activity.Scene,
                    ApproverLogonName = (item.Activity.Instance.Candidates.Count > 0 ? item.Activity.Instance.Candidates[0].User.LogOnName : "")
                };
                if (item.Activity.Instance.Candidates.Count > 1) clientActivity.Approvers += "...";
                clientActivity.ApproverList = string.Join(",", item.Activity.Instance.Candidates.Select(a => a.User.Name + "(" + a.User.LogOnName + ")").ToArray());
                if (item.Activity.Instance.Status == WfActivityStatus.Running)
                {
                    clientProcess.CurrentActivity = clientActivity;
                }
                else if (item.Activity.Instance.Status == WfActivityStatus.Completed || item.Activity.Instance.Status == WfActivityStatus.Aborted)
                {
                    var opinion = opinionCollection.Where(o => o.ActivityID == clientActivity.ActivityID).FirstOrDefault();
                    if (opinion != null)
                    {
                        clientActivity.Comment = opinion.Content;
                        clientActivity.Action = opinion.OpinionType;
                        clientActivity.Approver = opinion.IssuePerson.DisplayName;
                        clientActivity.ApprovalTime = opinion.IssueDatetime;
                        clientActivity.ApprovalElapsedTime = FormatElapsedTime(opinion.IssueDatetime - activity.StartTime);
                    }
                }

                clientProcess.Activities.Add(clientActivity);
            }

            return clientProcess;
        }
        public static WFClientProcess GetClientProcess(string processID)
        {
            IWfProcess process = WfRuntime.GetProcessByProcessID(processID);

            return GetClientProcess(process);
        }

        public static WFClientProcess Moveto(WfClientMovetoParameters parameters)
        {
            IUser user = GetUser(parameters.UserLogonName);

            IWfProcess process = WfRuntime.GetProcessByProcessID(parameters.ProcessID);

            IWfActivity activity = process.CurrentActivity;
            if (activity == null || activity.Assignees == null)// || !activity.Assignees.Contains(UserHelper.UserId))
            {
                return null;
            }

            WfTransferParams transferParams = WfTransferParams.FromNextDefaultActivity(process);

            UserTaskCollection userTask = new UserTaskCollection();
            WfMoveToExecutor executor = new WfMoveToExecutor(activity, activity, transferParams);

            //调整待办的url和标题
            executor.PrepareMoveToTasks += (dataContext, tasks) =>
            {
                PrepareUserTasks(tasks, parameters.TaskUrl, parameters.TaskTitle, transferParams.NextActivityDescriptor.Name);
            };

            //opinion
            GenericOpinion opinion = new GenericOpinion();
            opinion.ID = Guid.NewGuid().ToString();
            opinion.ProcessID = process.ID;
            opinion.ActivityID = activity.ID;
            opinion.ResourceID = process.ResourceID;
            opinion.Content = parameters.Comment;
            opinion.IssueDatetime = DateTime.Now;
            opinion.IssuePerson = user;

            if (activity.Descriptor.ActivityType == WfActivityType.InitialActivity)
                opinion.OpinionType = "提交";
            else if (activity.Descriptor.ActivityType == WfActivityType.CompletedActivity)
                opinion.OpinionType = "";
            else if (activity.Descriptor.ActivityType == WfActivityType.NormalActivity)
                opinion.OpinionType = "同意";

            GenericOpinionAdapter.Instance.Update(opinion);

            executor.Execute();

            return GetClientProcess(process.ID);
        }

        public static WFClientProcess Cancel(WfClientCancelParameters parameters)
        {
            IUser user = GetUser(parameters.UserLogonName);

            IWfProcess process = WfRuntime.GetProcessByProcessID(parameters.ProcessID);

            IWfActivity activity = process.CurrentActivity;
            if (activity == null || activity.Assignees == null)// || !activity.Assignees.Contains(UserHelper.UserId))
            {
                return null;
            }

            WfCancelProcessExecutor executor = new WfCancelProcessExecutor(activity, process);

            //opinion
            GenericOpinion opinion = new GenericOpinion();
            opinion.ID = Guid.NewGuid().ToString();
            opinion.ProcessID = process.ID;
            opinion.ActivityID = activity.ID;
            opinion.ResourceID = process.ResourceID;
            opinion.Content = parameters.Comment;
            opinion.IssueDatetime = DateTime.Now;
            opinion.IssuePerson = user;
            opinion.OpinionType = "拒绝";

            GenericOpinionAdapter.Instance.Update(opinion);

            executor.Execute();

            return GetClientProcess(process.ID);
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
                task.Url = string.Format(taskUrl, task.ProcessID, task.ActivityID, task.ResourceID);
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
        private static string FormatElapsedTime(TimeSpan ts)
        {
            if (ts.TotalDays > 1) return ts.ToString("\\Sdd\\天hh\\小\\时mm\\分\\钟ss\\秒").Replace("S0", "").Replace("天0", "天").Replace("时0", "时").Replace("钟0", "钟").Replace("S", "");
            else if (ts.TotalHours > 1) return ts.ToString("\\Shh\\小\\时mm\\分\\钟ss\\秒").Replace("S0", "").Replace("天0", "天").Replace("时0", "时").Replace("钟0", "钟").Replace("S", "");
            else if (ts.TotalMinutes > 1) return ts.ToString("\\Smm\\分\\钟ss\\秒").Replace("S0", "").Replace("天0", "天").Replace("时0", "时").Replace("钟0", "钟").Replace("S", "");
            else if (ts.TotalSeconds > 1) return ts.ToString("\\Sss\\秒").Replace("S0", "").Replace("天0", "天").Replace("时0", "时").Replace("钟0", "钟").Replace("S", "");
            else return "0秒";
        }
    }
}
