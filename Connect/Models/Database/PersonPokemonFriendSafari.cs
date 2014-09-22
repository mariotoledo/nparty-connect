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
    public class PersonPokemonFriendSafari : NPartyDbModel<PersonPokemonFriendSafari>
    {
        [Required]
        [DatabaseColumn(DatabaseColumnKind.PrimaryKey)]
        public int PersonId { get; set; }

        [DatabaseColumn]
        [Required]
        public int PokemonTypeId { get; set; }

        [DatabaseColumn]
        [Required]
        public int PokemonSlot1Id { get; set; }

        [DatabaseColumn]
        [Required]
        public int PokemonSlot2Id { get; set; }

        [DatabaseColumn]
        [Required]
        public int PokemonSlot3Id { get; set; }
    }
}