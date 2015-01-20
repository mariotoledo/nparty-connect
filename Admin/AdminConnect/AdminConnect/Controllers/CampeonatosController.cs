using AdminConnect.Models.Database;
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
                AdminConnect.Models.View.DetalhesCampeonato detalhesCampeonato = new AdminConnect.Models.View.DetalhesCampeonato(id.Value);
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

        [AuthenticationRequired]
        public ActionResult NaoIniciado(int? id)
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

                if (c.IdStatus != 2)
                {
                    FlashMessage("O campeonato só pode voltar a não iniciado se ele estiver no estado de 'em andamento'", MessageType.Error);
                    return Redirect("~/Campeonatos/Detalhes/" + id.Value);
                }

                c.IdStatus = 1;
                NPartyDb<Campeonatos>.Instance.Update(c);

                FlashMessage("O status do campeonato foi alterado para 'Não Iniciado'", MessageType.Success);
                return Redirect("~/Campeonatos/Detalhes/" + id.Value);
            }
            catch (Exception e)
            {
                FlashMessage("Ops, ocorreu o seguinte erro: " + e.Message, MessageType.Error);
                return RedirectToAction("Gerenciar");
            }
        }

        [AuthenticationRequired]
        [HttpGet]
        public ActionResult FinalizarCampeonato(int? id)
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

                if (c.IdStatus != 2)
                {
                    FlashMessage("O campeonato só pode ser finalizado se ele estiver no estado de 'em andamento'", MessageType.Error);
                    return Redirect("~/Campeonatos/Detalhes/" + id.Value);
                }

                List<InscricaoUsuario> inscricoes = InscricaoUsuario.Select().Where("IdCampeonato", c.Id).OrderBy("NomeUsuario").ToList();
                if (inscricoes == null || inscricoes.Count == 0)
                {
                    FlashMessage("Não existe nenhum inscrito neste campeonato", MessageType.Error);
                    return Redirect("~/Campeonatos/Detalhes/" + id.Value);
                }

                ViewData["Inscricoes"] = inscricoes;

                AdminConnect.Models.View.DetalhesCampeonato detalhesCampeonato = new AdminConnect.Models.View.DetalhesCampeonato(id.Value);
                return View(detalhesCampeonato);
            }
            catch (Exception e)
            {
                FlashMessage("Ops, ocorreu o seguinte erro: " + e.Message, MessageType.Error);
                return Redirect("~/Eventos/Gerenciar");
            }

        }
	}
}