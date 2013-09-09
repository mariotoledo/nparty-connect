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
        public Eventos evento { get; set; }
        public Campeonatos campeonato { get; set; }
        public IEnumerable<InscricaoUsuario[]> jogadoresInscritos { get; set; }

        public CampeonatosDetailView(int campeonatoId)
        {
            campeonato = Campeonatos.WithIdentity(campeonatoId);
            jogo = Jogos.WithIdentity(campeonato.IdJogo);
            evento = Eventos.WithIdentity(campeonato.IdEvento);

            jogadoresInscritos = InscricaoUsuario.Select().Where("IdCampeonato", campeonato.Id).ToList().Segment(6);
        }
    }
}