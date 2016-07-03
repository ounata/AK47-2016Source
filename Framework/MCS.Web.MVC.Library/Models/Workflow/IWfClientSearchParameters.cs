using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Web.MVC.Library.Models.Workflow
{
    /// <summary>
    /// 流程相关的ID
    /// </summary>
    public interface IWfClientSearchParameters
    {
        string ProcessID
        {
            get;
            set;
        }

        string ActivityID
        {
            get;
            set;
        }

        string ResourceID
        {
            get;
            set;
        }
    }
}
