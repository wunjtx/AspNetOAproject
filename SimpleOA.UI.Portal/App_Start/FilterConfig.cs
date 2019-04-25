using System.Web;
using System.Web.Mvc;

namespace SimpleOA.UI.Portal
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new WebExceptionFilterAttribute());
            //filters.Add(new LoginCheckFilterAttribute() { LoginCheck = true });
        }
    }
}
