using EixoX.Data;
using EixoX.Restrictions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampeonatosNParty.Models.Database
{
    [DatabaseTable]
    public class SorteioUsuario
    {
        [DatabaseColumn(DatabaseColumnKind.Identity)]
        public int Id { get; set; }

        [DatabaseColumn]
        [MinLength(3)]
        [MaxLength(3)]
        [Required]
        public string Nome { get; set; }

        [DatabaseColumn]
        [MinLength(3)]
        [MaxLength(3)]
        [Required]
        public string Sobrenome { get; set; }

        [DatabaseColumn]
        [Email]
        [Required]
        public string Email { get; set; }

        [DatabaseColumn]
        [Required]
        public long IdEstado { get; set; }

        [DatabaseColumn]
        [Required]
        public long IdCidade { get; set; }

        [DatabaseColumn]
        public bool FoiGanhador { get; set; }

        [DatabaseColumn]
        public DateTime DataRegistro { get; set; }

        [DatabaseColumn]
        public bool EmailConfirmado { get; set; }

        public string getConfirmationUrl()
        {
            return "http://connect.nparty.com.br/Sorteio/DemoSmash?confirmationKey=" + CampeonatosNParty.Helpers.EncryptHelper.Encrypt(this.Id);
        }
    }
}