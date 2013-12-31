using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EixoX.Restrictions;
using EixoX.Data;
using EixoX.Interceptors;
using EixoX.UI;
using CampeonatosNParty.Models.Database;

namespace CampeonatosNParty.Models.StructModel
{
    public class DadosNintendisticos
    {
        [DatabaseColumn]
        public string UserId { get; set; }

        [DatabaseColumn]
        [UISingleline]
        [MaxLength(50)]
        public string MiiverseId { get; set; }

        [DatabaseColumn]
        [UISingleline]
        [MaxLength(14)]
        [MinLength(14)]
        public string FriendCode { get; set; }

        public List<PersonGame> Jogos { get; set; }

        public PersonPokemonFriendSafari friendSafari { get; set; }
    }
}