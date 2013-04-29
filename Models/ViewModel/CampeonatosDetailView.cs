using CampeonatosNParty.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampeonatosNParty.Models.ViewModel
{
    public class CampeonatosDetailView
    {
        public CampeonatoPorEvento campeonato { get; set; }
        public List<ClassificacaoCampeonato> classificacao { get; set; }

        public CampeonatosDetailView(int campeonatoId)
        {
            campeonato = CampeonatoPorEvento.WithMember("IdCampeonato", campeonatoId);
            if (campeonato == null)
            {
                campeonato = new CampeonatoPorEvento();
                classificacao = new List<ClassificacaoCampeonato>();
            }
            else
            {
                classificacao = ClassificacaoCampeonato.Select().Where("IdCampeonato", campeonato.IdCampeonato).OrderBy("Pontuacao", EixoX.Data.SortDirection.Descending).ToList();
            }
        }
    }
}