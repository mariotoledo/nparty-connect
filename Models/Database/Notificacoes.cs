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
    public class Notificacoes : NPartyDbModel<Notificacoes>
    {
        [DatabaseColumn(DatabaseColumnKind.Identity)]
        public int Id { get; set; }

        [DatabaseColumn]
        [Required]
        public int PersonId { get; set; }

        [DatabaseColumn]
        [Required]
        [MaxLength(255)]
        public string Titulo { get; set; }

        [DatabaseColumn]
        [Required]
        [MaxLength(4000)]
        public string Corpo { get; set; }

        [DatabaseColumn]
        public DateTime DateCreated { get; set; }

        [DatabaseColumn]
        public DateTime DateSent { get; set; }
    }
}