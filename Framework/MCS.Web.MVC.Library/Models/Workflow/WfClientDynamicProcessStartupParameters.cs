using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Web.MVC.Library.Models.Workflow
{
    public class WfClientDynamicProcessStartupParameters
    {
        private Dictionary<string, object> _ProcessParameters = null;

        public WfClientActivityDescriptorParameters InitialActivityDescriptor { get; set; }

        public string ProcessKey { get; set; }

        public string ResourceID { get; set; }

        public string TaskTitle { get; set; }

        public string TaskUrl { get; set; }

        public string RuntimeProcessName { get; set; }

        public Dictionary<string, object> ProcessParameters
        {
            get
            {
                if (this._ProcessParameters == null)
                    this._ProcessParameters = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

                return this._ProcessParameters;
            }
            set
            {
                this._ProcessParameters = value;
            }
        }
    }
}
