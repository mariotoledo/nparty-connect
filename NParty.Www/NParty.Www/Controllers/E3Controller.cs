using Google.Apis.Blogger.v3;
using NParty.Www.Helpers;
using NParty.Www.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Www.Controllers
{
    public class E3Controller : NPartyController
    {
        //
        // GET: /E3/

        public ActionResult Index()
        {
            string apiKey = System.Configuration.ConfigurationManager.AppSettings["PodcastBloggerApiKey"];
            string appName = System.Configuration.ConfigurationManager.AppSettings["PodcastGoogleAppName"];

            BloggerService service = new BloggerService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                ApiKey = apiKey,
                ApplicationName  = appName
            });

            ArticlesHelper helper = new ArticlesHelper(apiKey, appName, service);

            List<Article> nintendoa3Articles = helper.GetArticlesFromBlog(NintendoBlogId, "Nintendo", 100, 1, "E3", "/Artigos/Ler/");
            List<Article> costaACostaArticles = helper.GetArticlesFromBlog(MainBlogId, "Home", 100, 1, "E3", "/NPartyCostaACosta/Ouvir/");

            List<Article> podcasts = new List<Article>();
            podcasts.AddRange(nintendoa3Articles);
            podcasts.AddRange(costaACostaArticles);

            ViewData["articles"] = podcasts.OrderByDescending(t => t.DatePublished).ToList();

            return View();
        }

    }
}
