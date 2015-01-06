using AdminConnect.Models.View;
using CampeonatosNParty.Models.Database;
using EixoX.Web.AuthComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminConnect.Controllers
{
    public class CampeonatosController : NPartyController
    {
        [AuthenticationRequired]
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

        [AuthenticationRequired]
        public ActionResult EmAndamento(int? id)
        {
            if (!id.HasValue)
            {
                FlashMessage("Você precisa selecionar um campeonato", MessageType.Error);
                return Redirect("~/Eventos/Gerenciar");
            }

            try
            {
                Campeonatos c = Campeonatos.Select().Where("Id", id.Value).SingleResult();

                if (c == null)
                {
                    FlashMessage("Você precisa selecionar um campeonato", MessageType.Error);
                    return Redirect("~/Eventos/Gerenciar");
                }

                if ((CurrentUser.AdminSupremo || (CurrentUser.PodeEditarCampeonato && CurrentUser.IdOrganizador == c.IdOrganizador)) == false)
                {
                    FlashMessage("Você não tem permissão para editar este campeonato", MessageType.Error);
                    return Redirect("~/Campeonatos/Detalhes/" + id.Value);
                }

                if (c.IdStatus != 1)
                {
                    FlashMessage("O campeonato não pode ter sido iniciado para ser alterado.", MessageType.Error);
                    return Redirect("~/Campeonatos/Detalhes/" + id.Value);
                }

                c.IdStatus = 2;
                NPartyDb<Campeonatos>.Instance.Update(c);

                FlashMessage("O status do campeonato foi alterado para 'Em andamento'", MessageType.Success);
                return Redirect("~/Campeonatos/Detalhes/" + id.Value);
            }
            catch (Exception e)
            {
                FlashMessage("Ops, ocorreu o seguinte erro: " + e.Message, MessageType.Error);
                return RedirectToAction("Gerenciar");
            }
        }
	}
}