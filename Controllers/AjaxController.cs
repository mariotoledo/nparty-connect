using CampeonatosNParty.Models.Database;
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
    }
}
