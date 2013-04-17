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
    public class Classificacao : NPartyDbModel<Classificacao>
    {
        [DatabaseColumn(DatabaseColumnKind.Identity)]
        public int IdInscricao { get; set; }

        [DatabaseColumn]
        [Required]
        public int NumeroClassificacao { get; set; }

        [DatabaseColumn]
        [Required]
        public int Pontuacao { get; set; }

        [DatabaseColumn]
        public string IdInscricaoOriginal { get; set; }
    }
}