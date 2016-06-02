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

        public List<WfClientActivity> Activities { get; set; }

        public WFClientProcess()
        {
            Activities = new List<WfClientActivity>();
        }
    }
}
