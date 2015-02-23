using AdminConnect.Models.Database;
using CampeonatosNParty.Helpers;
using CampeonatosNParty.Models.Database;
using EixoX.Web.AuthComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminConnect.Controllers
{
    public class DashboardController : NPartyController
    {
        [AuthenticationRequired]
        public ActionResult Index()
        {
            return View();
        }

        [AuthenticationRequired]
        [HttpGet]
        public ActionResult TrocarSenha()
        {
            return View();
        }

        [AuthenticationRequired]
        [HttpPost]
        public ActionResult TrocarSenha(FormCollection form)
        {
            try
            {
                if (string.IsNullOrEmpty(form["SenhaAtual"]))
                {
                    FlashMessage("Você precisa digitar sua senha atual", MessageType.Error);
                    return View();
                }

                if (CurrentUser.Senha.CompareTo(EncryptHelper.CriptografarDES3((string)form["SenhaAtual"])) != 0)
                {
                    FlashMessage("Senha atual é inválida", MessageType.Error);
                    return View();
                }

                if (string.IsNullOrEmpty(form["NovaSenha"]))
                {
                    FlashMessage("Você precisa digitar sua nova senha", MessageType.Error);
                    return View();
                }

                if (((string)form["NovaSenha"]).Length < 6)
                {
                    FlashMessage("Sua nova senha precisa ter, pelo menos, 6 dígitos", MessageType.Error);
                    return View();
                }

                if (string.IsNullOrEmpty(form["ConfirmarSenha"]))
                {
                    FlashMessage("Você precisa digitar a confirmação da nova senha", MessageType.Error);
                    return View();
                }

                if (((string)form["NovaSenha"]).CompareTo((string)form["ConfirmarSenha"]) != 0)
                {
                    FlashMessage("A senha digitada é diferente da confirmação", MessageType.Error);
                    return View();
                }

                CurrentUser.Senha = EncryptHelper.CriptografarDES3((string)form["NovaSenha"]);
                NPartyDb<Administrador>.Instance.Update(CurrentUser);

                FlashMessage("Senha alterada com sucesso", MessageType.Success);

                return View();
            }
            catch (Exception e)
            {
                FlashMessage("Ocorreu o seguinte erro: " + e.Message, MessageType.Error);

                Log log = new Log()
                {
                    Descricao = e.Message,
                    IdUsuario = CurrentUser.Id,
                    Tipo = 1,
                    Titulo = "Erro ao tentar mudar a senha",
                    DataCriacao = DateTime.Now
                };

                return View();
            }
        }

        [AuthenticationRequired]
        public ActionResult Logout()
        {
            this.CurrentUser = null;
            return Redirect("~/Login/Index");
        }
    }
}