using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EixoX.Restrictions;
using EixoX.Data;
using EixoX.Interceptors;
using EixoX.UI;
using CampeonatosNParty.Models.Database;

namespace CampeonatosNParty.Models.Database
{
    [DatabaseTable]
    public class Eventos : NPartyDbModel<Eventos>
    {
        [DatabaseColumn(DatabaseColumnKind.Identity)]
        [UIHidden]
        public int Id { get; set; }

        [DatabaseColumn]
        [Required]
        [MaxLength(50)]
        [UISingleline]
        public string Nome { get; set; }

        [DatabaseColumn]
        [Required]
        public int TipoEvento { get; set; }

        [DatabaseColumn]
        [MaxLength(50)]
        [UISingleline]
        public string Local { get; set; }

        [DatabaseColumn]
        public int IdCidade { get; set; }

        [DatabaseColumn]
        public int IdEstado { get; set; }

        [DatabaseColumn]
        [Required]
        [UIDatepicker]
        public DateTime DataEventoInicio { get; set; }

        [DatabaseColumn]
        [UIDatepicker]
        public DateTime DataEventoFim { get; set; }

        [DatabaseColumn]
        public DateTime DataCadastro { get; set; }

        [DatabaseColumn]
        [UISingleline]
        public string ImagemURL { private get; set; }

        [DatabaseColumn]
        [UISingleline]
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
    }
}