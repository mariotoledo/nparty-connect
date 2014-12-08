using CampeonatosNParty.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminConnect.Models.View
{
    public class DetalhesCampeonato
    {
            public Jogos jogo { get; set; }
            public Consoles console { get; set; }
            public Eventos evento { get; set; }
            public Campeonatos campeonato { get; set; }
            public IEnumerable<ClassificacaoCampeonato[]> classificacao { get; set; }

            public DetalhesCampeonato(int campeonatoId)
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
                                .OrderBy("Classificacao", EixoX.Data.SortDirection.Ascending).Segment(6);
            }
    }
}