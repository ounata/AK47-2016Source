using Owin;
using PPTS.ExtServices.UnionPay.App_Start;
using System.Web.Http;

namespace PPTS.ExtServices.UnionPay
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var configuration = new HttpConfiguration();
            WebApiConfig.Register(configuration);
            app.UseWebApi(configuration);
        }
    }
}