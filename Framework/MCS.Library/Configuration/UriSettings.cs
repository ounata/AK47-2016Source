using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class UriSettings : ConfigurationSection
    {
        /// <summary>
        /// URL配置信息的配置节
        /// </summary>
        /// <returns></returns>
        public static UriSettings GetConfig()
        {
            UriSettings result = (UriSettings)ConfigurationBroker.GetSection("uriSettings");

            if (result == null)
                result = new UriSettings();

            return result;
        }

        private UriSettings()
        {
        }

        /// <summary>
        /// Url组
        /// </summary>
        [ConfigurationProperty("groups", IsRequired = false)]
        public UriGroupConfigurationCollection Groups
        {
            get
            {
                return (UriGroupConfigurationCollection)this["groups"];
            }
        }

        /// <summary>
        /// 得到Url，如果没有找到，则返回null
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="urlName"></param>
        /// <returns></returns>
        public Uri GetUrl(string groupName, string urlName)
        {
            Uri result = null;

            UriGroupConfigurationElement group = this.Groups[groupName];

            if (group != null)
            {
                UriConfigurationElement uriElement = group.Urls[urlName];

                if (uriElement != null)
                    result = uriElement.Uri;
            }

            return result;
        }

        /// <summary>
        /// 得到Url，如果没有找到，则抛出异常
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="urlName"></param>
        /// <returns></returns>
        public Uri CheckAndGet(string groupName, string urlName)
        {
            return this.Groups.CheckAndGet(groupName).Urls.CheckAndGet(urlName).Uri;
        }

        /// <summary>
        /// 得到某个group下的UriConfigurationElement
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public Dictionary<string, UriConfigurationElement> GetUrlsInGroup(string groupName)
        {
            groupName.CheckStringIsNullOrEmpty("groupName");

            Dictionary<string, UriConfigurationElement> result = new Dictionary<string, UriConfigurationElement>();

            UriGroupConfigurationElement groupElement = this.Groups[groupName];

            if (groupElement != null)
            {
                for (int i = 0; i < groupElement.Urls.Count; i++)
                {
                    UriConfigurationElement uriElem = groupElement.Urls[i];

                    result[uriElem.Name] = uriElem;
                }
            }

            return result;
        }
    }
}
