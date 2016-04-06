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
    public class NintendoA3Controller : PodcastsController
    {
        //
        // GET: /Podcasts/NintendoA3/

        public ActionResult Ouvir(string id)
        {
            ArticlesHelper helper = new ArticlesHelper(
                System.Configuration.ConfigurationManager.AppSettings["GoogleAppName"],
                System.Configuration.ConfigurationManager.AppSettings["BloggerApiKey"]
            );

            if (string.IsNullOrEmpty(id))
            {
                return Redirect("~/Podcasts/Home/Index");
            }

            Article article = helper.GetSingleArticleFromBlog(NintendoBlogId, "Podcasts", id);

            if (article == null)
                return Redirect("~/Ops/NaoEncontrado");

            ViewData["article"] = article;

            return View();
        }

        public ActionResult Index(int? page)
        {
            int pageValue = page.HasValue && page.Value > 0 ? page.Value : 0;

            NPartyArticlesHelper helper = new NPartyArticlesHelper();
            List<Article> nintendoa3Articles = helper.GetArticlesFromBlog(NintendoBlogId, "Podcasts", 20, (20 * pageValue) + 1, "Nintendo a 3", "/NintendoA3/Ouvir/");

            ViewData["articles"] = nintendoa3Articles;
            ViewData["currentPage"] = pageValue;

            return View();
        }
    }
}