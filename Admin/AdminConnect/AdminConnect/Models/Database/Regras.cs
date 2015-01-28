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
    public class Regras : NPartyDbModel<Regras>
    {
        [DatabaseColumn(DatabaseColumnKind.Identity)]
        public long Id { get; set; }

        [DatabaseColumn]
        public long IdJogo { get; set; }

        [DatabaseColumn]
        public string Descricao { get; set; }

        [DatabaseColumn]
        public string Nome { get; set; }
    }
}