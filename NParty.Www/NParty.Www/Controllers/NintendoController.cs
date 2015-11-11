﻿using NParty.Www.Helpers;
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

            Article article = helper.GetSingleArticleFromBlog(NintendoBlogId, id);

            if(article == null)
            {
                FlashMessage("Ops, não encontramos o artigo desejado", MessageType.Error);
                return RedirectToAction("Index");
            }

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
