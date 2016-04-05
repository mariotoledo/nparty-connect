using NParty.Www.Helpers;
using NParty.Www.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Www.Areas.Podcasts.Controllers
{
    public class HomeController : PodcastsController
    {
        //
        // GET: /Podcasts/Home/

        public ActionResult Index(int? page)
        {
            int pageValue = page.HasValue && page.Value > 0 ? page.Value : 0;

            NPartyArticlesHelper helper = new NPartyArticlesHelper();
            List<Article> nintendoa3Articles = helper.GetArticlesFromBlog(NintendoBlogId, "Podcasts", 20, (20 * pageValue) + 1, "Nintendo a 3", "/NintendoA3/Ouvir/");
            List<Article> costaACostaArticles = helper.GetArticlesFromBlog(MainBlogId, "Podcasts", 20, (20 * pageValue) + 1, "N-Party Costa a Costa", "/NPartyCostaACosta/Ouvir/");

            List<Article> podcasts = new List<Article>();
            podcasts.AddRange(nintendoa3Articles);
            podcasts.AddRange(costaACostaArticles);

            ViewData["articles"] = podcasts.OrderByDescending(t => t.DatePublished).ToList();
            ViewData["currentPage"] = pageValue;

            return View();
        }
    }
}