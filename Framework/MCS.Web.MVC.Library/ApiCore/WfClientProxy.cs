using MCS.Library.Configuration;
using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.OGUPermission;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.Workflow;
using MCS.Web.MVC.Library.Models.Workflow;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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
        public static WfClientProcess Startup(WfClientStartupParameters parameters)
        {
            //获取启动工作流的用户
            IUser user = DeluxeIdentity.CurrentUser;

            //设置启动参数
            WfProcessStartupParams startupParams = new WfProcessStartupParams();
            startupParams.ProcessDescriptor = WorkflowSettings.GetConfig().GetDescriptorManager().GetDescriptor(parameters.ProcessKey);
            startupParams.Creator = user;
            startupParams.Assignees.Add(user);
            startupParams.ResourceID = parameters.ResourceID.ToString();
            startupParams.AutoCommit = true;
            startupParams.DefaultTaskTitle = parameters.TaskTitle;
            startupParams.DefaultUrl = parameters.TaskUrl;
            startupParams.RuntimeProcessName = parameters.RuntimeProcessName;

            //设置工作流运行时参数
            SetWorkflowParameters(parameters.ProcessParameters, startupParams.ApplicationRuntimeParameters);

            //准备执行器
            WfStartWorkflowExecutor executor = new WfStartWorkflowExecutor(null, startupParams, null, true);

            //调整待办的url和标题
            executor.PrepareMoveToTasks += (dataContext, tasks) =>
            {
                PrepareUserTasks(tasks, null, null, startupParams.ProcessDescriptor.InitialActivity.Name);
            };

            //添加审批意见
            executor.AfterSaveApplicationData += (dataContext) =>
            {
                PrepareStartupOpinions(dataContext, user, "请审批");
            };

            IWfProcess process = executor.Execute();

            return GetClientProcess(process);
        }

        public static WfClientProcess Startup(WfClientStartupFreeStepsParameters parameters, string viewUrl)
        {
            parameters.NullCheck("parameters");
            parameters.Approvers.NullCheck("Approvers");

            (parameters.Approvers.Count > 0).FalseThrow("必须包含至少一个以上的审批人");

            WfProcessDescriptor processDesc = new WfProcessDescriptor();

            processDesc.Key = UuidHelper.NewUuidString();
            processDesc.Name = "自由流程";
            processDesc.ApplicationName = "秘书服务";
            processDesc.ProgramName = "部门通知";

            WfActivityDescriptor initActDesp = new WfActivityDescriptor("Initial", WfActivityType.InitialActivity);
            initActDesp.Name = "起草";
            initActDesp.CodeName = "Initial Activity";
            initActDesp.Properties.SetValue("AutoSendUserTask", false);
            initActDesp.Properties.TrySetValue("AllowWithdraw", true);

            processDesc.Activities.Add(initActDesp);

            foreach (IUser user in parameters.Approvers)
            {
                string key = processDesc.FindNotUsedActivityKey();
                WfActivityDescriptor normalActDesp = new WfActivityDescriptor(key, WfActivityType.NormalActivity);
                normalActDesp.Name = user.DisplayName;
                normalActDesp.CodeName = key;
                normalActDesp.Properties.SetValue("AutoAppendSecretary", true);

                WfUserResourceDescriptor userResourceDesc = new WfUserResourceDescriptor(user);
                normalActDesp.Resources.Add(userResourceDesc);

                processDesc.Activities.Add(normalActDesp);
            }

            WfActivityDescriptor completedActDesp = new WfActivityDescriptor("Completed", WfActivityType.CompletedActivity);
            completedActDesp.Name = "完成";
            completedActDesp.CodeName = "Completed Activity";

            processDesc.Activities.Add(completedActDesp);

            for (int j = 0; j < processDesc.Activities.Count - 1; j++)
            {
                processDesc.Activities[j].ToTransitions.AddForwardTransition(processDesc.Activities[j + 1]);
            }

            WfProcessStartupParams startupParams = new WfProcessStartupParams();

            startupParams.ProcessDescriptor = processDesc;
            startupParams.Creator = DeluxeIdentity.CurrentUser;
            startupParams.Assignees.Add(DeluxeIdentity.CurrentUser);
            startupParams.DefaultTaskTitle = "${Subject}$";
            startupParams.RuntimeProcessName = "${Subject}$";
            startupParams.ResourceID = UuidHelper.NewUuidString();
            startupParams.Department = DeluxeIdentity.CurrentUser.Parent;
            startupParams.DefaultUrl = viewUrl;

            string subject = parameters.Title;

            if (subject.IsNullOrEmpty())
                subject = "自由审批流程";

            startupParams.ApplicationRuntimeParameters["Subject"] = subject;

            WfStartWorkflowExecutor executor = new WfStartWorkflowExecutor(null, startupParams, null, true);

            //添加审批意见
            executor.AfterSaveApplicationData += (dataContext) =>
            {
                PrepareStartupOpinions(dataContext, DeluxeIdentity.CurrentUser, "请审批");
            };

            IWfProcess process = executor.Execute();

            return GetClientProcess(process);
        }

        public static WfClientProcess GetClientProcess(IWfClientSearchParameters parameters)
        {
            IWfProcess process = null;

            if (parameters.ActivityID.IsNotEmpty())
            {
                process = WfRuntime.GetProcessByActivityID(parameters.ActivityID);
            }
            else
            {
                if (parameters.ProcessID.IsNotEmpty())
                    process = WfRuntime.GetProcessByProcessID(parameters.ProcessID);
                else
                {
                    if (parameters.ResourceID.IsNotEmpty())
                        process = WfRuntime.GetProcessByResourceID(parameters.ResourceID).FirstOrDefault();
                }
            }

            return GetClientProcess(process);
        }

        public static WfClientProcess GetClientProcess(IWfProcess process)
        {
            process.NullCheck("process");

            WfClientProcess clientProcess = new WfClientProcess()
            {
                ProcessID = process.ID,
                ProcessStatus = process.Status.ToString(),
                Activities = new List<WfClientActivity>(),
                ResourceID = process.ResourceID
            };

            if (process.Status == WfProcessStatus.Completed)
                clientProcess.ProcessStatusString = "已完成";
            else
            if (process.Status == WfProcessStatus.Aborted)
                clientProcess.ProcessStatusString = "已驳回";

            clientProcess.UISwitches.FillByProcess(process, process.CurrentActivity.ID, DeluxeIdentity.CurrentUser);

            GenericOpinionCollection opinions = GenericOpinionAdapter.Instance.Load(
                builder => builder.AppendItem("RESOURCE_ID", process.ResourceID));

            clientProcess.CurrentOpinion = GetUserActivityOpinion(opinions, process.CurrentActivity, DeluxeIdentity.CurrentUser);

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
                    StartTime = item.Activity.Instance.StartTime
                };

                if (item.Activity.Instance.Candidates.Count > 1)
                    clientActivity.Approvers += "...";

                clientActivity.ApproverList = string.Join(",", item.Activity.Instance.Candidates.Select(a => a.User.Name).ToArray());

                if (item.Activity.Instance.Status == WfActivityStatus.Running)
                {
                    clientProcess.CurrentActivity = clientActivity;
                }
                else if (item.Activity.Instance.Status == WfActivityStatus.Completed || item.Activity.Instance.Status == WfActivityStatus.Aborted)
                {
                    var opinion = opinions.Where(o => o.ActivityID == clientActivity.ActivityID).FirstOrDefault();
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

            foreach (GenericOpinion go in opinions)
            {
                WfClientActivity parentActivity = clientProcess.Activities.Where(a => a.ActivityID == go.ActivityID).FirstOrDefault();
                if (parentActivity != null)
                {
                    WfClientActivityHistory history = new WfClientActivityHistory()
                    {
                        ID = go.ID,
                        ActivityID = go.ActivityID,
                        ActivityName = parentActivity.ActivityName,
                        Action = go.OpinionType,
                        Comment = go.Content,
                        Approver = go.IssuePerson.DisplayName,
                        ApprovalType = parentActivity.ApprovalType,
                        ApprovalTime = go.IssueDatetime,
                        ApprovalElapsedTime = FormatElapsedTime(go.IssueDatetime - parentActivity.StartTime)
                    };
                    clientProcess.ActivityHistories.Add(history);
                }
            }
            clientProcess.ActivityHistories = clientProcess.ActivityHistories.OrderBy(a => a.ApprovalTime).ToList();

            foreach (string key in process.ApplicationRuntimeParameters.Keys)
            {
                clientProcess.ProcessParameters.Add(key, process.ApplicationRuntimeParameters[key]);
            }

            return clientProcess;
        }

        public static WfClientProcess GetClientProcessByProcessID(string processID)
        {
            IWfProcess process = WfRuntime.GetProcessByProcessID(processID);

            return GetClientProcess(process);
        }

        public static WfClientProcess GetClientProcessByActivityID(string activityID)
        {
            IWfProcess process = WfRuntime.GetProcessByActivityID(activityID);

            return GetClientProcess(process);
        }

        public static WfClientProcess Moveto(WfClientMovetoParameters parameters)
        {
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
                PrepareUserTasks(tasks, null, null, transferParams.NextActivityDescriptor.Name);
            };

            GenericOpinion serverOpinion = PrepareOpinion(process.CurrentActivity, parameters.CurrentOpinion, DeluxeIdentity.CurrentUser);

            if (activity.Descriptor.ActivityType == WfActivityType.InitialActivity)
                serverOpinion.OpinionType = "提交";
            else if (activity.Descriptor.ActivityType == WfActivityType.CompletedActivity)
                serverOpinion.OpinionType = string.Empty;
            else if (activity.Descriptor.ActivityType == WfActivityType.NormalActivity)
            {
                serverOpinion.OpinionType = "同意";

                if (serverOpinion.Content.IsNullOrEmpty())
                    serverOpinion.Content = "同意";
            }

            executor.PrepareApplicationData += dataContext => GenericOpinionAdapter.Instance.Update(serverOpinion);

            return GetClientProcess(executor.Execute());
        }

        public static WfClientProcess Save(WfClientSaveParameters parameters)
        {
            IWfProcess process = WfRuntime.GetProcessByProcessID(parameters.ProcessID);

            IWfActivity activity = process.CurrentActivity;

            WfSaveDataExecutor executor = new WfSaveDataExecutor(activity, activity);

            WfClientOpinion opinion = parameters.CurrentOpinion;

            GenericOpinion serverOpinion = PrepareOpinion(process.CurrentActivity, parameters.CurrentOpinion, DeluxeIdentity.CurrentUser);

            executor.PrepareApplicationData += dataContext => GenericOpinionAdapter.Instance.Update(serverOpinion);

            return GetClientProcess(executor.Execute());
        }

        public static WfClientProcess Cancel(WfClientCancelParameters parameters)
        {
            IWfProcess process = WfRuntime.GetProcessByProcessID(parameters.ProcessID);

            IWfActivity activity = process.CurrentActivity;

            if (activity == null || activity.Assignees == null)// || !activity.Assignees.Contains(UserHelper.UserId))
            {
                return null;
            }

            WfCancelProcessExecutor executor = new WfCancelProcessExecutor(activity, process);

            GenericOpinion serverOpinion = PrepareOpinion(process.CurrentActivity, parameters.CurrentOpinion, DeluxeIdentity.CurrentUser);

            serverOpinion.OpinionType = "拒绝";

            if (serverOpinion.Content.IsNullOrEmpty())
                serverOpinion.Content = "拒绝";

            executor.PrepareApplicationData += dataContext => GenericOpinionAdapter.Instance.Update(serverOpinion);

            return GetClientProcess(executor.Execute());
        }

        public static WfClientProcess Withdraw(WfClientWithdrawParameters parameters)
        {
            IWfProcess process = WfRuntime.GetProcessByProcessID(parameters.ProcessID);

            WfWithdrawExecutor executor = new WfWithdrawExecutor(process.CurrentActivity, process.CurrentActivity, true);

            executor.Execute();

            return GetClientProcessByProcessID(process.ID);
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
                task.Body = taskBody;
            });
        }

        private static GenericOpinion PrepareOpinion(IWfActivity activity, WfClientOpinion opinion, IUser user)
        {
            GenericOpinion serverOpinion = null;

            if (opinion != null)
                serverOpinion = opinion.ToGenericOpinion();
            else
                serverOpinion = new GenericOpinion() { ID = UuidHelper.NewUuidString() };

            serverOpinion.ProcessID = activity.Process.ID;
            serverOpinion.ActivityID = activity.ID;
            serverOpinion.ResourceID = activity.Process.ResourceID;

            serverOpinion.IssuePerson = user;
            serverOpinion.AppendPerson = user;

            return serverOpinion;
        }

        private static void PrepareStartupOpinions(WfExecutorDataContext dataContext, IUser user, string content)
        {
            GenericOpinion opinion = new GenericOpinion();

            opinion.ID = Guid.NewGuid().ToString();
            opinion.ProcessID = dataContext.CurrentProcess.ID;
            opinion.ActivityID = dataContext.CurrentProcess.Activities[0].ID;
            opinion.ResourceID = dataContext.CurrentProcess.ResourceID;
            opinion.Content = content;
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

        public static WfClientProcess DynamicProcessStartup(WfClientDynamicProcessStartupParameters parameters)
        {
            WfProcessDescriptor processDesc = new WfProcessDescriptor();

            processDesc.Key = string.IsNullOrEmpty(parameters.ProcessKey) ? UuidHelper.NewUuidString() : parameters.ProcessKey;
            processDesc.Name = parameters.RuntimeProcessName;
            processDesc.ApplicationName = "动态流程";
            processDesc.ProgramName = "动态流程";
            processDesc.DefaultTaskTitle = "${TaskTitle}$";
            processDesc.Url = "${TaskUrl}$";

            processDesc.DefaultTaskTitle = "${Subject}$";

            WfActivityDescriptor initActDesp = new WfActivityDescriptor("Initial", WfActivityType.InitialActivity);
            initActDesp.Name = parameters.InitialActivityDescriptor.ActivityName;
            initActDesp.CodeName = "Initial Activity";
            initActDesp.Properties.SetValue("AutoSendUserTask", false);
            initActDesp.Properties.TrySetValue("AllowWithdraw", true);

            foreach (WfClientUserResourceDescriptorParameters userResourceDescriptor in parameters.InitialActivityDescriptor.UserResourceList)
            {
                IUser user = userResourceDescriptor.User;
                WfUserResourceDescriptor userResourceDesc = new WfUserResourceDescriptor(user);
                initActDesp.Resources.Add(userResourceDesc);
            }

            processDesc.Activities.Add(initActDesp);

            WfActivityDescriptor completedActDesp = new WfActivityDescriptor("Completed", WfActivityType.CompletedActivity);
            completedActDesp.Name = "完成";
            completedActDesp.CodeName = "Completed Activity";

            processDesc.Activities.Add(completedActDesp);

            for (int j = 0; j < processDesc.Activities.Count - 1; j++)
            {
                processDesc.Activities[j].ToTransitions.AddForwardTransition(processDesc.Activities[j + 1]);
            }

            WfProcessStartupParams startupParams = new WfProcessStartupParams();

            startupParams.ProcessDescriptor = processDesc;
            startupParams.Creator = DeluxeIdentity.CurrentUser;
            startupParams.Assignees.Add(DeluxeIdentity.CurrentUser);
            startupParams.ResourceID = parameters.ResourceID.ToString();
            startupParams.AutoCommit = true;
            startupParams.DefaultTaskTitle = parameters.TaskTitle;
            startupParams.DefaultUrl = parameters.TaskUrl;
            startupParams.RuntimeProcessName = parameters.RuntimeProcessName;
            startupParams.Department = DeluxeIdentity.CurrentUser.Parent;
            startupParams.RuntimeProcessName = parameters.RuntimeProcessName;

            //设置工作流运行时参数
            SetWorkflowParameters(parameters.ProcessParameters, startupParams.ApplicationRuntimeParameters);

            //准备执行器
            WfStartWorkflowExecutor executor = new WfStartWorkflowExecutor(null, startupParams, null, false);   //根据需要设是否自动往下走一步

            //调整待办的url和标题
            executor.PrepareMoveToTasks += (dataContext, tasks) =>
            {
                PrepareUserTasks(tasks, null, null, startupParams.ProcessDescriptor.InitialActivity.Name);
            };

            //添加审批意见
            executor.AfterSaveApplicationData += (dataContext) =>
            {
                //根据实际需要添加审批意见
                //PrepareStartupOpinions(dataContext, user, "请审批");
            };

            IWfProcess process = executor.Execute();

            return GetClientProcess(process);
        }

        public static WfClientProcess DynamicProcessMoveto(WfClientDynamicProcessMovetoParameters parameters)
        {
            IWfProcess process = WfRuntime.GetProcessByActivityID(parameters.ActivityID);

            IWfActivity activity = process.CurrentActivity;

            if (parameters.MovetoActivityDescriptor != null)
            {
                WfActivityDescriptorCreateParams createParams = new WfActivityDescriptorCreateParams();

                createParams = new WfActivityDescriptorCreateParams();
                createParams.Users = new OguDataCollection<IUser>();
                createParams.Name = parameters.MovetoActivityDescriptor.ActivityName;

                foreach (WfClientUserResourceDescriptorParameters userResource in parameters.MovetoActivityDescriptor.UserResourceList)
                {
                    createParams.Users.Add(userResource.User);
                }

                WfAddActivityExecutor executor = new WfAddActivityExecutor(activity, activity, createParams);

                executor.Execute();
            }

            //工作流流向下一个节点
            WfClientMovetoParameters movetoParameters = new WfClientMovetoParameters()
            {
                ProcessID = process.ID,
                ActivityID = activity.ID,
                ResourceID = process.ResourceID,
                Comment = parameters.Comment
            };
            WfClientProcess clientProcess = WfClientProxy.Moveto(movetoParameters);

            return clientProcess;
        }

        private static WfClientOpinion GetUserActivityOpinion(GenericOpinionCollection opinions, IWfActivity originalActivity, IUser user)
        {
            WfClientOpinion opinion = null;

            GenericOpinion serverOpinion = opinions.Find(o =>
                string.Compare(o.ActivityID, originalActivity.ID, true) == 0 &&
                string.Compare(o.IssuePerson.ID, user.ID, true) == 0);

            if (serverOpinion != null)
                opinion = new WfClientOpinion(serverOpinion);
            else
                opinion = new WfClientOpinion() { ID = UuidHelper.NewUuidString() };

            return opinion;
        }

        /// <summary>
        /// 为待办url增加重定向器，用于解决认证后，不能重定向带#的url问题
        /// </summary>
        /// <param name="taskUrl"></param>
        /// <returns></returns>
        private static string AddRedirector(string taskUrl)
        {
            string result = taskUrl;

            Uri redirector = UriSettings.GetConfig().GetUrl("wfPlatformService", "taskRedirector");

            if (redirector != null)
            {
                NameValueCollection parameters = new NameValueCollection();

                parameters.Add("ru", HttpUtility.UrlEncode(taskUrl));

                result = UriHelper.CombineUrlParams(redirector.ToString(), parameters);
            }

            return result;
        }
    }
}
