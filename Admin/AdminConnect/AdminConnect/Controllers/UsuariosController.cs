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
    public class UsuariosController : NPartyController
    {
        [AuthenticationRequired]
        public ActionResult Detalhes(int? id)
        {
            if (!id.HasValue)
            {
                FlashMessage("Você precisa selecionar um usuário", MessageType.Error);
                return Redirect("~/Usuarios/Gerenciar");
            }

            try
            {
                DetalhesUsuario u = DetalhesUsuario.Select().Where("Id", id.Value).SingleResult();
                List<AdminConnect.Models.Database.InscricaoUsuario> dt = AdminConnect.Models.Database.InscricaoUsuario.Select().Where("IdUsuario", id.Value).OrderBy("DataCampeonato", EixoX.Data.SortDirection.Descending).ToList();
                ViewData["Inscricoes"] = dt;

                return View(u);
            }
            catch (Exception e)
            {
                FlashMessage("Ops, ocorreu o seguinte erro: " + e.Message, MessageType.Error);
                return RedirectToAction("Gerenciar");
            }
        }

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
	}
}