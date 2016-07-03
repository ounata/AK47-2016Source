using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Web.MVC.Library.Models.Workflow
{
    public class WfClientActivity
    {
        /// <summary>
        /// 流程节点主键
        /// </summary>
        public string ActivityID { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string ActivityName { get; set; }
        public string Approvers { get; set; }
        public string ApprovalType { get; set; }
        public bool IsActive { get; set; }
        public string Comment { get; set; }
        public int ApproverCount { get; set; }
        public string ApproverList { get; set; }
        public string ActivityScene { get; set; }
        public string ActivityStatus { get; set; }
        public string Action { get; set; }
        public string Approver { get; set; }
        public string ApproverLogonName { get; set; }
        public DateTime? ApprovalTime { get; set; }
        public string ApprovalElapsedTime { get; set; }
        public DateTime StartTime { get; set; }
    }
}
