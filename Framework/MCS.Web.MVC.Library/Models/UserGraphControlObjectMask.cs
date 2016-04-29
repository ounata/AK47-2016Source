using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCS.Web.MVC.Library.Models
{
    /// <summary>
    /// 所选择的对象掩码
    /// </summary>
    [Flags]
    public enum UserGraphControlObjectMask
    {
        /// <summary>
        /// 空
        /// </summary>
        None = 0,

        /// <summary>
		/// 机构
		/// </summary>
		Organization = 1,

        /// <summary>
        /// 人员
        /// </summary>
        User = 2,

        /// <summary>
        /// 组
        /// </summary>
        Group = 4,

        /// <summary>
        /// 兼职
        /// </summary>
        Sideline = 8,

        /// <summary>
        /// 所有人员
        /// </summary>
        All = 15
    }
}