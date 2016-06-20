using EixoX;
using NParty.Admin.Usecases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Admin.Areas.ESports.Controllers
{
    public class TeamController : NPartyController, Viewee
    {
        // GET: ESports/Team
        public ActionResult Index()
        {
            ViewData["teams"] = new TeamLister().All(this);
            return View();
        }

        public void OnException(Exception ex)
        {
            FlashMessage("Ocorreu um erro inesperado: " + ex.Message, MessageType.Error);
        }
    }
}