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
    public class Estado : NPartyDbModel<Estado>
    {
        [DatabaseColumn(DatabaseColumnKind.PrimaryKey)]
        public int EstadoId { get; set; }

        [DatabaseColumn]
        [MaxLength(2)]
        public string Sigla { get; set; }
    }
}