using NParty.Www.Helpers;
using NParty.Www.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Www.Controllers
{
    public class ESportsController : NPartyController
    {
        //
        // GET: /ESports/

        public ActionResult Index(int? page)
        {
            int pageValue = page.HasValue && page.Value > 0 ? page.Value : 0;

            NPartyArticlesHelper helper = new NPartyArticlesHelper();
            List<Article> nintendoArticles = helper.GetArticlesFromBlog(ESportsBlogId, "ESports", MaxPosts, (MaxPosts * pageValue) + 1);
            List<Article> hilights = helper.GetArticlesFromBlog(ESportsBlogId, "ESports", 5, 0, "Destaque");

            ViewData["articles"] = nintendoArticles;
            ViewData["hilights"] = hilights;
            ViewData["currentPage"] = pageValue;

            return View();
        }

        private ActionResult OpenPageByLabel(string label, int? page)
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

        public ActionResult Ler(string id)
        {
            ArticlesHelper helper = new ArticlesHelper(
                System.Configuration.ConfigurationManager.AppSettings["GoogleAppName"],
                System.Configuration.ConfigurationManager.AppSettings["BloggerApiKey"]
            );

            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index");
            }

            Article article = helper.GetSingleArticleFromBlog(ESportsBlogId, "ESports", id);

            ViewData["article"] = article;

            List<Article> hilights = helper.GetArticlesFromBlog(ESportsBlogId, "ESports", 5, 0, "Destaque");
            ViewData["hilights"] = hilights;

            return View();
        }

        public ActionResult Dicas(int? page) { return OpenPageByLabel("Dicas", page); }
        public ActionResult LeagueOfLegends(int? page) { return OpenPageByLabel("League of Legends", page); }
        public ActionResult CounterStrike(int? page) { return OpenPageByLabel("Counter Strike", page); }

        public JsonResult GetRelatedPosts(string label)
        {
            ArticlesHelper helper = new ArticlesHelper();
            return Json(helper.GetArticlesFromBlog(ESportsBlogId, "ESports", 3, 0, label), JsonRequestBehavior.AllowGet);
        }

    }
}
