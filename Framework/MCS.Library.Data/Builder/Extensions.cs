using MCS.Library.Configuration;
using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data.Builder
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// 得到数据当前时间的函数。根据配置信息进行判断
        /// </summary>
        /// <param name="context"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static string DBCurrentTimeFunction(this TimePointContext context, ISqlBuilder builder)
        {
            context.NullCheck("context");
            builder.NullCheck("builder");

            string result = builder.DBCurrentTimeFunction;

            TimeZoneContextSettings settings = TimeZoneContextSettings.GetConfig();

            if (settings.Enabled && settings.TimePointKind == DateTimeKind.Utc)
                result = TSqlBuilder.Instance.DBCurrentUtcTimeFunction;

            return result;
        }
    }
}
