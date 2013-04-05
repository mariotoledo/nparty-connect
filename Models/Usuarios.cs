using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EixoX.Restrictions;
using EixoX.Data;
using EixoX.Interceptors;
using EixoX.UI;

namespace CampeonatosNParty.Models
{
    [DatabaseTable]
    public class Usuarios : NPartyDbModel<Usuarios>
    {
        [DatabaseColumn(DatabaseColumnKind.Identity)]
        [UIHidden]
        public int Id { get; set; }

        [DatabaseColumn]
        public string Nome { get; set; }

        [DatabaseColumn]
        [Required]
        [MaxLength(255)]
        [UISingleline]
        public string Apelido { get; set; }
    }
}