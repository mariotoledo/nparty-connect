using CampeonatosNParty.Models.Database;
using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampeonatosNParty.Models.ViewModel
{
    public class BlackFridayModel
    {
        public EixoX.Data.ClassSelectResult<CampeonatosNParty.Models.Database.BlackFridayAnuncioDetalhes> result { get; set; }
        public Dictionary<long, BlackFridayAnuncioVoto> votos { get; set; }

        public BlackFridayModel(long userId, string currentPage, string currentFilter)
        {
            try
            {
                int page = 0;
                int.TryParse(currentPage, out page);

                int filter = 0;
                int.TryParse(currentFilter, out filter);

                ClassSelect<BlackFridayAnuncioDetalhes> classSelect = BlackFridayAnuncioDetalhes.Select().Where("FoiAprovado", true);

                if (filter == 0)
                    classSelect.OrderBy("DataCriacao", SortDirection.Descending);
                if (filter == 1)
                    classSelect.OrderBy("DataCriacao", SortDirection.Ascending);
                if (filter == 2)
                    classSelect.OrderBy("ProporcaoPontos", SortDirection.Descending);
                if (filter == 3)
                    classSelect.OrderBy("ProporcaoPontos", SortDirection.Ascending);

                result = classSelect.Page(15, page).ToResult();

                votos = BlackFridayAnuncioVoto.Select().Where("IdUsuario", userId).GroupBy(t => t.IdBlackFridayAnuncio).ToDictionary(f => f.Key, c => c.Single());
            }
            catch (Exception e)
            {

            }
        }
    }
}