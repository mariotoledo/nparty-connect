﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Www.Controllers
{
    public class NintendoController : NPartyController
    {
        //
        // GET: /Nintendo/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Ler(int id)
        {
            return View();
        }
    }
}
