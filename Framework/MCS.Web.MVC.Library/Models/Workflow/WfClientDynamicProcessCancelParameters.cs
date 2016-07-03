using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Web.MVC.Library.Models.Workflow
{
    public class WfClientDynamicProcessCancelParameters
    {
        public string ProcessID { get; set; }

        /// <summary>
        /// 一般存业务相关的某个主键字段
        /// </summary>
        public string ResourceID { get; set; }

        public WfClientOpinion CurrentOpinion
        {
            get;
            set;
        }

        public WfClientDynamicProcessCancelParameters()
        {
        }
    }
}
