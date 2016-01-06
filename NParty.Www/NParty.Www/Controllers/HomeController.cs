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
            NPartyArticlesHelper helper = new NPartyArticlesHelper();

            Dictionary<string, string> blogDomains = new Dictionary<string, string>();
            blogDomains.Add(NintendoBlogId, "Nintendo");
            blogDomains.Add(ESportsBlogId, "ESports");
            blogDomains.Add(EventosBlogId, "Eventos");

            List<Article> hilights = helper.GetHomeHilights(blogDomains);
            List<Article> nintendoArticles = helper.GetArticlesFromBlog(NintendoBlogId, "Nintendo", 4);
            List<Article> esportsArticles = helper.GetArticlesFromBlog(ESportsBlogId, "ESports", 4);
            List<Article> eventosArticles = helper.GetArticlesFromBlog(EventosBlogId, "Eventos", 4);

            ViewData["Hilights"] = hilights;
            ViewData["NintendoPosts"] = nintendoArticles;
            ViewData["eSportsPosts"] = esportsArticles;
            ViewData["EventosPosts"] = eventosArticles;

            return View();
        }

        [HttpGet]
        public ActionResult Busca(string q, int? page)
        {
            ArticlesHelper helper = new ArticlesHelper();

            int pageValue = page.HasValue && page.Value > 0 ? page.Value : 0;

            ViewData["query"] = q;
            ViewData["currentPage"] = pageValue;
            ViewData["SearchResults"] = helper.SearchArticlesInBlog(NintendoBlogId, "Nintendo", MaxPosts, (MaxPosts * pageValue) + 1, q);

            return View();
        }
    }
}
