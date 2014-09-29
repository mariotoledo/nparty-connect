using CampeonatosNParty.Models.Cookie;
using CampeonatosNParty.Models.Database;
using CampeonatosNParty.Models.StructModel;
using CampeonatosNParty.Models.ViewModel;
using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

        /*[HttpGet]
        public ActionResult AdjustName()
        {
            foreach (Usuarios u in Usuarios.Select().ToList())
            {
                string[] nome = u.Nome.Split();
                u.Nome = nome[0];
                if (nome.Length > 1)
                {
                    u.Sobrenome = nome[1];
                    if (nome.Length > 2)
                    {
                        for (int i = 2; i < nome.Length; i++)
                        {
                            u.Sobrenome = u.Sobrenome + " " + nome[i];
                        }
                    }
                }

                NPartyDb<Usuarios>.Instance.Save(u);
            }
            return Redirect("~/Home/Index");
        }*/

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
                            isMiiverse = false,
                            isFriendCode = false
                        };
                        NPartyDb<PersonGamingRelation>.Instance.Insert(relation);
                    }
                    else
                    {
                        relation.isPSN = true;
                        NPartyDb<PersonGamingRelation>.Instance.Update(relation);
                    }

                    string fileContents = System.IO.File.ReadAllText(Server.MapPath(Url.Content("~/Static/htmlTemplates/adicionarContaSocial.txt")));
                    fileContents = fileContents.Replace("[=PersonId]", CurrentUsuario.Id.ToString());
                    fileContents = fileContents.Replace("[=FriendName]", CurrentUsuario.getFullName());
                    fileContents = fileContents.Replace("[=AccountName]", "PSN");
                    fileContents = fileContents.Replace("[=Id]", CurrentUsuario.PsnId);

                    Notificacoes notificacao = new Notificacoes()
                    {
                        PersonId = id.Value,
                        Titulo = "Solicitação de amizade na PSN",
                        Corpo = fileContents,
                        DateCreated = DateTime.Now,
                        DateSent = DateTime.Now,
                        FoiLida = false
                    };

                    NPartyDb<Notificacoes>.Instance.Insert(notificacao);

                    if (usuario.Newsletter)
                    {
                        CampeonatosNParty.Helpers.EmailTemplate emailTemplate = new CampeonatosNParty.Helpers.EmailTemplate();
                        emailTemplate.Load(Server.MapPath(Url.Content("~/Static/EmailTemplates/adicionarContaSocial.xml")));

                        IDictionary<string, string> infoChanges = new Dictionary<string, string>();

                        infoChanges.Add("[=PersonId]", CurrentUsuario.Id.ToString());
                        infoChanges.Add("[=PersonName]", usuario.getFullName());
                        infoChanges.Add("[=FriendName]", CurrentUsuario.getFullName());
                        infoChanges.Add("[=AccountName]", "PSN");
                        infoChanges.Add("[=Id]", CurrentUsuario.PsnId);

                        emailTemplate.Send(infoChanges, "Solicitação de amizade na PSN - N-Party Connect", usuario.Email);
                    }

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
                            isMiiverse = false,
                            isFriendCode = false
                        };
                        NPartyDb<PersonGamingRelation>.Instance.Insert(relation);
                    }
                    else
                    {
                        relation.isLive = true;
                        NPartyDb<PersonGamingRelation>.Instance.Update(relation);
                    }

                    string fileContents = System.IO.File.ReadAllText(Server.MapPath(Url.Content("~/Static/htmlTemplates/adicionarContaSocial.txt")));
                    fileContents = fileContents.Replace("[=PersonId]", CurrentUsuario.Id.ToString());
                    fileContents = fileContents.Replace("[=FriendName]", CurrentUsuario.getFullName());
                    fileContents = fileContents.Replace("[=AccountName]", "Live");
                    fileContents = fileContents.Replace("[=Id]", CurrentUsuario.LiveId);

                    Notificacoes notificacao = new Notificacoes()
                    {
                        PersonId = id.Value,
                        Titulo = "Solicitação de amizade na Live",
                        Corpo = fileContents,
                        DateCreated = DateTime.Now,
                        DateSent = DateTime.Now,
                        FoiLida = false
                    };

                    NPartyDb<Notificacoes>.Instance.Insert(notificacao);

                    if (usuario.Newsletter)
                    {
                        CampeonatosNParty.Helpers.EmailTemplate emailTemplate = new CampeonatosNParty.Helpers.EmailTemplate();
                        emailTemplate.Load(Server.MapPath(Url.Content("~/Static/EmailTemplates/adicionarContaSocial.xml")));

                        IDictionary<string, string> infoChanges = new Dictionary<string, string>();

                        infoChanges.Add("[=PersonId]", CurrentUsuario.Id.ToString());
                        infoChanges.Add("[=PersonName]", usuario.Nome);
                        infoChanges.Add("[=FriendName]", CurrentUsuario.getFullName());
                        infoChanges.Add("[=AccountName]", "Live");
                        infoChanges.Add("[=Id]", CurrentUsuario.LiveId);

                        emailTemplate.Send(infoChanges, "Solicitação de amizade na Live - N-Party Connect", usuario.Email);
                    }

                    return Redirect("~/Jogadores/Detalhes/" + id.Value);
                }
            }

            return Redirect("~/Jogadores");
        }

        [HttpGet]
        public ActionResult AdicionarMiiverse(int? id)
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
                            isMiiverse = true,
                            isFriendCode = false
                        };
                        NPartyDb<PersonGamingRelation>.Instance.Insert(relation);
                    }
                    else
                    {
                        relation.isMiiverse = true;
                        NPartyDb<PersonGamingRelation>.Instance.Update(relation);
                    }

                    string fileContents = System.IO.File.ReadAllText(Server.MapPath(Url.Content("~/Static/htmlTemplates/adicionarMiiverse.txt")));
                    fileContents = fileContents.Replace("[=PersonId]", CurrentUsuario.Id.ToString());
                    fileContents = fileContents.Replace("[=FriendName]", CurrentUsuario.getFullName());
                    fileContents = fileContents.Replace("[=Id]", CurrentUsuario.MiiverseId);

                    Notificacoes notificacao = new Notificacoes()
                    {
                        PersonId = id.Value,
                        Titulo = "Solicitação de amizade no Miiverse",
                        Corpo = fileContents,
                        DateCreated = DateTime.Now,
                        DateSent = DateTime.Now,
                        FoiLida = false
                    };

                    NPartyDb<Notificacoes>.Instance.Insert(notificacao);

                    if (usuario.Newsletter)
                    {
                        CampeonatosNParty.Helpers.EmailTemplate emailTemplate = new CampeonatosNParty.Helpers.EmailTemplate();
                        emailTemplate.Load(Server.MapPath(Url.Content("~/Static/EmailTemplates/adicionarMiiverse.xml")));

                        IDictionary<string, string> infoChanges = new Dictionary<string, string>();

                        infoChanges.Add("[=PersonId]", CurrentUsuario.Id.ToString());
                        infoChanges.Add("[=PersonName]", usuario.getFullName());
                        infoChanges.Add("[=FriendName]", CurrentUsuario.getFullName());
                        infoChanges.Add("[=Id]", CurrentUsuario.MiiverseId);

                        emailTemplate.Send(infoChanges, "Solicitação de amizade no Miiverse - N-Party Connect", usuario.Email);
                    }

                    return Redirect("~/Jogadores/Detalhes/" + id.Value);
                }
            }

            return Redirect("~/Jogadores");
        }

        
        [HttpGet]
        public ActionResult AnimeDreams()
        {
            
                foreach(Usuarios u in Usuarios.Select().Where("Id_Estado", 26))
                {
                    string fileContents = System.IO.File.ReadAllText(Server.MapPath(Url.Content("~/Static/htmlTemplates/ressaca.txt")));

                    Notificacoes notificacao = new Notificacoes()
                    {
                        PersonId = u.Id,
                        Titulo = "Compareça no MeetUp N-Party + LOP-SP",
                        Corpo = fileContents,
                        DateCreated = DateTime.Now,
                        DateSent = DateTime.Now,
                        FoiLida = false
                    };

                    NPartyDb<Notificacoes>.Instance.Insert(notificacao);
                }

            return Redirect("~/Jogadores");
        }

        [HttpGet]
        public ActionResult AdicionarFriendCode(int? id)
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
                            isMiiverse = false,
                            isFriendCode = true
                        };
                        NPartyDb<PersonGamingRelation>.Instance.Insert(relation);
                    }
                    else
                    {
                        relation.isFriendCode = true;
                        NPartyDb<PersonGamingRelation>.Instance.Update(relation);
                    }

                    string fileContents = System.IO.File.ReadAllText(Server.MapPath(Url.Content("~/Static/htmlTemplates/adicionarFriendCode.txt")));
                    fileContents = fileContents.Replace("[=PersonId]", CurrentUsuario.Id.ToString());
                    fileContents = fileContents.Replace("[=FriendName]", CurrentUsuario.getFullName());
                    fileContents = fileContents.Replace("[=Id]", CurrentUsuario.FriendCode);

                    Notificacoes notificacao = new Notificacoes()
                    {
                        PersonId = id.Value,
                        Titulo = "Solicitação de amizade no 3DS",
                        Corpo = fileContents,
                        DateCreated = DateTime.Now,
                        DateSent = DateTime.Now,
                        FoiLida = false
                    };

                    NPartyDb<Notificacoes>.Instance.Insert(notificacao);

                    if (usuario.Newsletter)
                    {
                        CampeonatosNParty.Helpers.EmailTemplate emailTemplate = new CampeonatosNParty.Helpers.EmailTemplate();
                        emailTemplate.Load(Server.MapPath(Url.Content("~/Static/EmailTemplates/adicionarFriendCode.xml")));

                        IDictionary<string, string> infoChanges = new Dictionary<string, string>();

                        infoChanges.Add("[=PersonId]", CurrentUsuario.Id.ToString());
                        infoChanges.Add("[=PersonName]", usuario.getFullName());
                        infoChanges.Add("[=FriendName]", CurrentUsuario.getFullName());
                        infoChanges.Add("[=Id]", CurrentUsuario.FriendCode);

                        emailTemplate.Send(infoChanges, "Solicitação de amizade no 3DS - N-Party Connect", usuario.Email);
                    }

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
            string teste = form["BirthdayYear"];
            int teste2 = Int32.Parse(form["BirthdayYear"]);
            model.Nascimento = new DateTime(Int32.Parse(form["BirthdayYear"]), Int32.Parse(form["BirthdayMonth"]), Int32.Parse(form["BirthdayDay"]));

            if (EixoX.Restrictions.RestrictionAspect<Usuarios>.Instance.Validate(model))
            {
                if (form["Senha"].Length < 6 || form["Senha"].Length > 8)
                {
                    ViewData["RegisterError"] = "Senha possui tamanho inválido. Por favor, digite uma senha de 6 a 8 dígitos";
                }
                else if (form["Senha"] != form["ConfirmacaoSenha"])
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
                        model.UrlFotoPerfil = "/Static/img/playerPhoto/default.jpg";
                        model.EmailConfirmado = false;
                        NPartyDb<Usuarios>.Instance.Insert(model);

                        Session["CurrentUser"] = null;

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
                    string newPassword = CampeonatosNParty.Helpers.RegisterHelper.GetRandWord(6);
                    user.Senha = CampeonatosNParty.Helpers.RegisterHelper.GetEncryptedPassword(newPassword);
                    NPartyDb<Usuarios>.Instance.Save(user);

                    CampeonatosNParty.Helpers.EmailTemplate emailTemplate = new CampeonatosNParty.Helpers.EmailTemplate();
                    emailTemplate.Load(Server.MapPath(Url.Content("~/Static/EmailTemplates/esqueciSenhaTemplate.xml")));

                    IDictionary<string, string> infoChanges = new Dictionary<string, string>();

                    infoChanges.Add("[=PersonName]", user.Nome);
                    infoChanges.Add("[=PersonPassword]", newPassword);

                    emailTemplate.Send(infoChanges, "N-Party Connect - Nova senha", user.Email);

                    ViewData["Success"] = "Enviamos uma nova senha para seu email :)";
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult EnviarEmailConfirmacao()
        {
            int personId = ((Usuarios)Session["CurrentUser"]).Id;
            Usuarios usuario = Usuarios.WithIdentity(personId);
            if (usuario != null)
            {
                CampeonatosNParty.Helpers.EmailTemplate emailTemplate = new CampeonatosNParty.Helpers.EmailTemplate();
                emailTemplate.Load(Server.MapPath(Url.Content("~/Static/EmailTemplates/confirmarEmail.xml")));

                IDictionary<string, string> infoChanges = new Dictionary<string, string>();

                infoChanges.Add("[=PersonName]", usuario.Nome);
                infoChanges.Add("[=PersonLink]", usuario.getConfirmationUrl());

                emailTemplate.Send(infoChanges, "N-Party Connect - Confirmação de email", usuario.Email);
            }

            return RedirectToAction("ConfirmarEmail");
        }

        /*[HttpGet]
        public ActionResult EnviarEmailConfirmacaoUltimate()
        {
            foreach(Usuarios u in Usuarios.Select().ToList())
            if (u.EmailConfirmado == false)
            {
                CampeonatosNParty.Helpers.EmailTemplate emailTemplate = new CampeonatosNParty.Helpers.EmailTemplate();
                emailTemplate.Load(Server.MapPath(Url.Content("~/Static/EmailTemplates/confirmarEmail.xml")));

                IDictionary<string, string> infoChanges = new Dictionary<string, string>();

                infoChanges.Add("[=PersonName]", u.Nome);
                infoChanges.Add("[=PersonLink]", u.getConfirmationUrl());

                emailTemplate.Send(infoChanges, "N-Party Connect - Confirmação de email", u.Email);
            }

            return RedirectToAction("ConfirmarEmail");
        }*/

        [HttpGet]
        public ActionResult ConfirmarEmail()
        {
            return View();
        }

        [HttpGet]
        [AuthenticationRequired]
        public ActionResult NotificacoesManeira()
        {
            foreach (Usuarios usuario in Usuarios.Select())
            {
                Notificacoes notificacao = new Notificacoes()
                {
                    PersonId = usuario.Id,
                    Titulo = "Compareça ao espaço da N-Party no Ressaca Friends 2013",
                    Corpo = System.IO.File.ReadAllText(Server.MapPath(Url.Content("~/Static/htmlTemplates/ressaca.txt"))),
                    DateCreated = DateTime.Now,
                    DateSent = DateTime.Now,
                    FoiLida = false
                };

                NPartyDb<Notificacoes>.Instance.Insert(notificacao);

                if (usuario.Newsletter)
                {
                    CampeonatosNParty.Helpers.EmailTemplate emailTemplate = new CampeonatosNParty.Helpers.EmailTemplate();
                    emailTemplate.Load(Server.MapPath(Url.Content("~/Static/EmailTemplates/ressaca.xml")));

                    IDictionary<string, string> infoChanges = new Dictionary<string, string>();

                    emailTemplate.Send(infoChanges, "Compareça ao espaço da N-Party no Ressaca Friends 2013", usuario.Email);
                }
            }

            return Redirect("~/");
        }

        [HttpGet]
        [AuthenticationRequired]
        public ActionResult Notificacoes()
        {
            int page = 0;
            int.TryParse(Request.QueryString["page"], out page);

            ClassSelect<Notificacoes> notificacoes = NPartyDb<Notificacoes>.Instance.Select()
                                                .Where("PersonId", CurrentUsuario.Id)
                                                .OrderBy("DateSent", SortDirection.Descending);

            notificacoes.Page(15, page);

            ClassSelectResult<Notificacoes> result = notificacoes.ToResult();

            return View(result);
        }

        [AuthenticationRequired]
        public ActionResult MinhasConexoes()
        {
            string pageString = Request.QueryString["page"];

            int filter = 0;
            int.TryParse(Request.QueryString["filter"], out filter);

            MinhasConexoesView view = new MinhasConexoesView(CurrentUsuario, pageString, filter);

            return View(view);
        }
    }
}
