using MCS.Library.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.SOA.DataObjects.Configuration
{
    /// <summary>
    /// 固定时间任务执行的设置时间
    /// </summary>
    public class FixedTimeTaskSettings : DeluxeConfigurationSection
    {
        public static FixedTimeTaskSettings GetConfig()
        {
            FixedTimeTaskSettings settings = (FixedTimeTaskSettings)ConfigurationBroker.GetSection("fixedTimeTaskSettings");

            if (settings == null)
                settings = new FixedTimeTaskSettings();

            return settings;
        }

        private FixedTimeTaskSettings()
        {
        }

        /// <summary>
        /// 时间检查的容忍度。这里配置的当前时间的正负时间误差
        /// </summary>
        [ConfigurationProperty("timeTolerance", IsRequired = false, DefaultValue = "00:00:30")]
        public TimeSpan TimeTolerance
        {
            get
            {
                return (TimeSpan)this["timeTolerance"];
            }
        }

        /// <summary>
        /// 过期（清理）的时间
        /// </summary>
        [ConfigurationProperty("expireTime", IsRequired = false, DefaultValue = "1:00:00:00")]
        public TimeSpan ExpireTime
        {
            get
            {
                return (TimeSpan)this["expireTime"];
            }
        }
    }
}
