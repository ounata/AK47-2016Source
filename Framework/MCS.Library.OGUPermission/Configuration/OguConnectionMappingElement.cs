using MCS.Library.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.OGUPermission
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class OguConnectionMappingElement : NamedConfigurationElement
    {
        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty("destination", IsRequired = true)]
        public string Destination
        {
            get
            {
                return (string)this["destination"];
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class OguConnectionMappingElementCollection : NamedConfigurationElementCollection<OguConnectionMappingElement>
    {
    }
}
