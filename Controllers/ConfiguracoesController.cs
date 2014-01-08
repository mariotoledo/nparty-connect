using CampeonatosNParty.Helpers;
using CampeonatosNParty.Models.Database;
using CampeonatosNParty.Models.StructModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace CampeonatosNParty.Controllers
{
    public class ConfiguracoesController : AuthenticationBasedController
    {
        //
        // GET: /Configuracoes/

        [AuthenticationRequired]
        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("DadosBasicos");
        }

        [HttpGet]
        [AuthenticationRequired]
        public ActionResult DadosBasicos()
        {
            DadosBasicos dados = new DadosBasicos();
            dados.Apelido = CurrentUsuario.Apelido;
            dados.Telefone = CurrentUsuario.Telefone;
            dados.Id_Estado = CurrentUsuario.Id_Estado;
            dados.Id_Cidade = CurrentUsuario.Id_Cidade;

            if (CurrentUsuario.Nascimento != null)
            {
                dados.BirthdayDay = CurrentUsuario.Nascimento.Day;
                dados.BirthdayMonth = CurrentUsuario.Nascimento.Month;
                dados.BirthdayYear = CurrentUsuario.Nascimento.Year;
            }
            
            return View(dados);
        }

        [HttpPost]
        [AuthenticationRequired]
        public ActionResult DadosBasicos(FormCollection form, DadosBasicos model)
        {
            if (EixoX.Restrictions.RestrictionAspect<DadosBasicos>.Instance.Validate(model))
            {
                if (string.IsNullOrEmpty(form["Id_Estado"]) || Int32.Parse(form["Id_Estado"]) == 0)
                {
                    ViewData["RegisterError"] = "Por favor, selecione o estado onde mora";
                }
                else if (string.IsNullOrEmpty(form["Id_Cidade"]) || Int32.Parse(form["Id_Cidade"]) == 0)
                {
                    ViewData["RegisterError"] = "Por favor, selecione a cidade onde mora";
                }
                else
                {
                    CurrentUsuario.Apelido = model.Apelido;
                    CurrentUsuario.Telefone = model.Telefone;
                    CurrentUsuario.Id_Estado = model.Id_Estado > 0 ? model.Id_Estado : CurrentUsuario.Id_Estado;
                    CurrentUsuario.Id_Cidade = model.Id_Cidade > 0 ? model.Id_Cidade : CurrentUsuario.Id_Cidade;

                    CurrentUsuario.Nascimento = new DateTime(model.BirthdayYear, model.BirthdayMonth, model.BirthdayDay);

                    NPartyDb<Usuarios>.Instance.Update(CurrentUsuario);

                    ViewData["RegisterSuccess"] = "Dados atualizados com sucesso.";
                }
            }
            return View(model);
        }

        [HttpGet]
        [AuthenticationRequired]
        public ActionResult DadosConexao()
        {
            DadosConexao dados = new DadosConexao();
            dados.UserId = CurrentUsuario.Id.ToString();
            dados.FacebookId = CurrentUsuario.FacebookId;
            dados.PSNId = CurrentUsuario.PsnId;
            dados.LiveId = CurrentUsuario.LiveId;
            dados.SteamId = CurrentUsuario.SteamId;

            return View(dados);
        }

        [HttpPost]
        [AuthenticationRequired]
        public ActionResult DadosConexao(FormCollection form, DadosConexao model)
        {
            model.FacebookId = CurrentUsuario.FacebookId;

            if (EixoX.Restrictions.RestrictionAspect<DadosConexao>.Instance.Validate(model))
            {
                if (!string.IsNullOrEmpty(model.PSNId) &&
                         (model.PSNId.Length < 3 || model.PSNId.Length > 16 || model.PSNId.Contains(" ")))
                {
                    ViewData["RegisterError"] = "Por favor, digite um ID válido para a PSN";
                }
                else if (!string.IsNullOrEmpty(model.LiveId) &&
                     (model.LiveId.Length < 3 || model.LiveId.Length > 16 || model.LiveId.Contains(" ")))
                {
                    ViewData["RegisterError"] = "Por favor, digite um ID válido para a Live";
                }
                else if (!string.IsNullOrEmpty(model.SteamId) &&
                 (model.SteamId.Length < 3 || model.SteamId.Length > 16 || model.SteamId.Contains(" ")))
                {
                    ViewData["RegisterError"] = "Por favor, digite um ID válido para o Steam";
                }
                else
                {
                    CurrentUsuario.PsnId = model.PSNId;
                    CurrentUsuario.LiveId = model.LiveId;
                    CurrentUsuario.SteamId = model.SteamId; 

                    NPartyDb<Usuarios>.Instance.Update(CurrentUsuario);

                    ViewData["RegisterSuccess"] = "Dados atualizados com sucesso.";
                }
            }
            return View(model);
        }

        [HttpGet]
        [AuthenticationRequired]
        public ActionResult DadosNintendisticos()
        {
            DadosNintendisticos dados = new DadosNintendisticos();
            dados.FriendCode = CurrentUsuario.FriendCode;
            dados.MiiverseId = CurrentUsuario.MiiverseId;
            dados.Jogos = PersonGame.Select().Where("PersonId", CurrentUsuario.Id).ToList();
            dados.friendSafari = PersonPokemonFriendSafari.Select().Where("PersonId", CurrentUsuario.Id).SingleResult();
            return View(dados);
        }

        [HttpPost]
        [AuthenticationRequired]
        public ActionResult DadosNintendisticos(FormCollection form, DadosNintendisticos model)
        {
            if (EixoX.Restrictions.RestrictionAspect<DadosNintendisticos>.Instance.Validate(model))
            {
                if (!string.IsNullOrEmpty(model.MiiverseId) &&
                 (model.MiiverseId.Length < 3 || model.MiiverseId.Length > 16 || model.MiiverseId.Contains(" ")))
                {
                    ViewData["RegisterError"] = "Por favor, digite um ID válido para o Miiverse";
                }
                else if (!string.IsNullOrEmpty(model.FriendCode) &&
                 (Regex.Matches(model.FriendCode, @"[a-zA-Z]").Count > 0))
                {
                    ViewData["RegisterError"] = "Por favor, digite um Friend Code válido para o 3DS";
                }
                else if (Int32.Parse(form["PokemonType"]) != 0 &&
                    (String.IsNullOrEmpty(form["PokemonSlot1"]) ||
                     String.IsNullOrEmpty(form["PokemonSlot2"]) ||
                     String.IsNullOrEmpty(form["PokemonSlot3"])))
                {
                    ViewData["RegisterError"] = "Atenção: para editar seu Friend Safari, você precisa selecionar os campos corretamente.";
                }
                else
                {
                    CurrentUsuario.MiiverseId = model.MiiverseId;
                    CurrentUsuario.FriendCode = model.FriendCode;

                    NPartyDb<Usuarios>.Instance.Update(CurrentUsuario);

                    NPartyDb<PersonGame>.Instance.Database.ExecuteNonQuery(System.Data.CommandType.Text,
                        "delete from PersonGame where PersonId = " + CurrentUsuario.Id, new object[0]);

                    List<PersonGame> personGameToInsert = new List<PersonGame>();

                    bool foundPokemonXY = false;

                    foreach (string key in form.AllKeys)
                    {
                        if(key.StartsWith("gameId-")){
                            int id = Int32.Parse(key.Substring(7));

                            if (id == 11)
                                foundPokemonXY = true;

                            PersonGame personGame = new PersonGame();
                            personGame.GameId = id;
                            personGame.PersonId = CurrentUsuario.Id;
                            personGameToInsert.Add(personGame);
                        }
                    }

                    NPartyDb<PersonGame>.Instance.Insert(personGameToInsert);

                    if (foundPokemonXY)
                    {
                        if (!String.IsNullOrEmpty(form["PokemonType"]) && Int32.Parse(form["PokemonType"]) > 0 &&
                        !String.IsNullOrEmpty(form["PokemonSlot1"]) && Int32.Parse(form["PokemonSlot1"]) > 0 &&
                        !String.IsNullOrEmpty(form["PokemonSlot2"]) && Int32.Parse(form["PokemonSlot2"]) > 0 &&
                        !String.IsNullOrEmpty(form["PokemonSlot3"]) && Int32.Parse(form["PokemonSlot3"]) > 0)
                        {
                            PersonPokemonFriendSafari friendSafari = PersonPokemonFriendSafari.Select().Where("PersonId", CurrentUsuario.Id).SingleResult();
                            if (friendSafari == null)
                            {
                                friendSafari = new PersonPokemonFriendSafari()
                                {
                                    PersonId = CurrentUsuario.Id,
                                    PokemonSlot1Id = Int32.Parse(form["PokemonSlot1"]),
                                    PokemonSlot2Id = Int32.Parse(form["PokemonSlot2"]),
                                    PokemonSlot3Id = Int32.Parse(form["PokemonSlot3"]),
                                    PokemonTypeId = Int32.Parse(form["PokemonType"])
                                };
                                NPartyDb<PersonPokemonFriendSafari>.Instance.Insert(friendSafari);
                            }
                            else
                            {
                                friendSafari.PersonId = CurrentUsuario.Id;
                                friendSafari.PokemonSlot1Id = Int32.Parse(form["PokemonSlot1"]);
                                friendSafari.PokemonSlot2Id = Int32.Parse(form["PokemonSlot2"]);
                                friendSafari.PokemonSlot3Id = Int32.Parse(form["PokemonSlot3"]);
                                friendSafari.PokemonTypeId = Int32.Parse(form["PokemonType"]);
                                NPartyDb<PersonPokemonFriendSafari>.Instance.Save(friendSafari);
                            }

                            model.friendSafari = friendSafari;
                        }
                    }
                    else
                    {
                        PersonPokemonFriendSafari friendSafari = PersonPokemonFriendSafari.Select().Where("PersonId", CurrentUsuario.Id).SingleResult();
                        if (friendSafari != null)
                            NPartyDb<PersonPokemonFriendSafari>.Instance.Database.ExecuteNonQuery(System.Data.CommandType.Text,
                        "delete from PersonPokemonFriendSafari where PersonId = " + CurrentUsuario.Id, new object[0]);
                    }

                    ViewData["RegisterSuccess"] = "Dados atualizados com sucesso.";
                }
            }
            model.Jogos = PersonGame.Select().Where("PersonId", CurrentUsuario.Id).ToList();
            return View(model);
        }

        [HttpGet]
        [AuthenticationRequired]
        public ActionResult FotoPerfil()
        {
            ViewData["FotoURL"] = CurrentUsuario.getUrlFotoPerfil();
            return View();
        }

        [HttpPost]
        [AuthenticationRequired]
        public ActionResult FotoPerfil(FormCollection form)
        {
            HttpFileCollectionBase requestFiles = Request.Files;
            HttpPostedFileBase profileImage = requestFiles["ProfileImage"];

            if (!ImageHelper.IsImage(profileImage))
            {
                ViewData["RegisterError"] = "Por favor, insira uma imagem válida.";
            }
            else
            {
                AmazonS3Manager manager = new AmazonS3Manager();
                string key = CurrentUsuario.Id.ToString() + "_" + DateTime.Now.ToString("ddMMyyyyhhmmss");

                manager.Delete(CurrentUsuario.getUrlFotoPerfil());
                manager.Save(key, ImageHelper.ResizeAndCropStream(500, profileImage.InputStream));

                CurrentUsuario.UrlFotoPerfil = manager.GetUrl(key);
                NPartyDb<Usuarios>.Instance.Save(CurrentUsuario);

                ViewData["RegisterSuccess"] = "Foto atualizada com sucesso.";
            }

            ViewData["FotoURL"] = CurrentUsuario.getUrlFotoPerfil();
            return View();
        }

        [HttpGet]
        [AuthenticationRequired]
        public ActionResult AlterarSenha()
        {
            return View();
        }

        [HttpPost]
        [AuthenticationRequired]
        public ActionResult AlterarSenha(FormCollection form)
        {
            string currentPassword = form["CurrentPassword"];
            string newPassword = form["NewPassword"];
            string confirmNewPassword = form["NewPasswordConfirmation"];

            if (String.IsNullOrEmpty(currentPassword))
            {
                ViewData["Error"] = "Por favor, digite sua senha atual";
            }
            else if (String.IsNullOrEmpty(newPassword))
            {
                ViewData["Error"] = "Por favor, digite sua nova senha";
            }
            else if (String.IsNullOrEmpty(confirmNewPassword))
            {
                ViewData["Error"] = "Por favor, confirme sua nova senha";
            }
            else
            {
                if (CampeonatosNParty.Helpers.RegisterHelper.CheckValidPassword(CurrentUsuario.Senha, currentPassword))
                {
                    if (newPassword.Length < 6 || newPassword.Length > 8)
                    {
                        ViewData["Error"] = "Atenção, sua nova senha deve conter 6 a 8 dígitos";
                    }
                    else if (newPassword.CompareTo(confirmNewPassword) != 0)
                    {
                        ViewData["Error"] = "Por favor, confirme sua nova senha corretamente";
                    }
                    else
                    {
                        CurrentUsuario.Senha = CampeonatosNParty.Helpers.RegisterHelper.GetEncryptedPassword(newPassword);
                        NPartyDb<Usuarios>.Instance.Update(CurrentUsuario);
                        ViewData["Success"] = "Senha alterada com sucesso :)";
                    }
                }
                else
                {
                    ViewData["Error"] = "Senha atual inválida.";
                }
            }

            return View();
        }
    }
}
