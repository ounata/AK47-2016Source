﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Core;
using MCS.Library.OGUPermission;
using MCS.Library.SOA.DataObjects.Workflow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MCS.Library.SOA.DataObjects.Test.Executor
{
	/// <summary>
	/// 测试退回功能的帮助类
	/// </summary>
	public static class ReturnExecutorTestHelper
	{
		/// <summary>
		/// 准备流程，然后流转到活动D
		/// </summary>
		/// <returns></returns>
		public static IWfActivity PrepareAndMoveToSpecialActivity()
		{
			IWfProcessDescriptor processDesp = ReturnExecutorTestHelper.PrepareSpecialReturnProcessDesp();

			IWfProcess process = ReturnExecutorTestHelper.StartSpecialReturnProcess(processDesp);

			WfProcessTestCommon.MoveToNextDefaultActivity(process);	//To B
			WfProcessTestCommon.MoveToNextDefaultActivity(process);	//To C
			WfProcessTestCommon.MoveToNextDefaultActivity(process);	//To D

			return process.CurrentActivity;
		}

		/// <summary>
		/// 准备同意/不同意流程，流转到到选择同意/不同意的活动（B）
		/// </summary>
		/// <returns></returns>
		public static IWfActivity PrepareAndMoveToAgreeSelectorActivity()
		{
			//准备同意/不同意的退回流程。在B环节有两根出线BC和BA，BA是退回线，退回到A。其中BA默认是没有选择的
			IWfProcessDescriptor processDesp = ReturnExecutorTestHelper.PrepareAgreeReturnProcessDesp();

			IWfProcess process = ReturnExecutorTestHelper.StartSpecialReturnProcess(processDesp);

			WfProcessTestCommon.MoveToNextDefaultActivity(process);	//To B

			return process.CurrentActivity;
		}

		#region Prepare Descriptor
		/// <summary>
		/// 准备专用退件流程，B有BC和BE两条出线，IsCLine为True走BC，否则走BE
		/// </summary>
		/// <returns></returns>
		public static IWfProcessDescriptor PrepareSpecialReturnProcessDesp()
		{
			WfProcessDescriptor processDesp = new WfProcessDescriptor();

			processDesp.Key = "TestProcess" + UuidHelper.NewUuidString().Substring(0, 8);
			processDesp.Name = "专用退回流程";
			processDesp.ApplicationName = "TEST_APP_NAME";
			processDesp.ProgramName = "TEST_PROGRAM_NAME";
			processDesp.Url = "/MCS_Framework/WebTestProject/defaultHandler.aspx";

			WfActivityDescriptor initActivity = new WfActivityDescriptor("A", WfActivityType.InitialActivity);
			initActivity.Name = "A";
			initActivity.CodeName = "A";
			initActivity.Resources.Add(new WfUserResourceDescriptor((IUser)OguObjectSettings.GetConfig().Objects["requestor"].Object));

			processDesp.Activities.Add(initActivity);

			IWfActivityDescriptor activityB = CreateNormalDescriptor("B", "B");

			activityB.Resources.Add(new WfUserResourceDescriptor((IUser)OguObjectSettings.GetConfig().Objects["approver1"].Object));
			processDesp.Activities.Add(activityB);

			IWfActivityDescriptor activityC = CreateNormalDescriptor("C", "C");
			processDesp.Activities.Add(activityC);

			IWfActivityDescriptor activityE = CreateNormalDescriptor("E", "E");
			processDesp.Activities.Add(activityE);

			IWfActivityDescriptor activityD = CreateNormalDescriptor("D", "D");
			processDesp.Activities.Add(activityD);

			WfActivityDescriptor completedActivity = new WfActivityDescriptor("F", WfActivityType.CompletedActivity);
			completedActivity.Name = "F";
			completedActivity.CodeName = "F";

			processDesp.Activities.Add(completedActivity);

			//A到B
			initActivity.ToTransitions.AddForwardTransition(activityB);

			//B有两根出线，分别是C和E
			WfTransitionDescriptor transitionBC = (WfTransitionDescriptor)activityB.ToTransitions.AddForwardTransition(activityC);
			transitionBC.Condition = new WfConditionDescriptor(transitionBC) { Expression = "IsCLine" };

			WfTransitionDescriptor transitionBE = (WfTransitionDescriptor)activityB.ToTransitions.AddForwardTransition(activityE);
			transitionBE.Condition = new WfConditionDescriptor(transitionBE) { Expression = "!IsCLine" };

			//C和E都汇聚到D
			activityC.ToTransitions.AddForwardTransition(activityD);
			activityE.ToTransitions.AddForwardTransition(activityD);

			//D到结束点
			activityD.ToTransitions.AddForwardTransition(completedActivity);

			return processDesp;
		}

		/// <summary>
		/// 准备加签退件流程
		/// </summary>
		/// <returns></returns>
		public static IWfProcessDescriptor PrepareAddApproverReturnProcessDesp()
		{
			WfProcessDescriptor processDesp = new WfProcessDescriptor();

			processDesp.Key = "TestProcess" + UuidHelper.NewUuidString().Substring(0, 8);
			processDesp.Name = "加签退回流程";
			processDesp.ApplicationName = "TEST_APP_NAME";
			processDesp.ProgramName = "TEST_PROGRAM_NAME";
			processDesp.Url = "/MCS_Framework/WebTestProject/defaultHandler.aspx";

			WfActivityDescriptor initActivity = new WfActivityDescriptor("A", WfActivityType.InitialActivity);
			initActivity.Name = "A";
			initActivity.CodeName = "A";

			processDesp.Activities.Add(initActivity);

			IWfActivityDescriptor activityB = CreateNormalDescriptor("B", "B");

			processDesp.Activities.Add(activityB);

			IWfActivityDescriptor activityC = CreateNormalDescriptor("C", "C");
			processDesp.Activities.Add(activityC);

			WfActivityDescriptor completedActivity = new WfActivityDescriptor("D", WfActivityType.CompletedActivity);
			completedActivity.Name = "D";
			completedActivity.CodeName = "D";

			processDesp.Activities.Add(completedActivity);

			//A到B
			initActivity.ToTransitions.AddForwardTransition(activityB);

			//B到C
			activityB.ToTransitions.AddForwardTransition(activityC);

			//C到结束点
			activityC.ToTransitions.AddForwardTransition(completedActivity);

			return processDesp;
		}

		/// <summary>
		/// 准备同意/不同意的退回流程。在B环节有两根出线BC和BA，BA是退回线，退回到A。其中BA默认是没有选择的
		/// </summary>
		/// <returns></returns>
		public static IWfProcessDescriptor PrepareAgreeReturnProcessDesp()
		{
			WfProcessDescriptor processDesp = new WfProcessDescriptor();

			processDesp.Key = "TestProcess" + UuidHelper.NewUuidString().Substring(0, 8);
			processDesp.Name = "专用退回流程";
			processDesp.ApplicationName = "TEST_APP_NAME";
			processDesp.ProgramName = "TEST_PROGRAM_NAME";
			processDesp.Url = "/MCS_Framework/WebTestProject/defaultHandler.aspx";

			WfActivityDescriptor initActivity = new WfActivityDescriptor("A", WfActivityType.InitialActivity);
			initActivity.Name = "A";
			initActivity.CodeName = "A";

			processDesp.Activities.Add(initActivity);

			IWfActivityDescriptor activityB = CreateNormalDescriptor("B", "B");

			processDesp.Activities.Add(activityB);

			IWfActivityDescriptor activityC = CreateNormalDescriptor("C", "C");
			processDesp.Activities.Add(activityC);

			IWfActivityDescriptor activityD = CreateNormalDescriptor("D", "D");
			processDesp.Activities.Add(activityD);

			WfActivityDescriptor completedActivity = new WfActivityDescriptor("F", WfActivityType.CompletedActivity);
			completedActivity.Name = "F";
			completedActivity.CodeName = "F";

			processDesp.Activities.Add(completedActivity);

			//A->B
			initActivity.ToTransitions.AddForwardTransition(activityB);

			//B有两根出线，分别是C和A，A是退回线
			WfTransitionDescriptor transitionBC = (WfTransitionDescriptor)activityB.ToTransitions.AddForwardTransition(activityC);
			transitionBC.Enabled = true;
			transitionBC.DefaultSelect = true;
			WfTransitionDescriptor transitionBA = (WfTransitionDescriptor)activityB.ToTransitions.AddBackwardTransition(initActivity);
			//transitionBA.Enabled = false;
			transitionBA.Enabled = true;
			transitionBC.DefaultSelect = false;
			transitionBA.IsBackward = true;

			//C->D
			activityC.ToTransitions.AddForwardTransition(activityD);

			//D到结束点
			activityD.ToTransitions.AddForwardTransition(completedActivity);

			return processDesp;
		}

		/// <summary>
		/// 生成用于复制退件活动的测试流程
		/// A为起点、两条分支，A、B、C、D和A、E、D，D为终点
		/// </summary>
		/// <returns></returns>
		public static IWfProcessDescriptor PrepareCopyTestProcessDesp()
		{
			WfProcessDescriptor processDesp = new WfProcessDescriptor();

			processDesp.Key = "TestProcess" + UuidHelper.NewUuidString().Substring(0, 8);
			processDesp.Name = "专用退回流程";
			processDesp.ApplicationName = "TEST_APP_NAME";
			processDesp.ProgramName = "TEST_PROGRAM_NAME";
			processDesp.Url = "/MCS_Framework/WebTestProject/defaultHandler.aspx";

			WfActivityDescriptor initActivity = new WfActivityDescriptor("A", WfActivityType.InitialActivity);
			initActivity.Name = "A";
			initActivity.CodeName = "A";

			processDesp.Activities.Add(initActivity);

			IWfActivityDescriptor activityB = CreateNormalDescriptor("B", "B");

			processDesp.Activities.Add(activityB);

			IWfActivityDescriptor activityC = CreateNormalDescriptor("C", "C");
			processDesp.Activities.Add(activityC);

			IWfActivityDescriptor activityE = CreateNormalDescriptor("E", "E");
			processDesp.Activities.Add(activityE);

			WfActivityDescriptor completedActivity = new WfActivityDescriptor("D", WfActivityType.CompletedActivity);
			completedActivity.Name = "D";
			completedActivity.CodeName = "D";

			processDesp.Activities.Add(completedActivity);

			//A到B
			WfTransitionDescriptor transitionAB = (WfTransitionDescriptor)initActivity.ToTransitions.AddForwardTransition(activityB);
			transitionAB.Enabled = true;

			//A到E
			WfTransitionDescriptor transitionAE = (WfTransitionDescriptor)initActivity.ToTransitions.AddBackwardTransition(activityE);
			transitionAE.Enabled = false;

			//B到C
			activityB.ToTransitions.AddForwardTransition(activityC);

			//E到C
			activityE.ToTransitions.AddForwardTransition(activityC);

			//C到结束点
			activityC.ToTransitions.AddForwardTransition(completedActivity);

			return processDesp;
		}

		/// <summary>
		/// 生成用于复制退件活动的测试流程
		/// A为起点、两条分支，A、B、C、D和A、E、D，D为终点。B和A之间有一条退回线
		/// </summary>
		/// <returns></returns>
		public static IWfProcessDescriptor PrepareCopyTestProcessWithReturnLineDesp()
		{
			IWfProcessDescriptor processDesp = PrepareCopyTestProcessDesp();

			IWfActivityDescriptor activityB = processDesp.Activities["B"];

			WfTransitionDescriptor transition = (WfTransitionDescriptor)activityB.ToTransitions.AddForwardTransition(processDesp.InitialActivity);

			transition.IsBackward = true;

			return processDesp;
		}

		/// <summary>
		/// 准备一条简单的直线流程，主要用于两次退回等场景
		/// 流程为A->B->C->D
		/// </summary>
		/// <returns></returns>
		public static IWfProcessDescriptor PrepareStraightProcessDesp()
		{
			WfProcessDescriptor processDesp = new WfProcessDescriptor();

			processDesp.Key = "TestProcess" + UuidHelper.NewUuidString().Substring(0, 8);
			processDesp.Name = "简单直线流程";
			processDesp.ApplicationName = "TEST_APP_NAME";
			processDesp.ProgramName = "TEST_PROGRAM_NAME";
			processDesp.Url = "/MCS_Framework/WebTestProject/defaultHandler.aspx";

			WfActivityDescriptor initActivity = new WfActivityDescriptor("A", WfActivityType.InitialActivity);
			initActivity.Name = "A";
			initActivity.CodeName = "A";
			initActivity.Resources.Add(new WfUserResourceDescriptor((IUser)OguObjectSettings.GetConfig().Objects["requestor"].Object));

			processDesp.Activities.Add(initActivity);

			IWfActivityDescriptor activityB = CreateNormalDescriptor("B", "B");

			processDesp.Activities.Add(activityB);
			activityB.Resources.Add(new WfUserResourceDescriptor((IUser)OguObjectSettings.GetConfig().Objects["approver1"].Object));

			IWfActivityDescriptor activityC = CreateNormalDescriptor("C", "C");
			processDesp.Activities.Add(activityC);

			WfActivityDescriptor completedActivity = new WfActivityDescriptor("D", WfActivityType.CompletedActivity);
			completedActivity.Name = "D";
			completedActivity.CodeName = "D";

			processDesp.Activities.Add(completedActivity);

			//A到B
			WfTransitionDescriptor transitionAB = (WfTransitionDescriptor)initActivity.ToTransitions.AddForwardTransition(activityB);
			transitionAB.Enabled = true;


			//B到C
			activityB.ToTransitions.AddForwardTransition(activityC);

			//C到结束点
			activityC.ToTransitions.AddForwardTransition(completedActivity);

			return processDesp;
		}
		#endregion Prepare Descriptor

		public static IWfProcess StartSpecialReturnProcess(IWfProcessDescriptor processDesp)
		{
			WfProcessStartupParams startupParams = new WfProcessStartupParams();
			startupParams.ProcessDescriptor = processDesp;
			startupParams.ApplicationRuntimeParameters["IsCLine"] = true;

			return WfRuntime.StartWorkflow(startupParams);
		}

		private static IWfActivityDescriptor CreateNormalDescriptor(string key, string name)
		{
			WfActivityDescriptor activityDesp = new WfActivityDescriptor(key, WfActivityType.NormalActivity);

			activityDesp.Name = name;
			activityDesp.CodeName = key;

			return activityDesp;
		}

		#region Validation
		/// <summary>
		/// 验证B的衍生点的出线是否是两根
		/// </summary>
		public static void ValidateBRelativeActivityOutTransitions(IWfActivity currentActivity)
		{
			IWfActivity relativeBActivity = FindRelativeActivityByKey(currentActivity, "B");

			Assert.IsNotNull(relativeBActivity, string.Format("不能在{0}后找到B的衍生活动", currentActivity.Descriptor.Key));

			//衍生线也是两根
			WfTransitionDescriptorCollection transitions = relativeBActivity.Descriptor.ToTransitions;

			Assert.AreEqual(2, transitions.Count);
			Assert.IsTrue(transitions.Exists(t => t.ToActivity.AssociatedActivityKey == "C"));
			Assert.IsTrue(transitions.Exists(t => t.ToActivity.AssociatedActivityKey == "E"));

			transitions = relativeBActivity.Descriptor.ToTransitions.GetAllCanTransitForwardTransitions();

			Assert.AreEqual(1, transitions.Count);
			Assert.IsTrue(transitions.Exists(t => t.ToActivity.AssociatedActivityKey == "C"));
		}

		/// <summary>
		/// 验证主活动点的Key
		/// </summary>
		/// <param name="activityKeys"></param>
		public static void ValidateMainStreamActivities(IWfProcess process, params string[] expectedActKeys)
		{
			WfMainStreamActivityDescriptorCollection mainStreamActivities = process.GetMainStreamActivities(true);//process.Descriptor.GetMainStreamActivities();

			List<string> mainStreamKeys = new List<string>();

			foreach (WfMainStreamActivityDescriptor msActDesp in mainStreamActivities)
			{
				string actKey = msActDesp.Activity.Key;

				mainStreamKeys.Add(actKey);
			}

			Assert.AreEqual(expectedActKeys.Length, mainStreamActivities.Count, "主活动点和预期的个数不符");

			for (int i = 0; i < expectedActKeys.Length; i++)
				Assert.AreEqual(expectedActKeys[i], mainStreamKeys[i], string.Format("第{0}个主活动的Key不一致", i));
		}
		#endregion Validation

		#region Helper
		public static void ExecuteReturnOperation(IWfActivity currentActivity, string targetKey)
		{
			IWfActivity targetActivity = currentActivity.Process.Activities.FindActivityByDescriptorKey(targetKey);
			WfReturnExecutor executor = new WfReturnExecutor(currentActivity, targetActivity);

			executor.ExecuteNotPersist();
		}

		private static IWfActivity FindRelativeActivityByKey(IWfActivity currentActivity, string key)
		{
			IWfActivity result = null;

			IWfActivityDescriptor actDesp = FindAssociatedActivityByKey(currentActivity.Descriptor, key);

			if (actDesp != null)
				result = actDesp.Instance;

			return result;
		}

		private static IWfActivityDescriptor FindAssociatedActivityByKey(IWfActivityDescriptor actDesp, string key)
		{
			IWfActivityDescriptor result = null;

			if (actDesp.AssociatedActivityKey == key)
				result = actDesp;
			else
			{
				WfTransitionDescriptorCollection transitions = actDesp.ToTransitions.GetAllCanTransitForwardTransitions();

				foreach (IWfTransitionDescriptor transition in transitions)
				{
					result = FindAssociatedActivityByKey(transition.ToActivity, key);

					if (result != null)
						break;
				}
			}

			return result;
		}
		#endregion Helper

		#region Output
		public static void OutputMainStream(IWfProcess process)
		{
			WfMainStreamActivityDescriptorCollection mainStreamActivities = process.GetMainStreamActivities(true);

			StringBuilder strB = new StringBuilder();

			foreach (WfMainStreamActivityDescriptor msActDesp in mainStreamActivities)
			{
				if (strB.Length > 0)
					strB.Append("->");

				strB.Append(msActDesp.Activity.Key);

				if (msActDesp.Activity.AssociatedActivityKey.IsNotEmpty())
					strB.AppendFormat("({0})", msActDesp.Activity.AssociatedActivityKey);
			}

			Console.WriteLine("Main Stream: {0}", strB.ToString());
		}

		public static void OutputEveryActivities(IWfProcess process)
		{
			StringBuilder strB = new StringBuilder();

			Dictionary<string, IWfTransitionDescriptor> elapsedTransitions = new Dictionary<string, IWfTransitionDescriptor>();

			OutputActivityInfoRecursively(process.Descriptor.InitialActivity, elapsedTransitions, strB);

			Console.WriteLine("Every Step: {0}", strB.ToString());
		}

		private static void OutputActivityInfoRecursively(IWfActivityDescriptor actDesp, Dictionary<string, IWfTransitionDescriptor> elapsedTransitions, StringBuilder strB)
		{
			if (strB.Length > 0)
				strB.Append("->");

			strB.Append(actDesp.Key);

			if (actDesp.AssociatedActivityKey.IsNotEmpty())
				strB.AppendFormat("[{0}]", actDesp.AssociatedActivityKey);

			OutputCandidates(actDesp, strB);

			IWfTransitionDescriptor transition = actDesp.ToTransitions.FindElapsedTransition();

			if (transition == null)
				transition = actDesp.ToTransitions.GetAllEnabledTransitions().GetAllCanTransitTransitions().FirstOrDefault();

			if (transition != null)
			{
				if (elapsedTransitions.ContainsKey(transition.Key) == false)
				{
					elapsedTransitions.Add(transition.Key, transition);
					OutputActivityInfoRecursively(transition.ToActivity, elapsedTransitions, strB);
				}
			}
		}

		private static void OutputCandidates(IWfActivityDescriptor actDesp, StringBuilder strB)
		{
			if (actDesp.Instance != null)
			{
				StringBuilder strInnerB = new StringBuilder();

				foreach (IUser user in actDesp.Instance.Candidates.ToUsers())
				{
					if (strInnerB.Length > 0)
						strInnerB.Append(",");

					strInnerB.AppendFormat("{0}", user.DisplayName);
				}

				if (strInnerB.Length > 0)
					strB.AppendFormat("({0})", strInnerB.ToString());
			}
		}
		#endregion Output
	}
}