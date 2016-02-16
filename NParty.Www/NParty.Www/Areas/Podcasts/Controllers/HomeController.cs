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
            List<Article> nintendoArticles = helper.GetArticlesFromBlog(NintendoBlogId, "Nintendo", 20, (20 * pageValue) + 1, "Podcast");
            List<Article> podcastsArticles = helper.GetArticlesFromBlog(MainBlogId, "Podcasts", 20, (20 * pageValue) + 1, "Podcast");

            List<Article> podcasts = new List<Article>();
            podcasts.AddRange(nintendoArticles);
            podcasts.AddRange(podcastsArticles);

            ViewData["articles"] = podcasts.OrderByDescending(t => t.DatePublished).ToList();
            ViewData["currentPage"] = pageValue;

            return View();
        }
    }
}
