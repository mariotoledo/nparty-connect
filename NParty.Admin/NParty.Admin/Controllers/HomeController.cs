using EixoX;
using NParty.Admin.Usecases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Admin.Controllers
{
    public class HomeController : Controller, Viewee
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewData["games"] = new GameLister().All(this);
            return View();
        }

        public void OnException(Exception ex)
        {
        }
    }
}