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
    public class PokemonFriendSafari : NPartyDbModel<PokemonFriendSafari>
    {
        [DatabaseColumn(DatabaseColumnKind.Identity)]
        public int Id { get; set; }

        [DatabaseColumn]
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [DatabaseColumn]
        [Required]
        public int TypeId { get; set; }

        [DatabaseColumn]
        [Required]
        public int PokedexNumber { get; set; }

        [DatabaseColumn]
        [Required]
        public int SlotNumber { get; set; }

        public string getImageUrl()
        {
            return "http://www.serebii.net/xy/pokemon/" + PokedexNumber.ToString("D3") + ".png";
        }
    }
}