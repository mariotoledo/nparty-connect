using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EixoX.Restrictions;
using EixoX.Data;
using EixoX.Interceptors;
using EixoX.UI;

namespace CampeonatosNParty.Models.Database
{
    [DatabaseTable]
    public class CampeonatoPorEvento : NPartyDbModel<CampeonatoPorEvento>
    {
        [DatabaseColumn]
        public int IdCampeonato { get; set; }

        [DatabaseColumn]
        [Required]
        public string NomeJogo { get; set; }

        [DatabaseColumn]
        [Required]
        public int IdEvento { get; set; }

        [DatabaseColumn]
        public string NomeEvento { get; set; }

        [DatabaseColumn]
        public DateTime DataCampeonato { get; set; }

        [DatabaseColumn]
        public string ImagemURL { private get; set; }

        public string getImagemUrl()
        {
            if (String.IsNullOrEmpty(this.ImagemURL))
                return "/Static/img/gameCovers/default.jpg";
            return this.ImagemURL;
        }
    }
}