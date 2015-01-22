using CampeonatosNParty.Models.Database;
using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminConnect.Models.Database
{
    [DatabaseTable]
    public class DetalhesCampeonato : NPartyDbModel<DetalhesCampeonato>
    {
        [DatabaseColumn]
        public long IdCampeonato { get; set; }

        [DatabaseColumn]
        public long IdEvento { get; set; }

        [DatabaseColumn]
        public string NomeEvento { get; set; }

        [DatabaseColumn]
        public long IdJogo { get; set; }

        [DatabaseColumn]
        public string NomeJogo { get; set; }

        [DatabaseColumn]
        public string ImagemURL { get; set; }

        [DatabaseColumn]
        public string CoverURL { get; set; }

        [DatabaseColumn]
        public string NomeConsole { get; set; }

        [DatabaseColumn]
        public DateTime DataCampeonato { get; set; }

        [DatabaseColumn]
        public long IdStatus { get; set; }

        [DatabaseColumn]
        public string NomeStatus { get; set; }

        [DatabaseColumn]
        public long IdOrganizador { get; set; }

        [DatabaseColumn]
        public string NomeOrganizador { get; set; }

        [DatabaseColumn]
        public decimal ValorInscricao { get; set; }

        public string getCoverUrl()
        {
            if (String.IsNullOrEmpty(this.CoverURL))
                return "/Static/img/gameCovers/defaultCover.jpg";
            return this.CoverURL;
        }

        public string getImagemUrl()
        {
            if (String.IsNullOrEmpty(this.ImagemURL))
                return "/Static/img/gameCovers/default.jpg";
            return this.ImagemURL;
        }
    }
}