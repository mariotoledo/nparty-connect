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
    public class JogosPorConsole : NPartyDbModel<JogosPorConsole>
    {
        [DatabaseColumn]
        public int IdJogo { get; set; }

        [DatabaseColumn]
        public string NomeJogo { get; set; }

        [DatabaseColumn]
        public string ImagemJogo { get; set; }

        [DatabaseColumn]
        public string NomeConsole { get; set; }
    }
}