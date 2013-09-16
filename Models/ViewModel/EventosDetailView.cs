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
        public IEnumerable<CampeonatoPorEvento[]> campeonatos { get; set; }
        public Cidade cidade { get; set; }
        public Estado estado { get; set; }
        public TipoEvento tipoEvento { get; set; }

        public EventosDetailView(int eventoId)
        {
            evento = Eventos.WithIdentity(eventoId);
            campeonatos = CampeonatoPorEvento.Select().Where("IdEvento", eventoId).Segment<CampeonatoPorEvento>(4);
            cidade = Cidade.WithIdentity(eventoId);
            estado = Estado.WithIdentity(eventoId);
            if (evento != null)
                tipoEvento = TipoEvento.WithIdentity(evento.TipoEvento);
            else
                tipoEvento = TipoEvento.WithIdentity(0);
        }
    }
}