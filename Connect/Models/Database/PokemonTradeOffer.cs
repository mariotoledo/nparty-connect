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
    public class PokemonTradeOffer : NPartyDbModel<PokemonTradeOffer>
    {
        [DatabaseColumn(DatabaseColumnKind.Identity)]
        public int Id { get; set; }

        [DatabaseColumn]
        [Required]
        public int PersonId { get; set; }

        [DatabaseColumn]
        [Required]
        public int PokemonId { get; set; }

        [DatabaseColumn]
        public int PokemonLevel { get; set; }

        [DatabaseColumn]
        public int PokemonNatureId { get; set; }

        [DatabaseColumn]
        [MaxLength(250)]
        public string PersonMessage { get; set; }

        [DatabaseColumn]
        public bool HasIvHp { get; set; }

        [DatabaseColumn]
        public bool HasIvAttack { get; set; }

        [DatabaseColumn]
        public bool HasIvDefense { get; set; }

        [DatabaseColumn]
        public bool HasIvSpAttack { get; set; }

        [DatabaseColumn]
        public bool HasIvSpDefense { get; set; }

        [DatabaseColumn]
        public bool HasIvSpeed { get; set; }

        [DatabaseColumn]
        [MinInclusive(0)]
        [MaxInclusive(252)]
        public int HpEv { get; set; }

        [DatabaseColumn]
        [MinInclusive(0)]
        [MaxInclusive(252)]
        public int AttackEv { get; set; }

        [DatabaseColumn]
        [MinInclusive(0)]
        [MaxInclusive(252)]
        public int DefenseEv { get; set; }

        [DatabaseColumn]
        [MinInclusive(0)]
        [MaxInclusive(252)]
        public int SpAttackEv { get; set; }

        [DatabaseColumn]
        [MinInclusive(0)]
        [MaxInclusive(252)]
        public int SpDefenseEv { get; set; }

        [DatabaseColumn]
        [MinInclusive(0)]
        [MaxInclusive(252)]
        public int SpeedEv { get; set; }

        [DatabaseColumn]
        public bool IsEgg { get; set; }

        [DatabaseColumn]
        public string ShinyValue { get; set; }

        [DatabaseColumn]
        public bool IsShiny { get; set; }
    }
}