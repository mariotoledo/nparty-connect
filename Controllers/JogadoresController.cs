using CampeonatosNParty.Models.Cookie;
using CampeonatosNParty.Models.Database;
using CampeonatosNParty.Models.StructModel;
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

        public ActionResult Index()
        {
            int page = 0;
            int.TryParse(Request.QueryString["page"], out page);

            ClassSelect<JogadoresItem> search = JogadoresItem.Search(Request.QueryString["filter"]);
            search.Page(18, page);
            search.OrderBy("NomeUsuario");

            return View(search.ToResult());
        }

        public ActionResult Detalhes(int? id)
        {
            CampeonatosNParty.Models.ViewModel.UsuariosDetailView view = new CampeonatosNParty.Models.ViewModel.UsuariosDetailView(id.Value);
            view.setPersonGamingRelation(CurrentUsuario);
            return View(view);
        }

        [HttpGet]
        public ActionResult AdicionarPSN(int? id)
        {
            if (id.HasValue)
            {
                Usuarios usuario = Usuarios.WithIdentity(id.Value);
                if (usuario != null)
                {
                    PersonGamingRelation relation = PersonGamingRelation.getPersonGamingRelations(CurrentUsuario, usuario);
                    if (relation == null)
                    {
                        relation = new PersonGamingRelation()
                        {
                            PersonId1 = CurrentUsuario.Id,
                            PersonId2 = id.Value,
                            isPSN = true,
                            isLive = false,
                            isNintendoNetwork = false
                        };
                        NPartyDb<PersonGamingRelation>.Instance.Insert(relation);
                    }
                    else
                    {
                        relation.isPSN = true;
                        NPartyDb<PersonGamingRelation>.Instance.Update(relation);
                    }

                    CampeonatosNParty.Helpers.EmailTemplate emailTemplate = new CampeonatosNParty.Helpers.EmailTemplate();
                    emailTemplate.Load(Server.MapPath(Url.Content("~/Static/EmailTemplates/adicionarContaSocial.xml")));

                    IDictionary<string, string> infoChanges = new Dictionary<string, string>();

                    infoChanges.Add("[=PersonName]", CurrentUsuario.Nome);
                    infoChanges.Add("[=FriendName]", usuario.Nome);
                    infoChanges.Add("[=AccountName]", "PSN");
                    infoChanges.Add("[=Id]", usuario.PsnId);

                    emailTemplate.Send(infoChanges, "Solicitação de amizade na PSN - Campeonatos N-Party", CurrentUsuario.Email);

                    return Redirect("~/Jogadores/Detalhes/" + id.Value);
                }                
            }

            return Redirect("~/Jogadores");
        }

        [HttpGet]
        public ActionResult AdicionarLive(int? id)
        {
            if (id.HasValue)
            {
                Usuarios usuario = Usuarios.WithIdentity(id.Value);
                if (usuario != null)
                {
                    PersonGamingRelation relation = PersonGamingRelation.getPersonGamingRelations(CurrentUsuario, usuario);
                    if (relation == null)
                    {
                        relation = new PersonGamingRelation()
                        {
                            PersonId1 = CurrentUsuario.Id,
                            PersonId2 = id.Value,
                            isPSN = false,
                            isLive = true,
                            isNintendoNetwork = false
                        };
                        NPartyDb<PersonGamingRelation>.Instance.Insert(relation);
                    }
                    else
                    {
                        relation.isLive = true;
                        NPartyDb<PersonGamingRelation>.Instance.Update(relation);
                    }

                    CampeonatosNParty.Helpers.EmailTemplate emailTemplate = new CampeonatosNParty.Helpers.EmailTemplate();
                    emailTemplate.Load(Server.MapPath(Url.Content("~/Static/EmailTemplates/adicionarContaSocial.xml")));

                    IDictionary<string, string> infoChanges = new Dictionary<string, string>();

                    infoChanges.Add("[=PersonName]", CurrentUsuario.Nome);
                    infoChanges.Add("[=FriendName]", usuario.Nome);
                    infoChanges.Add("[=AccountName]", "Live");
                    infoChanges.Add("[=Id]", usuario.LiveId);

                    emailTemplate.Send(infoChanges, "Solicitação de amizade na Live - Campeonatos N-Party", CurrentUsuario.Email);

                    return Redirect("~/Jogadores/Detalhes/" + id.Value);
                }
            }

            return Redirect("~/Jogadores");
        }

        [HttpGet]
        public ActionResult AdicionarNintendoNetwork(int? id)
        {
            if (id.HasValue)
            {
                Usuarios usuario = Usuarios.WithIdentity(id.Value);
                if (usuario != null)
                {
                    PersonGamingRelation relation = PersonGamingRelation.getPersonGamingRelations(CurrentUsuario, usuario);
                    if (relation == null)
                    {
                        relation = new PersonGamingRelation()
                        {
                            PersonId1 = CurrentUsuario.Id,
                            PersonId2 = id.Value,
                            isPSN = false,
                            isLive = false,
                            isNintendoNetwork = true
                        };
                        NPartyDb<PersonGamingRelation>.Instance.Insert(relation);
                    }
                    else
                    {
                        relation.isNintendoNetwork = true;
                        NPartyDb<PersonGamingRelation>.Instance.Update(relation);
                    }

                    CampeonatosNParty.Helpers.EmailTemplate emailTemplate = new CampeonatosNParty.Helpers.EmailTemplate();
                    emailTemplate.Load(Server.MapPath(Url.Content("~/Static/EmailTemplates/adicionarContaSocial.xml")));

                    IDictionary<string, string> infoChanges = new Dictionary<string, string>();

                    infoChanges.Add("[=PersonName]", CurrentUsuario.Nome);
                    infoChanges.Add("[=FriendName]", usuario.Nome);
                    infoChanges.Add("[=AccountName]", "Nintendo Network");
                    infoChanges.Add("[=Id]", usuario.NintendoNetworkId);

                    emailTemplate.Send(infoChanges, "Solicitação de amizade na Nintendo Network - Campeonatos N-Party", CurrentUsuario.Email);

                    return Redirect("~/Jogadores/Detalhes/" + id.Value);
                }
            }

            return Redirect("~/Jogadores");
        }

        [HttpGet]
        public ActionResult Registrar()
        {
            Session["captcha"] = CampeonatosNParty.Helpers.RegisterHelper.GetRandWord(5);
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
                else if (string.IsNullOrEmpty(form["Captcha"]) || form["Captcha"].ToLower() != ((string)Session["captcha"]).ToLower())
                {
                    ViewData["RegisterError"] = "Por favor, digite os caracteres do captcha corretamente";
                }
                else
                {
                    Usuarios u = Usuarios.Select().Where("Email", model.Email).SingleOrDefault();
                    if (u == null)
                    {
                        model.Senha = CampeonatosNParty.Helpers.RegisterHelper.GetEncryptedPassword(model.Senha);
                        model.EmailConfirmado = false;
                        NPartyDb<Usuarios>.Instance.Insert(model);

                        NPartyCookie.UserId = model.Id;
                        NPartyCookie.IsLoggedIn = false;
                        NPartyDb<Cookie>.Instance.Update(NPartyCookie);

                        //return View("BemVindo");
                        return RedirectToAction("EnviarEmailConfirmacao");
                    }
                    else
                    {
                        ViewData["RegisterError"] = "Este e-mail já está registrado.";
                    }
                }
            }
            Session["captcha"] = CampeonatosNParty.Helpers.RegisterHelper.GetRandWord(5);
            return View(model);
        }

        public FileContentResult GetCaptchaImage()
        {
            System.Drawing.ImageConverter converter = new System.Drawing.ImageConverter();
            byte[] bytes = (byte[])converter.ConvertTo(CampeonatosNParty.Helpers.RegisterHelper.GenerateImage(200, 100, ((string)Session["captcha"])), typeof(byte[])); 
            return File(bytes, "image/jpeg");
        }

        [HttpGet]
        [AuthenticationRequired]
        public ActionResult EditarMinhasInformacoes()
        {
            return View(new MinhasInformacoes()
            {
                Apelido = CurrentUsuario.Apelido,
                Telefone = CurrentUsuario.Telefone,
                UrlFotoPerfil = CurrentUsuario.getUrlFotoPerfil(),
                Id_Estado = CurrentUsuario.Id_Estado,
                Id_Cidade = CurrentUsuario.Id_Cidade,
                Newsletter = CurrentUsuario.Newsletter,
                PsnId = CurrentUsuario.PsnId,
                LiveId = CurrentUsuario.LiveId
            });
        }

        [HttpPost]
        [AuthenticationRequired]
        public ActionResult EditarMinhasInformacoes(FormCollection form, MinhasInformacoes model)
        {
            if (EixoX.Restrictions.RestrictionAspect<MinhasInformacoes>.Instance.Validate(model))
            {
                if (string.IsNullOrEmpty(form["Id_Estado"]) || Int32.Parse(form["Id_Estado"]) == 0)
                {
                    ViewData["RegisterError"] = "Por favor, selecione o estado onde mora";
                }
                else if (string.IsNullOrEmpty(form["Id_Cidade"]) || Int32.Parse(form["Id_Cidade"]) == 0)
                {
                    ViewData["RegisterError"] = "Por favor, selecione a cidade onde mora";
                }
                else
                {

                    CurrentUsuario.Apelido = model.Apelido;
                    CurrentUsuario.Telefone = model.Telefone;
                    CurrentUsuario.UrlFotoPerfil = string.IsNullOrEmpty(model.UrlFotoPerfil) ? "/Static/img/playerPhoto/default.jpg" : model.UrlFotoPerfil;
                    CurrentUsuario.Id_Estado = model.Id_Estado > 0 ? model.Id_Estado : CurrentUsuario.Id_Estado;
                    CurrentUsuario.Id_Cidade = model.Id_Cidade > 0 ? model.Id_Cidade : CurrentUsuario.Id_Cidade;
                    CurrentUsuario.Newsletter = model.Newsletter;
                    CurrentUsuario.PsnId = model.PsnId;
                    CurrentUsuario.LiveId = model.LiveId;

                    NPartyDb<Usuarios>.Instance.Update(CurrentUsuario);

                    ViewData["RegisterSuccess"] = "Dados atualizados com sucesso.";
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult EsqueciMinhaSenha()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EsqueciMinhaSenha(FormCollection form)
        {
            if (string.IsNullOrEmpty(form["Email"]))
            {
                ViewData["Error"] = "Por favor, digite seu email.";
            }
            else if (!EixoX.Restrictions.Email.IsEmail(form["Email"]))
            {
                ViewData["Error"] = "Por favor, digite um email válido.";
            }
            else
            {
                Usuarios user = Usuarios.WithMember("Email", form["Email"]);
                if (user == null)
                {
                    ViewData["Error"] = "Este email não esta cadastrado em nosso banco. Por favor, verifique se o digitou corretamente.";
                }
                else
                {
                    string newPassword = CampeonatosNParty.Helpers.RegisterHelper.GetRandWord(5);
                    user.Senha = CampeonatosNParty.Helpers.RegisterHelper.GetEncryptedPassword(newPassword);
                    NPartyDb<Usuarios>.Instance.Save(user);

                    CampeonatosNParty.Helpers.EmailTemplate emailTemplate = new CampeonatosNParty.Helpers.EmailTemplate();
                    emailTemplate.Load(Server.MapPath(Url.Content("~/Static/EmailTemplates/esqueciSenhaTemplate.xml")));

                    IDictionary<string, string> infoChanges = new Dictionary<string, string>();

                    infoChanges.Add("[=PersonName]", user.Nome);
                    infoChanges.Add("[=PersonPassword]", newPassword);

                    emailTemplate.Send(infoChanges, "Campeonatos N-Party - Nova senha", user.Email);

                    ViewData["Success"] = "Enviamos uma nova senha para seu email :)";
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult EnviarEmailConfirmacao()
        {
            int personId = NPartyCookie.UserId;
            Usuarios usuario = Usuarios.WithIdentity(personId);
            if (usuario != null)
            {
                CampeonatosNParty.Helpers.EmailTemplate emailTemplate = new CampeonatosNParty.Helpers.EmailTemplate();
                emailTemplate.Load(Server.MapPath(Url.Content("~/Static/EmailTemplates/confirmarEmail.xml")));

                IDictionary<string, string> infoChanges = new Dictionary<string, string>();

                infoChanges.Add("[=PersonName]", usuario.Nome);
                infoChanges.Add("[=PersonLink]", usuario.getConfirmationUrl());

                emailTemplate.Send(infoChanges, "Campeonatos N-Party - Confirmação de email", usuario.Email);
            }

            return RedirectToAction("ConfirmarEmail");
        }

        [HttpGet]
        public ActionResult ConfirmarEmail()
        {
            return View();
        }

        [HttpGet]
        [AuthenticationRequired]
        public ActionResult Notificacoes()
        {
            List<Notificacoes> notificacoes = NPartyDb<Notificacoes>.Instance.Select()
                                                .Where("PersonId", CurrentUsuario.Id)
                                                .Or("PersonId", 0)
                                                .OrderBy("DateSent", SortDirection.Descending)
                                                .ToList();
            return View(notificacoes);
        }
    }
}
