using CIIC.HSR.TSP.WF.Bizlet.Common;
using CIIC.HSR.TSP.WF.BizObject;
using CIIC.HSR.TSP.WF.UI.Control.Controls;
using CIIC.HSR.TSP.WF.UI.Control.ModelBinding;
using MCS.Library.WF.Contracts.Ogu;
using MCS.Library.WF.Contracts.Proxies;
using MCS.Library.WF.Contracts.Workflow.DataObjects;
using MCS.Library.WF.Contracts.Workflow.Runtime;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CIIC.HSR.TSP.WF.UI.Control.Interfaces
{
    /// <summary>
    /// 工作流启动参数
    /// </summary>
    [ModelBinder(typeof(WFStartWorkflowParameterBinder))]
    public class WFStartWorkflowParameter : WFParameterWithResponseBase
    {
        private bool _AutoNext = true;
        private WFMoveToTargetParameter _Target = null;
        /// <summary>
        /// 是否跳过开始节点
        /// </summary>
        public bool AutoNext
        {
            get { return _AutoNext; }
            set { _AutoNext = value; }
        }

        /// <summary>
        /// 流程模板名称
        /// </summary>
        public string TemplateKey { get; set; }

        /// <summary>
        /// 业务表单地址
        /// </summary>
        public string BusinessUrl { get; set; }

        /// <summary>
        /// 待办标题
        /// </summary>
        public string TaskTitle { get; set; }

        /// <summary>
        /// 部门编号
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }

        public WFMoveToTargetParameter Target
        {
            get
            {
                if (this._Target == null)
                    this._Target = new WFMoveToTargetParameter();

                return this._Target;
            }
            set
            {
                this._Target = value;
            }
        }

        /// <summary>
        /// 流程启动参数
        /// </summary>
        public WfClientProcessStartupParams ProcessStartupParams { get; set; }

        /// <summary>
        /// 流转相关的参数，如开始节点、结束节点等
        /// </summary>
        public WfClientTransferParams TransferParameter { get; set; }


        /// <summary>
        /// 设置审批人
        /// </summary>
        /// <param name="name">用户名</param>
        public void AddWfClientAssignee(List<WfClientUser> clientUserList)
        {
            foreach (var item in clientUserList)
            {
                WfClientAssignee wfClientAssigneeTemp = new WfClientAssignee();
                //审批人赋值
                wfClientAssigneeTemp.User = new WfClientUser();
                wfClientAssigneeTemp.User.ID = item.ID;
                wfClientAssigneeTemp.User.Name = item.Name;
                wfClientAssigneeTemp.User.DisplayName = item.DisplayName;

                var userIdList = this.Target.Candidates.Select(x => x.User.ID).ToList();

                if (userIdList.Contains(wfClientAssigneeTemp.User.ID) == false)
                    this.Target.Candidates.Add(wfClientAssigneeTemp);
            }
        }

        protected override void InternalExecute(ResponseData response)
        {
            this.ProcessStartupParams.ProcessDescriptorKey = GetNotEmptyValue(this.ProcessStartupParams.ProcessDescriptorKey, this.TemplateKey);
            this.ProcessStartupParams.DefaultUrl = GetNotEmptyValue(this.ProcessStartupParams.DefaultUrl, this.BusinessUrl);
            this.ProcessStartupParams.DefaultTaskTitle = GetNotEmptyValue(this.ProcessStartupParams.DefaultTaskTitle, this.TaskTitle);
            this.ProcessStartupParams.ResourceID = GetNotEmptyValue(this.ProcessStartupParams.ResourceID, this.ResourceId);

            foreach (KeyValuePair<string, object> kp in this.RuntimeContext.ApplicationRuntimeParameters)
                this.ProcessStartupParams.ApplicationRuntimeParameters[kp.Key] = kp.Value;

            //加入起始活动不发送待办的参数
            this.ProcessStartupParams.AutoCommit = false;
            this.ProcessStartupParams.OverridableInitialActivityProperties["AutoSendUserTask"] = false;

            //加入部门参数
            this.ProcessStartupParams.ApplicationRuntimeParameters.Add("Department", GetDepartment());
            this.ProcessStartupParams.Department = new WfClientOrganization();
            this.ProcessStartupParams.Department.ID = this.DepartmentCode;
            this.ProcessStartupParams.Department.Name = this.DepartmentName;
            //启动意见参数赋值
            this.ProcessStartupParams.Opinion = new WfClientOpinion();
            this.ProcessStartupParams.Opinion = this.RuntimeContext.Opinion;
            if (string.IsNullOrEmpty(ProcessStartupParams.ProcessDescriptorKey)
                || ProcessStartupParams.Creator.IsNullOrEmpty() || string.IsNullOrEmpty(ProcessStartupParams.ResourceID))
            {
                throw new ArgumentException("模板Key、当前操作人Id、ResourceId不能为空。");
            }

            if (this.ProcessStartupParams.Assignees.Count == 0)
                this.ProcessStartupParams.Assignees.Add(ProcessStartupParams.Creator);

            SetTaskAlarmDefaultParm();
            DefaultFillEmailCollector(this.ProcessStartupParams.ApplicationRuntimeParameters);

            if (this.AutoNext)
            {
                if (this.IsSelectCandidates)
                {
                    this.TransferParameter.FromTransitionDescriptorKey = this.Target.FromTransitionDescriptorKey;
                    this.TransferParameter.NextActivityDescriptorKey = this.Target.NextActivityDescriptorKey;
                    this.TransferParameter.Assignees.CopyFrom(this.Target.Candidates);
                }
                else
                {
                    this.TransferParameter = null;
                }
                response.ProcessInfo = WfClientProcessRuntimeServiceProxy.Instance.StartWorkflowAndMoveTo(this.ProcessStartupParams, this.TransferParameter, this.RuntimeContext);
            }
            else
            {
                response.ProcessInfo = WfClientProcessRuntimeServiceProxy.Instance.StartWorkflow(this.ProcessStartupParams);
            }
                

            #region 沈峥注释 2015/4/6
            //response.ProcessInfo = WfClientProcessRuntimeServiceProxy.Instance.StartWorkflow(this.ProcessStartupParams);

            ////需要跳过提交节点
            //if (AutoNext)
            //{
            //    if (response.ProcessInfo.Status == WfClientProcessStatus.Running &&
            //        response.ProcessInfo.CurrentActivity.Status == WfClientActivityStatus.Running)
            //    {
            //        if (this.IsSelectCandidates)
            //        {
            //            this.TransferParameter.FromTransitionDescriptorKey = response.ProcessInfo.NextActivities.FirstOrDefault().Transition.Key; ;
            //            this.TransferParameter.NextActivityDescriptorKey = response.ProcessInfo.NextActivities.FirstOrDefault().Activity.DescriptorKey;
            //            this.TransferParameter.Assignees.CopyFrom(this.Target.Candidates);

            //            response.ProcessInfo =
            //                WfClientProcessRuntimeServiceProxy.Instance.MoveTo(response.ProcessInfo.ID, this.TransferParameter, this.RuntimeContext);
            //        }
            //        else
            //        {
            //            response.ProcessInfo =
            //                WfClientProcessRuntimeServiceProxy.Instance.MoveToNextDefaultActivity(response.ProcessInfo.ID, this.RuntimeContext);
            //        }
            //    }
            //}
            #endregion
        }

        private static string GetNotEmptyValue(string preferValue, string defaultValue)
        {
            string result = preferValue;

            if (string.IsNullOrEmpty(result))
                result = defaultValue;

            return result;
        }

        private string GetDepartment()
        {
            Department departmentBo = new Department();

            departmentBo.DepartmentCode = this.DepartmentCode;
            departmentBo.DepartmentName = this.DepartmentName;

            return JsonConvert.SerializeObject(departmentBo);
        }

        protected override MailCollectorArg GetMailCollectorArgs()
        {
            var processDescriptor = WfClientProcessDescriptorServiceProxy.Instance.GetDescriptor(this.TemplateKey);

            MailCollectorArg arg = new MailCollectorArg();
            if (processDescriptor.Properties.ContainsKey(Consts.CompletedAlarmTypeCode))
            {
                arg.CompletedAlarmTypeCode = processDescriptor.Properties[Consts.CompletedAlarmTypeCode].StringValue;
            }
            if (processDescriptor.Properties.ContainsKey(Consts.TaskAlarmTypeCode))
            {
                arg.TaskAlarmTypeCode = processDescriptor.Properties[Consts.TaskAlarmTypeCode].StringValue;
            }

            return arg;
        }

        private void SetTaskAlarmDefaultParm()
        {
            //为流程上无法自动替换的参数默认赋值
            //流程标题
            if (!string.IsNullOrEmpty(this.TaskTitle))
            this.EMailCollector.MailTaskArguments.TemplateKeyValues.ExistsSkip(MailArgKeys.TaskTitle, this.TaskTitle);
            //流程请求时间
            this.EMailCollector.MailTaskArguments.TemplateKeyValues.ExistsSkip(MailArgKeys.RequestTime, DateTime.Now.ToString());
            //当前用户编号
            if (null != ProcessStartupParams.Creator)
            this.EMailCollector.UserId = ProcessStartupParams.Creator.ID;
            
        }


    }
}
