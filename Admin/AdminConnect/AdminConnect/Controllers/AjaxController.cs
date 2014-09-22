using AdminConnect.Models.Database;
using CampeonatosNParty.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminConnect.Controllers
{
    public class AjaxController : Controller
    {
        //
        // GET: /Ajax/
        public JsonResult GetEventos()
        {
            return Json(NPartyDb<DetalhesEvento>.Instance.Select().OrderBy("DataEventoInicio", EixoX.Data.SortDirection.Descending), JsonRequestBehavior.AllowGet);
        }
	}
}