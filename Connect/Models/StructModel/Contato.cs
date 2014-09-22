using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EixoX.Restrictions;
using EixoX.Data;
using EixoX.Interceptors;
using EixoX.UI;

namespace CampeonatosNParty.Models.StructModel
{
    public class Contato
    {
        [Required]
        [Email]
        [UISingleline]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [UISingleline]
        [MaxLength(50)]
        public string Nome { get; set; }

        public string Mensagem { get; set; }

        public bool Enviado { get; set; }
    }
}