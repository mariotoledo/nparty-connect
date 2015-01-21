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
            public IEnumerable<InscricaoUsuario[]> inscricoes { get; set; }
            public long numeroParticipantes { get; set; }

            public DetalhesCampeonato(int campeonatoId)
            {
                detalhesCampeonato = AdminConnect.Models.Database.DetalhesCampeonato.Select().Where("IdCampeonato", campeonatoId).SingleResult();

                if (detalhesCampeonato.IdStatus == 3)
                {
                    ClassSelect<ClassificacaoCampeonato> classificacaoCampeonato = ClassificacaoCampeonato.Select().Where("IdCampeonato", campeonatoId).OrderBy("Pontuacao", SortDirection.Descending);
                    numeroParticipantes = classificacaoCampeonato.Count();

                    classificacao = classificacaoCampeonato.Segment(4);
                }
                else
                {
                    ClassSelect<InscricaoUsuario> inscricoesCampeonato = InscricaoUsuario.Select().Where("IdCampeonato", campeonatoId).OrderBy("NomeUsuario");
                    numeroParticipantes = inscricoesCampeonato.Count();

                    inscricoes = inscricoesCampeonato
                                    .OrderBy("NomeUsuario", EixoX.Data.SortDirection.Ascending).Segment(4);
                }
            }
    }
}