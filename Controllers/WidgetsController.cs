﻿using CampeonatosNParty.Models.Database;
using CampeonatosNParty.Models.ViewModel;
using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CampeonatosNParty.Controllers
{
    public class WidgetsController : AuthenticationBasedController
    {
        //
        // GET: /Widget/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult BlackFriday()
        {
            BlackFridayModel model = new BlackFridayModel(CurrentUsuario == null ? 0 : CurrentUsuario.Id, Request.QueryString["p"], Request.QueryString["f"]);
            return View(model);
        }

        [HttpPost]
        public ActionResult BlackFriday(FormCollection form)
        {
            try
            {
                if (form["registrar"] != null)
                {
                    BlackFridayModel model = new BlackFridayModel(CurrentUsuario == null ? 0 : CurrentUsuario.Id, Request.QueryString["p"], Request.QueryString["f"]);

                    string nome = form["Nome"];
                    if (string.IsNullOrEmpty(nome))
                    {
                        ViewData["Error"] = "Nome não pode estar vazio";
                        return View(model);
                    }

                    if (nome.Length < 3 || nome.Length > 20)
                    {
                        ViewData["Error"] = "Nome possui tamanho inválido";
                        return View(model);
                    }

                    string sobrenome = form["Sobrenome"];
                    if (string.IsNullOrEmpty(sobrenome))
                    {
                        ViewData["Error"] = "Sobrenome não pode estar vazio";
                    }

                    if (sobrenome.Length < 3 || sobrenome.Length > 20)
                    {
                        ViewData["Error"] = "Sobrenome possui tamanho inválido";
                    }

                    string email = form["Email"];
                    if (string.IsNullOrEmpty(email))
                    {
                        ViewData["Error"] = "Email não pode estar vazio";
                        return View(model);
                    }

                    if (!EixoX.Restrictions.Email.IsEmail(email))
                    {
                        ViewData["Error"] = "Email inválido";
                        return View(model);
                    }

                    Usuarios u = Usuarios.Select().Where("Email", email).SingleResult();
                    if (u != null)
                    {
                        ViewData["Error"] = "Usuário já existe";
                        return View(model);
                    }

                    if (string.IsNullOrEmpty(form["IdEstado"]))
                    {
                        ViewData["Error"] = "Você deve selecionar um estado";
                        return View(model);
                    }

                    int idEstado = Int32.Parse(form["IdEstado"]);
                    if (idEstado == 0)
                    {
                        ViewData["Error"] = "Você deve selecionar um estado";
                        return View(model);
                    }

                    if (string.IsNullOrEmpty(form["IdCidade"]))
                    {
                        ViewData["Error"] = "Você deve selecionar uma cidade";
                        return View(model);
                    }

                    int idCidade = Int32.Parse(form["IdCidade"]);
                    if (idCidade == 0)
                    {
                        ViewData["Error"] = "Você deve selecionar uma cidade";
                        return View(model);
                    }

                    string senha = form["Senha"];
                    if (string.IsNullOrEmpty(senha))
                    {
                        ViewData["Error"] = "Senha não pode estar vazia";
                        return View(model);
                    }

                    if (senha.Length < 6)
                    {
                        ViewData["Error"] = "Senha não pode ter menos que 6 dígitos";
                        return View(model);
                    }

                    string confirmarSenha = form["ConfirmarSenha"];
                    if (senha.CompareTo(confirmarSenha) != 0)
                    {
                        ViewData["Error"] = "Confirmação de senha é diferente da senha digitada";
                        return View(model);
                    }

                    Usuarios usu = new Usuarios()
                    {
                        Nome = nome,
                        Sobrenome = sobrenome,
                        Email = email,
                        Id_Estado = idEstado,
                        Id_Cidade = idCidade,
                        Data_Cadastro = DateTime.Now,
                        EmailConfirmado = true,
                        Senha = CampeonatosNParty.Helpers.RegisterHelper.GetEncryptedPassword(senha),
                        UrlFotoPerfil = "/Static/img/playerPhoto/default.jpg"
                    };

                    NPartyDb<Usuarios>.Instance.Insert(usu);

                    TempData["Sucesso"] = "Cadastro realizado com sucesso. Seja bem vindo!";
                    Session["CurrentUser"] = usu;

                    return Redirect("~/Widgets/BlackFriday");
                }

                if (form["login"] != null)
                {
                    string email = form["EmailLogin"];
                    if (string.IsNullOrEmpty(email))
                    {
                        BlackFridayModel model = new BlackFridayModel(CurrentUsuario == null ? 0 : CurrentUsuario.Id, Request.QueryString["p"], Request.QueryString["f"]);
                        ViewData["Error"] = "Email não pode estar vazio";
                        return View(model);
                    }

                    string senha = form["SenhaLogin"];
                    if (string.IsNullOrEmpty(senha))
                    {
                        BlackFridayModel model = new BlackFridayModel(CurrentUsuario == null ? 0 : CurrentUsuario.Id, Request.QueryString["p"], Request.QueryString["f"]);
                        ViewData["Error"] = "Senha não pode estar vazia";
                        return View(model);
                    }

                    string encsenha = CampeonatosNParty.Helpers.RegisterHelper.GetEncryptedPassword(senha);
                    Usuarios usuario = Usuarios.Select().Where("Email", email).SingleResult();


                    if (usuario != null && !string.IsNullOrEmpty(usuario.Senha))
                    {
                        if (CampeonatosNParty.Helpers.RegisterHelper.CheckValidPassword(usuario.Senha, senha))
                        {
                            Session["CurrentUser"] = usuario;
                            return Redirect("~/Widgets/BlackFriday");
                        }
                        else
                        {
                            ViewData["Error"] = "Usuário ou senha inválidos.";
                            BlackFridayModel model = new BlackFridayModel(CurrentUsuario == null ? 0 : CurrentUsuario.Id, Request.QueryString["p"], Request.QueryString["f"]);
                            return View(model);
                        }
                    }
                    else
                    {
                        ViewData["Error"] = "Usuário não registrado.";
                        BlackFridayModel model = new BlackFridayModel(CurrentUsuario == null ? 0 : CurrentUsuario.Id, Request.QueryString["p"], Request.QueryString["f"]);
                        return View(model);
                    }                    
                }

                if (form["criarAnuncio"] != null && CurrentUsuario != null)
                {
                    BlackFridayModel model = new BlackFridayModel(CurrentUsuario == null ? 0 : CurrentUsuario.Id, Request.QueryString["p"], Request.QueryString["f"]);

                    string nomeAnuncio = form["NomeAnuncio"];
                    if (string.IsNullOrEmpty(nomeAnuncio))
                    {
                        ViewData["Error"] = "O nome do anúncio não pode estar vazio";
                        return View(model);
                    }

                    string urlAnuncio = form["URLAnuncio"];
                    if (string.IsNullOrEmpty(urlAnuncio))
                    {
                        ViewData["Error"] = "A URL do anúncio não pode estar vazia";
                        return View(model);
                    }

                    if (!urlAnuncio.StartsWith("http://") && !urlAnuncio.StartsWith("https://"))
                    {
                        urlAnuncio = "http://" + urlAnuncio;
                    }

                    string valorAnuncio = form["ValorAnuncio"];
                    if (string.IsNullOrEmpty(valorAnuncio))
                    {
                        ViewData["Error"] = "O valor do produto não pode estar vazio";
                        return View(model);
                    }

                    decimal valorDoAnuncio = 0;

                    try { valorDoAnuncio = Decimal.Parse(valorAnuncio.Replace(',', '.')); }
                    catch (Exception e)
                    {
                        ViewData["Error"] = "Valor do produto é inválido. Por favor, digite um valor numérico, em reais.";
                        return View(model);
                    }

                    BlackFridayAnuncio anuncio = new BlackFridayAnuncio()
                    {
                        DataCriacao = DateTime.Now,
                        FoiAprovado = false,
                        IdUsuario = CurrentUsuario.Id,
                        NomeAnuncio = nomeAnuncio,
                        UrlAnuncio = urlAnuncio,
                        Valor = valorDoAnuncio
                    };

                    NPartyDb<BlackFridayAnuncio>.Instance.Insert(anuncio);

                    TempData["Sucesso"] = "Anuncio criado com sucesso! Ele aparecerá na página assim que for aprovado.";

                    return Redirect("~/Widgets/BlackFriday");
                }
            }
            catch (Exception e)
            {
                ViewData["Error"] = "Ops, algo de inesperado aconteceu. Por favor, tente novamente mais tarde";
            }
            return View();
        }

        [HttpGet]
        public ActionResult LogoutBlackFriday()
        {
            Session["CurrentUser"] = null;
            return Redirect("~/Widgets/BlackFriday");
        }

        [HttpGet]
        [AuthenticationRequired]
        public ActionResult BlackFridayAdmin()
        {
            if(CurrentUsuario.Email.CompareTo("mariotoledo12@gmail.com") == 0){
                return View();
            }

            return Redirect("~/Widgets/BlackFriday");
        }

        [HttpGet]
        [AuthenticationRequired]
        public ActionResult ApproveBlackFriday(int? id)
        {
            if (CurrentUsuario.Email.CompareTo("mariotoledo12@gmail.com") == 0)
            {
                BlackFridayAnuncio a = BlackFridayAnuncio.Select().Where("Id", id.Value).SingleResult();
                a.FoiAprovado = true;
                NPartyDb<BlackFridayAnuncio>.Instance.Update(a);
                return Redirect("~/Widgets/BlackFridayAdmin");
            }

            return Redirect("~/Widgets/BlackFriday");
        }

        [HttpGet]
        public ActionResult SuperSmashBrosChallenger()
        {
            SmashBrosChallengerView view = new SmashBrosChallengerView(CurrentUsuario, 
                Request.QueryString["page"], 
                Request.QueryString["cId"] == null ? 0 : Int32.Parse(Request.QueryString["cId"]));
            return View(view);
        }

        [HttpGet]
        public ActionResult PokemonFriendSafariFinder()
        {
            PokemonFinderView view = new PokemonFinderView(CurrentUsuario, 
                Request.QueryString["page"],
                Request.QueryString["Type"] == null ? 0 : Int32.Parse(Request.QueryString["Type"]),
                Request.QueryString["Pokemon"] == null ? 0 : Int32.Parse(Request.QueryString["Pokemon"]));

            return View(view);
        }

        [HttpPost]
        [AuthenticationRequired]
        public ActionResult SuperSmashBrosChallenger(FormCollection form)
        {
            SmashBrosChallengerView view = new SmashBrosChallengerView(CurrentUsuario,
                            Request.QueryString["page"],
                            Request.QueryString["cId"] == null ? 0 : Int32.Parse(Request.QueryString["cId"])); 
            
            SuperSmashBrosChallengerItem item = SuperSmashBrosChallengerItem.Select().Where("IdUsuario", CurrentUsuario.Id).SingleResult();
            
            if (item != null)
            {
                ViewData["RegisterError"] = "Atenção: Você já registrou seus personagens do Smash registrado. Se deseja registrar um novo, remova os antigos.";
                return View(view);
            }

            int idPersonagem1 = Int32.Parse(form["Personagem1"]);
            int idPersonagem2 = Int32.Parse(form["Personagem2"]);
            int idPersonagem3 = Int32.Parse(form["Personagem3"]);

            if (idPersonagem1 == 0 || idPersonagem2 == 0 || idPersonagem3 == 0)
            {
                ViewData["RegisterError"] = "Atenção: Você precisa selecionar todos os personagens.";
                return View(view);
            }

            if(idPersonagem1 == idPersonagem2 || idPersonagem1 == idPersonagem3 || idPersonagem2 == idPersonagem3){
                ViewData["RegisterError"] = "Atenção: Os personagens escolhidos não podem ser iguais";
                return View(view);
            }

            //validando personagem1
            SuperSmashBrosPersonagem personagem1 = SuperSmashBrosPersonagem.WithIdentity(idPersonagem1);
            if (personagem1 == null)
            {
                ViewData["RegisterError"] = "Atenção: Você selecionou um personagem inexistente como primeiro personagem.";
                return View(view);
            }

            int corPersonagem1 = Int32.Parse(form["Personagem1Cor"]);
            if (corPersonagem1 > personagem1.NumeroRoupas)
            {
                ViewData["RegisterError"] = "Atenção: Você selecionou uma cor de roupa inexistente para seu primeiro personagem";
                return View(view);
            }

            //validando personagem2
            SuperSmashBrosPersonagem personagem2 = SuperSmashBrosPersonagem.WithIdentity(idPersonagem2);
            if (personagem2 == null)
            {
                ViewData["RegisterError"] = "Atenção: Você selecionou um personagem inexistente como segundo personagem.";
                return View(view);
            }

            int corPersonagem2 = Int32.Parse(form["Personagem2Cor"]);
            if (corPersonagem2 > personagem2.NumeroRoupas)
            {
                ViewData["RegisterError"] = "Atenção: Você selecionou uma cor de roupa inexistente para seu segundo personagem";
                return View(view);
            }

            //validando personagem3
            SuperSmashBrosPersonagem personagem3 = SuperSmashBrosPersonagem.WithIdentity(idPersonagem3);
            if (personagem3 == null)
            {
                ViewData["RegisterError"] = "Atenção: Você selecionou um personagem inexistente como terceiro personagem.";
                return View(view);
            }

            int corPersonagem3 = Int32.Parse(form["Personagem3Cor"]);
            if (corPersonagem3 > personagem3.NumeroRoupas)
            {
                ViewData["RegisterError"] = "Atenção: Você selecionou uma cor de roupa inexistente para seu terceiro personagem";
                return View(view);
            }

            SuperSmashBrosChallenger newChallenger = new SuperSmashBrosChallenger()
            {
                IdPersonagem1 = idPersonagem1,
                IdCorPersonagem1 = corPersonagem1,
                IdPersonagem2 = idPersonagem2,
                IdCorPersonagem2 = corPersonagem2,
                IdPersonagem3 = idPersonagem3,
                IdCorPersonagem3 = corPersonagem3,
                IdUsuario = CurrentUsuario.Id,
                DataCriacao = DateTime.Now
            };

            NPartyDb<SuperSmashBrosChallenger>.Instance.Insert(newChallenger);

            ViewData["RegisterSuccess"] = "Personagens registrados com sucesso.";

            view = new SmashBrosChallengerView(CurrentUsuario,
                            Request.QueryString["page"],
                            Request.QueryString["cId"] == null ? 0 : Int32.Parse(Request.QueryString["cId"]));
            return View(view);
        }

        [HttpPost]
        [AuthenticationRequired]
        public ActionResult PokemonFriendSafariFinder(FormCollection form)
        {
            PersonPokemonFriendSafari friendSafari = PersonPokemonFriendSafari.Select().Where("PersonId", CurrentUsuario.Id).SingleResult();
            if (friendSafari != null)
            {
                ViewData["RegisterError"] = "Atenção: Você já tem um Friend Safari registrado. Se deseja registrar um novo, remova o antigo.";
            }
            if (Int32.Parse(form["PokemonType"]) == 0)
            {
                ViewData["RegisterError"] = "Atenção: Você precisa selecionar o tipo do Safari antes de salva-lo.";
            }
            else if (Int32.Parse(form["PokemonType"]) != 0 &&
                      (String.IsNullOrEmpty(form["PokemonSlot1"]) ||
                      String.IsNullOrEmpty(form["PokemonSlot2"]) ||
                      String.IsNullOrEmpty(form["PokemonSlot3"])))
            {
                ViewData["RegisterError"] = "Atenção: Você precisa selecionar todos os Pokémon de seu Safari antes de salva-lo";
            }
            else
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

                ViewData["RegisterSuccess"] = "Friend Safari registrado com sucesso.";
            }

            PokemonFinderView view = new PokemonFinderView(CurrentUsuario,
                Request.QueryString["page"],
                Request.QueryString["Type"] == null ? 0 : Int32.Parse(Request.QueryString["Type"]),
                Request.QueryString["Pokemon"] == null ? 0 : Int32.Parse(Request.QueryString["Pokemon"]));

            return View(view);
        }

        [HttpGet]
        [AuthenticationRequired]
        public ActionResult RemoverMeuFriendSafari()
        {
            PersonPokemonFriendSafari friendSafari = PersonPokemonFriendSafari.WithMember("PersonId", CurrentUsuario.Id);
            if (friendSafari != null)
                NPartyDb<PersonPokemonFriendSafari>.Instance.Delete(friendSafari);

            return RedirectToAction("PokemonFriendSafariFinder");
        }

        [HttpPost]
        [AuthenticationRequired]
        public JsonResult CadastrarVoto(string idAnuncio, bool isPositivo)
        {
            try
            {
                BlackFridayAnuncioVoto voto = new BlackFridayAnuncioVoto()
                {
                    IdBlackFridayAnuncio = CampeonatosNParty.Helpers.EncryptHelper.Decrypt(idAnuncio),
                    IdUsuario = CurrentUsuario.Id,
                    Pontuacao = isPositivo ? 1 : -1
                };

                NPartyDb<BlackFridayAnuncioVoto>.Instance.Insert(voto);

                return Json("", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("", JsonRequestBehavior.DenyGet);
            }
        }

        [HttpGet]
        [AuthenticationRequired]
        public ActionResult RemoverMeusPersonagensSmash()
        {
            SuperSmashBrosChallenger item = NPartyDb<SuperSmashBrosChallenger>.Instance.Select().Where("IdUsuario", CurrentUsuario.Id).SingleOrDefault();
            if (item != null)
                NPartyDb<SuperSmashBrosChallenger>.Instance.Delete(item);

            return RedirectToAction("SuperSmashBrosChallenger");
        }

        [HttpGet]
        public ActionResult AdicionarFriendCodeDoSafari(int? id)
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
                }
            }

            string page = Request.QueryString["page"];
            string type = Request.QueryString["Type"];
            string pokemon = Request.QueryString["Pokemon"];

            string parameters = "";

            if(!string.IsNullOrEmpty(page) ||
               !string.IsNullOrEmpty(type) ||
               !string.IsNullOrEmpty(pokemon))
            {
                parameters = parameters + "?";
                if(!string.IsNullOrEmpty(page))
                    parameters = parameters + "page=" + page + "&";
                if (!string.IsNullOrEmpty(type))
                    parameters = parameters + "Type=" + type + "&";
                if (!string.IsNullOrEmpty(pokemon))
                    parameters = parameters + "Pokemon=" + pokemon;
            }

            return Redirect("~/Widgets/PokemonFriendSafariFinder" + parameters);
        }

        [HttpGet]
        public ActionResult AdicionarFriendCodeSuperSmash(int? id)
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
                }
            }

            string page = Request.QueryString["page"];
            string cId = Request.QueryString["cId"];

            string parameters = "";

            if (!string.IsNullOrEmpty(page) ||
               !string.IsNullOrEmpty(cId))
            {
                parameters = parameters + "?";
                if (!string.IsNullOrEmpty(page))
                    parameters = parameters + "page=" + page + "&";
                if (!string.IsNullOrEmpty(cId))
                    parameters = parameters + "cId=" + cId;
            }

            return Redirect("~/Widgets/SuperSmashBrosChallenger" + parameters);
        }

        [HttpGet]
        public ActionResult ClassificadosPokemon()
        {
            ViewData["usuarioLogado"] = CurrentUsuario != null && CurrentUsuario.Id > 0 ? "logado" : null;

            ClassificadosPokemonView view = new ClassificadosPokemonView(CurrentUsuario, 
                Request.QueryString["page"], 
                Request.QueryString["PokemonFilter"] == null ? 0 : Int32.Parse(Request.QueryString["PokemonFilter"]), 
                Request.QueryString["NatureFilter"] == null ? 0 : Int32.Parse(Request.QueryString["NatureFilter"]));

            return View(view);
        }        

        [HttpPost]
        [AuthenticationRequired]
        public ActionResult ClassificadosPokemon(FormCollection form)
        {
            int pokemonId = 0;

            int hpEv, attackEv, defenseEv, spAttackEv, spDefenseEv, speedEv = 0;
            int.TryParse(form["HpEv"], out hpEv);
            int.TryParse(form["AttackEv"], out attackEv);
            int.TryParse(form["DefenseEv"], out defenseEv);
            int.TryParse(form["SpAttackEv"], out spAttackEv);
            int.TryParse(form["SpDefenseEv"], out spDefenseEv);
            int.TryParse(form["SpeedEv"], out speedEv);
            
            int.TryParse(form["PokemonTradeId"], out pokemonId);
            if(pokemonId == 0){
                ViewData["Error"] = "Você precisa selecionar o Pokémon para anunciar.";
            } else if (form["PokemonTradeMessage"] != null && form["PokemonTradeMessage"].Length > 250){
                ViewData["Error"] = "A mensagem não pode ser maior que 250 caracteres.";
            } else if (form["PokemonLevel"] != null && Int32.Parse(form["PokemonLevel"]) < 100
                        && Int32.Parse(form["PokemonLevel"]) > 0) {
                ViewData["Error"] = "O level do Pokémon está inválido";
            } else if (form["PokemonNature"] != null && PokemonNature.WithIdentity(Int32.Parse(form["PokemonNature"])) == null) {
                ViewData["Error"] = "A nature do Pokémon está inválida.";
            }
            else if (hpEv + attackEv + defenseEv + spAttackEv + spDefenseEv + speedEv > 510)
            {
                ViewData["Error"] = "A soma dos EVs está incorreta.";
            }
            else
            {
                if (form["IsEgg"] != null)
                {
                    PokemonTradeOffer tradeOffer = new PokemonTradeOffer();
                    tradeOffer.IsEgg = true;
                    tradeOffer.PersonId = CurrentUsuario.Id;
                    tradeOffer.PokemonId = pokemonId;
                    tradeOffer.ShinyValue = form["ShinyValue"];
                    tradeOffer.PersonMessage = form["PokemonTradeMessage"];

                    NPartyDb<PokemonTradeOffer>.Instance.Insert(tradeOffer);
                }
                else
                {
                    PokemonTradeOffer tradeOffer = new PokemonTradeOffer();
                    tradeOffer.IsEgg = false;
                    tradeOffer.PersonId = CurrentUsuario.Id;
                    tradeOffer.PokemonId = pokemonId;
                    tradeOffer.IsShiny = form["IsShiny"] != null;
                    tradeOffer.PersonMessage = form["PokemonTradeMessage"];

                    int pokemonLevel = 1;
                    int.TryParse(form["PokemonLevel"], out pokemonLevel);
                    tradeOffer.PokemonLevel = pokemonLevel == 0 ? 1 : pokemonLevel;

                    int pokemonNature = 0;
                    int.TryParse(form["PokemonNature"], out pokemonNature);
                    tradeOffer.PokemonNatureId = pokemonNature;

                    tradeOffer.HpEv = hpEv;
                    tradeOffer.AttackEv = attackEv;
                    tradeOffer.DefenseEv = defenseEv;
                    tradeOffer.SpAttackEv = spAttackEv;
                    tradeOffer.SpDefenseEv = spDefenseEv;
                    tradeOffer.SpeedEv = speedEv;

                    tradeOffer.HasIvHp = form["HasIvHp"] != null;
                    tradeOffer.HasIvAttack = form["HasIvAttack"] != null;
                    tradeOffer.HasIvDefense = form["HasIvDefense"] != null;
                    tradeOffer.HasIvSpAttack = form["HasIvSpAttack"] != null;
                    tradeOffer.HasIvSpDefense = form["HasIvSpDefense"] != null;
                    tradeOffer.HasIvSpeed = form["HasIvSpeed"] != null;

                    NPartyDb<PokemonTradeOffer>.Instance.Insert(tradeOffer);
                }
            }            

            ViewData["usuarioLogado"] = CurrentUsuario != null && CurrentUsuario.Id > 0 ? "logado" : null;
            ClassificadosPokemonView view = new ClassificadosPokemonView(CurrentUsuario,
                Request.QueryString["page"],
                Request.QueryString["PokemonFilter"] == null ? 0 : Int32.Parse(Request.QueryString["PokemonFilter"]),
                Request.QueryString["NatureFilter"] == null ? 0 : Int32.Parse(Request.QueryString["NatureFilter"]));
            return View(view);
        }
    }
}