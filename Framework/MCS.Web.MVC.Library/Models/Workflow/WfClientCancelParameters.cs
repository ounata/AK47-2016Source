using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Web.MVC.Library.Models.Workflow
{
    public class WfClientCancelParameters
    {
        public string ProcessID { get; set; }
        public string ActivityID { get; set; }
        /// <summary>
        /// 一般存业务相关的某个主键字段
        /// </summary>
        public string ResourceID { get; set; }

        //public string Comment { get; set; }

        public WfClientOpinion CurrentOpinion
        {
            get;
            set;
        }

        public WfClientCancelParameters()
        {
        }
    }
}
