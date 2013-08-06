using CampeonatosNParty.Models.Cookie;
using CampeonatosNParty.Models.Database;
using CampeonatosNParty.Models.ViewModel;
using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CampeonatosNParty.Controllers
{
    public class JogadoresController : AuthenticationBasedController
    {
        //
        // GET: /Jogadores/

        ClassSelectResult<JogadoresItem> result;
        static string captcha = "";

        public ActionResult Index()
        {
            int page = 0;
            int.TryParse(Request.QueryString["page"], out page);

            ClassSelect<JogadoresItem> search = JogadoresItem.Search(Request.QueryString["filter"]);
            search.Page(18, page);
            search.OrderBy("NomeUsuario");

            result = search.ToResult();

            return View(result);
        }

        public ActionResult Detalhes(int? id)
        {
            CampeonatosNParty.Models.ViewModel.UsuariosDetailView view = new CampeonatosNParty.Models.ViewModel.UsuariosDetailView(id.Value);
            return View(view);
        }

        [HttpGet]
        public ActionResult Registrar()
        {
            captcha = CampeonatosNParty.Helpers.RegisterHelper.GetRandWord(5);
            return View(new CampeonatosNParty.Models.Database.Usuarios());
        }

        [HttpPost]
        public ActionResult Registrar(FormCollection form, Usuarios model)
        {
            model.Data_Cadastro = DateTime.Now;
            model.Nivel_Permissao = 0;
            model.Nascimento = new DateTime(Int32.Parse(form["BirthdayYear"]), Int32.Parse(form["BirthdayMonth"]), Int32.Parse(form["BirthdayDay"]));

            if (EixoX.Restrictions.RestrictionAspect<Usuarios>.Instance.Validate(model))
            {
                if (form["Senha"] != form["ConfirmacaoSenha"])
                {
                    ViewData["RegisterError"] = "Senha e confirmação diferem. Por favor, confirme-as novamente.";
                }
                else if (string.IsNullOrEmpty(form["Id_Estado"]) || Int32.Parse(form["Id_Estado"]) == 0)
                {
                    ViewData["RegisterError"] = "Por favor, selecione o estado onde mora";
                }
                else if (string.IsNullOrEmpty(form["Id_Cidade"]) || Int32.Parse(form["Id_Cidade"]) == 0)
                {
                    ViewData["RegisterError"] = "Por favor, selecione a cidade onde mora";
                }
                else if (string.IsNullOrEmpty(form["Captcha"]) || form["Captcha"].ToLower() != captcha.ToLower())
                {
                    ViewData["RegisterError"] = "Por favor, digite os caracteres do captcha corretamente";
                }
                else
                {
                    Usuarios u = Usuarios.Select().Where("Email", model.Email).SingleOrDefault();
                    if (u == null)
                    {
                        model.Senha = CampeonatosNParty.Helpers.RegisterHelper.GetEncryptedPassword(model.Senha);
                        NPartyDb<Usuarios>.Instance.Insert(model);

                        NPartyCookie.UserId = model.Id;
                        NPartyCookie.IsLoggedIn = true;
                        NPartyDb<Cookie>.Instance.Update(NPartyCookie);

                        return View("BemVindo");
                    }
                    else
                    {
                        ViewData["RegisterError"] = "Este e-mail já está registrado.";
                    }
                }
            }
            return View(model);
        }

        public FileContentResult GetCaptchaImage()
        {
            System.Drawing.ImageConverter converter = new System.Drawing.ImageConverter();
            byte[] bytes = (byte[])converter.ConvertTo(CampeonatosNParty.Helpers.RegisterHelper.GenerateImage(200, 100, captcha), typeof(byte[])); 
            return File(bytes, "image/jpeg");
        }
    }
}
