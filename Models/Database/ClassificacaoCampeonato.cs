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
        [Required]
        public string Apelido { get; set; }

        [DatabaseColumn]
        public int NumeroClassificacao { get; set; }

        [DatabaseColumn]
        public int Pontuacao { get; set; }
    }
}