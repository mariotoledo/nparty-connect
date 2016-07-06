using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Admin.Areas.ESports.Controllers
{
    public class TeamFormationController : NPartyController
    {
        // GET: ESports/TeamFormation
        public ActionResult Index()
        {
            return View();
        }
    }
}