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
    public class GameController : NPartyController, Viewee, GameCreatorViewee, GameEditorViewee
    {
        // GET: ESports/Game
        public ActionResult Index()
        {
            ViewData["games"] = new GameLister().All(this);
            return View();
        }

        [HttpGet]
        public ActionResult New()
        {
            return View(new GameCreator());
        }

        [HttpPost]
        public ActionResult New(FormCollection form)
        {
            GameCreator gameCreator = new GameCreator();

            HttpFileCollectionBase requestFiles = Request.Files;
            HttpPostedFileBase logoImage = requestFiles["GameLogoUrl"];

            if (gameCreator.Execute(form, logoImage, this))
            {
                FlashMessage("Jogo competitivo criado com sucesso", MessageType.Success);
                return RedirectToAction("Index");
            }

            return View(gameCreator);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(new GameLister().Single(id, this));
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            GameEditor usecase = new GameEditor();

            HttpFileCollectionBase requestFiles = Request.Files;
            HttpPostedFileBase logoImage = requestFiles["GameLogoUrl"];

            if (usecase.Execute(id, form, logoImage, this))
            {
                FlashMessage("Jogo competitivo editado com sucesso", MessageType.Success);
                return RedirectToAction("Index");
            }

            return View(usecase.ESportsGame);
        }

        public void OnException(Exception ex)
        {
            FlashMessage("Ocorreu um erro inesperado: " + ex.Message, MessageType.Error);
        }

        public void OnGameNameIsNullOrEmpty()
        {
            FlashMessage("O campo nome não pode estar vazio", MessageType.Error);
        }

        public void OnGameNameLengthIsLarge()
        {
            FlashMessage("O campo nome não pode ter mais de 50 caracteres", MessageType.Error);
        }

        public void OnGameAlreadyExists()
        {
            FlashMessage("Um jogo com este nome já foi criado", MessageType.Error);
        }

        public void OnGameNotFound()
        {
            FlashMessage("O jogo indicado não foi encontrado", MessageType.Error);
        }
    }
}