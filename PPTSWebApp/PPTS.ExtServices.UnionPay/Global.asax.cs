﻿using PPTS.ExtServices.UnionPay.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace PPTS.ExtServices.UnionPay
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGloablFilters(GlobalFilters.Filters);
        }

        
    }
}