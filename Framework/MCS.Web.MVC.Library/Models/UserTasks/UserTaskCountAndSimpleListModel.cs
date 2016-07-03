using MCS.Library.SOA.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Web.MVC.Library.Models.UserTasks
{
    /// <summary>
    /// 待办个数和简要的待办列表
    /// </summary>
    public class UserTaskCountAndSimpleListModel
    {
        public string ServerTag
        {
            get;
            set;
        }

        public UserTaskCount CountData
        {
            get;
            set;
        }

        public UserTaskCollection TaskData
        {
            get;
            set;
        }

        public UserTaskCollection NotifyData
        {
            get;
            set;
        }
    }
}
