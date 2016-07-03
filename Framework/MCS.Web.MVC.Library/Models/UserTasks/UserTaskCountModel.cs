using MCS.Library.SOA.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Web.MVC.Library.Models.UserTasks
{
    /// <summary>
    /// 待办个数的Model
    /// </summary>
    public class UserTaskCountModel
    {
        /// <summary>
        /// 服务器端的变更标签
        /// </summary>
        public string ServerTag
        {
            get;
            set;
        }

        public UserTaskCount Data
        {
            get;
            set;
        }
    }
}
