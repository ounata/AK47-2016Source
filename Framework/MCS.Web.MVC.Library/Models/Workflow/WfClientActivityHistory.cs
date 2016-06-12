using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Web.MVC.Library.Models.Workflow
{
    public class WFClientActivityHistory
    {
        public string ActivityId { get; set; }
        public string ActivityName { get; set; }
        public DateTime ApprovalTime { get; set; }
        public string Approver { get; set; }
        public string Comment { get; set; }
        public string Action { get; set; }
        public string ApprovalElapsedTime { get; set; }
    }
}
