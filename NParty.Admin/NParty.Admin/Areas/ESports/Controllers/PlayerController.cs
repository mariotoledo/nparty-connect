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
    public class PlayerController : NPartyController, PlayerCreatorViewee, PlayerEditorViewee, Viewee
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewData["players"] = new PlayerLister().All(this);
            return View();
        }

        [HttpGet]
        public ActionResult New()
        {
            return View(new PlayerCreator());
        }

        [HttpPost]
        public ActionResult New(FormCollection form)
        {
            PlayerCreator playerCreator = new PlayerCreator();

            HttpFileCollectionBase requestFiles = Request.Files;
            HttpPostedFileBase photoImage = requestFiles["PlayerPhotoUrl"];

            if (playerCreator.Execute(form, photoImage, this))
            {
                FlashMessage("Jogador criado com sucesso", MessageType.Success);
                return RedirectToAction("Index");
            }

            return View(playerCreator);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(new PlayerLister().Single(id, this));
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            PlayerEditor usecase = new PlayerEditor();

            HttpFileCollectionBase requestFiles = Request.Files;
            HttpPostedFileBase logoImage = requestFiles["PlayerPhotoUrl"];

            if (usecase.Execute(id, form, logoImage, this))
            {
                FlashMessage("Jogador editado com sucesso", MessageType.Success);
                return RedirectToAction("Index");
            }

            return View(usecase.ESportsPlayer);
        }

        public void OnException(Exception ex)
        {
            FlashMessage("Ocorreu um erro inesperado: " + ex.Message, MessageType.Error);
        }

        public void OnPlayerAlreadyExists()
        {
            FlashMessage("Um jogador com estes nomes já foi criado", MessageType.Error);
        }

        public void OnPlayerFirstNameIsNullOrEmpty()
        {
            FlashMessage("O campo primeiro nome não pode estar vazio", MessageType.Error);
        }

        public void OnPlayerFirstNameLengthIsLarge()
        {
            FlashMessage("O campo primeiro nome não pode ter mais de 50 caracteres", MessageType.Error);
        }

        public void OnPlayerLastNameIsNullOrEmpty()
        {
            FlashMessage("O campo sobrenome não pode estar vazio", MessageType.Error);
        }

        public void OnPlayerLastNameLengthIsLarge()
        {
            FlashMessage("O campo último nome não pode ter mais de 50 caracteres", MessageType.Error);
        }

        public void OnPlayerNicknameIsNullOrEmpty()
        {
            FlashMessage("O campo nick não pode estar vazio", MessageType.Error);
        }

        public void OnPlayerNicknameLengthIsLarge()
        {
            FlashMessage("O campo apelido não pode ter mais de 50 caracteres", MessageType.Error);
        }

        public void OnPlayerNotFound()
        {
            FlashMessage("O jogador indicado não foi encontrado", MessageType.Error);
        }
    }
}