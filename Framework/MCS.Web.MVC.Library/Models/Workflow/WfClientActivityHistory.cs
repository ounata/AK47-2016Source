using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Web.MVC.Library.Models.Workflow
{
    public class WfClientActivityHistory
    {
        public string ID { get; set; }
        public string ActivityID { get; set; }
        public string ActivityName { get; set; }
        public string Comment { get; set; }
        public string Action { get; set; }
        public string ApprovalType { get; set; }
        public string Approver { get; set; }
        public string ApproverLogonName { get; set; }
        public DateTime? ApprovalTime { get; set; }
        public string ApprovalElapsedTime { get; set; }
    }
}
