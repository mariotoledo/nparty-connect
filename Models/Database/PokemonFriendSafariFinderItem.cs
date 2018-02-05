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
    public class PokemonFriendSafariFinderItem : NPartyDbModel<PokemonFriendSafariFinderItem>
    {
        [DatabaseColumn]
        [Required]
        public int PersonId { get; set; }

        [DatabaseColumn]
        [Required]
        public string PersonName { get; set; }

        [DatabaseColumn]
        [Required]
        public int SafariTypeId { get; set; }

        [DatabaseColumn]
        [Required]
        public string SafariTypeName { get; set; }

        [DatabaseColumn]
        [Required]
        public int Pokemon1DexNumber { get; set; }

        [DatabaseColumn]
        [Required]
        public string Pokemon1Name { get; set; }

        [DatabaseColumn]
        [Required]
        public int Pokemon2DexNumber { get; set; }

        [DatabaseColumn]
        [Required]
        public string Pokemon2Name { get; set; }

        [DatabaseColumn]
        [Required]
        public int Pokemon3DexNumber { get; set; }

        [DatabaseColumn]
        [Required]
        public string Pokemon3Name { get; set; }

        [DatabaseColumn]
        [Required]
        public string FriendCode { get; set; }

        public string getImageUrl(int pokemonNumber)
        {
            return "http://www.serebii.net/xy/pokemon/" + pokemonNumber.ToString("D3") + ".png";
        }
    }
}