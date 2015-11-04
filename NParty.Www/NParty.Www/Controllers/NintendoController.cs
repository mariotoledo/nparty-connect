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

            ViewData["articles"] = nintendoArticles;
            ViewData["currentPage"] = pageValue;

            return View();
        }

        public ActionResult Ler(int id)
        {
            return View();
        }
    }
}
