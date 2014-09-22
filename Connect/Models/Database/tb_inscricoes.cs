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
    public class tb_inscricoes : NPartyDbModel<tb_inscricoes>
    {
        [DatabaseColumn]
        public string ID_Inscricao { get; set; }

        [DatabaseColumn]
        public string ID_Usuario { get; set; }

        [DatabaseColumn]
        public string ID_Campeonato { get; set; }

        [DatabaseColumn]
        public string ID_Evento { get; set; }

        [DatabaseColumn]
        public int NR_Pontos { get; set; }
    }
}