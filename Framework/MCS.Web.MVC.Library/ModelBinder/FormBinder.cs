using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;

namespace MCS.Web.MVC.Library.ModelBinder
{
    public class FormBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            var body = actionContext.Request.Content.ReadAsStringAsync().Result;

            var j = actionContext.ControllerContext.Configuration.Formatters.JsonFormatter;
            var stream = new MemoryStream(HttpUtility.UrlDecodeToBytes(body.Substring(body.IndexOf("=") + 1)));

            j.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;

            var model = j.ReadFromStream(bindingContext.ModelType, stream, Encoding.UTF8, null);
            bindingContext.Model = model;
            return true;
        }
    }
}
