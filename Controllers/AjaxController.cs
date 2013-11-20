using CampeonatosNParty.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CampeonatosNParty.Controllers
{
    public class AjaxController : Controller
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
    }
}
