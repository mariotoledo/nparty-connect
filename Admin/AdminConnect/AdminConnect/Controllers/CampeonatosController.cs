using AdminConnect.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminConnect.Controllers
{
    public class CampeonatosController : NPartyController
    {
        public ActionResult Detalhes(int? id)
        {
            if (!id.HasValue)
            {
                FlashMessage("Você precisa selecionar um campeonato", MessageType.Error);
                return Redirect("~/Eventos/Gerenciar");
            }

            try
            {
                DetalhesCampeonato detalhesCampeonato = new DetalhesCampeonato(id.Value);
                return View(detalhesCampeonato);
            }
            catch (Exception e)
            {
                FlashMessage("Ops, ocorreu o seguinte erro: " + e.Message, MessageType.Error);
                return RedirectToAction("Gerenciar");
            }
        }
	}
}