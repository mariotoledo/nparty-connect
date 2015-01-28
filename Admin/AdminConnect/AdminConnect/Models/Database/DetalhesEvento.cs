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

        [DatabaseColumn(true)]
        public object DataEventoFim { get; set; }

        public string DataEventoFimString
        {
            get
            {
                return DataEventoFim != null ? ((DateTime)DataEventoFim).ToString("dd/MM/yyyy") : "";
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
                return "/img/defaultCover.jpg";
            return this.CoverURL;
        }

        public string getImagemUrl()
        {
            if (String.IsNullOrEmpty(this.ImagemURL))
                return "/img/default.jpg";
            return this.ImagemURL;
        }

        [DatabaseColumn]
        public string NomeTipoEvento { get; set; }

        [DatabaseColumn]
        public string NomeCidade { get; set; }

        [DatabaseColumn]
        public string NomeEstado { get; set; }

        [DatabaseColumn]
        public string NomeOrganizador { get; set; }

        [DatabaseColumn]
        public long IdOrganizador { get; set; }

        [DatabaseColumn]
        public string Descricao { get; set; }

        public string getDataEventoAsString
        {
            get{ 
                return DataEventoFim == null ? DataEventoInicio.ToString("dd/MM/yyyy") : DataEventoInicio.ToString("dd/MM/yyyy") + " até " + ((DateTime)DataEventoFim).ToString("dd/MM/yyyy");
            }
        }
    }
}