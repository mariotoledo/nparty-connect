using CampeonatosNParty.Models.Database;
using CampeonatosNParty.Models.StructModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CampeonatosNParty.Controllers
{
    public class AjaxController : AuthenticationBasedController
    {
        //
        // GET: /Ajax/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CarregarCidades(int id)
        {
            return Json(NPartyDbModel<Cidade>.Select().Where("EstadoId", id), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CarregarPokemonFriendSafariSlot1(int id)
        {
            return Json(NPartyDbModel<PokemonFriendSafari>.Select().Where("TypeId", id).And("SlotNumber", 1), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CarregarPokemonFriendSafariSlot2(int id)
        {
            return Json(NPartyDbModel<PokemonFriendSafari>.Select().Where("TypeId", id).And("SlotNumber", 2), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CarregarPokemonFriendSafariSlot3(int id)
        {
            return Json(NPartyDbModel<PokemonFriendSafari>.Select().Where("TypeId", id).And("SlotNumber", 3), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CarregarPokemonFriendSafariSearch(int id)
        {
            if(id > 0)
                return Json(NPartyDbModel<PokemonFriendSafari>.Select().Where("TypeId", id), JsonRequestBehavior.AllowGet);
            return Json(NPartyDbModel<PokemonFriendSafari>.Select(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CarregarTodosPokemon()
        {
            return Json(NPartyDbModel<Pokemon>.Select(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult NumeroNotificacoesNaoLidas(int id)
        {
            long success = NPartyDb<Notificacoes>.Instance.Select().Where("PersonId", id).And("FoiLida", false).Count();
            return Json(success, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult MarcarLidaNotificacao(string notificationId)
        {
            int idN = Int32.Parse(notificationId);
            Notificacoes n = Notificacoes.WithIdentity(idN);
            if (n != null)
            {
                n.FoiLida = true;
                NPartyDb<Notificacoes>.Instance.Save(n);

                return Json(NPartyDb<Notificacoes>.Instance.Select().Where("PersonId", n.PersonId).And("FoiLida", false).Count(), JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public JsonResult SaveFacebookProfile(string facebookId)
        {
            if (CurrentUsuario != null  && facebookId != null)
            {
                CurrentUsuario.FacebookId = facebookId;
                NPartyDb<Usuarios>.Instance.Save(CurrentUsuario);

                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public JsonResult NullifyFacebookProfile()
        {
            if (CurrentUsuario != null)
            {
                CurrentUsuario.FacebookId = null;
                NPartyDb<Usuarios>.Instance.Save(CurrentUsuario);

                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public JsonResult ConfirmPerson1Add(string person2IdString)
        {
            if (CurrentUsuario != null)
            {
                int personId2 = 0;
                int.TryParse(person2IdString, out personId2);
                PersonGamingRelation pg = PersonGamingRelation.Select().Where("PersonId1", CurrentUsuario.Id).And("PersonId2", personId2).First();
                if (pg != null)
                {
                    pg.Person1DidAdd = true;
                    NPartyDb<PersonGamingRelation>.Instance.Save(pg);
                }

                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public JsonResult ConfirmPerson2Add(string person1IdString)
        {
            if (CurrentUsuario != null)
            {
                int personId1 = 0;
                int.TryParse(person1IdString, out personId1);
                PersonGamingRelation pg = PersonGamingRelation.Select().Where("PersonId2", CurrentUsuario.Id).And("PersonId1", personId1).First();
                if (pg != null)
                {
                    pg.Person2DidAdd = true;
                    NPartyDb<PersonGamingRelation>.Instance.Save(pg);
                }

                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [HttpGet]
        public ActionResult DoesHaveSentPokemonTradeOfferMessage(int id)
        {
            if (CurrentUsuario != null)
            {
                PokemonTradeOfferSentMessages p = NPartyDbModel<PokemonTradeOfferSentMessages>.Select().Where("PersonId", CurrentUsuario.Id).And("TradeOfferId", id).SingleOrDefault();
                if(p != null)
                    return Json(1, JsonRequestBehavior.AllowGet);
            }

            return Json(0, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AllowCrossSiteJsonAttribute]
        public ActionResult GetLastOrNextEvent()
        {
            Eventos e = Eventos.Select().OrderBy("DataEventoInicio", EixoX.Data.SortDirection.Descending).First();

            try
            {
                if (e != null)
                {
                    LastOrNextEvent evento = new LastOrNextEvent();

                    evento.eventDate = (e.DataEventoFim != null &&
                                       e.DataEventoFim != DateTime.MinValue &&
                                       e.DataEventoInicio.CompareTo(e.DataEventoFim) != 0) ?
                                       e.DataEventoInicio.ToString("dd/MM/yyyy") + " a " + e.DataEventoFim.ToString("dd/MM/yyyy") :
                                       e.DataEventoInicio.ToString("dd/MM/yyyy");
                    evento.isNextEvent = e.DataEventoInicio != null &&
                               e.DataEventoInicio.CompareTo(DateTime.Now) > 0;
                    evento.eventImageUrl = e.getCoverUrl();

                    string cidade = Cidade.WithIdentity(e.IdCidade).Nome;
                    string estado = Estado.WithIdentity(e.IdEstado).Sigla;
                    evento.eventLocation = e.Local + " - " + cidade + ", " + estado;
                    evento.eventName = e.Nome;
                    evento.eventType = TipoEvento.WithIdentity(e.TipoEvento).Nome;
                    evento.eventUrl = "http://connect.nparty.com.br/Eventos/Detalhes/" + e.Id;

                    return Json(evento, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) { }

            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AllowCrossSiteJsonAttribute]
        public ActionResult GetRankingList()
        {
            List<Ranking> list = new List<Ranking>();

            foreach(Ranking r in Ranking.Select().OrderBy("Pontos", EixoX.Data.SortDirection.Descending).Where("Ano", DateTime.Now.Year).Take(6)){
                if(r.UrlFotoPerfil.Equals("/Static/img/playerPhoto/default.jpg"))
                    r.UrlFotoPerfil = "http://connect.nparty.com.br/Static/img/playerPhoto/default.jpg";
                list.Add(r);
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SendMessagePokemonTradeOffer(string tradeOfferId, string pokemonName, string message)
        {
            if (CurrentUsuario != null)
            {
                int offerId = 0;
                int.TryParse(tradeOfferId, out offerId);

                PokemonTradeOffer offer = PokemonTradeOffer.WithIdentity(offerId);

                if (offer != null)
                {
                    Usuarios u = Usuarios.WithIdentity(offer.PersonId);
                    if (u != null)
                    {
                        PokemonTradeOfferSentMessages offerSent = 
                            PokemonTradeOfferSentMessages.Select().Where("TradeOfferId", offerId).And("PersonId", CurrentUsuario.Id).SingleOrDefault();

                        if (offerSent == null)
                        {
                            CampeonatosNParty.Helpers.EmailTemplate emailTemplate = new CampeonatosNParty.Helpers.EmailTemplate();
                            emailTemplate.Load(Server.MapPath(Url.Content("~/Static/EmailTemplates/pokemonTradeOffer.xml")));

                            emailTemplate.From = new System.Net.Mail.MailAddress(u.Email);

                            IDictionary<string, string> infoChanges = new Dictionary<string, string>();

                            infoChanges.Add("[=PersonName]", CurrentUsuario.getFullName());
                            infoChanges.Add("[=PersonId]", u.Id.ToString());
                            infoChanges.Add("[=PokemonName]", pokemonName);
                            infoChanges.Add("[=Message]", message);

                            emailTemplate.Send(infoChanges, "Você recebeu uma nova mensagem do Classificados de Troca Pokémon - N-Party Connect", u.Email);

                            offerSent = new PokemonTradeOfferSentMessages()
                            {
                                PersonId = CurrentUsuario.Id,
                                TradeOfferId = offerId
                            };

                            NPartyDb<PokemonTradeOfferSentMessages>.Instance.Insert(offerSent);

                            return Json(new { success = true });
                        }
                    }
                }
            }
            return Json(new { success = false });
        }
    }
}

public class AllowCrossSiteJsonAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "*");
        filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Methods", "GET");
        filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
        base.OnActionExecuting(filterContext);
    }
}
