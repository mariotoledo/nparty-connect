using CampeonatosNParty.Models.Database;
using CampeonatosNParty.Models.ViewModel;
using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CampeonatosNParty.Controllers
{
    public class EventosController : Controller
    {
        //
        // GET: /Home/

        ClassSelectResult<Eventos> result;

        public ActionResult Index()
        {
            int page = 0;
            int.TryParse(Request.QueryString["page"], out page);

            ClassSelect<Eventos> search = Eventos.Search(Request.QueryString["filter"]);
            search.Page(12, page);
            search.OrderBy("DataEventoInicio", SortDirection.Descending);

            result = search.ToResult();

            return View(result);
        }

        public ActionResult Detalhes(int? id)
        {
            EventosDetailView view = new EventosDetailView(id.Value);
            return View(view);
        }
    }
}
