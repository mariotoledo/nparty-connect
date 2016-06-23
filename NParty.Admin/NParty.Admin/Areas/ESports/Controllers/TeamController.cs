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
    public class TeamController : NPartyController, Viewee, TeamCreatorViewee, TeamEditorViewee
    {
        // GET: ESports/Team
        public ActionResult Index()
        {
            ViewData["teams"] = new TeamLister().All(this);
            return View();
        }

        [HttpGet]
        public ActionResult New()
        {
            return View(new TeamCreator());
        }

        [HttpPost]
        public ActionResult New(FormCollection form)
        {
            TeamCreator teamCreator = new TeamCreator();

            HttpFileCollectionBase requestFiles = Request.Files;
            HttpPostedFileBase photoImage = requestFiles["TeamLogoUrl"];

            if (teamCreator.Execute(form, photoImage, this))
            {
                FlashMessage("Time criado com sucesso", MessageType.Success);
                return RedirectToAction("Index");
            }

            return View(teamCreator);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(new TeamLister().Single(id, this));
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            TeamEditor usecase = new TeamEditor();

            HttpFileCollectionBase requestFiles = Request.Files;
            HttpPostedFileBase logoImage = requestFiles["TeamLogoUrl"];

            if (usecase.Execute(id, form, logoImage, this))
            {
                FlashMessage("Time editado com sucesso", MessageType.Success);
                return RedirectToAction("Index");
            }

            return View(usecase.ESportsTeam);
        }

        public void OnException(Exception ex)
        {
            FlashMessage("Ocorreu um erro inesperado: " + ex.Message, MessageType.Error);
        }

        public void OnTeamAlreadyExists()
        {
            FlashMessage("Um time com este nome já foi criado", MessageType.Error);
        }

        public void OnTeamNameIsNullOrEmpty()
        {
            FlashMessage("O campo nome não pode estar vazio", MessageType.Error);
        }

        public void OnTeamNameLengthIsLarge()
        {
            FlashMessage("O campo nome não pode ter mais de 50 caracteres", MessageType.Error);
        }

        public void OnTeamNotFound()
        {
            FlashMessage("O time indicado não foi encontrado", MessageType.Error);
        }
    }
}