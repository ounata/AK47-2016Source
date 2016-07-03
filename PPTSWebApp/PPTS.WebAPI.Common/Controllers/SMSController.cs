using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PPTS.Data.Common.Service;


namespace PPTS.WebAPI.Common.Controllers
{
    /// <summary>
    /// For SMS client test
    /// </summary>
    public class SMSController : ApiController
    {


        [HttpGet]
        public string Do()
        {

            SMSTask.Instance.SendSMSWithTask("13651164199", "验证码123456", "1122");
            return "ok";
        }
    }
}
