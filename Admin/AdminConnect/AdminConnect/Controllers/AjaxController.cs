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
                int limit = string.IsNullOrEmpty(Request.QueryString["limit"]) ? 50 : Int32.Parse(Request.QueryString["limit"]);

                List<object> inscritos = NPartyDb<Inscricao>.Instance.SelectMember("IdUsuario").Where("IdCampeonato", id.Value).ToList();

                IEnumerable<Usuarios> usuarios;
                if(inscritos == null || inscritos.Count == 0){
                    usuarios = NPartyDb<Usuarios>.Instance.Select().OrderBy("Nome").Take(limit);
                } else {
                    usuarios = NPartyDb<Usuarios>.Instance.Select().Where("Id", EixoX.Data.FilterComparison.NotInCollection, inscritos).OrderBy("Nome").Take(limit);
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
                int limit = string.IsNullOrEmpty(Request.QueryString["limit"]) ? 50 : Int32.Parse(Request.QueryString["limit"]);

                return Json(NPartyDb<DetalhesUsuario>.Instance.Select().OrderBy("Data_Cadastro", EixoX.Data.SortDirection.Descending).Take(limit), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("", JsonRequestBehavior.DenyGet);
            }
        }

        public JsonResult GetCampeonatosByIdJogo(int idJogo)
        {
            try
            {
                return Json(NPartyDb<DetalhesCampeonato>.Instance.Select().Where("IdJogo", idJogo).OrderBy("DataCampeonato", EixoX.Data.SortDirection.Descending), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("", JsonRequestBehavior.DenyGet);
            }
        }

        public JsonResult GetRegras(int idCampeonato)
        {
            try
            {
                return Json(NPartyDb<Campeonatos>.Instance.Select().Where("IdCampeonato", idCampeonato).OrderBy("Nome", EixoX.Data.SortDirection.Descending), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("", JsonRequestBehavior.DenyGet);
            }
        }
	}
}