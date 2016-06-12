using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Web.MVC.Library.Models.Workflow
{
    public class WFClientProcess
    {
        public string ProcessID { get; set; }
        public string ProcessStatus { get; set; }
        public string ProcessStatusString { get; set; }
        public string ResourceID { get; set; }

        public List<WfClientActivity> Activities { get; set; }
        public WfClientActivity CurrentActivity { get; set; }

        //public List<WFClientActivityHistory> ActivityHistories { get; set; }

        public WFClientProcess()
        {
            Activities = new List<WfClientActivity>();
            //ActivityHistories = new List<WFClientActivityHistory>();
        }
    }
}
