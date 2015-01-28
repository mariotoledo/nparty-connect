using AdminConnect.Models.Database;
using AdminConnect.Models.View;
using CampeonatosNParty.Models.Database;
using EixoX.Web.AuthComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminConnect.Controllers
{
    public class UsuariosController : NPartyController
    {
        [AuthenticationRequired]
        public ActionResult Detalhes(int? id)
        {
            if (!id.HasValue)
            {
                FlashMessage("Você precisa selecionar um usuário", MessageType.Error);
                return Redirect("~/Usuarios/Gerenciar");
            }

            try
            {
                DetalhesUsuario u = DetalhesUsuario.Select().Where("Id", id.Value).SingleResult();
                List<AdminConnect.Models.Database.InscricaoUsuario> dt = AdminConnect.Models.Database.InscricaoUsuario.Select().Where("IdUsuario", id.Value).OrderBy("DataCampeonato", EixoX.Data.SortDirection.Descending).ToList();
                ViewData["Inscricoes"] = dt;

                return View(u);
            }
            catch (Exception e)
            {
                FlashMessage("Ops, ocorreu o seguinte erro: " + e.Message, MessageType.Error);
                return RedirectToAction("Gerenciar");
            }
        }

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
        [HttpGet]
        public ActionResult Novo(int? idCampeonato)
        {
            try
            {
                if ((CurrentUser.AdminSupremo || (CurrentUser.PodeInserirUsuario)) == false)
                {
                    FlashMessage("Você não tem permissão para inserir usuários", MessageType.Error);
                    return Redirect("Gerenciar");
                }

                ViewData["Estados"] = Estado.Select();
                if (idCampeonato.HasValue)
                {
                    ViewData["Campeonato"] = Campeonatos.Select().Where("Id", idCampeonato.Value).SingleResult();
                }
            }
            catch (Exception e)
            {
                FlashMessage("Ops, ocorreu o seguinte erro: " + e.Message, MessageType.Error);
                return RedirectToAction("Gerenciar");
            }
            return View(new Usuarios());
        }

        [AuthenticationRequired]
        [HttpPost]
        public ActionResult Novo(int? idCampeonato, FormCollection form, Usuarios usuario)
        {
            try
            {
                if ((CurrentUser.AdminSupremo || (CurrentUser.PodeInserirUsuario)) == false)
                {
                    FlashMessage("Você não tem permissão para inserir usuários", MessageType.Error);
                    return Redirect("Gerenciar");
                }

                IEnumerable<Estado> estados = Estado.Select();
                ViewData["Estados"] = estados;

                if (string.IsNullOrEmpty(usuario.Nome))
                {
                    FlashMessage("O nome do usuário não pode estar vazio", MessageType.Error);
                    return View(usuario);
                }

                if (usuario.Nome.Length < 3)
                {
                    FlashMessage("O nome do usuário precisa ter pelo menos 3 dígitos", MessageType.Error);
                    return View(usuario);
                }

                if (string.IsNullOrEmpty(usuario.Sobrenome))
                {
                    FlashMessage("O sobrenome do usuário não pode estar vazio", MessageType.Error);
                    return View(usuario);
                }

                if (usuario.Sobrenome.Length < 3)
                {
                    FlashMessage("O sobrenome do usuário precisa ter pelo menos 3 dígitos", MessageType.Error);
                    return View(usuario);
                }

                if (string.IsNullOrEmpty(usuario.Email))
                {
                    FlashMessage("O email do usuário não pode estar vazio", MessageType.Error);
                    return View(usuario);
                }

                if (!EixoX.Restrictions.Email.IsEmail(usuario.Email))
                {
                    FlashMessage("O email digitado é inválido", MessageType.Error);
                    return View(usuario);
                }

                if (Usuarios.Select().Where("Email", usuario.Email).Count() > 0)
                {
                    FlashMessage("Já existe um usuário com o email digitado", MessageType.Error);
                    return View(usuario);
                }

                if (usuario.Id_Estado <= 0)
                {
                    FlashMessage("Você precisa selecionar um estado", MessageType.Error);
                    return View(usuario);
                }

                if (estados.Where(t => t.EstadoId == usuario.Id_Estado).Count() == 0)
                {
                    FlashMessage("Este estado não existe", MessageType.Error);
                    return View(usuario);
                }

                if (usuario.Id_Cidade <= 0)
                {
                    FlashMessage("Você precisa selecionar uma cidade", MessageType.Error);
                    return View(usuario);
                }

                Cidade cidade = Cidade.Select().Where("CidadeId", usuario.Id_Cidade).SingleResult();

                if (cidade == null)
                {
                    FlashMessage("Esta cidade não existe", MessageType.Error);
                    return View(usuario);
                }

                usuario.Data_Cadastro = DateTime.Now;

                NPartyDb<Usuarios>.Instance.Insert(usuario);

                if (idCampeonato.HasValue)
                {
                    try
                    {
                        Campeonatos c = Campeonatos.Select().Where("Id", idCampeonato.Value).SingleResult();
                        if (c == null)
                        {
                            FlashMessage("Usuário cadastrado com sucesso. Porém, não encontramos o campeonato indicado, então a inscrição não pode ser feita.", MessageType.Info);
                            return RedirectToAction("Gerenciar");
                        }

                        if ((CurrentUser.AdminSupremo || (CurrentUser.PodeEditarCampeonato && CurrentUser.IdOrganizador == c.IdOrganizador)) == false)
                        {
                            FlashMessage("Usuário cadastrado com sucesso. Porém, voce não possui permissão para inscrever alguém no campeonato.", MessageType.Info);
                            return Redirect("~/Campeonatos/Detalhes/" + c.Id);
                        }

                        if (c.IdStatus != 1)
                        {
                            FlashMessage("Usuário cadastrado com sucesso. Porém, você só pode inscrever alguém se o campeonato estiver no estado de 'não iniciado'", MessageType.Info);
                            return Redirect("~/Campeonatos/Detalhes/" + c.Id);
                        }

                        Inscricao insc = new Inscricao()
                        {
                            IdCampeonato = c.Id,
                            IdUsuario = usuario.Id,
                            IsPago = form["IsPago"] != null
                        };

                        NPartyDb<Inscricao>.Instance.Insert(insc);

                        FlashMessage("Cadastro de usuário e inscrição realizados com sucesso", MessageType.Success);
                        return Redirect("~/Campeonatos/Detalhes/" + c.Id);
                    }
                    catch (Exception e)
                    {
                        FlashMessage("Usuário cadastrado com sucesso. Porém, ocorreu o seguinte erro ao tentar fazer a inscrição (ela não pode ser realizada): " + e.Message, MessageType.Info);
                        return RedirectToAction("Gerenciar");
                    }
                }
                else
                {
                    FlashMessage("Usuário cadastrado com sucesso", MessageType.Success);
                    return RedirectToAction("Gerenciar");
                }                
            }
            catch (Exception e)
            {
                FlashMessage("Ops, ocorreu o seguinte erro: " + e.Message, MessageType.Error);
                return RedirectToAction("Gerenciar");
            }
        }
	}
}