using MCS.Library.Core;
using MCS.Web.MVC.Library.Models;
using MCS.Web.Responsive.Library;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MCS.Web.MVC.Library.Providers
{
    public class ScriptProgressResponser : IProcessProgressResponser
    {
        public static readonly ScriptProgressResponser Instance = new ScriptProgressResponser();

        private ScriptProgressResponser()
        {

        }

        public void Register(ProcessProgress progress)
        {
            progress.MinStep = 0;
            progress.MaxStep = 100;
            progress.CurrentStep = 1;
            progress.StatusText = string.Empty;
        }

        public void Response(ProcessProgress progress)
        {
            HttpResponse response = HttpContext.Current.Response;

            string script = GetChangeProcessInfoScript(progress, "progressUpdate", true);
            response.Write(script);

            response.Flush();
        }

        public void ResponseResult(string funcName, ProcessFileResult pfResult)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();

            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            string json = ScriptHelper.CheckScriptString(JsonConvert.SerializeObject(pfResult, settings), false);

            string script = string.Format("{0}(\"{1}\")", funcName, json);

            script = string.Format("<script type=\"text/javascript\">\n    parent.{0}\n</script>", script);

            HttpResponse response = HttpContext.Current.Response;

            response.Write(script);

            response.Flush();
        }

        private static string GetChangeProcessInfoScript(ProcessProgress progress, string funcName, bool addScriptTag)
        {
            string json = JsonConvert.SerializeObject(new
            {
                minStep = progress.MinStep,
                maxStep = progress.MaxStep,
                currentStep = progress.CurrentStep,
                statusText = progress.StatusText
            });

            json = ScriptHelper.CheckScriptString(json);

            string script = string.Format("{0}(\"{1}\")", funcName, json);

            if (addScriptTag)
                script = string.Format("<script type=\"text/javascript\">\n    parent.{0}\n</script>", script);

            return script;
        }
    }
}
