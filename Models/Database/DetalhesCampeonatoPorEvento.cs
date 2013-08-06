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
    public class DetalhesCampeonatoPorEvento : NPartyDbModel<DetalhesCampeonatoPorEvento>
    {   
        [DatabaseColumn]
        public int IdEvento { get; set; }

        [DatabaseColumn]
        public int IdCampeonato { get; set; }

        [DatabaseColumn]
        public int IdJogo { get; set; }

        [DatabaseColumn]
        public string Nome { get; set; }

        [DatabaseColumn]
        public int IdUsuario { get; set; }

        [DatabaseColumn]
        public string NomeJogador { get; set; }

        [DatabaseColumn]
        public string ApelidoJogador { get; set; }

        [DatabaseColumn]
        public int Pontuacao { get; set; }

        [DatabaseColumn]
        public DateTime DataCampeonato { get; set; }

        [DatabaseColumn]
        public string ImagemJogo { get; set; }

        [DatabaseColumn]
        public string ImagemJogador { get; set; }
    }
}