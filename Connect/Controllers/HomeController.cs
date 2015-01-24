using CampeonatosNParty.Models.Cookie;
using CampeonatosNParty.Models.Database;
using CampeonatosNParty.Models.ViewModel;
using EixoX.Data;
using EixoX.Restrictions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CampeonatosNParty.Controllers
{
    public class HomeController : AuthenticationBasedController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View(new HomeView());
        }

        public ActionResult ConfirmarCadastro()
        {
            string encryptedPersonId = Request.QueryString["confirmationKey"];
            int personId = CampeonatosNParty.Helpers.EncryptHelper.Decrypt(encryptedPersonId);
            
            if(personId == 0)
                return RedirectToAction("ConfirmarEmail", "Jogadores");

            Usuarios u = NPartyDb<Usuarios>.Instance.WithIdentity(personId);
            if (u != null)
            {
                if (!u.EmailConfirmado)
                {
                    Session["CurrentUser"] = u;

                    u.EmailConfirmado = true;
                    NPartyDb<Usuarios>.Instance.Update(u);

                    ViewData["Titulo"] = "Obrigado por confirmar seu cadastro!";
                }
                else
                {
                    ViewData["Titulo"] = "Este email já foi confirmado.";
                }                

                return View();
            }
            return RedirectToAction("ConfirmarEmail", "Jogadores");
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View (new CampeonatosNParty.Models.StructModel.Login());
        }

        [HttpPost]
        public ActionResult Login(CampeonatosNParty.Models.StructModel.Login model)
        {
            string senha = CampeonatosNParty.Helpers.RegisterHelper.GetEncryptedPassword(model.Senha);
            if (RestrictionAspect<CampeonatosNParty.Models.StructModel.Login>.Instance.Validate(model))
            {
                Usuarios usuario = Usuarios.Select().Where("Email", model.Email).SingleResult();
                if (usuario != null && !string.IsNullOrEmpty(usuario.Senha))
                {
                    if (CampeonatosNParty.Helpers.RegisterHelper.CheckValidPassword(usuario.Senha, model.Senha))
                    {
                        Session["CurrentUser"] = usuario;

                        if(!usuario.EmailConfirmado)
                            return RedirectToAction("ConfirmarEmail", "Jogadores");

                        string redir = Request.QueryString["redir"];

                        if (string.IsNullOrEmpty(redir))
                            redir = Url.Content("~/");

                        return Redirect(redir);
                    }
                    else
                    {
                        ViewData["LoginError"] = "Usuário ou senha inválidos.";
                    }
                }
                else
                {
                    ViewData["LoginError"] = "Usuário não registrado.";
                }
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session["CurrentUser"] = null;
            return Redirect("~/Home/");
        }

        [HttpGet]
        public ActionResult Sobre()
        {
            return View("Sobre");
        }

        [HttpGet]
        public ActionResult Privacidade()
        {
            return View("Privacidade");
        }

        [HttpGet]
        public ActionResult FAQ()
        {
            return View("FAQ");
        }

        [HttpGet]
        public ActionResult Contato()
        {
            if (CurrentUsuario == null)
            {
                return View("Contato", new CampeonatosNParty.Models.StructModel.Contato()); 
            }
            return View("Contato", new CampeonatosNParty.Models.StructModel.Contato{
                Nome = CurrentUsuario.Nome,
                Email = CurrentUsuario.Email,
                Enviado = false
            });
        }

        [HttpPost]
        public ActionResult Contato(FormCollection form, CampeonatosNParty.Models.StructModel.Contato contato)
        {
            if (String.IsNullOrEmpty(form["Nome"]))
            {
                ViewData["Error"] = "O campo nome não pode estar vazio.";
                return View("Contato", contato);
            }
            if (String.IsNullOrEmpty(form["Email"]))
            {
                ViewData["Error"] = "O campo email não pode estar vazio.";
                return View("Contato", contato);
            }
            if (!EixoX.ValidationHelper.IsEmail(form["Email"]))
            {
                ViewData["Error"] = "Por favor, digite um email válido.";
                return View("Contato", contato);
            }
            if (String.IsNullOrEmpty(form["Mensagem"]))
            {
                ViewData["Error"] = "O campo de mensagem não pode estar vazio";
                return View("Contato", contato);
            }
            if (contato.Enviado)
            {
                ViewData["Error"] = "Uma mensagem já havia sido enviada, e nós não gostamos de flood :(";
                return View("Contato", contato);
            }
            
            CampeonatosNParty.Helpers.EmailTemplate emailTemplate = new CampeonatosNParty.Helpers.EmailTemplate();
            emailTemplate.Load(Server.MapPath(Url.Content("~/Static/EmailTemplates/contatoTemplate.xml")));

            IDictionary<string, string> infoChanges = new Dictionary<string, string>();

            infoChanges.Add("[=PersonName]", contato.Nome);
            infoChanges.Add("[=PersonEmail]", contato.Email);
            infoChanges.Add("[=PersonMessage]", contato.Mensagem);

            emailTemplate.Send(infoChanges, "Contato N-Party Connect", "contato@nparty.com.br");

            ViewData["Success"] = "Obrigado por entrar em contato. Em breve responderemos a sua mensagem! :)";

            //removendo a mensagem antes de enviar
            contato.Mensagem = "";
            //setando uma flag para minimizar o flood
            contato.Enviado = true;
            return View("Contato", contato);
        }

        public ActionResult Ranking()
        {
            int page = 0;
            int.TryParse(Request.QueryString["page"], out page);

            int year;
            int.TryParse(Request.QueryString["Year"], out year);

            year = year == 0 ? DateTime.Now.Year : year;

            ClassSelect<CampeonatosNParty.Models.Database.Ranking> search = CampeonatosNParty.Models.Database.Ranking.Select().Where("Ano", year);
            search.Page(18, page);
            search.OrderBy("Pontos", SortDirection.Descending);

            return View(search.ToResult());
        }
    }
}