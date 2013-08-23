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
    public class EditarInformacoes
    {
        [DatabaseColumn(DatabaseColumnKind.Identity)]
        [UIHidden]
        public int Id { get; set; }

        [DatabaseColumn]
        [MaxLength(50)]
        [Required]
        [UISingleline]
        public string Nome { get; set; }

        [DatabaseColumn]
        [MaxLength(50)]
        [UISingleline]
        public string Apelido { get; set; }

        [DatabaseColumn]
        [MaxLength(50)]
        [Required]
        [UISingleline]
        public string Email { get; set; }

        [DatabaseColumn]
        [UISingleline]
        [Required]
        public DateTime Nascimento {get; set;}

        [DatabaseColumn]
        public int Id_Cidade { get; set; }

        [DatabaseColumn]
        public int Id_Estado { get; set; }

        [DatabaseColumn]
        [UISingleline]
        public string Telefone { get; set; }
    }
}