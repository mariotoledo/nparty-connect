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
    public class Ranking : NPartyDbModel<Ranking>
    {
        [DatabaseColumn]
        public int ID_Usuario { get; set; }

        [DatabaseColumn]
        public string NM_Usuario { get; set; }

        [DatabaseColumn]
        public string NM_Apelido { get; set; }

        [DatabaseColumn]
        public int Pontos { get; set; }
    }
}