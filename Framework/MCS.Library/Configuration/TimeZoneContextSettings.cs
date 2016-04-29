using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Configuration
{
    /// <summary>
    /// 时区偏移的配置信息
    /// </summary>
    public class TimeZoneContextSettings : DeluxeConfigurationSection
    {
        /// <summary>
        /// 得到时区偏移的配置信息
        /// </summary>
        /// <returns></returns>
        public static TimeZoneContextSettings GetConfig()
        {
            TimeZoneContextSettings settings = (TimeZoneContextSettings)ConfigurationBroker.GetSection("timeZoneContextSettings");

            if (settings == null)
                settings = new TimeZoneContextSettings();

            return settings;
        }

        /// <summary>
        /// 是否启用时区偏移
        /// </summary>
        [ConfigurationProperty("enabled", IsRequired = false, DefaultValue = false)]
        public bool Enabled
        {
            get
            {
                return (bool)this["enabled"];
            }
        }

        /// <summary>
        /// 时区的偏移量
        /// </summary>
        [ConfigurationProperty("timeOffset", IsRequired = false, DefaultValue = "08:00:00")]
        public TimeSpan TimeOffset
        {
            get
            {
                return (TimeSpan)this["timeOffset"];
            }
        }

        /// <summary>
        /// TimeZone的ID
        /// </summary>
        [ConfigurationProperty("timeZoneID", IsRequired = false, DefaultValue = "TimeZoneContextID")]
        public string TimeZoneID
        {
            get
            {
                return (string)this["timeZoneID"];
            }
        }

        /// <summary>
        /// TimeZone的ID
        /// </summary>
        [ConfigurationProperty("timeZoneName", IsRequired = false, DefaultValue = "东八区")]
        public string TimeZoneName
        {
            get
            {
                return (string)this["timeZoneName"];
            }
        }

        /// <summary>
        /// 时间点的类型，用于时间切片的默认当前时间。默认是Local。如果是UTC，则在使用时间片的时候
        /// </summary>
        [ConfigurationProperty("timePointKind", IsRequired = false, DefaultValue = DateTimeKind.Local)]
        public DateTimeKind TimePointKind
        {
            get
            {
                return (DateTimeKind)this["timePointKind"];
            }
        }
    }
}
