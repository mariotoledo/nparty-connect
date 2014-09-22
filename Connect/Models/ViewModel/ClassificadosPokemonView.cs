using CampeonatosNParty.Models.Database;
using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampeonatosNParty.Models.ViewModel
{
    public class ClassificadosPokemonView
    {
        public ClassSelectResult<PokemonTradeOfferItem> result;
        public List<PokemonTradeOfferItem> myOffers;
        public bool isLoggedIn;

        public ClassificadosPokemonView(Usuarios currentUsuario, string pageString, int pokemonFilter, int natureFilter)
        {
            int page = 0;
            int.TryParse(pageString, out page);

            if(currentUsuario != null)
                myOffers = PokemonTradeOfferItem.Select().Where("PersonId", currentUsuario.Id).ToList();

            ClassSelect<PokemonTradeOfferItem> search = null;

            if (pokemonFilter > 0)
            {
                search = PokemonTradeOfferItem.Select().Where("PokemonId", pokemonFilter);

                if (natureFilter > 0)
                {
                    search = search.And("PokemonNatureId", natureFilter);
                }
            }
            else
            {
                if (natureFilter > 0)
                {
                    search = PokemonTradeOfferItem.Select().Where("PokemonNatureId", natureFilter);
                }
            }

            if (search == null)
            {
                search = PokemonTradeOfferItem.Select();
            }

            search.Page(18, page);
            result = search.ToResult();

            if (currentUsuario == null)
            {
                isLoggedIn = false;
            }
            else
            {
                isLoggedIn = true;
            }
        }
    }
}