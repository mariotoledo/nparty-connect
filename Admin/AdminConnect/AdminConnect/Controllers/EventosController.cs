using AdminConnect.Models.Attributes;
using AdminConnect.Models.Database;
using CampeonatosNParty.Models.Database;
using EixoX.Web.AuthComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminConnect.Controllers
{
    public class EventosController : NPartyController
    {
        //
        // GET: /Eventos/
        [AuthenticationRequired]
        public ActionResult Index()
        {
            return RedirectToAction("Gerenciar");
        }

        [AuthenticationRequired]
        public ActionResult Gerenciar()
        {
            return View();
        }

        [AuthenticationRequired]
        public ActionResult Detalhes(int? id)
        {
            if (!id.HasValue)
            {
                FlashMessage("Você precisa selecionar um evento", MessageType.Error);
                return RedirectToAction("Gerenciar");
            }

            try
            {
                DetalhesEvento evento = NPartyDb<DetalhesEvento>.Instance.Select().Where("Id", id.Value).SingleResult();
                return View(evento);
            }
            catch (Exception e)
            {
                FlashMessage("Ops, ocorreu o seguinte erro: " + e.Message, MessageType.Error);
                return RedirectToAction("Gerenciar");
            }
        }

        [AuthenticationRequired]
        [AccessRequired(Models.Attributes.AccessType.CanCreateEvent)]
        public ActionResult Novo()
        {
            try
            {
                ViewData["TipoEvento"] = TipoEvento.Select();
                ViewData["Estados"] = Estado.Select();
            }
            catch (Exception e)
            {
                FlashMessage("Ops, ocorreu o seguinte erro: " + e.Message, MessageType.Error);
                return RedirectToAction("Gerenciar");
            }

            return View(new Eventos());
        }

        [AuthenticationRequired]
        [HttpGet]
        public JsonResult GetCidades(long idEstado)
        {
            try
            {
                return Json(Cidade.Select().Where("EstadoId", idEstado).OrderBy("Nome"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
	}
}