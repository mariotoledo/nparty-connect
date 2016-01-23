﻿using NParty.Www.Controllers;
using NParty.Www.Helpers;
using NParty.Www.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Www.Areas.ESports.Controllers
{
    public class ESportsController : NPartyController
    {
        //
        // GET: /ESports/ESports/

        protected ActionResult OpenPageByLabel(string label, int? page)
        {
            int pageValue = page.HasValue && page.Value > 0 ? page.Value : 0;

            NPartyArticlesHelper helper = new NPartyArticlesHelper();
            List<Article> articles = helper.GetArticlesFromBlog(ESportsBlogId, "ESports", MaxPosts, (MaxPosts * pageValue) + 1, label);
            List<Article> hilights = helper.GetArticlesFromBlog(ESportsBlogId, "ESports", 5, 0, "Destaque");

            ViewData["articles"] = articles;
            ViewData["hilights"] = hilights;
            ViewData["currentPage"] = pageValue;

            return View();
        }

    }
}
