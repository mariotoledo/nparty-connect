using CampeonatosNParty.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampeonatosNParty.Models.ViewModel
{
    public class HomeView
    {
        public List<Ranking> ranking{
            get{
                return NPartyDb<Ranking>.Instance.Select().OrderBy("Pontos", EixoX.Data.SortDirection.Descending).Take(5).ToList();
            }
        }

        public List<tb_eventos> eventos
        {
            get
            {
                return NPartyDb<tb_eventos>.Instance.Select().Take(5).ToList();
            }
        }

        public List<tb_campeonatos> campeonatos{
            get
            {
                return NPartyDb<tb_campeonatos>.Instance.Select().Take(5).ToList();
            }
        }
    }
}