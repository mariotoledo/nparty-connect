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
        public int IdUsuario { get; set; }

        [DatabaseColumn]
        public int Pontuacao { get; set; }

        [DatabaseColumn]
        public DateTime DataCampeonato { get; set; }

        [DatabaseColumn]
        public string NomeCampeonato { get; set; }

        [DatabaseColumn]
        public string ImagemJogoCampeonato { get; set; }

        [DatabaseColumn]
        public string NomeEvento { get; set; }
    }
}