using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Net.SNTP
{
    /// <summary>
    /// 日期类型
    /// </summary>
    public enum SNTPDateTimeKind
    {
        /// <summary>
        /// 由TimeZoneContext决定
        /// </summary>
        ByTimeZoneContext = 0,

        /// <summary>
        /// Utc时间
        /// </summary>
        Utc = 1,

        /// <summary>
        /// 本地时间
        /// </summary>
        Local = 2
    }
}
