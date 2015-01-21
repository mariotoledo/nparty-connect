using CampeonatosNParty.Models.Database;
using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminConnect.Models.Database
{
    [DatabaseTable]
    public class InscricaoUsuario : NPartyDbModel<InscricaoUsuario>
    {
            [DatabaseColumn]
            public int IdCampeonato { get; set; }

            [DatabaseColumn]
            public int IdUsuario { get; set; }

            [DatabaseColumn]
            public string NomeUsuario { get; set; }

            [DatabaseColumn]
            public string Apelido { get; set; }

            [DatabaseColumn]
            public string UrlFotoPerfil { get; set; }

            public string getUrlFotoPerfil()
            {
                if (String.IsNullOrEmpty(this.UrlFotoPerfil))
                    return "/Static/img/playerPhoto/default.jpg";
                return this.UrlFotoPerfil;
            }
    }
}