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

        /*public ActionResult SendEmailPraGalera()
        {
            List<Usuarios> usuario = Usuarios.Select().Where("Senha", null).ToList();
            foreach (Usuarios u in usuario)
            {
                string senha = CampeonatosNParty.Helpers.RegisterHelper.GetRandWord(6);
                u.Senha = CampeonatosNParty.Helpers.RegisterHelper.GetEncryptedPassword(senha);
                NPartyDb<Usuarios>.Instance.Save(u);

                CampeonatosNParty.Helpers.EmailTemplate emailTemplate = new CampeonatosNParty.Helpers.EmailTemplate();
                emailTemplate.Load(Server.MapPath(Url.Content("~/Static/EmailTemplates/senhaNull.xml")));

                IDictionary<string, string> infoChanges = new Dictionary<string, string>();

                infoChanges.Add("[=PersonName]", u.Nome);
                infoChanges.Add("[=PersonPassword]", senha);

                emailTemplate.Send(infoChanges, "Bem vindo ao N-Party Connect", u.Email);
            }
            return Redirect("~/");
        }*/

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
                    NPartyCookie.UserId = personId;
                    NPartyCookie.IsLoggedIn = true;
                    NPartyDb<Cookie>.Instance.Update(NPartyCookie);

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

       /* public ActionResult MigrateInscription()
        {
            List<tb_inscricoes> inscricoes = tb_inscricoes.Select().ToList();

            foreach (tb_inscricoes insc in inscricoes)
            {
                Inscricao i = Inscricao.WithMember("IdOriginal", insc.ID_Inscricao);
                if (i != null)
                    continue;

                i = new Inscricao();

                Campeonatos camp = Campeonatos.WithMember("idOriginalk", insc.ID_Campeonato);
                Usuarios u = Usuarios.WithMember("IdOriginal", insc.ID_Usuario);

                i.IdOriginal = insc.ID_Inscricao;
                i.IdCampeonato = camp.Id;
                i.IdUsuario = u.Id;
                i.IsPago = true;
                i.Pontuacao = insc.NR_Pontos;

                NPartyDb<Inscricao>.Instance.Insert(i);
            }
            return Redirect("~/");
        }

        public ActionResult MigrateUsers()
        {
            List<tb_usuarios> usuarios = tb_usuarios.Select().ToList();

            foreach (tb_usuarios user in usuarios)
            {
                Usuarios u = Usuarios.WithMember("IdOriginal", user.Id_Usuario);
                if (u != null)
                    continue;

                Usuarios ui = Usuarios.WithMember("Email", user.NM_Email);
                if (ui != null)
                    continue;

                Usuarios newUser = new Usuarios();
                newUser.Nome = user.NM_Usuario;
                newUser.Apelido = user.NM_Apelido;
                newUser.Email = user.NM_Email;

                int idEstado = string.IsNullOrEmpty(user.NM_Estado) ? 0 : Estado.Search(user.NM_Estado.ToUpper()).FirstOrDefault().EstadoId;
                newUser.Id_Estado = idEstado;

                int idCidade = string.IsNullOrEmpty(user.NM_Cidade) ? 0 : Cidade.Select().Where("EstadoId", idEstado).And("Nome", EixoX.Data.FilterComparison.Like, RemoveAccents(user.NM_Cidade)).FirstOrDefault().CidadeId;
                newUser.Id_Cidade = idCidade;

                newUser.Telefone = user.NR_Telefone;

                newUser.Data_Cadastro = user.Dt_Cadastro;

                newUser.Nivel_Permissao = 0;

                newUser.Senha = null;

                string[] nascimentoString = user.Dt_Nascimento == null ? null : user.Dt_Nascimento.Split('/');

                if (nascimentoString != null)
                {
                    if (nascimentoString[2].Length == 2)
                    {
                        nascimentoString[2] = "19" + nascimentoString[2];
                    }
                    newUser.Nascimento = new DateTime(Int32.Parse(nascimentoString[2]), Int32.Parse(nascimentoString[1]), Int32.Parse(nascimentoString[0]));
                }
                else
                {
                    newUser.Nascimento = DateTime.MaxValue;
                }

                NPartyDb<Usuarios>.Instance.Insert(newUser);
            }
            return View(new HomeView());
        }*/

        public static string RemoveAccents(string text)
        {
            StringBuilder sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();

            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }
            return sbReturn.ToString();
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
                Usuarios usuario = Usuarios.Select().Where("Email", model.Email).SingleOrDefault();
                if (usuario != null && !string.IsNullOrEmpty(usuario.Senha))
                {
                    if (CampeonatosNParty.Helpers.RegisterHelper.CheckValidPassword(usuario.Senha, model.Senha))
                    {
                        if(!usuario.EmailConfirmado)
                            return RedirectToAction("ConfirmarEmail", "Jogadores");

                        NPartyCookie.UserId = usuario.Id;
                        NPartyCookie.IsLoggedIn = true;
                        NPartyDb<Cookie>.Instance.Save(NPartyCookie);

                        string redir = Request.QueryString["redir"];

                        if (string.IsNullOrEmpty(redir))
                            redir = Url.Content("~/Home/Dashboard");

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
            this.NPartyCookie.IsLoggedIn = false;
            NPartyDb<Cookie>.Instance.Save(NPartyCookie);
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

            ClassSelect<CampeonatosNParty.Models.Database.Ranking> search = CampeonatosNParty.Models.Database.Ranking.Search(Request.QueryString["filter"]);
            search.Page(18, page);
            search.OrderBy("Pontos", SortDirection.Descending);

            return View(search.ToResult());
        }

        [HttpGet]
        [AuthenticationRequired]
        public ActionResult Dashboard()
        {
            DashboardView view = new DashboardView(CurrentUsuario);
            return View(view);
        }
    }
}