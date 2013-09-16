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
        public Consoles console { get; set; }
        public Eventos evento { get; set; }
        public Campeonatos campeonato { get; set; }
        public IEnumerable<ClassificacaoCampeonato[]> classificacao { get; set; }

        public CampeonatosDetailView(int campeonatoId)
        {
            campeonato = Campeonatos.WithIdentity(campeonatoId);
            if (campeonato != null)
            {
                jogo = Jogos.WithIdentity(campeonato.IdJogo);
                console = Consoles.WithIdentity(jogo.IdConsole);
                evento = Eventos.WithIdentity(campeonato.IdEvento);
            }

            classificacao = ClassificacaoCampeonato.Select()
                            .Where("IdCampeonato", campeonatoId)
                            .OrderBy("Pontuacao", EixoX.Data.SortDirection.Descending).Segment(6);
        }
    }
}