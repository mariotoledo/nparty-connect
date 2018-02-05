using CampeonatosNParty.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampeonatosNParty.Models.ViewModel
{
    public class HomeView
    {
        public Eventos lastEvent { get; private set; }
        public Cidade lastEventCidade { get; set; }
        public Estado lastEventEstado { get; set; }
        public TipoEvento lastEventType { get; set; }
        public List<Ranking> ranking { get; set; }

        public HomeView()
        {
            lastEvent = Eventos.Select().OrderBy("DataEventoInicio", EixoX.Data.SortDirection.Descending).First();
            if (lastEvent != null)
            {
                lastEventCidade = Cidade.WithIdentity(lastEvent.IdCidade);
                lastEventEstado = Estado.WithIdentity(lastEvent.IdEstado);
                lastEventType = TipoEvento.WithIdentity(lastEvent.TipoEvento);
            }
            ranking = Ranking.Select().OrderBy("Pontos", EixoX.Data.SortDirection.Descending).Where("Ano", DateTime.Now.Year).Take(6).ToList();
        }
    }
}