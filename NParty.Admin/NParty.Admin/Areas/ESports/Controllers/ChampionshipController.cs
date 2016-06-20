using NParty.Database.ESports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Admin.Areas.ESports.Controllers
{
    public class ChampionshipController : NPartyController
    {
        // GET: ESports/Championship
        public ActionResult Index()
        {
            List<ESportsChampionship> championships = ESportsChampionship.Select().ToList();
            ViewData["championships"] = championships;

            return View();
        }
    }
}