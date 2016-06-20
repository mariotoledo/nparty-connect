using System.Web.Mvc;

namespace NParty.Admin.Areas.ESports
{
    public class ESportsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ESports";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ESports_default",
                "ESports/{controller}/{action}/{id}/{desc}",
                new { action = "Index", controller = "Home", id = UrlParameter.Optional, desc = UrlParameter.Optional },
                namespaces: new[] { "NParty.Admin.Areas.ESports.Controllers" }
            );
        }
    }
}