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
    public class ClassificadosPokemonController : AuthenticationBasedController
    {
        //
        // GET: /Widget/

        public ActionResult Index()
        {
            ViewData["usuarioLogado"] = CurrentUsuario != null && CurrentUsuario.Id > 0 ? "logado" : null;

            ClassificadosPokemonView view = new ClassificadosPokemonView(CurrentUsuario,
                Request.QueryString["page"],
                Request.QueryString["PokemonFilter"] == null ? 0 : Int32.Parse(Request.QueryString["PokemonFilter"]),
                Request.QueryString["NatureFilter"] == null ? 0 : Int32.Parse(Request.QueryString["NatureFilter"]));

            return View(view);
        }

        [HttpGet]
        [AuthenticationRequired]
        public ActionResult ApagarClassificado(int? id)
        {
            if (id.HasValue)
            {
                PokemonTradeOffer tradeOffer = PokemonTradeOffer.WithIdentity(id.Value);
                if (tradeOffer != null && CurrentUsuario != null && CurrentUsuario.Id == tradeOffer.PersonId)
                {
                    NPartyDb<PokemonTradeOffer>.Instance.Delete(tradeOffer);
                }
            }

            return RedirectToAction("ClassificadosPokemon");
        }

        [HttpGet]
        [AuthenticationRequired]
        public ActionResult NovoAnuncio()
        {
            return View();
        }
    }
}