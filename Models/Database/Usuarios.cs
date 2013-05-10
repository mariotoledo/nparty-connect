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
    public class Usuarios : NPartyDbModel<Usuarios>
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
        public DateTime Nascimento;

        [DatabaseColumn]
        public int Id_Cidade { get; set; }

        [DatabaseColumn]
        public int Id_Estado { get; set; }

        [DatabaseColumn]
        [UISingleline]
        public string Telefone { get; set; }

        [DatabaseColumn]
        public DateTime Data_Cadastro { get; set; }

        [DatabaseColumn]
        public int Nivel_Permissao { get; set; }

        [DatabaseColumn]
        [MaxLength(255)]
        [UIPassword]
        [Required]
        public string Senha { get; set; }

        [DatabaseColumn]
        public int IdOriginal { get; set; }

        [DatabaseColumn]
        [UISingleline]
        [MaxLength(255)]
        public string UrlFotoPerfil { get; set; }

        [DatabaseColumn]
        [UISingleline]
        [MaxLength(50)]
        public string PsnId { get; set; }

        [DatabaseColumn]
        [UISingleline]
        [MaxLength(50)]
        public string LiveId { get; set; }

        [DatabaseColumn]
        [UICheckbox]
        public bool Newsletter { get; set; }
    }
}