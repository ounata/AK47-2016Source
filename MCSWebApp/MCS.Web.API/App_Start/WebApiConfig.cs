using MCS.Library.Core;
using MCS.Web.MVC.Library.Configuration;
using MCS.Web.MVC.Library.Converters;
using MCS.Web.MVC.Library.Filters;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Formatting;
using System.Web.Http;

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

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            JsonDotNetConvertersSettings.GetConfig().GetConverters().ForEach(converter => config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(converter));

            config.Filters.Add(new ApiExceptionFilterAttribute());
        }
    }
}
