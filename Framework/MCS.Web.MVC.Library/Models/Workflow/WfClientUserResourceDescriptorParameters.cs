using MCS.Library.OGUPermission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Web.MVC.Library.Models.Workflow
{
    public class WfClientUserResourceDescriptorParameters
    {
        public IUser User { get; set; }

        public WfClientUserResourceDescriptorParameters()
        {
        }
    }
}
