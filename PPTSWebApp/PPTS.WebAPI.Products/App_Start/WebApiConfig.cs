using System.Web.Http;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Formatting;
using MCS.Web.MVC.Library.Converters;
using Newtonsoft.Json.Converters;
using MCS.Web.MVC.Library.Filters;

namespace PPTS.WebAPI.Products
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
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
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new JavascriptUpwardDatetimeConverter());
            config.Filters.Add(new ApiExceptionFilterAttribute());

        }
    }
}
