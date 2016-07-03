using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using MCS.Web.MVC.Library.Models.Workflow;
using MCS.Web.MVC.Library.ApiCore;
using Newtonsoft.Json;

namespace MCS.Web.API.Test
{
    [TestClass]
    public class WfClientProxyTest
    {
        [TestMethod]
        public void StartupTest()
        {
            string resourceID = DateTime.Now.ToString("yyyy-MM-ddHHmmss");
            WfClientStartupParameters parameters = new WfClientStartupParameters()
            {
                ProcessKey = "unittest",
                //UserLogonName = "zhangxiaoyan_2",
                ResourceID = resourceID,
                //TaskTitle = "单元测试任务" + resourceID,
                //TaskUrl = "http://www.microsoft.com/?id=" + resourceID,
                ProcessParameters = new System.Collections.Generic.Dictionary<string, object>()
            };
            parameters.ProcessParameters.Add("是否需要二级审批", false);

            WfClientProcess process = WfClientProxy.Startup(parameters);

            string json = JsonConvert.SerializeObject(process);

            Console.WriteLine(json); 
        }
    }
}
