using CampeonatosNParty.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CampeonatosNParty.Controllers
{
    public class AjaxController : Controller
    {
        //
        // GET: /Ajax/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CarregarCidades(int id)
        {
            return Json(NPartyDbModel<Cidade>.Select().Where("EstadoId", id), JsonRequestBehavior.AllowGet);
        }
    }
}
