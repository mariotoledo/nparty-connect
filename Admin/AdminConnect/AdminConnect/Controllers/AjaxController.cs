using AdminConnect.Models.Database;
using CampeonatosNParty.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminConnect.Controllers
{
    public class AjaxController : Controller
    {
        //
        // GET: /Ajax/
        public JsonResult GetEventos()
        {
            try
            {
                return Json(NPartyDb<DetalhesEvento>.Instance.Select().OrderBy("DataEventoInicio", EixoX.Data.SortDirection.Descending), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("", JsonRequestBehavior.DenyGet);
            }
        }

        public JsonResult GetUsersNotSubscribedToChampionship(int? id)
        {
            try
            {
                List<object> inscritos = NPartyDb<Inscricao>.Instance.SelectMember("IdUsuario").Where("IdCampeonato", id.Value).ToList();

                IEnumerable<Usuarios> usuarios;
                if(inscritos == null || inscritos.Count == 0){
                    usuarios = NPartyDb<Usuarios>.Instance.Select().OrderBy("Nome");
                } else {
                    usuarios = NPartyDb<Usuarios>.Instance.Select().Where("Id", EixoX.Data.FilterComparison.NotInCollection, inscritos).OrderBy("Nome");
                }
                return Json(usuarios, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("", JsonRequestBehavior.DenyGet);
            }
        }

        public JsonResult GetUsuarios()
        {
            try
            {
                return Json(NPartyDb<DetalhesUsuario>.Instance.Select().OrderBy("Data_Cadastro", EixoX.Data.SortDirection.Descending), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("", JsonRequestBehavior.DenyGet);
            }
        }
	}
}