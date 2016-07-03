using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Web.MVC.Library.Models.Workflow
{
    public class WfClientActivityDescriptorParameters
    {
        public string ActivityName { get; set; }
        public List<WfClientUserResourceDescriptorParameters> UserResourceList { get; set; }

        public WfClientActivityDescriptorParameters()
        {
            UserResourceList = new List<WfClientUserResourceDescriptorParameters>();
        }
    }
}
