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
    public class GamePlayerPositionController : NPartyController, Viewee, GamePlayerPositionCreatorViewee, GamePlayerPositionEditorViewee
    {
        public ActionResult Index(int id)
        {
            ViewData["gamePlayerPositionData"] = new GamePlayerPositionLister().AllFromGameId(id, this);
            return View();
        }

        [HttpGet]
        public ActionResult New(int id)
        {
            GamePlayerPositionCreator usecase = new GamePlayerPositionCreator();
            usecase.LoadESportsGame(id);

            return View(usecase);
        }

        public ActionResult New(int id, FormCollection form)
        {
            GamePlayerPositionCreator usecase = new GamePlayerPositionCreator();
            if (usecase.Execute(id, form, this))
            {
                FlashMessage("Posição criada com sucesso", MessageType.Success);
                return Redirect("~/ESports/GamePlayerPosition/Index/" + id);
            }

            return View(usecase);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            GamePlayerPositionEditor usecase = new GamePlayerPositionEditor();
            usecase.LoadFromId(id);

            return View(usecase);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            GamePlayerPositionEditor usecase = new GamePlayerPositionEditor();
            if (usecase.Execute(id, form, this))
            {
                FlashMessage("Posição editada com sucesso", MessageType.Success);
                return Redirect("~/ESports/GamePlayerPosition/Index/" + usecase.Game.ESportsGameId);
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

        public void OnGamePlayerPositionNameAlreadyExists()
        {
            FlashMessage("Uma posição com este nome já foi criada", MessageType.Error);
        }

        public void OnGamePlayerPositionNameIsNullOrEmpty()
        {
            FlashMessage("Você precisa inserir um nome para esta posição", MessageType.Error);
        }

        public void OnGamePlayerPositionOrderIsInvalid()
        {
            FlashMessage("O valor de ordem para este campo é inválido", MessageType.Error);
        }

        public void OnGamePlayerPositionDoesNotExists()
        {
            FlashMessage("A posição indicada não existe. Você não digitou algum id errado?", MessageType.Error);
        }
    }
}