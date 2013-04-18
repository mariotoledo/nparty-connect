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
    public class JogadoresItem : NPartyDbModel<JogadoresItem>
    {
        [DatabaseColumn]
        public int IdUsuario { get; set; }

        [DatabaseColumn]
        public string NomeUsuario { get; set; }

        [DatabaseColumn]
        public string ApelidoUsuario { get; set; }

        [DatabaseColumn]
        public string Estado { get; set; }

        [DatabaseColumn]
        public int Pontos { get; set; }
    }
}