using NParty.Www.Controllers;
using NParty.Www.Helpers;
using NParty.Www.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Www.Areas.Podcasts.Controllers
{
    public class PodcastsController : NPartyController
    {
        //
        // GET: /Podcasts/Podcasts/

        protected ActionResult OpenPageByLabel(string label, int? page)
        {
            int pageValue = page.HasValue && page.Value > 0 ? page.Value : 0;

            NPartyArticlesHelper helper = new NPartyArticlesHelper();
            List<Article> articles = helper.GetArticlesFromBlog(MainBlogId, "Podcasts", MaxPosts, (MaxPosts * pageValue) + 1, label, "/Ouvir/");

            ViewData["articles"] = articles;
            ViewData["currentPage"] = pageValue;

            return View();
        }
    }
}