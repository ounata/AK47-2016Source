using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Cloud.Sms.Web2.Utility;

namespace PPTS.Services.Common.Services
{
    // SMS Service Interface.
    [ServiceContract]
    public interface ISMSService
    {
        [OperationContract]
        void SendSMS(string mobileNumbers, string msg, string fromSystem);

        [OperationContract]
        void SendSMSWithAd(string mobileNumbers, string msg, string fromSystem);

        [OperationContract]
        void SendScheduleSMS(string mobileNumbers, string msg, DateTime startTime, string fromSystem);
    }
}
