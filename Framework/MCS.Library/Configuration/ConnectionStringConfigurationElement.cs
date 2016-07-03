using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Configuration
{
    /// <summary>
    /// 用于连接串的配置项
    /// </summary>
    public class ConnectionStringConfigurationElementBase : NamedConfigurationElement
    {
        /// <summary>
        /// 连接串属性
        /// </summary>
        [ConfigurationProperty("connectionString")]
        public string ConnectionString
        {
            get
            {
                return (string)this["connectionString"];
            }
        }
    }

    /// <summary>
    /// 用于连接串的配置项集合
    /// </summary>
    public class ConnectionStringConfigurationElementBaseCollection : NamedConfigurationElementCollection<ConnectionStringConfigurationElementBase>
    {
    }
}
