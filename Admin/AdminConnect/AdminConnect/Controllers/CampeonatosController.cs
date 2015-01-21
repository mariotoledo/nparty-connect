﻿using AdminConnect.Models.Database;
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

        [AuthenticationRequired]
        [HttpPost]
        public ActionResult FinalizarCampeonato(int? id, FormCollection form)
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

                OrganizadorMultiplicadorPontuacao multiplicadores = OrganizadorMultiplicadorPontuacao.Select().Where("IdOrganizador", CurrentUser.IdOrganizador).SingleResult();

                List<Classificacao> classificacoes = new List<Classificacao>();

                foreach (InscricaoUsuario insc in inscricoes)
                {
                    int colocacao = 0;

                    if (insc.IdUsuario == Int32.Parse(form["primeiroLugar"]))
                        colocacao = 1;
                    else if (insc.IdUsuario == Int32.Parse(form["segundoLugar"]))
                        colocacao = 2;
                    else if (insc.IdUsuario == Int32.Parse(form["terceiroLugar"]))
                        colocacao = 3;
                    else if (insc.IdUsuario == Int32.Parse(form["quartoLugar"]))
                        colocacao = 4;

                    int pontuacao = 0;

                    if(colocacao == 1)
                        pontuacao = Convert.ToInt32(inscricoes.Count * multiplicadores.MultiplicadorPrimeiroLugar);
                    else if(colocacao == 2)
                        pontuacao = Convert.ToInt32(inscricoes.Count * multiplicadores.MultiplicadorSegundoLugar);
                    else if(colocacao == 3)
                        pontuacao = Convert.ToInt32(inscricoes.Count * multiplicadores.MultiplicadorTerceiroLugar);
                    else if(colocacao == 4)
                        pontuacao = Convert.ToInt32(inscricoes.Count * multiplicadores.MultiplicadorQuartoLugar);
                    else
                        pontuacao = Convert.ToInt32(inscricoes.Count * multiplicadores.MultiplicadorPadrao);

                    classificacoes.Add(new Classificacao()
                    {
                        Colocacao = colocacao,
                        IdCampeonato = c.Id,
                        IdUsuario = insc.IdUsuario,
                        Pontuacao = pontuacao
                    });
                }

                NPartyDb<Classificacao>.Instance.Insert(classificacoes);

                c.IdStatus = 3;
                NPartyDb<Campeonatos>.Instance.Save(c);

                FlashMessage("Campeonato encerado com sucesso!", MessageType.Success);
                return Redirect("~/Campeonatos/Detalhes/" + id.Value);
            }
            catch (Exception e)
            {
                FlashMessage("Ops, ocorreu o seguinte erro: " + e.Message, MessageType.Error);
                return Redirect("~/Eventos/Gerenciar");
            }
        }
	}
}