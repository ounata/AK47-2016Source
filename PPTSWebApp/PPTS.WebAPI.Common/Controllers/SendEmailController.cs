using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PPTS.Data.Common.Service;

namespace PPTS.WebAPI.Common.Controllers
{
    public class SendEmailController : ApiController
    {
        public string Send()
        {
            SendEmailService.Instance.SendEmail("liucy1898@hotmail.com", "hi", "i am liucy");
            return "success";
        }
    }
}
