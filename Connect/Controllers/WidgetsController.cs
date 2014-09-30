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
    public class WidgetsController : AuthenticationBasedController
    {
        //
        // GET: /Widget/

        public ActionResult Index()
        {
            return View();
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
                IdUsuario = CurrentUsuario.Id
            };

            NPartyDb<SuperSmashBrosChallenger>.Instance.Insert(newChallenger);

            ViewData["RegisterSuccess"] = "Personagens registrados com sucesso.";

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