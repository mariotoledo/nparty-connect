using AdminConnect.Models.Database;
using CampeonatosNParty.Models.Database;
using EixoX.Data;
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
            public long numeroParticipantes { get; set; }
            public StatusCampeonato statusCampeonato;
            public Organizador organizador { get; set; }

            public DetalhesCampeonato(int campeonatoId)
            {
                campeonato = Campeonatos.WithIdentity(campeonatoId);
                if (campeonato != null)
                {
                    jogo = Jogos.WithIdentity(campeonato.IdJogo);
                    console = Consoles.WithIdentity(jogo.IdConsole);
                    evento = Eventos.WithIdentity(campeonato.IdEvento);
                }

                ClassSelect<ClassificacaoCampeonato> classificacaoCampeonato = ClassificacaoCampeonato.Select().Where("IdCampeonato", campeonatoId);
                numeroParticipantes = classificacaoCampeonato.Count();

                classificacao = classificacaoCampeonato
                                .OrderBy("Classificacao", EixoX.Data.SortDirection.Ascending).Segment(4);

                statusCampeonato = StatusCampeonato.WithIdentity(campeonato.IdStatus);

                organizador = Organizador.Select().Where("Id", campeonato.IdOrganizador).SingleResult();
            }
    }
}