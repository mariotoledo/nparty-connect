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
    public class Jogos : NPartyDbModel<Jogos>
    {
        [DatabaseColumn(DatabaseColumnKind.Identity)]
        public int Id { get; set; }

        [DatabaseColumn]
        [Required]
        public string Nome { get; set; }

        [DatabaseColumn]
        [Required]
        public int IdConsole { get; set; }

        [DatabaseColumn]
        public string ImagemURL { private get; set; }

        [DatabaseColumn]
        public string CoverURL { private get; set; }

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