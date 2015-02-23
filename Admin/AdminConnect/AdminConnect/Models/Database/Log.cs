using CampeonatosNParty.Models.Database;
using EixoX.Data;
using EixoX.Restrictions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminConnect.Models.Database
{
    [DatabaseTable]
    public class Log : NPartyDbModel<Log>
    {

        [DatabaseColumn(DatabaseColumnKind.Identity)]
        public long Id { get; set; }

        [DatabaseColumn]
        public string Titulo { get; set; }

        [DatabaseColumn]
        public long IdUsuario { get; set; }

        [DatabaseColumn]
        public int Tipo { get; set; }

        [DatabaseColumn]
        public string Descricao { get; set; }

        [DatabaseColumn]
        public DateTime DataCriacao { get; set; }
    }
}