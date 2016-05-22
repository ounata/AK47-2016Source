using MCS.Library.Configuration;
using MCS.Library.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Web.MVC.Library.Configuration
{
    public sealed class JsonDotNetConvertersSettings : DeluxeConfigurationSection
    {
        public static JsonDotNetConvertersSettings GetConfig()
        {
            JsonDotNetConvertersSettings settings = (JsonDotNetConvertersSettings)ConfigurationBroker.GetSection("jsonDotNetConvertersSettings");

            if (settings == null)
                settings = new JsonDotNetConvertersSettings();

            return settings;
        }

        private JsonDotNetConvertersSettings()
        {
        }

        /// <summary>
        /// 得到配置的JsonConverter集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<JsonConverter> GetConverters()
        {
            foreach (TypeConfigurationElement element in this.ConverterElements)
            {
                JsonConverter converter = ExceptionHelper.DoSilentFunc(() => element.CreateInstance<JsonConverter>(), (JsonConverter)null);

                if (converter != null)
                    yield return converter;
            }
        }

        [ConfigurationProperty("converters")]
        private TypeConfigurationCollection ConverterElements
        {
            get
            {
                return (TypeConfigurationCollection)this["converters"];
            }
        }
    }
}
