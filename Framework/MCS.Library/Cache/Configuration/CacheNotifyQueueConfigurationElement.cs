using MCS.Library.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Caching
{
    /// <summary>
    /// 通知项配置
    /// </summary>
    public class CacheNotifyQueueConfigurationElement : NamedConfigurationElement
    {
        /// <summary>
        /// Cache队列的
        /// </summary>
        [ConfigurationProperty("queueName")]
        public string QueueName
        {
            get
            {
                return (string)this["queueName"];
            }
        }
    }

    /// <summary>
    /// 通知项的集合
    /// </summary>
    public class CacheNotifyQueueConfigurationElementCollection : NamedConfigurationElementCollection<CacheNotifyQueueConfigurationElement>
    {
    }
}
