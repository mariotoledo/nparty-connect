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
    public class ClassificacaoPorJogador : NPartyDbModel<ClassificacaoPorJogador>
    {
        [DatabaseColumn]
        public int IdCampeonato { get; set; }

        [DatabaseColumn]
        public int IdUsuario { get; set; }

        [DatabaseColumn]
        public string NomeJogo { get; set; }

        [DatabaseColumn]
        public string ImagemURLJogo { private get; set; }

        [DatabaseColumn]
        public int IdEvento;

        [DatabaseColumn]
        public string NomeEvento { get; set; }

        [DatabaseColumn]
        public DateTime DataCampeonato { get; set; }

        [DatabaseColumn]
        public int Classificacao { get; set; }

        public string getImagemURLJogo()
        {
            if (String.IsNullOrEmpty(this.ImagemURLJogo))
                return "/Static/img/gameCovers/default.jpg";
            return this.ImagemURLJogo;
        }
    }
}