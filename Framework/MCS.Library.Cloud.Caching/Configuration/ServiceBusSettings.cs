using MCS.Library.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.Caching.Configuration
{
    public class ServiceBusSettings : ConfigurationSection
    {
        public static ServiceBusSettings GetConfig()
        {
            ServiceBusSettings settings = (ServiceBusSettings)ConfigurationBroker.GetSection("serviceBusSettings");

            settings.CheckSectionNotNull("ServiceBusSettings");

            return settings;
        }

        [ConfigurationProperty("connectionStrings", IsRequired = false)]
        public ConnectionStringConfigurationElementBaseCollection ConnectionStrings
        {
            get
            {
                return (ConnectionStringConfigurationElementBaseCollection)this["connectionStrings"];
            }
        }
    }
}
