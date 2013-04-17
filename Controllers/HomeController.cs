using CampeonatosNParty.Models.Database;
using CampeonatosNParty.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CampeonatosNParty.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View(new HomeView());
        }

        public ActionResult MigrateUsers()
        {
            List<tb_usuarios> usuarios = tb_usuarios.Select().ToList();
            foreach (tb_usuarios user in usuarios)
            {
                Usuarios newUser = new Usuarios();
                newUser.Nome = user.NM_Usuario;
                newUser.Apelido = user.NM_Apelido;
                newUser.Email = user.NM_Email;

                int idEstado = string.IsNullOrEmpty(user.NM_Estado) ? 0 : Estado.Search(user.NM_Estado.ToUpper()).FirstOrDefault().EstadoId;
                newUser.Id_Estado = idEstado;

                int idCidade = string.IsNullOrEmpty(user.NM_Cidade) ? 0 : Cidade.Select().Where("EstadoId", idEstado).And("Nome", EixoX.Data.FilterComparison.Like, RemoveAccents(user.NM_Cidade)).FirstOrDefault().CidadeId;
                newUser.Id_Cidade = idCidade;

                newUser.Telefone = user.NR_Telefone;

                newUser.Data_Cadastro = user.Dt_Cadastro;

                newUser.Nivel_Permissao = 0;

                newUser.Senha = null;

                string[] nascimentoString = user.Dt_Nascimento == null ? null : user.Dt_Nascimento.Split('/');

                if (nascimentoString != null)
                {
                    if (nascimentoString[2].Length == 2)
                    {
                        nascimentoString[2] = "19" + nascimentoString[2];
                    }
                    newUser.Nascimento = new DateTime(Int32.Parse(nascimentoString[2]), Int32.Parse(nascimentoString[1]), Int32.Parse(nascimentoString[0]));
                }
                else
                {
                    newUser.Nascimento = DateTime.MaxValue;
                }

                NPartyDb<Usuarios>.Instance.Insert(newUser);
            }
            return View(new HomeView());
        }

        public static string RemoveAccents(string text)
        {
            StringBuilder sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();

            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }
            return sbReturn.ToString();
        }
    }
}
