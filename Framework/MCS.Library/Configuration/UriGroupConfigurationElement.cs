using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Configuration
{
    /// <summary>
    /// Uri配置组
    /// </summary>
    public class UriGroupConfigurationElement : NamedConfigurationElement
    {
        /// <summary>
        /// Uri的集合
        /// </summary>
        [ConfigurationProperty("urls", IsRequired = false)]
        public UriConfigurationCollection Urls
        {
            get
            {
                return (UriConfigurationCollection)this["urls"];
            }
        }
    }

    /// <summary>
    /// Uri配置组集合
    /// </summary>
    public class UriGroupConfigurationCollection : NamedConfigurationElementCollection<UriGroupConfigurationElement>
    {
    }
}
