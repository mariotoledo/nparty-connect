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
    public class EventosController : AuthenticationBasedController
    {
        //
        // GET: /Home/

        ClassSelectResult<Eventos> result;

        public ActionResult Index()
        {
            int page = 0;
            int.TryParse(Request.QueryString["page"], out page);

            int cidadeId = 0;
            int.TryParse(Request.QueryString["cidadeId"], out cidadeId);

            ClassSelect<Eventos> search;

            if(cidadeId > 0)
                search = Eventos.Search(Request.QueryString["filter"]).And("IdCidade", cidadeId);
            else
                search = Eventos.Search(Request.QueryString["filter"]);

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
