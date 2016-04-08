using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PPTS.WebAPI.Order.Controllers
{
    public class StuAsgmtController : ApiController
    {
        [HttpGet]
        public string GetAllStuUnAsgmt()
        {
            return "这是测试例子";
        }


    }
}
