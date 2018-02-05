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
    public class CidadeEventos : NPartyDbModel<CidadeEventos>
    {
        [DatabaseColumn]
        public int IdCidade { get; set; }

        [DatabaseColumn]
        public string Nome { get; set; }
    }
}