using CampeonatosNParty.Models.Database;
using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CampeonatosNParty.Controllers
{
    public class WidgetsController : AuthenticationBasedController
    {
        //
        // GET: /Widget/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PokemonFriendSafariFinder()
        {
            ClassSelectResult<PokemonFriendSafariFinderItem> result;

            int page = 0;
            int.TryParse(Request.QueryString["page"], out page);

            int pokemonType = Request.QueryString["Type"] == null ? 0 : Int32.Parse(Request.QueryString["Type"]);
            int pokemonId = Request.QueryString["Pokemon"] == null ? 0 : Int32.Parse(Request.QueryString["Pokemon"]);

            ClassSelect<PokemonFriendSafariFinderItem> search = null;

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

            return View(result);
        }
    }
}
