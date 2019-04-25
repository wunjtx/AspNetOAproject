using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleOA.UI.Portal
{
    public class LoginCheckFilterAttribute:ActionFilterAttribute
    {
        public bool LoginCheck { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (LoginCheck)
            {
                if (filterContext.HttpContext.Session["loginUser"]==null)
                {
                    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
                }
            }
        }
    }
}