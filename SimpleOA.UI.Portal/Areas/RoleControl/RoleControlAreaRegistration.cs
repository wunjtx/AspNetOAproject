using System.Web.Mvc;

namespace SimpleOA.UI.Portal.Areas.RoleControl
{
    public class RoleControlAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "RoleControl";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "RoleControl_default",
                "RoleControl/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}