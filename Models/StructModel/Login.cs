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
    public class Login
    {
        [Email]
        [Required]
        [UISingleline]
        [MaxLength(200)]
        public string Email { get; set; }

        [Required]
        [UIPassword]
        [MaxLength(50)]
        public string Senha { get; set; }
    }
}