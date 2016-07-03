using MCS.Library.Core;
using MCS.Web.MVC.Library.Configuration;
using MCS.Web.MVC.Library.Converters;
using MCS.Web.MVC.Library.Filters;
using MCS.Web.MVC.Library.Providers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Validation;

namespace MCS.Web.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Services.Replace(typeof(IBodyModelValidator), new MCSBodyModelValidator());

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            JsonDotNetConvertersSettings.GetConfig().GetConverters().ForEach(converter => config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(converter));
        }
    }
}
