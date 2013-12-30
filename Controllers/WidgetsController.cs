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

        public ActionResult PokemonFriendSafariFinder()
        {
            PokemonFinderView view = new PokemonFinderView(CurrentUsuario, 
                Request.QueryString["page"],
                Request.QueryString["Type"] == null ? 0 : Int32.Parse(Request.QueryString["Type"]),
                Request.QueryString["Pokemon"] == null ? 0 : Int32.Parse(Request.QueryString["Pokemon"]));

            return View(view);
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
    }
}
