using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Web.MVC.Library.Models.Workflow
{
    public class WfClientMovetoParameters
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
        /// <summary>
        /// 待办标题
        /// </summary>
        public string TaskTitle { get; set; }
        /// <summary>
        /// 待办url
        /// </summary>
        public string TaskUrl { get; set; }
        /// <summary>
        /// 流程上下文参数，在工作流配置里用到的每个变量
        /// </summary>
        public Dictionary<string,object> ProcessParameters { get; set; }
        public string Comment { get; set; }
        public WfClientMovetoParameters()
        {
            ProcessParameters = new Dictionary<string, object>();
        }
    }
}
