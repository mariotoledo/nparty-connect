using NParty.Www.Helpers;
using NParty.Www.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Www.Controllers
{
    public class EventosController : NPartyController
    {
        //
        // GET: /Eventos/

        public ActionResult Index(int? page)
        {
            int pageValue = page.HasValue && page.Value > 0 ? page.Value : 0;

            NPartyArticlesHelper helper = new NPartyArticlesHelper();
            List<Article> nintendoArticles = helper.GetArticlesFromBlog(EventosBlogId, "Eventos", MaxPosts, (MaxPosts * pageValue) + 1);
            List<Article> hilights = helper.GetArticlesFromBlog(EventosBlogId, "Eventos", 5, 0, "Destaque");

            ViewData["articles"] = nintendoArticles;
            ViewData["hilights"] = hilights;
            ViewData["currentPage"] = pageValue;

            return View();
        }

        private ActionResult OpenPageByLabel(string label, int? page)
        {
            int pageValue = page.HasValue && page.Value > 0 ? page.Value : 0;

            NPartyArticlesHelper helper = new NPartyArticlesHelper();
            List<Article> articles = helper.GetArticlesFromBlog(EventosBlogId, "Eventos", MaxPosts, (MaxPosts * pageValue) + 1, label);
            List<Article> hilights = helper.GetArticlesFromBlog(EventosBlogId, "Eventos", 5, 0, "Destaque");

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

            Article article = helper.GetSingleArticleFromBlog(EventosBlogId, "Eventos", id);

            ViewData["article"] = article;

            List<Article> hilights = helper.GetArticlesFromBlog(EventosBlogId, "Eventos", 5, 0, "Destaque");
            ViewData["hilights"] = hilights;

            return View();
        }

        public ActionResult Coberturas(int? page) { return OpenPageByLabel("Cobertura", page); }
        public ActionResult AnimeFriends(int? page) { return OpenPageByLabel("Anime Friends", page); }
        public ActionResult BGS(int? page) { return OpenPageByLabel("BGS", page); }
        public ActionResult BMA(int? page) { return OpenPageByLabel("XMA", page); }

        public JsonResult GetRelatedPosts(string label)
        {
            ArticlesHelper helper = new ArticlesHelper();
            return Json(helper.GetArticlesFromBlog(EventosBlogId, "Eventos", 3, 0, label), JsonRequestBehavior.AllowGet);
        }
    }
}
