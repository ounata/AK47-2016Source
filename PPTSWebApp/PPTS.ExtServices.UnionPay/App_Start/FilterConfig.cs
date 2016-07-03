using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PPTS.ExtServices.UnionPay.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGloablFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}