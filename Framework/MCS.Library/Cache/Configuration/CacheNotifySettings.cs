using MCS.Library.Configuration;
using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Caching
{
    /// <summary>
    /// UDP Cache通知时的分组配置
    /// </summary>
    public class CacheNotifySettings : DeluxeConfigurationSection
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static CacheNotifySettings GetConfig()
        {
            CacheNotifySettings result = (CacheNotifySettings)ConfigurationBroker.GetSection("cacheNotifySettings");

            if (result == null)
                result = new CacheNotifySettings();

            return result;
        }

        /// <summary>
        /// 通知项的组
        /// </summary>
        [ConfigurationProperty("notifyGroups", IsRequired = false)]
        public CacheNotifyGroupConfigurationElementCollection NotifyGroups
        {
            get
            {
                return (CacheNotifyGroupConfigurationElementCollection)this["notifyGroups"];
            }
        }

        /// <summary>
        /// 得到某个group下的UriConfigurationElement
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public IEnumerable<string> GetQueueNamesInGroup(string groupName)
        {
            groupName.CheckStringIsNullOrEmpty("groupName");

            List<string> result = new List<string>();

            CacheNotifyGroupConfigurationElement groupElement = this.NotifyGroups[groupName];

            if (groupElement != null)
            {
                foreach (CacheNotifyQueueConfigurationElement qc in groupElement.Queues)
                    result.Add(qc.QueueName);
            }

            return result;
        }

        private CacheNotifySettings()
        {
        }
    }
}
