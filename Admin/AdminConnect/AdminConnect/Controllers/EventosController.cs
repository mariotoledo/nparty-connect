using AdminConnect.Helpers;
using AdminConnect.Models.Attributes;
using AdminConnect.Models.Database;
using AdminConnect.Models.View;
using CampeonatosNParty.Helpers;
using CampeonatosNParty.Models.Database;
using EixoX.Data;
using EixoX.Web.AuthComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminConnect.Controllers
{
    public class EventosController : NPartyController
    {
        //
        // GET: /Eventos/
        [AuthenticationRequired]
        public ActionResult Index()
        {
            return RedirectToAction("Gerenciar");
        }

        [AuthenticationRequired]
        public ActionResult Gerenciar()
        {
            return View();
        }

        [AuthenticationRequired]
        public ActionResult Detalhes(int? id)
        {
            if (!id.HasValue)
            {
                FlashMessage("Você precisa selecionar um evento", MessageType.Error);
                return RedirectToAction("Gerenciar");
            }

            try
            {
                DetalhesEventoView detalhesEvento = new DetalhesEventoView(id.Value);
                return View(detalhesEvento);
            }
            catch (Exception e)
            {
                FlashMessage("Ops, ocorreu o seguinte erro: " + e.Message, MessageType.Error);

                Log log = new Log()
                {
                    Descricao = e.Message,
                    IdUsuario = CurrentUser.Id,
                    Tipo = 1,
                    Titulo = "Erro ao tentar abrir pagina de detalhes de evento",
                    DataCriacao = DateTime.Now
                };

                NPartyDb<Log>.Instance.Insert(log);

                return RedirectToAction("Gerenciar");
            }
        }

        [AuthenticationRequired]
        [AccessRequired(Models.Attributes.AccessType.CanEditEvent)]
        [HttpGet]
        public ActionResult Editar(int? id)
        {
            try
            {
                if(!id.HasValue){
                    FlashMessage("Você precisa selecionar um evento", MessageType.Error);
                    return RedirectToAction("Gerenciar");
                }

                var eventos = Eventos.Select();
                var evento = eventos.Where(t => t.Id == id.Value).SingleOrDefault();

                if (evento == null)
                {
                    FlashMessage("Este evento não existe", MessageType.Error);
                    return RedirectToAction("Gerenciar");
                }

                if (CurrentUser.AdminSupremo == false && evento.IdOrganizador != CurrentUser.IdOrganizador)
                {
                    FlashMessage("Você não tem permissão para editar este evento", MessageType.Error);
                    return RedirectToAction("Gerenciar");
                }

                ViewData["TipoEvento"] = TipoEvento.Select();
                ViewData["Estados"] = Estado.Select();
                ViewData["Eventos"] = eventos.OrderBy("Nome");
                ViewData["Organizadores"] = Organizador.Select().Where("OrganizadorPublico", 1).Or("Id", CurrentUser.IdOrganizador).OrderBy("Nome");

                return View(evento);
            }
            catch (Exception e)
            {
                FlashMessage("Ops, ocorreu o seguinte erro: " + e.Message, MessageType.Error);

                Log log = new Log()
                {
                    Descricao = e.Message,
                    IdUsuario = CurrentUser.Id,
                    Tipo = 1,
                    Titulo = "Erro ao tentar abrir pagina de edicao de evento",
                    DataCriacao = DateTime.Now
                };

                NPartyDb<Log>.Instance.Insert(log);

                return RedirectToAction("Gerenciar");
            }            
        }

        [AuthenticationRequired]
        [AccessRequired(Models.Attributes.AccessType.CanCreateEvent)]
        [HttpGet]
        public ActionResult Novo()
        {
            try
            {
                ViewData["TipoEvento"] = TipoEvento.Select();
                ViewData["Estados"] = Estado.Select();
                ViewData["Eventos"] = Eventos.Select().OrderBy("Nome");
                ViewData["Organizadores"] = Organizador.Select().Where("OrganizadorPublico", 1).Or("Id", CurrentUser.IdOrganizador).OrderBy("Nome");
            }
            catch (Exception e)
            {
                FlashMessage("Ops, ocorreu o seguinte erro: " + e.Message, MessageType.Error);

                Log log = new Log()
                {
                    Descricao = e.Message,
                    IdUsuario = CurrentUser.Id,
                    Tipo = 1,
                    Titulo = "Erro ao tentar abrir pagina de criacao de evento",
                    DataCriacao = DateTime.Now
                };

                NPartyDb<Log>.Instance.Insert(log);

                return RedirectToAction("Gerenciar");
            }

            return View(new Eventos());
        }

        [AuthenticationRequired]
        [AccessRequired(Models.Attributes.AccessType.CanCreateEvent)]
        [HttpPost]
        public ActionResult Novo(FormCollection form, Eventos evento)
        {
            try
            {
                IEnumerable<TipoEvento> tiposEvento = TipoEvento.Select();
                ViewData["TipoEvento"] = tiposEvento;

                IEnumerable<Estado> estados = Estado.Select();
                ViewData["Estados"] = estados;

                ViewData["Eventos"] = Eventos.Select().OrderBy("Nome");

                ViewData["Organizadores"] = Organizador.Select().Where("OrganizadorPublico", 1).Or("Id", CurrentUser.IdOrganizador).OrderBy("Nome");

                if (string.IsNullOrEmpty(evento.Nome))
                {
                    FlashMessage("Você precisa inserir um nome para o evento", MessageType.Error);
                    return View(evento);
                }

                if (evento.TipoEvento <= 0)
                {
                    FlashMessage("Você precisa selecionar o tipo do evento", MessageType.Error);
                    return View(evento);
                }

                if (tiposEvento.Where(t => t.Id == evento.TipoEvento).Count() == 0)
                {
                    FlashMessage("Este tipo de evento não existe", MessageType.Error);
                    return View(evento);
                }

                if (string.IsNullOrEmpty(evento.Local))
                {
                    FlashMessage("Você precisa inserir o nome do local do evento", MessageType.Error);
                    return View(evento);
                }

                if (evento.IdEstado <= 0)
                {
                    FlashMessage("Você precisa selecionar um estado", MessageType.Error);
                    return View(evento);
                }

                if (estados.Where(t => t.EstadoId == evento.IdEstado).Count() == 0)
                {
                    FlashMessage("Este estado não existe", MessageType.Error);
                    return View(evento);
                }

                if (evento.IdCidade <= 0)
                {
                    FlashMessage("Você precisa selecionar uma cidade", MessageType.Error);
                    return View(evento);
                }

                Cidade cidade = Cidade.Select().Where("CidadeId", evento.IdCidade).SingleResult();

                if (cidade == null)
                {
                    FlashMessage("Esta cidade não existe", MessageType.Error);
                    return View(evento);
                }

                DateTime dataEventoInicio;

                try
                {
                    string teste = form["DataInicioEvento"];
                    dataEventoInicio = DateTime.ParseExact(form["DataInicioEvento"], "dd/MM/yyyy HH:mm",
                                       System.Globalization.CultureInfo.InvariantCulture);
                }
                catch (Exception e)
                {
                    FlashMessage("Ocorreu um erro ao tentar formatar a data de início do evento", MessageType.Error);

                    Log log = new Log()
                    {
                        Descricao = e.Message,
                        IdUsuario = CurrentUser.Id,
                        Tipo = 1,
                        Titulo = "Erro ao tentar criar um evento (data de inicio)",
                        DataCriacao = DateTime.Now
                    };

                    NPartyDb<Log>.Instance.Insert(log);

                    return View(evento);
                }

                evento.DataEventoInicio = dataEventoInicio;

                if (form["UnoMas"] != null)
                {
                    DateTime dataEventoFim;

                    try
                    {
                        dataEventoFim = DateTime.ParseExact(form["DataFimEvento"], "dd/MM/yyyy HH:mm",
                                           System.Globalization.CultureInfo.InvariantCulture);

                        if (dataEventoInicio.CompareTo(dataEventoFim) > 0)
                        {
                            FlashMessage("Término do evento não pode acontecer antes do início do evento", MessageType.Error);
                            return View(evento);
                        }
                    }
                    catch (Exception e)
                    {
                        FlashMessage("Ocorreu um erro ao tentar formatar a data de término do evento", MessageType.Error);

                        Log log = new Log()
                        {
                            Descricao = e.Message,
                            IdUsuario = CurrentUser.Id,
                            Tipo = 1,
                            Titulo = "Erro ao tentar criar um evento (data de termino)",
                            DataCriacao = DateTime.Now
                        };

                        NPartyDb<Log>.Instance.Insert(log);

                        return View(evento);
                    }

                    evento.DataEventoFim = dataEventoFim;
                }

                int imageOption = Int32.Parse(form["UsoDeLogo"]);

                if (imageOption == 1)
                {
                    HttpFileCollectionBase requestFiles = Request.Files;
                    HttpPostedFileBase eventImage = requestFiles["ImagemURL"];

                    if (!ImageHelper.IsEventImage(eventImage))
                    {
                        FlashMessage("Por favor, insira uma imagem válida.", MessageType.Error);
                        return View(evento);
                    }
                    else
                    {
                        // imagem precisa ser menor que 300px de altura e 1000px de largura na big
                        // imagem precisa ser menor que 500 por 500 na small
                        AmazonS3Manager manager = new AmazonS3Manager();
                        manager.Delete(evento.getCoverUrl());
                        manager.Delete(evento.getImagemUrl());

                        string keySmall = evento.Id.ToString() + "_small_" + DateTime.Now.ToString("ddMMyyyyhhmmss");
                        manager.Save(keySmall, ImageHelper.CreateSmallEventImage(eventImage.InputStream));

                        string keyBig = evento.Id.ToString() + "_big_" + DateTime.Now.ToString("ddMMyyyyhhmmss");
                        manager.Save(keyBig, ImageHelper.CreateBigEventImage(eventImage.InputStream));

                        evento.CoverURL = manager.GetUrl(keyBig);
                        evento.ImagemURL = manager.GetUrl(keySmall);
                    }
                }
                else if (imageOption == 2)
                {
                    Eventos eventoParaImagem = Eventos.Select().Where("Id", form["ImagemDeEvento"]).SingleResult(); 
                    evento.CoverURL = eventoParaImagem.getCoverUrl();
                    evento.ImagemURL = eventoParaImagem.getImagemUrl();
                }
                else
                {
                    evento.CoverURL = null;
                    evento.ImagemURL = null;
                }

                evento.Descricao = form["Descricao"];
                evento.DataCadastro = DateTime.Now;

                NPartyDb<Eventos>.Instance.Insert(evento);

                FlashMessage("Evento criado com sucesso", MessageType.Success);
                return RedirectToAction("Gerenciar");
            }
            catch (Exception e)
            {
                FlashMessage("Ops, ocorreu o seguinte erro: " + e.Message, MessageType.Error);

                Log log = new Log()
                {
                    Descricao = e.Message,
                    IdUsuario = CurrentUser.Id,
                    Tipo = 1,
                    Titulo = "Erro ao tentar criar um evento",
                    DataCriacao = DateTime.Now
                };

                NPartyDb<Log>.Instance.Insert(log);

                return RedirectToAction("Gerenciar");
            }
        }

        [AuthenticationRequired]
        [AccessRequired(Models.Attributes.AccessType.CanEditEvent)]
        [HttpPost]
        public ActionResult Editar(int? id, FormCollection form, Eventos evento)
        {
            try
            {
                if (!id.HasValue)
                {
                    FlashMessage("Você precisa selecionar um evento", MessageType.Error);
                    return RedirectToAction("Gerenciar");
                }

                var eventos = Eventos.Select();
                Eventos eventoToUpdate = eventos.Where(t => t.Id == id.Value).SingleOrDefault();

                if (eventoToUpdate == null)
                {
                    FlashMessage("Evento não encontrado", MessageType.Success);
                    return RedirectToAction("Gerenciar");
                }

                IEnumerable<TipoEvento> tiposEvento = TipoEvento.Select();
                ViewData["TipoEvento"] = tiposEvento;

                IEnumerable<Estado> estados = Estado.Select();
                ViewData["Estados"] = estados;                

                ViewData["Eventos"] = eventos.OrderBy("Nome");

                if (string.IsNullOrEmpty(evento.Nome))
                {
                    FlashMessage("Você precisa inserir um nome para o evento", MessageType.Error);
                    return View(evento);
                }

                eventoToUpdate.Nome = evento.Nome;

                if (evento.TipoEvento <= 0)
                {
                    FlashMessage("Você precisa selecionar o tipo do evento", MessageType.Error);
                    return View(evento);
                }

                if (tiposEvento.Where(t => t.Id == evento.TipoEvento).Count() == 0)
                {
                    FlashMessage("Este tipo de evento não existe", MessageType.Error);
                    return View(evento);
                }

                eventoToUpdate.TipoEvento = evento.TipoEvento;

                if (string.IsNullOrEmpty(evento.Local))
                {
                    FlashMessage("Você precisa inserir o nome do local do evento", MessageType.Error);
                    return View(evento);
                }

                eventoToUpdate.Local = evento.Local;

                if (evento.IdEstado <= 0)
                {
                    FlashMessage("Você precisa selecionar um estado", MessageType.Error);
                    return View(evento);
                }

                if (estados.Where(t => t.EstadoId == evento.IdEstado).Count() == 0)
                {
                    FlashMessage("Este estado não existe", MessageType.Error);
                    return View(evento);
                }

                eventoToUpdate.IdEstado = evento.IdEstado;

                if (evento.IdCidade <= 0)
                {
                    FlashMessage("Você precisa selecionar uma cidade", MessageType.Error);
                    return View(evento);
                }

                Cidade cidade = Cidade.Select().Where("CidadeId", evento.IdCidade).SingleResult();

                if (cidade == null)
                {
                    FlashMessage("Esta cidade não existe", MessageType.Error);
                    return View(evento);
                }

                eventoToUpdate.IdCidade = evento.IdCidade;

                DateTime dataEventoInicio;

                try
                {
                    dataEventoInicio = DateTime.ParseExact(form["DataInicioEvento"], "dd/MM/yyyy HH:mm",
                                       System.Globalization.CultureInfo.InvariantCulture);
                }
                catch (Exception e)
                {
                    FlashMessage("Ocorreu um erro ao tentar formatar a data de início do evento", MessageType.Error);

                    Log log = new Log()
                    {
                        Descricao = e.Message,
                        IdUsuario = CurrentUser.Id,
                        Tipo = 1,
                        Titulo = "Erro ao tentar editar um evento (data de inicio)",
                        DataCriacao = DateTime.Now
                    };

                    NPartyDb<Log>.Instance.Insert(log);

                    return View(evento);
                }

                eventoToUpdate.DataEventoInicio = dataEventoInicio;

                if (form["UnoMas"] != null)
                {
                    DateTime dataEventoFim;

                    try
                    {
                        dataEventoFim = DateTime.ParseExact(form["DataFimEvento"], "dd/MM/yyyy HH:mm",
                                           System.Globalization.CultureInfo.InvariantCulture);

                        if (dataEventoInicio.CompareTo(dataEventoFim) > 0)
                        {
                            FlashMessage("Término do evento não pode acontecer antes do início do evento", MessageType.Error);
                            return View(evento);
                        }
                    }
                    catch (Exception e)
                    {
                        FlashMessage("Ocorreu um erro ao tentar formatar a data de término do evento", MessageType.Error);

                        Log log = new Log()
                        {
                            Descricao = e.Message,
                            IdUsuario = CurrentUser.Id,
                            Tipo = 1,
                            Titulo = "Erro ao tentar editar um evento (data de termino)",
                            DataCriacao = DateTime.Now
                        };

                        NPartyDb<Log>.Instance.Insert(log);

                        return View(evento);
                    }

                    eventoToUpdate.DataEventoFim = dataEventoFim;
                }

                int imageOption = Int32.Parse(form["UsoDeLogo"]);

                if (imageOption == 1)
                {
                    HttpFileCollectionBase requestFiles = Request.Files;
                    HttpPostedFileBase eventImage = requestFiles["ImagemURL"];

                    if (!ImageHelper.IsEventImage(eventImage))
                    {
                        FlashMessage("Por favor, insira uma imagem válida.", MessageType.Error);
                        return View(evento);
                    }
                    else
                    {
                        // imagem precisa ser menor que 300px de altura e 1000px de largura na big
                        // imagem precisa ser menor que 500 por 500 na small
                        AmazonS3Manager manager = new AmazonS3Manager();
                        manager.Delete(evento.getCoverUrl());
                        manager.Delete(evento.getImagemUrl());

                        string keySmall = evento.Id.ToString() + "_small_" + DateTime.Now.ToString("ddMMyyyyhhmmss");
                        manager.Save(keySmall, ImageHelper.CreateSmallEventImage(eventImage.InputStream));

                        string keyBig = evento.Id.ToString() + "_big_" + DateTime.Now.ToString("ddMMyyyyhhmmss");
                        manager.Save(keyBig, ImageHelper.CreateBigEventImage(eventImage.InputStream));

                        eventoToUpdate.CoverURL = manager.GetUrl(keyBig);
                        eventoToUpdate.ImagemURL = manager.GetUrl(keySmall);
                    }
                }
                else if (imageOption == 2)
                {
                    Eventos eventoParaImagem = Eventos.Select().Where("Id", form["ImagemDeEvento"]).SingleResult();
                    eventoToUpdate.CoverURL = eventoParaImagem.getCoverUrl();
                    eventoToUpdate.ImagemURL = eventoParaImagem.getImagemUrl();
                }
                else if (imageOption == 3)
                {
                    eventoToUpdate.CoverURL = null;
                    eventoToUpdate.ImagemURL = null;
                }

                eventoToUpdate.Descricao = form["Descricao"];
                eventoToUpdate.IdOrganizador = evento.IdOrganizador;

                NPartyDb<Eventos>.Instance.Save(eventoToUpdate);

                FlashMessage("Evento atualizado com sucesso", MessageType.Success);
                return RedirectToAction("Gerenciar");
            }
            catch (Exception e)
            {
                FlashMessage("Ops, ocorreu o seguinte erro: " + e.Message, MessageType.Error);

                Log log = new Log()
                {
                    Descricao = e.Message,
                    IdUsuario = CurrentUser.Id,
                    Tipo = 1,
                    Titulo = "Erro ao tentar editar um evento",
                    DataCriacao = DateTime.Now
                };

                NPartyDb<Log>.Instance.Insert(log);

                return RedirectToAction("Gerenciar");
            }
        }

        [AuthenticationRequired]
        [HttpGet]
        public JsonResult GetCidades(long idEstado)
        {
            try
            {
                return Json(Cidade.Select().Where("EstadoId", idEstado).OrderBy("Nome"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [AuthenticationRequired]
        [AccessRequired(Models.Attributes.AccessType.CanCreateChampionship)]
        public ActionResult AdicionarCampeonato(int? id)
        {
            try
            {
                if (!id.HasValue)
                {
                    FlashMessage("Você precisa selecionar um evento", MessageType.Error);
                    return RedirectToAction("Gerenciar");
                }

                Eventos evento = Eventos.Select().Where("Id", id.Value).SingleResult();
                if (evento == null)
                {
                    FlashMessage("Evento não encontrado", MessageType.Error);
                    return RedirectToAction("Gerenciar");
                }

                Organizador o = Organizador.Select().Where("Id", evento.IdOrganizador).SingleResult();
                if (o.OrganizadorPublico == false && evento.IdOrganizador != CurrentUser.IdOrganizador)
                {
                    FlashMessage("Você não pode criar campeonatos para este evento", MessageType.Error);
                    return RedirectToAction("Gerenciar");
                }

                ViewData["Evento"] = evento;
                ViewData["Jogos"] = Jogos.Select().OrderBy("Nome");

                Campeonatos campeonato = new Campeonatos();
                return View(campeonato);
            } catch (Exception e) {
                FlashMessage("Ops, ocorreu o seguinte erro: " + e.Message, MessageType.Error);
                return RedirectToAction("Gerenciar");
            }
        }

        [HttpPost]
        [AuthenticationRequired]
        [ValidateInput(false)]
        [AccessRequired(Models.Attributes.AccessType.CanCreateChampionship)]
        public ActionResult AdicionarCampeonato(int? id, FormCollection form, Campeonatos campeonato)
        {
            try
            {
                if (!id.HasValue)
                {
                    FlashMessage("Você precisa selecionar um evento", MessageType.Error);
                    return RedirectToAction("Gerenciar");
                }

                Eventos evento = Eventos.Select().Where("Id", id.Value).SingleResult();
                if (evento == null)
                {
                    FlashMessage("Evento não encontrado", MessageType.Error);
                    return RedirectToAction("Gerenciar");
                }

                Organizador o = Organizador.Select().Where("Id", evento.IdOrganizador).SingleResult();
                if (o.OrganizadorPublico == false && evento.IdOrganizador != CurrentUser.IdOrganizador)
                {
                    FlashMessage("Você não pode criar campeonatos para este evento", MessageType.Error);
                    return RedirectToAction("Gerenciar");
                }

                ClassSelect<Jogos> jogos = Jogos.Select().OrderBy("Nome");

                ViewData["Evento"] = evento;
                ViewData["Jogos"] = Jogos.Select().OrderBy("Nome");

                if (campeonato.IdJogo <= 0)
                {
                    FlashMessage("Você precisa selecionar um jogo", MessageType.Error);
                    return RedirectToAction("Gerenciar");
                }

                try
                {
                    int valorInscricao = string.IsNullOrEmpty(form["ValorInscricao"]) ? 0 : Int32.Parse(form["ValorInscricao"]);
                    campeonato.ValorInscricao = valorInscricao;
                }
                catch (Exception e)
                {
                    FlashMessage("Ocorreu um erro ao tentar formatar o valor da inscrição", MessageType.Error);
                    return View(campeonato);
                }                

                DateTime dataCampeonato;

                try
                {
                    string data = form["Data"];
                    dataCampeonato = DateTime.ParseExact(data, "dd/MM/yyyy HH:mm",
                                       System.Globalization.CultureInfo.GetCultureInfo(1046));
                }
                catch (Exception e)
                {
                    FlashMessage("Ocorreu um erro ao tentar formatar a data do campeonato", MessageType.Error);
                    return View(campeonato);
                }

                if (evento.DataEventoInicio.CompareTo(dataCampeonato) > 0)
                {
                    FlashMessage("A data do campeonato não pode ser antes da data do evento", MessageType.Error);
                    return View(campeonato);
                }

                if (evento.DataEventoFim != null && ((DateTime)evento.DataEventoFim).CompareTo(dataCampeonato) < 0)
                {
                    FlashMessage("A data do campeonato não pode ser depois da data do evento", MessageType.Error);
                    return View(campeonato);
                }

                string regras = form["Regras"];
                string premiacao = form["Premiacao"];

                campeonato.DataCadastro = DateTime.Now;
                campeonato.DataCampeonato = dataCampeonato;
                campeonato.IdStatus = 1;
                campeonato.IdEvento = evento.Id;
                campeonato.IdOrganizador = CurrentUser.IdOrganizador;
                campeonato.Regras = regras;
                campeonato.Premiacao = premiacao;

                NPartyDb<Campeonatos>.Instance.Insert(campeonato);

                FlashMessage("Campeonato criado com sucesso", MessageType.Success);
                return Redirect("~/Eventos/Detalhes/" + evento.Id);
            }
            catch (Exception e)
            {
                FlashMessage("Ops, ocorreu o seguinte erro: " + e.Message, MessageType.Error);
                return RedirectToAction("Gerenciar");
            }
        }
	}
}