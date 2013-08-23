using CampeonatosNParty.Models.Database;
using CampeonatosNParty.Models.StructModel;
using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CampeonatosNParty.Controllers
{
    public class AdminController : AuthenticationBasedController
    {
        //
        // GET: /Admin/
        [AuthenticationRequired]
        public ActionResult Index()
        {
            return View();
        }

        [AuthenticationRequired]
        public ActionResult Usuarios()
        {
            int page = 0;
            int.TryParse(Request.QueryString["page"], out page);

            ClassSelect<JogadoresItem> search = JogadoresItem.Search(Request.QueryString["filter"]);
            search.Page(18, page);
            search.OrderBy("NomeUsuario");

            return View(search.ToResult());
        }

        [AuthenticationRequired]
        [HttpGet]
        public ActionResult DetalhesUsuario(int? id)
        {
            CampeonatosNParty.Models.ViewModel.UsuariosDetailView view = new CampeonatosNParty.Models.ViewModel.UsuariosDetailView(id.Value);
            return View(view);
        }

        [AuthenticationRequired]
        [HttpGet]
        public ActionResult EditarUsuario(int? id)
        {
            CampeonatosNParty.Models.Database.Usuarios user = NPartyDb<Usuarios>.Instance.WithIdentity(id.Value);
            return View(new EditarInformacoes()
            {
                Apelido = user.Apelido,
                Email = user.Email,
                Id = user.Id,
                Id_Cidade = user.Id_Cidade,
                Id_Estado = user.Id_Estado, 
                Nascimento = user.Nascimento, 
                Nome = user.Nome, 
                Telefone = user.Telefone
            });
        }

        [AuthenticationRequired]
        [HttpPost]
        public ActionResult EditarUsuario(FormCollection form, EditarInformacoes model)
        {
            model.Nascimento = new DateTime(Int32.Parse(form["BirthdayYear"]), Int32.Parse(form["BirthdayMonth"]), Int32.Parse(form["BirthdayDay"]));

            if (EixoX.Restrictions.RestrictionAspect<EditarInformacoes>.Instance.Validate(model))
            {
                if (string.IsNullOrEmpty(form["Id_Estado"]) || Int32.Parse(form["Id_Estado"]) == 0)
                {
                    ViewData["ErrorMessage"] = "Por favor, selecione o estado onde mora";
                }
                else if (string.IsNullOrEmpty(form["Id_Cidade"]) || Int32.Parse(form["Id_Cidade"]) == 0)
                {
                    ViewData["ErrorMessage"] = "Por favor, selecione a cidade onde mora";
                }
                else
                {
                    Usuarios u = CampeonatosNParty.Models.Database.Usuarios.WithIdentity(model.Id);
                    if (u != null)
                    {
                        u.Apelido = model.Apelido;
                        u.Email = model.Email;
                        u.Id_Cidade = model.Id_Cidade;
                        u.Id_Estado = model.Id_Estado;
                        u.Nascimento = model.Nascimento;
                        u.Nome = model.Nome;
                        u.Telefone = model.Telefone;

                        NPartyDb<Usuarios>.Instance.Save(u);
                        ViewData["SuccessMessage"] = "Dados alterados com sucesso.";
                    }
                    else
                    {
                        ViewData["ErrorMessage"] = "Este e-mail já está registrado.";
                    }
                }
            }
            return View(model);
        }

    }
}
