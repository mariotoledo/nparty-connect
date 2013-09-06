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

        /*
         * =======================================================================
         *                              USUARIOS
         * =======================================================================
         */

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
        public ActionResult RegistrarUsuario()
        {
            return View(new CampeonatosNParty.Models.Database.Usuarios());
        }

        [AuthenticationRequired]
        [HttpPost]
        public ActionResult RegistrarUsuario(FormCollection form, Usuarios model)
        {
            model.Data_Cadastro = DateTime.Now;
            model.Nivel_Permissao = 0;
            model.Nascimento = new DateTime(Int32.Parse(form["BirthdayYear"]), Int32.Parse(form["BirthdayMonth"]), Int32.Parse(form["BirthdayDay"]));
            model.Senha = CampeonatosNParty.Helpers.RegisterHelper.GetEncryptedPassword(CampeonatosNParty.Helpers.RegisterHelper.GetRandWord(5));
            model.UrlFotoPerfil = "/Static/img/playerPhoto/default.jpg";

            if (EixoX.Restrictions.RestrictionAspect<Usuarios>.Instance.Validate(model))
            {
                if (string.IsNullOrEmpty(form["Id_Estado"]) || Int32.Parse(form["Id_Estado"]) == 0)
                {
                    ViewData["RegisterError"] = "Por favor, selecione o estado onde o usuário mora";
                }
                else if (string.IsNullOrEmpty(form["Id_Cidade"]) || Int32.Parse(form["Id_Cidade"]) == 0)
                {
                    ViewData["RegisterError"] = "Por favor, selecione a cidade onde o usuário mora";
                }
                else
                {
                    Usuarios u = CampeonatosNParty.Models.Database.Usuarios.Select().Where("Email", model.Email).SingleOrDefault();

                    if (u == null)
                    {
                        NPartyDb<Usuarios>.Instance.Insert(model);
                        return Redirect("~/Admin/DetalhesUsuario/" + model.Id);
                    }
                    else
                    {
                        ViewData["RegisterError"] = "Este e-mail já está registrado.";
                    }
                }
            }
            return View(model);
        }

        [AuthenticationRequired]
        [HttpGet]
        public ActionResult EditarUsuario(int? id)
        {
            CampeonatosNParty.Models.Database.Usuarios user = NPartyDb<Usuarios>.Instance.WithIdentity(id.Value);
            return View(new EditarUsuario()
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
        public ActionResult EditarUsuario(FormCollection form, EditarUsuario model)
        {
            model.Nascimento = new DateTime(Int32.Parse(form["BirthdayYear"]), Int32.Parse(form["BirthdayMonth"]), Int32.Parse(form["BirthdayDay"]));

            if (EixoX.Restrictions.RestrictionAspect<EditarUsuario>.Instance.Validate(model))
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

        /*
         * =======================================================================
         *                              EVENTOS
         * =======================================================================
         */

        [AuthenticationRequired]
        public ActionResult Eventos()
        {
            int page = 0;
            int.TryParse(Request.QueryString["page"], out page);

            ClassSelect<Eventos> search = CampeonatosNParty.Models.Database.Eventos.Search(Request.QueryString["filter"]);
            search.Page(18, page);
            search.OrderBy("DataEventoInicio");

            return View(search.ToResult());
        }

        [AuthenticationRequired]
        [HttpGet]
        public ActionResult DetalhesEvento(int? id)
        {
            CampeonatosNParty.Models.ViewModel.EventosDetailView view = 
                new CampeonatosNParty.Models.ViewModel.EventosDetailView(id.Value);
            return View(view);
        }

        [AuthenticationRequired]
        [HttpGet]
        public ActionResult EditarEvento(int? id)
        {
            CampeonatosNParty.Models.Database.Eventos evento = NPartyDb<Eventos>.Instance.WithIdentity(id.Value);
            if (evento.ImagemURL == null || evento.ImagemURL.CompareTo("/Static/img/eventsLogos/default.jpg") == 0)
            {
                evento.ImagemURL = "";
            }

            return View(new EditarEvento()
            {
                Id = evento.Id,
                Nome = evento.Nome,
                DataEventoFim = evento.DataEventoFim,
                DataEventoInicio = evento.DataEventoInicio, 
                IdCidade = evento.IdCidade,
                IdEstado = evento.IdEstado,
                Local = evento.Local,
                TipoEvento = evento.TipoEvento,
                ImagemURL = evento.ImagemURL
            });
        }

        [AuthenticationRequired]
        [HttpPost]
        public ActionResult EditarEvento(FormCollection form, EditarEvento model)
        {
            model.DataEventoInicio = new DateTime(Int32.Parse(form["DataEventoInicioYear"]), Int32.Parse(form["DataEventoInicioMonth"]), Int32.Parse(form["DataEventoInicioDay"]));
            model.DataEventoFim = new DateTime(Int32.Parse(form["DataEventoFimYear"]), Int32.Parse(form["DataEventoFimMonth"]), Int32.Parse(form["DataEventoFimDay"]));

            if (EixoX.Restrictions.RestrictionAspect<EditarEvento>.Instance.Validate(model))
            {
                if (string.IsNullOrEmpty(form["IdEstado"]) || Int32.Parse(form["IdEstado"]) == 0)
                {
                    ViewData["ErrorMessage"] = "Por favor, selecione o estado onde mora";
                }
                else if (string.IsNullOrEmpty(form["IdCidade"]) || Int32.Parse(form["IdCidade"]) == 0)
                {
                    ViewData["ErrorMessage"] = "Por favor, selecione a cidade onde mora";
                }
                else
                {
                    Eventos e = CampeonatosNParty.Models.Database.Eventos.WithIdentity(model.Id);
                    if (e != null)
                    {
                        e.Nome = model.Nome;
                        e.TipoEvento = model.TipoEvento;
                        e.Local = model.Local;
                        e.IdEstado = model.IdEstado;
                        e.IdCidade = model.IdCidade;

                        if (string.IsNullOrEmpty(model.ImagemURL))
                            e.ImagemURL = "/Static/img/eventsLogos/default.jpg";
                        else
                            e.ImagemURL = model.ImagemURL;

                        e.DataEventoInicio = model.DataEventoInicio;

                        if (form["MoreDays"] != null)
                            e.DataEventoFim = model.DataEventoFim;
                        else
                            e.DataEventoFim = model.DataEventoInicio;

                        NPartyDb<Eventos>.Instance.Save(e);
                        ViewData["SuccessMessage"] = "Dados alterados com sucesso.";
                    }
                    else
                    {
                        ViewData["ErrorMessage"] = "Não foi possível atualizar - Você fez alguma burrada? Por favor, tente novamente.";
                    }
                }
            }
            return View(model);
        }
    }
}
