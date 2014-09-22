using EixoX.Web.AuthComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminConnect.Controllers
{
    public class DashboardController : NPartyController
    {
        [AuthenticationRequired]
        public ActionResult Index()
        {
            return View();
        }
    }
}