using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Web.MVC.Library.Models
{
    /// <summary>
    /// 附件的状态
    /// </summary>
    public enum MaterualModelStatus
    {
        /// <summary>
        /// 未修改
        /// </summary>
        Unmodified = 0,

        /// <summary>
        /// 新增加
        /// </summary>
        Inserted = 1,

        /// <summary>
        /// 已修改
        /// </summary>
        Updated = 2,

        /// <summary>
        /// 已删除
        /// </summary>
        Deleted = 3
    }
}
