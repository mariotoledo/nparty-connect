using CampeonatosNParty.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampeonatosNParty.Models.ViewModel
{
    public class CampeonatosDetailView
    {
        public Jogos jogo { get; set; }
        public List<List<DetalhesCampeonatoPorEvento>> detalheCampeonatos { get; set; }

        public CampeonatosDetailView(int jogoId)
        {
            jogo = Jogos.WithIdentity(jogoId);

            if (jogo == null)
                jogo = new Jogos();

            int lastEventId = 0;

            detalheCampeonatos = new List<List<DetalhesCampeonatoPorEvento>>();

            foreach (DetalhesCampeonatoPorEvento detalheCampeonato in DetalhesCampeonatoPorEvento.Select().Where("IdJogo", jogo.Id).OrderBy("DataCampeonato", EixoX.Data.SortDirection.Descending))
            {
                if (lastEventId != detalheCampeonato.IdEvento)
                {
                    detalheCampeonatos.Add(new List<DetalhesCampeonatoPorEvento>());
                    lastEventId = detalheCampeonato.IdEvento;
                }

                detalheCampeonatos.Last().Add(detalheCampeonato);
            }
        }
    }
}