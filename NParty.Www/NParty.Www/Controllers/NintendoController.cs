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
        public ActionResult Pokemon20Anos(int? page) {
            ViewData["DailyMusicVideoId"] = GetDailyPokemonMusic()[DateTime.Now.Day];
            return OpenPageByLabel("Pokémon", page);
        }
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

            if (article == null)
                return Redirect("~/Ops/NaoEncontrado");

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

        private Dictionary<int, string>  GetDailyPokemonMusic()
        {
            Dictionary<int, string> youtubeVideoIdByDay = new Dictionary<int, string>();

            youtubeVideoIdByDay.Add(1, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(2, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(3, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(4, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(5, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(6, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(7, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(8, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(9, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(10, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(11, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(12, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(13, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(14, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(15, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(16, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(17, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(18, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(19, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(20, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(21, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(22, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(23, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(24, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(25, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(26, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(27, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(28, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(29, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(30, "vXJgQLknzfI");
            youtubeVideoIdByDay.Add(31, "vXJgQLknzfI");

            return youtubeVideoIdByDay;
        }
    }
}
