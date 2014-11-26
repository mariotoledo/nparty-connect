using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EixoX.Restrictions;
using EixoX.Data;
using EixoX.Interceptors;
using EixoX.UI;

namespace CampeonatosNParty.Models.Database
{
    [DatabaseTable]
    public class BlackFridayAnuncioDetalhes : NPartyDbModel<BlackFridayAnuncioDetalhes>
    {
        [DatabaseColumn(DatabaseColumnKind.Identity)]
        public long Id { get; set; }

        [DatabaseColumn]
        public long IdUsuario { get; set; }

        [DatabaseColumn]
        public string NomeAnuncio { get; set; }

        [DatabaseColumn]
        public string UrlAnuncio { get; set; }

        [DatabaseColumn]
        public decimal Valor { get; set; }

        [DatabaseColumn]
        public string Comentarios { get; set; }

        [DatabaseColumn]
        public bool FoiAprovado { get; set; }

        [DatabaseColumn]
        public DateTime DataCriacao { get; set; }

        [DatabaseColumn]
        public string Nome { get; set; }

        [DatabaseColumn]
        public string Sobrenome { get; set; }

        [DatabaseColumn]
        public long SomaPositivo { get; set; }

        [DatabaseColumn]
        public long TotalPositivo { get; set; }

        [DatabaseColumn]
        public long SomaNegativo { get; set; }

        [DatabaseColumn]
        public long TotalNegativo { get; set; }

        public double PorcentagemPositiva()
        {
            if (TotalNegativo + TotalPositivo == 0)
                return 50;
            return (TotalNegativo / (TotalNegativo + TotalPositivo)) * 100;
        }

        public double PorcentagemNegativa()
        {
            if (TotalNegativo + TotalPositivo == 0)
                return 50;
            return (TotalNegativo / (TotalNegativo + TotalPositivo)) + 100;
        }
    }
}