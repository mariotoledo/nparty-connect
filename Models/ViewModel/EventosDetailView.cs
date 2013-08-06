using CampeonatosNParty.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampeonatosNParty.Models.ViewModel
{
    public class EventosDetailView
    {
        public Eventos evento { get; set; }
        public List<List<DetalhesCampeonatoPorEvento>> detalheCampeonatos { get; set; }

        public EventosDetailView(int eventoId)
        {
            evento = Eventos.WithIdentity(eventoId);
            if (evento == null)
                evento = new Eventos();

            int lastGameId = 0;

            detalheCampeonatos = new List<List<DetalhesCampeonatoPorEvento>>();

            foreach(DetalhesCampeonatoPorEvento detalheCampeonato in DetalhesCampeonatoPorEvento.Select().Where("IdEvento", evento.Id).OrderBy("DataCampeonato", EixoX.Data.SortDirection.Descending))
            {
                if (lastGameId != detalheCampeonato.IdJogo)
                {
                    detalheCampeonatos.Add(new List<DetalhesCampeonatoPorEvento>());
                    lastGameId = detalheCampeonato.IdJogo;
                }

                detalheCampeonatos.Last().Add(detalheCampeonato);
            }
        }
    }
}