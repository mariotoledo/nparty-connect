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
        public long TotalPositivo { get; set; }

        [DatabaseColumn]
        public long TotalNegativo { get; set; }

        [DatabaseColumn]
        public long ProporcaoPontos { get; set; }

        public double PorcentagemPositiva()
        {
            if (TotalNegativo + TotalPositivo == 0)
                return 0;
            return (TotalPositivo * 100) / (TotalNegativo + TotalPositivo);
        }

        public double PorcentagemNegativa()
        {
            if (TotalNegativo + TotalPositivo == 0)
                return 0;
            return (TotalNegativo * 100) / (TotalNegativo + TotalPositivo);
        }

        public string getEncryptedId()
        {
            return CampeonatosNParty.Helpers.EncryptHelper.Encrypt((int)Id);
        }
    }
}