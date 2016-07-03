using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Cloud.Sms.Web2.Utility;
using MCS.Library.WcfExtensions;


namespace PPTS.Services.Common.Services
{
    //Service for outside business projects
    public class SMSService : ISMSService
    {



        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void SendScheduleSMS(string mobileNumbers, string msg, DateTime startTime, string fromSystem)
        {
            SmsFactory.CreateSendScheduleSMSProvider().SendScheduleSMSEx(mobileNumbers, msg, startTime, fromSystem);
        }


        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void SendSMS(string mobileNumbers, string msg, string fromSystem)
        {
            SmsFactory.CreateSendSmsProvider().SendSMSEx(mobileNumbers, msg, fromSystem);
        }


        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void SendSMSWithAd(string mobileNumbers, string msg, string fromSystem)
        {
            SmsFactory.CreateSendSMSWithAdProvider().SendSMSWithAdEx(mobileNumbers, msg, fromSystem);
        }
    }
}
