using CampeonatosNParty.Models.Database;
using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CampeonatosNParty.Controllers
{
    public class CampeonatosController : Controller
    {
        //
        // GET: /Campeonatos/

        ClassSelectResult<CampeonatosDashboardItem> result;

        public ActionResult Index()
        {
            int page = 0;
            int.TryParse(Request.QueryString["page"], out page);

            ClassSelect<CampeonatosDashboardItem> search = CampeonatosDashboardItem.Search(Request.QueryString["filter"]);
            search.Page(25, page);
            search.OrderBy("DataCampeonato");

            result = search.ToResult();

            return View(result);
        }
    }
}
