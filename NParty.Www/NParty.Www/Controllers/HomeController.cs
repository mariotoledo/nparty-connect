using Google.GData.Client;
using NParty.Www.Helpers;
using NParty.Www.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Www.Controllers
{
    public class HomeController : NPartyController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            NPartyArticlesHelper helper = new NPartyArticlesHelper(NintendoBlogId, ESportsBlogId, EventosBlogId);
            List<Article> hilights = helper.GetHomeHilights();
            List<Article> nintendoArticles = helper.GetArticlesFromBlog(4, NintendoBlogId);
            List<Article> esportsArticles = helper.GetArticlesFromBlog(4, ESportsBlogId);
            List<Article> eventosArticles = helper.GetArticlesFromBlog(4, EventosBlogId);

            ViewData["Hilights"] = hilights;
            ViewData["NintendoPosts"] = nintendoArticles;
            ViewData["eSportsPosts"] = esportsArticles;
            ViewData["EventosPosts"] = eventosArticles;

            return View();
        }

    }
}
