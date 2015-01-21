using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EixoX.Restrictions;
using EixoX.Data;
using EixoX.Interceptors;
using EixoX.UI;

namespace CampeonatosNParty.Models.Database
{
    [DatabaseTable]
    public class ClassificacaoCampeonato : NPartyDbModel<ClassificacaoCampeonato>
    {
        [DatabaseColumn]
        public int IdCampeonato { get; set; }

        [DatabaseColumn]
        public int IdUsuario { get; set; }

        [DatabaseColumn]
        public string Nome { get; set; }

        [DatabaseColumn]
        public string Apelido { get; set; }

        [DatabaseColumn]
        public int Pontuacao;

        [DatabaseColumn]
        public int Classificacao { get; set; }

        [DatabaseColumn]
        public string UrlFotoPerfil { private get; set; }

        public string getUrlFotoPerfil()
        {
            if (String.IsNullOrEmpty(this.UrlFotoPerfil))
                return "/Static/img/playerPhoto/default.jpg";
            return this.UrlFotoPerfil;
        }

        public string getPlayerPosition()
        {
            switch (Classificacao)
            {
                case 1:
                    return "1º lugar";
                case 2:
                    return "2º lugar";
                case 3:
                    return "3º lugar";
                case 4:
                    return "4º lugar";
                default:
                    return " - ";
            }
        }
    }
}