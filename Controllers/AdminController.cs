using CampeonatosNParty.Models.Database;
using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CampeonatosNParty.Controllers
{
    public class AdminController : AuthenticationBasedController
    {
        //
        // GET: /Admin/
        [AuthenticationRequired]
        public ActionResult Index()
        {
            return View();
        }

        [AuthenticationRequired]
        public ActionResult Usuarios()
        {
            int page = 0;
            int.TryParse(Request.QueryString["page"], out page);

            ClassSelect<JogadoresItem> search = JogadoresItem.Search(Request.QueryString["filter"]);
            search.Page(18, page);
            search.OrderBy("NomeUsuario");

            return View(search.ToResult());
        }

        [AuthenticationRequired]
        [HttpGet]
        public ActionResult DetalhesUsuario(int? id)
        {
            CampeonatosNParty.Models.ViewModel.UsuariosDetailView view = new CampeonatosNParty.Models.ViewModel.UsuariosDetailView(id.Value);
            return View(view);
        }

        [AuthenticationRequired]
        [HttpGet]
        public ActionResult EditarUsuario(int? id)
        {
            CampeonatosNParty.Models.Database.Usuarios view = NPartyDb<Usuarios>.Instance.WithIdentity(id.Value);
            return View(view);
        }

    }
}
