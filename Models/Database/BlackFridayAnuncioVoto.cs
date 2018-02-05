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
    public class BlackFridayAnuncioVoto : NPartyDbModel<BlackFridayAnuncioVoto>
    {
        [DatabaseColumn(DatabaseColumnKind.PrimaryKey)]
        public long IdBlackFridayAnuncio { get; set; }

        [DatabaseColumn(DatabaseColumnKind.PrimaryKey)]
        public long IdUsuario { get; set; }

        [DatabaseColumn]
        public int Pontuacao { get; set; }
    }
}