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
    public class Campeonatos : NPartyDbModel<Campeonatos>
    {
        [DatabaseColumn(DatabaseColumnKind.Identity)]
        [UIHidden]
        public int Id { get; set; }

        [DatabaseColumn]
        [Required]
        [UIHidden]
        public int IdEvento { get; set; }

        [DatabaseColumn]
        [Required]
        [UIHidden]
        public int IdJogo { get; set; }

        [DatabaseColumn]
        [Required]
        [UISingleline]
        public int NumeroJogadoresSala { get; set; }

        [DatabaseColumn]
        [Required]
        [UISingleline]
        public decimal ValorInscricao { get; set; }

        [DatabaseColumn]
        [Required]
        [UISingleline]
        public DateTime DataCampeonato { get; set; }

        [DatabaseColumn]
        [Required]
        [UISingleline]
        public DateTime DataCadastro { get; set; }

        [DatabaseColumn]
        [Required]
        [UISingleline]
        public int IdStatus { get; set; }

        [DatabaseColumn]
        public string IdOriginalk { get; set; }

        [DatabaseColumn]
        public long IdOrganizador { get; set; }

        [DatabaseColumn]
        public long IdRegras { get; set; }
    }
}