using MCS.Library.Core;
using MCS.Library.OGUPermission;
using MCS.Library.Passport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.SOA.DataObjects.Workflow
{
    public static class Extensions
    {
        /// <summary>
        /// 判断某个人员是否属于流转模式（判断流程的状态以及人是否在当前活动中）
        /// </summary>
        /// <param name="process"></param>
        /// <param name="originalActivityID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static bool GetInMoveToMode(this IWfProcess process, string originalActivityID, IUser user)
        {
            bool result = GetInMoveToStatus(process, originalActivityID);

            if (result && user != null)
                result = process.GetInAssignees(originalActivityID, user);

            return result;
        }

        /// <summary>
        /// 判断是否在某个流程活动的指派人中
        /// </summary>
        /// <param name="process"></param>
        /// <param name="originalActivityID"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool GetInAssignees(this IWfProcess process, string originalActivityID, IUser user)
        {
            bool result = false;

            if (originalActivityID.IsNotEmpty() && user != null)
            {
                IWfActivity currentActivity = process.Activities[originalActivityID];

                if (currentActivity != null)
                    result = IsUserInAssignees(user.ID, currentActivity.Assignees);
            }

            return result;
        }

        /// <summary>
        /// 是不是处于流转状态（判断流程状态以及当前活动的状态）
        /// </summary>
        /// <param name="process"></param>
        /// <param name="originalActivityID"></param>
        /// <returns></returns>
        public static bool GetInMoveToStatus(this IWfProcess process, string originalActivityID)
        {
            bool result = false;

            if (process != null && originalActivityID.IsNotEmpty())
            {
                IWfActivity currentActivity = process.Activities[originalActivityID];

                if (currentActivity != null)
                {
                    //锁判断
                    //result = this.LockResult == null || this.LockResult.Succeed;
                    result = process.Status == WfProcessStatus.Running
                                    && currentActivity.Status == WfActivityStatus.Running;
                }
            }

            return result;
        }

        /// <summary>
        /// 是否是流程的管理员
        /// </summary>
        /// <param name="process"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool GetIsProcessAdmin(this IWfProcess process, IUser user)
        {
            bool result = false;

            if (process != null && user != null)
            {
                result = RolesDefineConfig.GetConfig().IsCurrentUserInRoles(user, "ProcessAdmin");

                if (result == false)
                    result = WfApplicationAuthAdapter.Instance.GetUserApplicationAuthInfo(user).Contains(
                        process.Descriptor.ApplicationName, process.Descriptor.ProgramName, WfApplicationAuthType.FormAdmin);
            }

            return result;
        }

        /// <summary>
        /// 是否是流程的查看者。本方法仅返回流程分类授权的信息，即使是流程环节中的人，也可能返回为False
        /// </summary>
        /// <param name="process"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool GetIsProcessViewer(this IWfProcess process, IUser user)
        {
            bool result = false;

            if (process != null && user != null)
            {
                result = WfApplicationAuthAdapter.Instance.GetUserApplicationAuthInfo(user).Contains(
                        process.Descriptor.ApplicationName, process.Descriptor.ProgramName, WfApplicationAuthType.FormViewer);
            }

            return result;
        }

        /// <summary>
        /// 是否可以撤回，综合判断各种开关
        /// </summary>
        /// <param name="process"></param>
        public static bool CanWithdraw(this IWfProcess process, IUser user)
        {
            bool result = false;

            if (process != null && process.CurrentActivity != null && user != null)
            {
                result = process.CanWithdraw;

                if (result)
                {
                    if (process.GetIsProcessAdmin(user) == false)
                    {
                        IWfActivity previousActivity = process.CurrentActivity.GetPreviousActivity();

                        result = process.CurrentActivity.Descriptor.Properties.GetValue("AllowToBeWithdrawn", true) &&
                           previousActivity.Descriptor.Properties.GetValue("AllowWithdraw", true);

                        if (result)
                        {
                            //不是管理员，进行更严格的权限判断(前一个点的操作人是我)
                            result = previousActivity.Operator.IsNotNullOrEmpty() &&
                                string.Compare(previousActivity.Operator.ID, user.ID, true) == 0;
                        }
                    }
                }
            }

            return result;
        }

        public static bool CanCancel(this IWfProcess process, string originalActivityID, IUser user)
        {
            bool result = false;

            if (process != null && user != null)
            {
                result = process.CanCancel;

                if (result)
                {
                    //当前环节允许的话，要看是否是待办人或者流程已经办结
                    result = ((process.GetInMoveToMode(originalActivityID, user) || process.Status == WfProcessStatus.Completed) &&
                        process.CurrentActivity.Descriptor.Properties.GetValue("AllowAbortProcess", true));

                    if (result == false)
                        result = process.GetIsProcessAdmin(user);
                }
            }

            return result;
        }

        /// <summary>
        /// 得到之前的活动
        /// </summary>
        /// <param name="currentActivity"></param>
        /// <returns></returns>
        public static IWfActivity GetPreviousActivity(this IWfActivity currentActivity)
        {
            IWfActivity result = null;

            int startIndex = currentActivity.Process.ElapsedActivities.Count - 1;

            if (currentActivity.Descriptor.ActivityType == WfActivityType.CompletedActivity)
                startIndex--;

            if (startIndex >= 0)
                result = currentActivity.Process.ElapsedActivities[startIndex];

            return result;
        }

        public static bool IsNullOrEmpty(this IOguObject obj)
        {
            return obj == null || string.IsNullOrEmpty(obj.ID);
        }

        public static bool IsNotNullOrEmpty(this IOguObject obj)
        {
            return IsNullOrEmpty(obj) == false;
        }

        private static bool IsUserInAssignees(string userID, WfAssigneeCollection currentActivityAssignees)
        {
            return currentActivityAssignees.Exists(a => string.Compare(a.User.ID, userID, true) == 0);
        }
    }
}
