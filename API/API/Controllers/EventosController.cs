using API.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace API.Controllers
{
    public class EventosController : Controller
    {
        // GET: Eventos
        public JsonResult Index(int? pageIndex, int? itemsPerPage)
        {
            try
            {
                int index = pageIndex.HasValue ? pageIndex.Value : 0;
                int totalItems = itemsPerPage.HasValue ? itemsPerPage.Value : 50;

                List<DetalhesEvento> eventos = NPartyDb<DetalhesEvento>.Instance.Select().OrderBy("DataEventoInicio", EixoX.Data.SortDirection.Descending).Page(index, totalItems).ToList();
                return Json(eventos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("Ocorreu um erro inesperado. Por favor, tente novamente mais tarde", JsonRequestBehavior.AllowGet);
            }
        }
    }
}