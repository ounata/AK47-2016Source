using MCS.Library.OGUPermission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Web.MVC.Library.Models.Workflow
{
    /// <summary>
    /// 自由流程的启动参数
    /// </summary>
    public class WfClientStartupFreeStepsParameters
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// 审批人
        /// </summary>
        public List<IUser> Approvers
        {
            get;
            set;
        }
    }
}
