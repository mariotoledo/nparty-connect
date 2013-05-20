using CampeonatosNParty.Models.Database;
using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CampeonatosNParty.Controllers
{
    public class JogosController : Controller
    {
        //
        // GET: /Campeonatos/

        ClassSelectResult<Jogos> result;

        public ActionResult Index()
        {
            int page = 0;
            int.TryParse(Request.QueryString["page"], out page);

            ClassSelect<Jogos> search = Jogos.Search(Request.QueryString["filter"]);
            search.Page(25, page);
            search.OrderBy("DataCampeonato");

            result = search.ToResult();

            return View(result);
        }

        public ActionResult Detalhes(int? id)
        {
            CampeonatosNParty.Models.ViewModel.CampeonatosDetailView view = new CampeonatosNParty.Models.ViewModel.CampeonatosDetailView(id.Value);
            return View(view);
        }
    }
}
