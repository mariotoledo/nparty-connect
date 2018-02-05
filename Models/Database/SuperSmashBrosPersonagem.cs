using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampeonatosNParty.Models.Database
{
    [DatabaseTable]
    public class SuperSmashBrosPersonagem : NPartyDbModel<SuperSmashBrosPersonagem>
    {
        [DatabaseColumn(DatabaseColumnKind.Identity)]
        public long Id { get; set; }

        [DatabaseColumn]
        public string Nome { get; set; }

        [DatabaseColumn]
        public long NumeroRoupas { get; set; }
    }
}