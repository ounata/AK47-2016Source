using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Web.MVC.Library.Models.Workflow
{
    public class WfClientSearchParameters : IWfClientSearchParameters
    {
        public string ProcessID
        {
            get;
            set;
        }

        public string ActivityID
        {
            get;
            set;
        }

        public string ResourceID
        {
            get;
            set;
        }
    }
}
