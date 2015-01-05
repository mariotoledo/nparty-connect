using CampeonatosNParty.Models.Database;
using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminConnect.Models.Database
{
    [DatabaseTable]
    public class Organizador : NPartyDbModel<Organizador>
    {
        [DatabaseColumn(DatabaseColumnKind.PrimaryKey)]
        public long Id { get; set; }

        [DatabaseColumn]
        public string Nome { get; set; }

        [DatabaseColumn]
        public bool OrganizadorPublico { get; set; }
    }
}