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
    public class Inscricao : NPartyDbModel<Inscricao>
    {
        [DatabaseColumn(DatabaseColumnKind.Identity)]
        public int Id { get; set; }

        [DatabaseColumn(DatabaseColumnKind.PrimaryKey)]
        [Required]
        public int IdCampeonato { get; set; }

        [DatabaseColumn(DatabaseColumnKind.PrimaryKey)]
        [Required]
        public int IdUsuario { get; set; }

        [DatabaseColumn]
        public bool IsPago { get; set; }
    }
}