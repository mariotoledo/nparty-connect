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
                UrlFotoPerfil = CurrentUsuario.getUrlFotoPerfil().CompareTo("/Static/img/playerPhoto/default.jpg") == 0 ?
                                "" : CurrentUsuario.getUrlFotoPerfil(),
                Id_Estado = CurrentUsuario.Id_Estado,
                Id_Cidade = CurrentUsuario.Id_Cidade,
                Newsletter = CurrentUsuario.Newsletter,
                PsnId = CurrentUsuario.PsnId,
                LiveId = CurrentUsuario.LiveId,
                MiiverseId = CurrentUsuario.MiiverseId,
                FriendCode = CurrentUsuario.FriendCode,
                hasPokemonFriendSafari = NPartyDb<PersonPokemonFriendSafari>.Instance.Select().Where("PersonId", CurrentUsuario.Id).Count() > 0
            });
        }

        [HttpGet]
        [AuthenticationRequired]
        public ActionResult RemoverFriendSafari()
        {
            NPartyDb<PersonPokemonFriendSafari>.Instance.Database.ExecuteNonQueryText("delete from [dbo].PersonPokemonFriendSafari where PersonId = " + CurrentUsuario.Id, new object[0]);
            return RedirectToAction("EditarMinhasInformacoes");
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
                else if (!string.IsNullOrEmpty(model.UrlFotoPerfil) && 
                         ((!model.UrlFotoPerfil.StartsWith(@"http://") &&
                           !model.UrlFotoPerfil.StartsWith(@"https://")) ||
                         (!model.UrlFotoPerfil.EndsWith(@".jpg") && 
                         !model.UrlFotoPerfil.EndsWith(@".png"))))
                {
                    ViewData["RegisterError"] = "Por favor, insira uma URL de imagem válida para seu perfil.";
                }
                else if (!string.IsNullOrEmpty(model.UrlFotoPerfil) &&
                          model.UrlFotoPerfil.Contains("javascript"))
                {
                    ViewData["RegisterError"] = "Por favor, não tente realizar ações maliciosas.";
                }
                else if (!string.IsNullOrEmpty(model.PsnId) &&
                         (model.PsnId.Length < 3 || model.PsnId.Length > 16 || model.PsnId.Contains(" ")))
                {
                    ViewData["RegisterError"] = "Por favor, digite um ID válido para a PSN";
                }
                else if (!string.IsNullOrEmpty(model.LiveId) &&
                     (model.LiveId.Length < 3 || model.LiveId.Length > 16 || model.LiveId.Contains(" ")))
                {
                    ViewData["RegisterError"] = "Por favor, digite um ID válido para a Live";
                }
                else if (!string.IsNullOrEmpty(model.MiiverseId) &&
                 (model.MiiverseId.Length < 3 || model.MiiverseId.Length > 16 || model.MiiverseId.Contains(" ")))
                {
                    ViewData["RegisterError"] = "Por favor, digite um ID válido para o Miiverse";
                }
                else if (!string.IsNullOrEmpty(model.FriendCode) &&
                 (Regex.Matches(model.FriendCode, @"[a-zA-Z]").Count > 0))
                {
                    ViewData["RegisterError"] = "Por favor, digite um Friend Code válido para o 3DS";
                }
                else if (Int32.Parse(form["PokemonType"]) != 0 &&
                        (String.IsNullOrEmpty(form["PokemonSlot1"]) ||
                         String.IsNullOrEmpty(form["PokemonSlot2"]) ||
                         String.IsNullOrEmpty(form["PokemonSlot3"])))
                {
                    ViewData["RegisterError"] = "Atenção: para editar seu Friend Safari, você precisa selecionar os campos corretamente.";
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
                    CurrentUsuario.MiiverseId = model.MiiverseId;
                    CurrentUsuario.FriendCode = model.FriendCode;

                    if (!String.IsNullOrEmpty(form["PokemonType"]) && Int32.Parse(form["PokemonType"]) > 0 &&
                        !String.IsNullOrEmpty(form["PokemonSlot1"]) && Int32.Parse(form["PokemonSlot1"]) > 0 &&
                        !String.IsNullOrEmpty(form["PokemonSlot2"]) && Int32.Parse(form["PokemonSlot2"]) > 0 &&
                        !String.IsNullOrEmpty(form["PokemonSlot3"]) && Int32.Parse(form["PokemonSlot3"]) > 0)
                    {
                        PersonPokemonFriendSafari friendSafari = PersonPokemonFriendSafari.Select().Where("PersonId", CurrentUsuario.Id).SingleResult();
                        if (friendSafari == null)
                        {
                            friendSafari = new PersonPokemonFriendSafari()
                            {
                                PersonId = CurrentUsuario.Id,
                                PokemonSlot1Id = Int32.Parse(form["PokemonSlot1"]),
                                PokemonSlot2Id = Int32.Parse(form["PokemonSlot2"]),
                                PokemonSlot3Id = Int32.Parse(form["PokemonSlot3"]),
                                PokemonTypeId = Int32.Parse(form["PokemonType"])
                            };
                            NPartyDb<PersonPokemonFriendSafari>.Instance.Insert(friendSafari);
                            model.hasPokemonFriendSafari = true;
                        }
                        else
                        {
                            friendSafari.PersonId = CurrentUsuario.Id;
                            friendSafari.PokemonSlot1Id = Int32.Parse(form["PokemonSlot1"]);
                            friendSafari.PokemonSlot2Id = Int32.Parse(form["PokemonSlot2"]);
                            friendSafari.PokemonSlot3Id = Int32.Parse(form["PokemonSlot3"]);
                            NPartyDb<PersonPokemonFriendSafari>.Instance.Update(friendSafari);
                            model.hasPokemonFriendSafari = true;
                        }
                    }                    

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
            int personId = NPartyCookie.UserId;
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

        [HttpGet]
        [AuthenticationRequired]
        public ActionResult AlterarSenha()
        {
            return View();
        }

        [HttpPost]
        [AuthenticationRequired]
        public ActionResult AlterarSenha(FormCollection form)
        {
            string currentPassword = form["CurrentPassword"];
            string newPassword = form["NewPassword"];
            string confirmNewPassword = form["NewPasswordConfirmation"];

            if (String.IsNullOrEmpty(currentPassword))
            {
                ViewData["Error"] = "Por favor, digite sua senha atual";
            }
            else if (String.IsNullOrEmpty(newPassword))
            {
                ViewData["Error"] = "Por favor, digite sua nova senha";
            }
            else if (String.IsNullOrEmpty(confirmNewPassword))
            {
                ViewData["Error"] = "Por favor, confirme sua nova senha";
            }
            else
            {
                if (CampeonatosNParty.Helpers.RegisterHelper.CheckValidPassword(CurrentUsuario.Senha, currentPassword))
                {
                    if (newPassword.Length < 6 || newPassword.Length > 8)
                    {
                        ViewData["Error"] = "Atenção, sua nova senha deve conter 6 a 8 dígitos";
                    }
                    else if (newPassword.CompareTo(confirmNewPassword) != 0)
                    {
                        ViewData["Error"] = "Por favor, confirme sua nova senha corretamente";
                    }
                    else
                    {
                        CurrentUsuario.Senha = CampeonatosNParty.Helpers.RegisterHelper.GetEncryptedPassword(newPassword);
                        NPartyDb<Usuarios>.Instance.Update(CurrentUsuario);
                        ViewData["Success"] = "Senha alterada com sucesso :)";
                    }
                }
                else
                {
                    ViewData["Error"] = "Senha atual inválida.";
                }
            }
            
            return View();
        }
    }
}
