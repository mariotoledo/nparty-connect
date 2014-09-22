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
    public class DetalhesEvento : NPartyDbModel<Eventos>
    {
        [DatabaseColumn(DatabaseColumnKind.Identity)]
        public int Id { get; set; }

        [DatabaseColumn]
        [Required]
        [MaxLength(50)]
        public string Nome { get; set; }

        [DatabaseColumn]
        [Required]
        public int TipoEvento { get; set; }

        [DatabaseColumn]
        [MaxLength(50)]
        public string Local { get; set; }

        [DatabaseColumn]
        public int IdCidade { get; set; }

        [DatabaseColumn]
        public int IdEstado { get; set; }

        [DatabaseColumn]
        [Required]
        public DateTime DataEventoInicio { get; set; }

        public string DataEventoInicioString
        {
            get
            {
                return DataEventoInicio.ToString("dd/MM/yyyy");
            }
        }

        [DatabaseColumn]
        public DateTime DataEventoFim { get; set; }

        public string DataEventoFimString
        {
            get
            {
                return DataEventoFim.ToString("dd/MM/yyyy");
            }
        }

        [DatabaseColumn]
        public DateTime DataCadastro { get; set; }

        public string DataCadastromString
        {
            get
            {
                return DataCadastro.ToString("dd/MM/yyyy");
            }
        }

        [DatabaseColumn]
        public string ImagemURL { private get; set; }

        [DatabaseColumn]
        public string CoverURL { private get; set; }

        public string getCoverUrl()
        {
            if (String.IsNullOrEmpty(this.CoverURL))
                return "/Static/img/eventsLogos/defaultCover.jpg";
            return this.CoverURL;
        }

        public string getImagemUrl()
        {
            if (String.IsNullOrEmpty(this.ImagemURL))
                return "/Static/img/eventsLogos/default.jpg";
            return this.ImagemURL;
        }

        [DatabaseColumn]
        public string NomeTipoEvento { get; set; }

        [DatabaseColumn]
        public string NomeCidade { get; set; }

        [DatabaseColumn]
        public string NomeEstado { get; set; }
    }
}