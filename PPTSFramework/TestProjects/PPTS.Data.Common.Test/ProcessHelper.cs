using MCS.Library.Core;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Test
{
    public static class ProcessHelper
    {
        /// <summary>
        /// 创建一个简单的流程定义
        /// </summary>
        /// <returns></returns>
        public static IWfProcessDescriptor CreateSimpleProcessDescriptor()
        {
            WfProcessDescriptor processDesp = new WfProcessDescriptor();

            processDesp.Key = "TestProcess" + UuidHelper.NewUuidString().Substring(0, 8);
            processDesp.Name = "测试流程";
            processDesp.ApplicationName = "TEST_APP_NAME";
            processDesp.ProgramName = "TEST_PROGRAM_NAME";
            processDesp.Url = "/MCS_Framework/WebTestProject/defaultHandler.aspx";

            WfActivityDescriptor initAct = new WfActivityDescriptor("Initial", WfActivityType.InitialActivity);
            initAct.Name = "Initial";
            initAct.CodeName = "Initial Activity";

            processDesp.Activities.Add(initAct);

            WfActivityDescriptor normalAct = new WfActivityDescriptor("NormalActivity", WfActivityType.NormalActivity);

            normalAct.Name = "Normal";
            normalAct.CodeName = "Normal Activity";

            normalAct.Properties.SetValue("AutoMoveAfterPending", true);

            processDesp.Activities.Add(normalAct);

            processDesp.RelativeLinks.Add(new WfRelativeLinkDescriptor("TestLink") { Name = "测试链接", Url = "/MCSWebApp/Sample.htm" });

            WfActivityDescriptor completedAct = new WfActivityDescriptor("Completed", WfActivityType.CompletedActivity);
            completedAct.Name = "Completed";
            completedAct.CodeName = "Completed Activity";

            completedAct.RelativeLinks.Add(new WfRelativeLinkDescriptor("TestLink") { Name = "测试链接", Url = "/MCSWebApp/Sample.htm" });

            processDesp.Activities.Add(completedAct);

            initAct.ToTransitions.AddForwardTransition(normalAct);
            normalAct.ToTransitions.AddForwardTransition(completedAct);

            return processDesp;
        }

        /// <summary>
        /// 带上下文参数的启动流程
        /// </summary>
        /// <param name="processDesp"></param>
        /// <returns></returns>
        public static IWfProcess StartupProcessByExecutor(this IWfProcessDescriptor processDesp, Dictionary<string, object> runtimeParameters = null)
        {
            WfProcessStartupParams startupParams = processDesp.PrepareStartupParams(runtimeParameters);

            WfStartWorkflowExecutor executor = new WfStartWorkflowExecutor(startupParams);

            return executor.Execute();
        }

        /// <summary>
        /// 带上下文参数的启动流程
        /// </summary>
        /// <param name="processDesp"></param>
        /// <param name="runtimeParameters"></param>
        /// <returns></returns>
        public static IWfProcess StartupProcess(this IWfProcessDescriptor processDesp, Dictionary<string, object> runtimeParameters = null)
        {
            WfProcessStartupParams startupParams = PrepareStartupParams(processDesp, runtimeParameters);

            return WfRuntime.StartWorkflow(startupParams);
        }

        private static WfProcessStartupParams PrepareStartupParams(this IWfProcessDescriptor processDesp, Dictionary<string, object> runtimeParameters = null)
        {
            WfProcessStartupParams startupParams = new WfProcessStartupParams();
            startupParams.ResourceID = UuidHelper.NewUuidString();
            startupParams.ProcessDescriptor = processDesp;
            startupParams.Assignees.Add(OguObjectSettings.GetConfig().Objects["hq"].User);

            if (runtimeParameters != null)
                runtimeParameters.ForEach(kp => startupParams.ApplicationRuntimeParameters.Add(kp.Key, kp.Value));

            return startupParams;
        }
    }
}
