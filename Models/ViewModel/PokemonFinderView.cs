using CampeonatosNParty.Models.Database;
using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampeonatosNParty.Models.ViewModel
{
    public class PokemonFinderView
    {
        public ClassSelectResult<PokemonFriendSafariFinderItem> result;
        public bool isLoggedIn;
        public Dictionary<int, bool> isButtonActive;

        public PokemonFinderView(Usuarios currentUsuario, string pageString, int pokemonType, int pokemonId)
        {
            int page = 0;
            int.TryParse(pageString, out page);

            ClassSelect<PokemonFriendSafariFinderItem> search = null;
            isButtonActive = new Dictionary<int, bool>();

            if (pokemonType > 0)
            {
                search = PokemonFriendSafariFinderItem.Select().Where("SafariTypeId", pokemonType);

                if (search != null && pokemonId > 0)
                {
                    search = search.And("Pokemon1DexNumber", pokemonId)
                             .Or("Pokemon2DexNumber", pokemonId)
                             .Or("Pokemon3DexNumber", pokemonId);
                }
            }
            else
            {
                if (pokemonId > 0)
                {
                    search = PokemonFriendSafariFinderItem.Select().Where("Pokemon1DexNumber", pokemonId)
                    .Or("Pokemon2DexNumber", pokemonId)
                    .Or("Pokemon3DexNumber", pokemonId);
                }
            }

            if (search == null)
            {
                search = PokemonFriendSafariFinderItem.Select();
            }


            search.Page(18, page);
            search.OrderBy("PersonName", SortDirection.Ascending);

            result = search.ToResult();

            if (currentUsuario == null)
            {
                isLoggedIn = false;
            }
            else
            {
                isLoggedIn = true;
                foreach (PokemonFriendSafariFinderItem safari in result)
                {
                    PersonGamingRelation relation = 
                        PersonGamingRelation.getPersonGamingRelationsFromId(currentUsuario.Id, safari.PersonId);

                    if(relation == null)
                        isButtonActive.Add(safari.PersonId, true);
                    else
                        isButtonActive.Add(safari.PersonId, !relation.isFriendCode);
                }
            }
        }
    }
}