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
    public class tb_campeonatos : NPartyDbModel<tb_campeonatos>
    {
        [DatabaseColumn]
        [Required]
        [UISingleline]
        public string ID_Campeonato { get; set; }

        [DatabaseColumn]
        [Required]
        [UISingleline]
        public string ID_Registro { get; set; }

        [DatabaseColumn]
        [Required]
        [UISingleline]
        public string ID_Evento { get; set; }

        [DatabaseColumn]
        [Required]
        [UISingleline]
        public string NM_Campeonato { get; set; }

        [DatabaseColumn]
        [Required]
        [UISingleline]
        public string NM_Console { get; set; }

        [DatabaseColumn]
        [Required]
        [UISingleline]
        public string NR_SalaMax { get; set; }

        [DatabaseColumn]
        [Required]
        [UISingleline]
        public string NR_ValorInscricao { get; set; }

        [DatabaseColumn]
        [Required]
        [UISingleline]
        public string Data_Campeonato { get; set; }

        [DatabaseColumn]
        [Required]
        [UISingleline]
        public string Hora_Campeonato { get; set; }

        [DatabaseColumn]
        [Required]
        [UISingleline]
        public DateTime Dt_Cadastro { get; set; }

        [DatabaseColumn]
        [Required]
        [UISingleline]
        public DateTime Hr_Cadastro { get; set; }

        [DatabaseColumn]
        [Required]
        [UISingleline]
        public string Status { get; set; }
    }
}