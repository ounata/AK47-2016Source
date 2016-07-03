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
    /// 
    /// </summary>
    public class CacheNotifyGroupConfigurationElement : NamedConfigurationElement
    {
        /// <summary>
        /// 通知项
        /// </summary>
        [ConfigurationProperty("queues", IsRequired = false)]
        public CacheNotifyQueueConfigurationElementCollection Queues
        {
            get
            {
                return (CacheNotifyQueueConfigurationElementCollection)this["queues"];
            }
        }
    }

    /// <summary>
    /// 通知组的集合
    /// </summary>
    public class CacheNotifyGroupConfigurationElementCollection : NamedConfigurationElementCollection<CacheNotifyGroupConfigurationElement>
    {
    }
}
