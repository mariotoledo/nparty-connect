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
    public class Cidade : NPartyDbModel<Cidade>
    {
        [DatabaseColumn(DatabaseColumnKind.PrimaryKey)]
        public int CidadeId { get; set; }

        [DatabaseColumn]
        public string Nome { get; set; }

        [DatabaseColumn]
        public int EstadoId { get; set; }

        [DatabaseColumn]
        public bool Capital { get; set; }
    }
}