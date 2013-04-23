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

        public EventosDetailView(int eventoId)
        {
            evento = Eventos.WithIdentity(eventoId);
            if (evento == null)
                evento = new Eventos();
        }
    }
}