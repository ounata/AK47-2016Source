using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Web.MVC.Library.Models.Workflow
{
    public class WfClientCancelParameters
    {
        /// <summary>
        /// 测试用，临时属性
        /// </summary>
        public string UserLogonName { get; set; }

        public string ProcessID { get; set; }
        /// <summary>
        /// 一般存业务相关的某个主键字段
        /// </summary>
        public string ResourceID { get; set; }
        public string Comment { get; set; }
        public WfClientCancelParameters()
        {
        }
    }
}
