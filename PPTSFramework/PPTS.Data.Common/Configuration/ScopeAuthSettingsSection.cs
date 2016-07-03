using MCS.Library.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Configuration
{
    public class ScopeAuthSettingsSection : DeluxeConfigurationSection
    {
        /// <summary>
		/// 获取Cache的配置信息
		/// </summary>
		/// <returns>Cache的配置信息</returns>
		public static ScopeAuthSettingsSection GetConfig()
        {
            ScopeAuthSettingsSection result = (ScopeAuthSettingsSection)ConfigurationBroker.GetSection("scopeAuthSettings");

            if (result == null)
                result = new ScopeAuthSettingsSection();

            return result;
        }

        private ScopeAuthSettingsSection()
        {
        }

        /// <summary>
		/// 是否启用功能
		/// </summary>
		[ConfigurationProperty("enabled", DefaultValue = true, IsRequired = false)]
        public bool Eabled
        {
            get
            {
                return (bool)this["enabled"];
            }
        }
    }
}
