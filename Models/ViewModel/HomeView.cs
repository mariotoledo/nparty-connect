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

        public List<Eventos> eventos
        {
            get
            {
                return NPartyDb<Eventos>.Instance.Select().OrderBy("DataEventoInicio", EixoX.Data.SortDirection.Descending).Take(5).ToList();
            }
        }

        public List<CampeonatoPorEvento> campeonatos
        {
            get
            {
                return NPartyDb<CampeonatoPorEvento>.Instance.Select().ToList();
            }
        }

        public EixoX.Data.ClassSelect<JogosPorConsole> jogosPorConsole
        {
            get 
            {
                return NPartyDb<JogosPorConsole>.Instance.Select();
            }
        }
    }
}