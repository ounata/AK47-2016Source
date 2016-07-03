using MCS.Library.OGUPermission;
using MCS.Library.Principal;
using MCS.Web.MVC.Library.ApiCore;
using MCS.Web.MVC.Library.Models.Workflow;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common
{
    /// <summary>
    /// 工作流帮助类
    /// </summary>
    public class WorkflowHelper
    {
        private string _workflowName;
        private PPTSJob _job;
        public WorkflowHelper(string workflowName, IUser user)
        {
            _workflowName = workflowName;
            _job = user.GetCurrentJob();
        }

        WFRelationConfig _wfConfig = null;
        /// <summary>
        /// 检查工作流程是否存在
        /// </summary>
        /// <param name="throwException"></param>
        /// <returns></returns>
        public bool CheckWorkflow(bool throwException)
        {
            _wfConfig = this.InnerCheckWorkflow(throwException);
            return _wfConfig != null;
        }

        private WFRelationConfig InnerCheckWorkflow(bool throwException)
        {
            WFRelationConfig wfRelConfig = WFRelationConfigAdapter.Instance.LoadWFRelationConfig(_workflowName, string.Empty, _job.Organization(), _job.JobName);
            if (wfRelConfig == null || string.IsNullOrEmpty(wfRelConfig.ProcessKey))
            {
                if (throwException)
                    throw new Exception(string.Format("该岗位无权提交{0}流程", _workflowName));
                return null;
            }
            return wfRelConfig;
        }
        
        public void StartupWorkflow(WorkflowStartupParameter p)
        {
            //获取需要启动的工作流的key
            if (_wfConfig == null)
                _wfConfig = this.InnerCheckWorkflow(true);

            //启动工作流-准备流程上下文参数
            WfClientStartupParameters parameters = new WfClientStartupParameters()
            {
                ResourceID = p.ResourceID,
                ProcessKey = _wfConfig.ProcessKey,
                TaskTitle = p.TaskTitle,
                RuntimeProcessName = p.RuntimeProcessName,
                TaskUrl = p.TaskUrl
            };

            if (string.IsNullOrEmpty(parameters.TaskTitle))
                parameters.RuntimeProcessName = parameters.TaskTitle;

            //启动工作流-填充系统参数（包括校区、分公司、大区、总部）
            _job.FillRuntimeParameters(parameters.ProcessParameters);

            //启动工作流-填写一些业务参数
            if (p.Parameters != null && p.Parameters.Count > 0)
            {
                foreach (var item in p.Parameters)
                {
                    parameters.ProcessParameters.Add(item.Key, item.Value);
                }
            }

            //启动工作流
            WfClientProxy.Startup(parameters);
        }

        /// <summary>
        /// 可变下一处理人工作流启动
        /// </summary>
        /// <param name="p"></param>
        public void StartupDynamicWorkflow(WfClientDynamicProcessStartupParameter p)
        {

            //启动工作流-准备流程上下文参数
            WfClientDynamicProcessStartupParameters parameters = new WfClientDynamicProcessStartupParameters()
            {
                ResourceID = p.ResourceID,
                TaskTitle = p.TaskTitle,
                RuntimeProcessName = p.RuntimeProcessName,
                TaskUrl = p.TaskUrl
            };


            if (string.IsNullOrEmpty(parameters.TaskTitle))
                parameters.RuntimeProcessName = parameters.TaskTitle;

            //启动工作流-填充系统参数（包括校区、分公司、大区、总部）
            _job.FillRuntimeParameters(parameters.ProcessParameters);

            parameters.InitialActivityDescriptor = new WfClientActivityDescriptorParameters();
            parameters.InitialActivityDescriptor.ActivityName = p.ActivityName;
            parameters.InitialActivityDescriptor.UserResourceList = new List<WfClientUserResourceDescriptorParameters>()
            {
                new WfClientUserResourceDescriptorParameters()
                            {
                                User = p.User   //根据实际需要调整，可以是任何人
                            }
            };

            //启动工作流-填写一些业务参数
            if (p.Parameters != null && p.Parameters.Count > 0)
            {
                foreach (var item in p.Parameters)
                {
                    parameters.ProcessParameters.Add(item.Key, item.Value);
                }
            }

            //启动工作流
            WfClientProxy.DynamicProcessStartup(parameters);
        }

        /// <summary>
        /// 可变流程，下一个处理人
        /// </summary>
        /// <param name="p"></param>
        public void MovetoDynamicWorkflow(WfClientDynamicProcessMovetoParameter p)
        {
            WfClientDynamicProcessMovetoParameters parameters = new WfClientDynamicProcessMovetoParameters()
            {
                ResourceID = p.ResourceID,
                ActivityID = p.ActivityID,
                ProcessID =p.ProcessID,
                Comment = p.Comment
            };

            //启动工作流-填写一些业务参数
            if (p.Parameters != null && p.Parameters.Count > 0)
            {
                foreach (var item in p.Parameters)
                {
                    parameters.ProcessParameters.Add(item.Key, item.Value);
                }
            }

            parameters.MovetoActivityDescriptor = new WfClientActivityDescriptorParameters();
            parameters.MovetoActivityDescriptor.ActivityName = p.ActivityName;

            if (p.NextProcesser != null)
            {
                parameters.MovetoActivityDescriptor.UserResourceList = new List<WfClientUserResourceDescriptorParameters>()
                {
                    new WfClientUserResourceDescriptorParameters()
                                {
                                    User = new DeluxeIdentity(p.NextProcesser).User   //下一处理人，可以多个
                                }
                };
            }
            

            WfClientProcess process = WfClientProxy.DynamicProcessMoveto(parameters);
        }
    }

    /// <summary>
    /// 工作流启动参数
    /// </summary>
    public class WorkflowStartupParameter
    {
        /// <summary>
        /// 审批表单ID
        /// </summary>
        public string ResourceID { get; set; }
        
        /// <summary>
        /// 代办标题
        /// </summary>
        public string TaskTitle { get; set; }

        /// <summary>
        /// 运行时流程名称
        /// </summary>
        public string RuntimeProcessName { get; set; }

        /// <summary>
        /// 代办跳转URL
        /// </summary>
        public string TaskUrl { get; set; }

        /// <summary>
        /// 业务参数
        /// </summary>
        public Dictionary<string, object> Parameters
        {
            set
            {
                if (value == null)
                    _parameters.Clear();
                else
                    _parameters = value;
            }
            get
            {
                return _parameters;
            }
        }

        Dictionary<string, object> _parameters;
        public WorkflowStartupParameter()
        {
            _parameters = new Dictionary<string, object>();
        }
    }

    public class WfClientDynamicProcessStartupParameter: WorkflowStartupParameter
    {
        public IUser User { get; set; }
        public string ProcessKey { get; set; }
        public string ActivityName { get; set; }
        public WfClientDynamicProcessStartupParameter()
        {
        }
    }

    public class WfClientDynamicProcessMovetoParameter
    {
        public string ResourceID { get; set; }
        public string ActivityID { get; set; }
        public string Comment { get; set; }
        public string ProcessID { get; set; }
        public string ActivityName { get; set; }
        public string NextProcesser { get; set; }

        public Dictionary<string, object> Parameters
        {
            set
            {
                if (value == null)
                    _parameters.Clear();
                else
                    _parameters = value;
            }
            get
            {
                return _parameters;
            }
        }

        Dictionary<string, object> _parameters;
        public WfClientDynamicProcessMovetoParameter()
        {
            _parameters = new Dictionary<string, object>();
        }


    }

    /// <summary>
    /// 工作流名称
    /// </summary>
    public static class WorkflowNames
    {
        public const string CustomerRelease = "学员解冻";
        public const string Present = "买赠表审批";
        public const string Discount = "折扣表审批";
        public const string Product = "新建产品审批";
        public const string OrderExtraDiscount = "订购特殊折扣";
        public const string Debook_Youxue = "退订审批(游学)";
        public const string Refund_Inner = "退账户余额(制度内)";
        public const string Refund_Outer = "退账户余额(制度外)";
        public const string AccountTransfer = "账户余额转让";
        public const string CustomerTransfer_CrossBranch = "转学审批(跨分公司)";
        public const string CustomerTransfer_SameBranch = "转学审批(跨校区)";
        public const string CustomerServiceProcss = "客服处理";
    }

}
