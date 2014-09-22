using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampeonatosNParty.Models.Database
{
    [DatabaseTable]
    public class SuperSmashBrosChallengerItem
    {
        [DatabaseColumn]
        public long IdUsuario { get; set; }

        [DatabaseColumn]
        public long IdPersonagem1 { get; set; }

        [DatabaseColumn]
        public long IdCorPersonagem1 { get; set; }

        [DatabaseColumn]
        public string NomePersonagem1 { get; set; }

        [DatabaseColumn]
        public long IdPersonagem2 { get; set; }

        [DatabaseColumn]
        public long IdCorPersonagem2 { get; set; }

        [DatabaseColumn]
        public string NomePersonagem2 { get; set; }

        [DatabaseColumn]
        public long IdPersonagem3 { get; set; }

        [DatabaseColumn]
        public long IdCorPersonagem3 { get; set; }

        [DatabaseColumn]
        public string NomePersonagem3 { get; set; }
    }
}