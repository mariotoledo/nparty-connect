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
    public class UsuarioJogos : NPartyDbModel<UsuarioJogos>
    {
        [DatabaseColumn]
        public int IdJogadador { get; set; }

        [DatabaseColumn]
        public int JogoId { get; set; }

        [DatabaseColumn]
        public int JogoNome { get; set; }

        [DatabaseColumn]
        public string ImagemURL { private get; set; }

        [DatabaseColumn]
        public string CoverURL { private get; set; }

        [DatabaseColumn]
        public string ConsoleNome { get; set; }

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