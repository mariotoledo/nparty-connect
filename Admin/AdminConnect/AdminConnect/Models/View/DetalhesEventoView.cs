using AdminConnect.Models.Database;
using CampeonatosNParty.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminConnect.Models.View
{
    public class DetalhesEventoView
    {
        public DetalhesEvento detalhesEvento { get; set; }
        public List<CampeonatoPorEvento> campeonatos { get; set; }

        public DetalhesEventoView(int eventoId)
        {
            detalhesEvento = NPartyDb<DetalhesEvento>.Instance.Select().Where("Id", eventoId).SingleResult();
            campeonatos = NPartyDb<CampeonatoPorEvento>.Instance.Select().Where("IdEvento", eventoId).ToList();
        }
    }
}