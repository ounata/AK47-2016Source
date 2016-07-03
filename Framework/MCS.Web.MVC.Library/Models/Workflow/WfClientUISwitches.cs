using MCS.Library.OGUPermission;
using MCS.Library.SOA.DataObjects.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Web.MVC.Library.Models.Workflow
{
    /// <summary>
    /// 流程相关的UI开关
    /// </summary>
    public class WfClientUISwitches
    {
        /// <summary>
        /// 能否流转
        /// </summary>
        public bool CanMoveTo
        {
            get;
            set;
        }

        /// <summary>
        /// 能否撤回
        /// </summary>
        public bool CanWithdraw
        {
            get;
            set;
        }

        /// <summary>
        /// 能否保存
        /// </summary>
        public bool CanSave
        {
            get;
            set;
        }

        /// <summary>
        /// 能否作废（驳回）
        /// </summary>
        public bool CanCancel
        {
            get;
            set;
        }

        /// <summary>
        /// 能否暂停
        /// </summary>
        public bool CanPause
        {
            get;
            set;
        }

        /// <summary>
        /// 能否继续（取消暂停）
        /// </summary>
        public bool CanResume
        {
            get;
            set;
        }

        /// <summary>
        /// 能否还原流程
        /// </summary>
        public bool CanRestore
        {
            get;
            set;
        }

        public WfClientUISwitches FillByProcess(IWfProcess process, string originalActivityID, IUser user)
        {
            if (process != null)
            {
                this.CanMoveTo = process.GetInMoveToMode(originalActivityID, user) ||
                    (process.GetInMoveToStatus(originalActivityID) && process.GetIsProcessAdmin(user));

                this.CanSave = (process.GetInMoveToMode(originalActivityID, user) && process.CurrentActivity.Descriptor.Properties.GetValue("AllowSave", true)) ||
                    (process.GetInMoveToStatus(originalActivityID) && process.GetIsProcessAdmin(user));

                this.CanPause = process.CanPause && process.GetIsProcessAdmin(user);

                this.CanResume = process.CanResume && process.GetIsProcessAdmin(user);

                this.CanRestore = process.CanRestore && (process.GetInAssignees(originalActivityID, user) || process.GetIsProcessAdmin(user));

                this.CanWithdraw = process.CanWithdraw(user);
                this.CanCancel = process.CanCancel(originalActivityID, user);
            }

            return this;
        }
    }
}
