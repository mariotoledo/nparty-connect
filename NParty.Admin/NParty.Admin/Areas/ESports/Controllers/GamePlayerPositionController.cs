using EixoX;
using NParty.Admin.Usecases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Admin.Areas.ESports.Controllers
{
    public class GamePlayerPositionController : NPartyController, Viewee
    {
        public ActionResult Index(int id)
        {
            ViewData["gameRuleData"] = new GamePlayerPositionLister().AllFromGameId(id, this);
            return View();
        }

        public void OnException(Exception ex)
        {
            FlashMessage("Ocorreu um erro inesperado: " + ex.Message, MessageType.Error);
        }
    }
}