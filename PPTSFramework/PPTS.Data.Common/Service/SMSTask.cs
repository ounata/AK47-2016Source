using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Configuration;
using MCS.Library.Core;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.Workflow;


namespace PPTS.Data.Common.Service
{
    public class SMSTask
    {

        public static SMSTask Instance = new SMSTask();




        public void SendSMSWithTask(string mobileNumbers, string msg, string fromSystem)
        {
            InvokeServiceTask task = new InvokeServiceTask()
            {
                TaskID = UuidHelper.NewUuidString(),
                TaskTitle = "短信发送",
                ResourceID = UuidHelper.NewUuidString()
            };



            var url = UriSettings.GetConfig().GetUrl("pptsServices", "smsService").ToString();

            var parameters = new WfServiceOperationParameterCollection();
            parameters.Add(new WfServiceOperationParameter("mobileNumbers", mobileNumbers));
            parameters.Add(new WfServiceOperationParameter("msg", msg));
            parameters.Add(new WfServiceOperationParameter("fromSystem", fromSystem));

            var serviceDefine = new WfServiceOperationDefinition(new WfServiceAddressDefinition(WfServiceRequestMethod.Post, url, WfServiceContentType.Json), "SendSMS", parameters, "");


            task.SvcOperationDefs.Add(serviceDefine);

            task.FillData();


            InvokeServiceTaskAdapter.Instance.Push(task);

        }



        public void SendSMSWithAdTask(string mobileNumbers, string msg, string fromSystem)
        {
            InvokeServiceTask task = new InvokeServiceTask()
            {
                TaskID = UuidHelper.NewUuidString(),
                TaskTitle = "广告短信发送",
                ResourceID = UuidHelper.NewUuidString()
            };


            var url = UriSettings.GetConfig().GetUrl("pptsServices", "orderScopeAuthorizationService").ToString();

            var parameters = new WfServiceOperationParameterCollection();
            parameters.Add(new WfServiceOperationParameter("mobileNumbers", mobileNumbers));
            parameters.Add(new WfServiceOperationParameter("msg", msg));
            parameters.Add(new WfServiceOperationParameter("fromSystem", fromSystem));


            var serviceDefine = new WfServiceOperationDefinition(new WfServiceAddressDefinition(WfServiceRequestMethod.Post, url, WfServiceContentType.Json), "SendSMS", parameters, "");


            task.SvcOperationDefs.Add(serviceDefine);

            task.FillData();

            InvokeServiceTaskAdapter.Instance.Push(task);

        }



        public void SendScheduleSMSTask(string mobileNumbers, string msg, DateTime startTime, string fromSystem)
        {
            InvokeServiceTask task = new InvokeServiceTask()
            {
                TaskID = UuidHelper.NewUuidString(),
                TaskTitle = "定时短信发送",
                ResourceID = UuidHelper.NewUuidString()
            };


            var url = UriSettings.GetConfig().GetUrl("pptsServices", "orderScopeAuthorizationService").ToString();

            var parameters = new WfServiceOperationParameterCollection();
            parameters.Add(new WfServiceOperationParameter("mobileNumbers", mobileNumbers));
            parameters.Add(new WfServiceOperationParameter("msg", msg));
            parameters.Add(new WfServiceOperationParameter("startTime", startTime));
            parameters.Add(new WfServiceOperationParameter("fromSystem", fromSystem));


            var serviceDefine = new WfServiceOperationDefinition(new WfServiceAddressDefinition(WfServiceRequestMethod.Post, url, WfServiceContentType.Json), "SendSMS", parameters, "");


            task.SvcOperationDefs.Add(serviceDefine);

            task.FillData();

            InvokeServiceTaskAdapter.Instance.Push(task);

        }
    }
}
