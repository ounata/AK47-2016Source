using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Caching;
using MCS.Library.Core;

namespace MCS.Library.Core
{
    /// <summary>
    /// 与时间点数据相关的操作上下文，主要用于处理VersionStartTime和VersionEndTime相关操作
    /// </summary>
    [ActionContextDescription(Key = "TimePointActionContext")]
    public class DBTimePointActionContext : ActionContextBase<DBTimePointActionContext>
    {
        /// <summary>
        /// 版本时间的上限（相当于无限大）
        /// </summary>
        public static readonly DateTime MaxVersionEndTime = new DateTime(9999, 9, 9);

        /// <summary>
        /// 在TSql中使用的表示当前时间的变量
        /// </summary>
        public static readonly string CurrentTimeTSqlVarName = "@currentTime";

        /// <summary>
        /// 构造方法
        /// </summary>
		public DBTimePointActionContext()
        {
        }

        /// <summary>
        /// 获取或设置表示操作的时间点的<see cref="DateTime"/> ，为<see cref="DateTime.MinValue"/>时表示当前时间。
        /// </summary>
        public DateTime TimePoint
        {
            get;
            set;
        }
    }
}
