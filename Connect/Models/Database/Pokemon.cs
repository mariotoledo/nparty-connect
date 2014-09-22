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
    public class Pokemon : NPartyDbModel<Pokemon>
    {
        [DatabaseColumn(DatabaseColumnKind.Identity)]
        public int Id { get; set; }

        [DatabaseColumn]
        public string Name { get; set; }

        [DatabaseColumn]
        public string PokedexNumber { get; set; }

        public string getImageUrl()
        {
            return "http://www.serebii.net/xy/pokemon/" + PokedexNumber + ".png";
        }
    }
}