using System.Web.Mvc;

namespace API.Areas.Evento
{
    public class EventoAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Evento";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Evento_default",
                "Evento/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}