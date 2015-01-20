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
    public class Classificacao : NPartyDbModel<Classificacao>
    {

        [DatabaseColumn(DatabaseColumnKind.PrimaryKey)]
        public int IdCampeonato { get; set; }

        [DatabaseColumn(DatabaseColumnKind.PrimaryKey)]
        public int IdUsuario { get; set; }

        [DatabaseColumn]
        public int Pontuacao { get; set; }

        [DatabaseColumn]
        public int Colocacao { get; set; }
    }
}