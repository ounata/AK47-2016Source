using System.Web;
using System.Web.Mvc;

namespace PPTS.WebAPI.Product
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
