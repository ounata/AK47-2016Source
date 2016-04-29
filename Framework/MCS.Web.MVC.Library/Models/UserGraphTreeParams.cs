using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCS.Web.MVC.Library.Models
{
    /// <summary>
    /// 用户选择控件的参数
    /// </summary>
    public class UserGraphTreeParams
    {
        /// <summary>
        /// 构造方法，得到默认的参数
        /// </summary>
        public UserGraphTreeParams()
        {
            this.ShowDeletedObjects = false;
            this.ListMask = UserGraphControlObjectMask.All;
        }

        /// <summary>
        /// 当期的节点ID
        /// </summary>
        public string Id
        {
            get;
            set;
        }

        /// <summary>
        /// 是否显示被删除（禁用的）对象
        /// </summary>
        public bool ShowDeletedObjects
        {
            get;
            set;
        }

        /// <summary>
        /// 能够列出的对象类型
        /// </summary>
        public UserGraphControlObjectMask ListMask
        {
            get;
            set;
        }
    }
}