using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Web.MVC.Library.Models.UserTasks
{
    public class UserTaskSearchParams
    {
        public UserTaskSearchParams()
        {
            this.Top = 5;
        }

        /// <summary>
        /// 这个不应该使用。不应该从客户端传递过来
        /// </summary>
        public string UserLogonName { get; set; }

        public string OriginalServerTag
        {
            get;
            set;
        }

        /// <summary>
        /// 需要返回的个数
        /// </summary>
        public int Top
        {
            get;
            set;
        }
    }
}
