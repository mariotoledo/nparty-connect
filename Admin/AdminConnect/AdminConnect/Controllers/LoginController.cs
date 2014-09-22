using AdminConnect.Models.Struct;
using CampeonatosNParty.Helpers;
using CampeonatosNParty.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminConnect.Controllers
{
    public class LoginController : NPartyController
    {
        //
        // GET: /Login/
        [HttpGet]
        public ActionResult Index()
        {
            if(IsLoggedIn)
                return Redirect("~/Dashboard/Index");

            Login login = new Login();
            return View(login);
        }

        [HttpPost]
        public ActionResult Index(FormCollection form, Login model)
        {
            if (IsLoggedIn)
                return Redirect("~/Dashboard/Index");

            try{
                string login = form["login"];
                string senha = form["senha"];

                model.Username = login;
                model.Senha = senha;

                if (string.IsNullOrEmpty(login))
                {
                    FlashMessage("Login não pode estar em branco", MessageType.Error);
                    return View(model);
                }

                if (string.IsNullOrEmpty(senha))
                {
                    FlashMessage("Senha não pode estar em branca", MessageType.Error);
                    return View(model);
                }

                Administrador admin = NPartyDb<Administrador>.Instance.Select().Where("Email", login).SingleOrDefault();

                if (admin == null || login.CompareTo(admin.Email) != 0 || EncryptHelper.CriptografarDES3(senha).CompareTo(admin.Senha) != 0)
                {
                    FlashMessage("Email e/ou senha inválidos", MessageType.Error);
                    return View(model);
                }

                this.CurrentUser = admin;

                return Redirect("~/Dashboard/Index");
            }
            catch (Exception e)
            {
                FlashMessage("Ocorreu um erro inesperado. Tente novamente mais tarde.", MessageType.Error);
                return View(model);
            }            
        }
	}
}