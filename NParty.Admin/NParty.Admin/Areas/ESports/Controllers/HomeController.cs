using NParty.Database.ESports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Admin.Areas.ESports.Controllers
{
    public class HomeController : NPartyController
    {
        // GET: ESports/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}