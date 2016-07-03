using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Web.MVC.Library.Models.Workflow
{
    public class WfClientStartupParameters
    {
        private Dictionary<string, object> _ProcessParameters = null;

        /// <summary>
        /// 工作流key
        /// </summary>
        public string ProcessKey
        {
            get;
            set;
        }

        /// <summary>
        /// 一般存业务相关的某个主键字段
        /// </summary>
        public string ResourceID
        {
            get;
            set;
        }

        /// <summary>
        /// 待办标题
        /// </summary>
        public string TaskTitle
        {
            get;
            set;
        }

        /// <summary>
        /// 待办Url
        /// </summary>
        public string TaskUrl
        {
            get;
            set;
        }

        /// <summary>
        /// 运行时的流程名称
        /// </summary>
        public string RuntimeProcessName
        {
            get;
            set;

        }
        /// <summary>
        /// 流程上下文参数，在工作流配置里用到的每个变量
        /// </summary>
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

        public WfClientStartupParameters()
        {
        }
    }
}
