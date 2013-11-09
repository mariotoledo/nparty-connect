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
    public class UsuarioBadges : NPartyDbModel<UsuarioBadges>
    {
        [DatabaseColumn]
        public int PersonId { get; set; }

        [DatabaseColumn]
        public int BadgeId { get; set; }

        [DatabaseColumn]
        public string Nome { get; set; }

        [DatabaseColumn]
        public string Descricao { get; set; }

        [DatabaseColumn]
        public string ImagemURL { get; set; }
    }
}