using NParty.Www.Helpers;
using NParty.Www.Models;
using System;
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

        public ActionResult Index(int? page)
        {
            int pageValue = page.HasValue && page.Value > 0 ? page.Value : 0;

            NPartyArticlesHelper helper = new NPartyArticlesHelper();
            List<Article> nintendoArticles = helper.GetArticlesFromBlog(NintendoBlogId, "Nintendo", MaxPosts, (MaxPosts * pageValue) + 1);
            List<Article> hilights = helper.GetArticlesFromBlog(NintendoBlogId, "Nintendo", 5, 0, "Destaque");

            ViewData["articles"] = nintendoArticles;
            ViewData["hilights"] = hilights;
            ViewData["currentPage"] = pageValue;

            return View();
        }

        public ActionResult NintendoWiiU(int? page) { return OpenPageByLabel("Nintendo Wii U", page); }
        public ActionResult Nintendo3DS(int? page) { return OpenPageByLabel("Nintendo 3DS", page); }
        public ActionResult Previews(int? page) { return OpenPageByLabel("Preview", page); }
        public ActionResult Reviews(int? page){ return OpenPageByLabel("Review", page); }
        public ActionResult Retro(int? page) { return OpenPageByLabel("Retrô", page); }
        public ActionResult Top10(int? page) { return OpenPageByLabel("Top 10", page); }
        public ActionResult Mario(int? page) { return OpenPageByLabel("Mario", page); }
        public ActionResult Pokemon(int? page){ return OpenPageByLabel("Pokémon", page); }
        public ActionResult TheLegendOfZelda(int? page){ return OpenPageByLabel("The Legend of Zelda", page); }

        private ActionResult OpenPageByLabel(string label, int? page)
        {
            int pageValue = page.HasValue && page.Value > 0 ? page.Value : 0;

            NPartyArticlesHelper helper = new NPartyArticlesHelper();
            List<Article> articles = helper.GetArticlesFromBlog(NintendoBlogId, "Nintendo", MaxPosts, (MaxPosts * pageValue) + 1, label);
            List<Article> hilights = helper.GetArticlesFromBlog(NintendoBlogId, "Nintendo", 5, 0, "Destaque");

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

            Article article = helper.GetSingleArticleFromBlog(NintendoBlogId, "Nintendo", id);

            ViewData["article"] = article;

            List<Article> hilights = helper.GetArticlesFromBlog(NintendoBlogId, "Nintendo", 5, 0, "Destaque");
            ViewData["hilights"] = hilights;

            return View();
        }

        public JsonResult GetRelatedPosts(string label)
        {
            ArticlesHelper helper = new ArticlesHelper();
            return Json(helper.GetArticlesFromBlog(NintendoBlogId, "Nintendo", 3, 0, label), JsonRequestBehavior.AllowGet);
        }
    }
}
