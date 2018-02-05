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
    public class tb_eventos : NPartyDbModel<tb_eventos>
    {
        [DatabaseColumn]
        [Required]
        [UISingleline]
        public string ID_Evento { get; set; }

        [DatabaseColumn]
        [Required]
        [UISingleline]
        public string ID_Registro { get; set; }

        [DatabaseColumn]
        [Required]
        [UISingleline]
        public string NM_Evento { get; set; }

        [DatabaseColumn]
        [Required]
        [UISingleline]
        public string NM_Tipo { get; set; }

        [DatabaseColumn]
        [Required]
        [UISingleline]
        public string NM_Tipo_Espec { get; set; }

        [DatabaseColumn]
        [Required]
        [UISingleline]
        public string NM_Local { get; set; }

        [DatabaseColumn]
        [Required]
        [UISingleline]
        public string NM_Cidade { get; set; }

        [DatabaseColumn]
        [Required]
        [UISingleline]
        public string NM_Estado { get; set; }

        [DatabaseColumn]
        [Required]
        [UISingleline]
        public string Dt_Evento { get; set; }

        [DatabaseColumn]
        [Required]
        [UISingleline]
        public DateTime Dt_Cadastro { get; set; }

        [DatabaseColumn]
        [Required]
        [UISingleline]
        public DateTime Hr_Cadastro { get; set; }
    }
}