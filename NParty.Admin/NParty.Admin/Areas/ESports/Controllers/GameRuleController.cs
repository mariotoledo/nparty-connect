using EixoX;
using NParty.Admin.Usecases;
using NParty.Admin.Viewees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Admin.Areas.ESports.Controllers
{
    public class GameRuleController : NPartyController, GameRuleCreatorViewee, GameRuleEditorViewee, Viewee
    {
        // GET: ESports/GameRule
        public ActionResult Index(int id)
        {
            ViewData["gameRuleData"] = new GameRuleLister().AllFromGameId(id, this);
            return View();
        }

        [HttpGet]
        public ActionResult New(int id)
        {
            GameRuleCreator usecase = new GameRuleCreator();
            usecase.LoadESportsGame(id);

            return View(usecase);
        }

        [HttpPost]
        public ActionResult New(int id, FormCollection form)
        {
            GameRuleCreator usecase = new GameRuleCreator();
            if(usecase.Execute(id, form, this)){
                FlashMessage("Regra criada com sucesso", MessageType.Success);
                return Redirect("~/ESports/GameRule/Index/" + id);
            }

            return View(usecase);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            GameRuleEditor usecase = new GameRuleEditor();
            usecase.LoadFromId(id);

            return View(usecase);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            GameRuleEditor usecase = new GameRuleEditor();
            if (usecase.Execute(id, form, this))
            {
                FlashMessage("Regra editada com sucesso", MessageType.Success);
                return Redirect("~/ESports/GameRule/Index/" + usecase.Game.ESportsGameId);
            }

            return View(usecase);
        }

        public void OnException(Exception ex)
        {
            FlashMessage("Ocorreu um erro inesperado: " + ex.Message, MessageType.Error);
        }

        public void OnGameDoesNotExists()
        {
            FlashMessage("O jogo desejado não existe. Você não digitou algum id errado?", MessageType.Error);
        }

        public void OnGameRuleDescriptionIsNullOrEmpty()
        {
            FlashMessage("Você precisa inserir uma descrição para a regra", MessageType.Error);
        }

        public void OnGameRuleDescriptionIsTooLong()
        {
            FlashMessage("A descrição inserida é muito grande (máximo de 2000 caracteres)", MessageType.Error);
        }

        public void OnGameRuleNameAlreadyExists()
        {
            FlashMessage("Uma regra com este nome já foi criada", MessageType.Error);
        }

        public void OnGameRuleNameIsNullOrEmpty()
        {
            FlashMessage("Você precisa inserir um nome para esta regra", MessageType.Error);
        }

        public void OnRuleDoesNotExists()
        {
            FlashMessage("O regra desejada não existe. Você não digitou algum id errado?", MessageType.Error);
        }
    }
}