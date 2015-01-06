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
            public AdminConnect.Models.Database.DetalhesCampeonato detalhesCampeonato;
            public IEnumerable<ClassificacaoCampeonato[]> classificacao { get; set; }
            public long numeroParticipantes { get; set; }

            public DetalhesCampeonato(int campeonatoId)
            {
                detalhesCampeonato = AdminConnect.Models.Database.DetalhesCampeonato.Select().Where("IdCampeonato", campeonatoId).SingleResult();

                ClassSelect<ClassificacaoCampeonato> classificacaoCampeonato = ClassificacaoCampeonato.Select().Where("IdCampeonato", campeonatoId);
                numeroParticipantes = classificacaoCampeonato.Count();

                classificacao = classificacaoCampeonato
                                .OrderBy("Classificacao", EixoX.Data.SortDirection.Ascending).Segment(4);
            }
    }
}