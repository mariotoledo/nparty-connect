using CampeonatosNParty.Models.Database;
using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminConnect.Models.Database
{
    [DatabaseTable]
    public class OrganizadorMultiplicadorPontuacao : NPartyDbModel<OrganizadorMultiplicadorPontuacao>
    {
        [DatabaseColumn]
        public long IdOrganizador { get; set; }

        [DatabaseColumn]
        public double MultiplicadorPrimeiroLugar { get; set; }

        [DatabaseColumn]
        public double MultiplicadorSegundoLugar { get; set; }

        [DatabaseColumn]
        public double MultiplicadorTerceiroLugar { get; set; }

        [DatabaseColumn]
        public double MultiplicadorQuartoLugar { get; set; }

        [DatabaseColumn]
        public double MultiplicadorPadrao { get; set; }
    }
}