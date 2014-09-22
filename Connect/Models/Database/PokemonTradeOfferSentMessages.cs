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
    public class PokemonTradeOfferSentMessages : NPartyDbModel<PokemonTradeOfferSentMessages>
    {
        [DatabaseColumn]
        public int TradeOfferId { get; set; }

        [DatabaseColumn]
        public int PersonId { get; set; }
    }
}