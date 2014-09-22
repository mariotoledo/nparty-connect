using CampeonatosNParty.Helpers;
using CampeonatosNParty.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CampeonatosNParty.Controllers
{
    public class SorteioController : Controller
    {
        //
        // GET: /Sorteio/

        public static DateTime dataSorteio = new DateTime(2014, 9, 14, 23, 0, 0);

        public ActionResult Index()
        {
            return Redirect("~/Sorteio/DemoSmash");
        }

        [HttpGet]
        public ActionResult DemoSmash()
        {
            ViewData["DataSorteio"] = dataSorteio;

            try
            {
                if (DateTime.Now.CompareTo(dataSorteio) >= 0)
                {
                    SorteioUsuario ganhador = NPartyDb<SorteioUsuario>.Instance.Select().Where("FoiGanhador", true).SingleResult();
                    if (ganhador == null)
                    {
                        ViewData["Error"] = "Já foi encerrado o tempo do cadastro";
                    }
                    else
                    {
                        ViewData["Ganhador"] = ganhador;
                    }
                    
                    return View(new SorteioUsuario());
                }

                string cKey = Request.QueryString["confirmationKey"];
                if (!string.IsNullOrEmpty(cKey))
                {
                    int id = EncryptHelper.Decrypt(cKey);
                    SorteioUsuario u = NPartyDb<SorteioUsuario>.Instance.WithIdentity(id);

                    u.EmailConfirmado = true;
                    NPartyDb<SorteioUsuario>.Instance.Save(u);

                    ViewData["Sucesso"] = "Email confirmado com sucesso! Aguarde, pois em breve realizaremos o sorteio :)";
                }
            }
            catch (Exception e)
            {

            }
            
            return View(new SorteioUsuario());
        }

        [HttpPost]
        public ActionResult DemoSmash(FormCollection form, SorteioUsuario user)
        {
            ViewData["DataSorteio"] = dataSorteio;

            try
            {
                if (DateTime.Now.CompareTo(dataSorteio) >= 0)
                {
                    ViewData["Error"] = "Já foi encerrado o tempo do cadastro";
                    return View(user);
                }

                if (string.IsNullOrEmpty(user.Nome))
                {
                    ViewData["Error"] = "Nome não pode estar vazio";
                    return View(user);
                }

                if (user.Nome.Length < 3 || user.Nome.Length > 20)
                {
                    ViewData["Error"] = "Nome possui tamanho inválido";
                    return View(user);
                }

                if (string.IsNullOrEmpty(user.Sobrenome))
                {
                    ViewData["Error"] = "Sobrenome não pode estar vazio";
                    return View(user);
                }

                if (user.Sobrenome.Length < 3 || user.Sobrenome.Length > 20)
                {
                    ViewData["Error"] = "Sobrenome possui tamanho inválido";
                    return View(user);
                }

                if (string.IsNullOrEmpty(user.Email))
                {
                    ViewData["Error"] = "Email não pode estar vazio";
                    return View(user);
                }

                if (!EixoX.Restrictions.Email.IsEmail(user.Email))
                {
                    ViewData["Error"] = "Email inválido";
                    return View(user);
                }

                if (user.IdEstado == 0)
                {
                    ViewData["Error"] = "Você deve selecionar um estado";
                    return View(user);
                }

                if (user.IdCidade == 0)
                {
                    ViewData["Error"] = "Você deve selecionar uma cidade";
                    return View(user);
                }

                SorteioUsuario sorteioUsuario = NPartyDb<SorteioUsuario>.Instance.Select().Where("Email", user.Email).SingleResult();

                if(sorteioUsuario != null){
                    if (sorteioUsuario.EmailConfirmado)
                    {
                        ViewData["Error"] = "Já existe um usuário registrado com este email.";
                        return View(user);
                    }
                    else
                    {
                        EnviarEmailConfirmacao(sorteioUsuario);
                        ViewData["Info"] = "Este email já estava registrado, mas não havia sido confirmado. Enviamos um novo email, e pedimos a gentileza de que você o acesse e clique no link de confirmação.";
                        return View(user);
                    }                    
                }

                user.DataRegistro = DateTime.Now;
                user.FoiGanhador = false;
                user.EmailConfirmado = false;

                NPartyDb<SorteioUsuario>.Instance.Insert(user);

                EnviarEmailConfirmacao(user);

                ViewData["Info"] = "Estamos quase lá! Por favor, clique no link enviado em seu email para validar sua inscrição.";
                return View(new SorteioUsuario());
            }
            catch (Exception e)
            {
                ViewData["Error"] = "Ops, algo de inesperado aconteceu. Por favor, tente novamente mais tarde";
                return View(user);
            }
        }

        public void EnviarEmailConfirmacao(SorteioUsuario user)
        {
            CampeonatosNParty.Helpers.EmailTemplate emailTemplate = new CampeonatosNParty.Helpers.EmailTemplate();
            emailTemplate.Load(Server.MapPath(Url.Content("~/Static/EmailTemplates/registroSorteio.xml")));

            IDictionary<string, string> infoChanges = new Dictionary<string, string>();

            infoChanges.Add("[=PersonName]", user.Nome);
            infoChanges.Add("[=PersonLink]", user.getConfirmationUrl());

            emailTemplate.Send(infoChanges, "Sorteio Smash - Confirmação de email", user.Email);
        }
    }
}
