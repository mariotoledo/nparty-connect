using NParty.Www.Controllers;
using NParty.Www.Helpers;
using NParty.Www.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Www.Areas.Eventos.Controllers
{
    public class EventosController : NPartyController
    {
        //
        // GET: /Eventos/Eventos/

        protected ActionResult OpenPageByLabel(string label, int? page)
        {
            int pageValue = page.HasValue && page.Value > 0 ? page.Value : 0;

            NPartyArticlesHelper helper = new NPartyArticlesHelper();
            List<Article> articles = helper.GetArticlesFromBlog(EventosBlogId, "Eventos", MaxPosts, (MaxPosts * pageValue) + 1, label, "/Artigos/Ler/");
            List<Article> hilights = helper.GetArticlesFromBlog(EventosBlogId, "Eventos", 5, 0, "Destaque", "/Artigos/Ler/");

            ViewData["articles"] = articles;
            ViewData["hilights"] = hilights;
            ViewData["currentPage"] = pageValue;

            return View();
        }

    }
}
