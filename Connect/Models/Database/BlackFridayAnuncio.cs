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
    public class BlackFridayAnuncio : NPartyDbModel<BlackFridayAnuncio>
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
    }
}