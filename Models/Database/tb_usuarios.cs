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
    public class tb_usuarios : NPartyDbModel<tb_usuarios>
    {
        [DatabaseColumn(DatabaseColumnKind.Identity)]
        [UIHidden]
        public int Id_Usuario { get; set; }

        [DatabaseColumn]
        [Required]
        [UISingleline]
        public string NM_Usuario { get; set; }

        [DatabaseColumn]
        [MaxLength(255)]
        [UISingleline]
        public string NM_Apelido { get; set; }

        [DatabaseColumn]
        public string Dt_Nascimento { get; set; }

        [DatabaseColumn]
        [Required]
        public string NM_Email { get; set; }

        [DatabaseColumn]
        [Required]
        public string NM_Cidade { get; set; }

        [DatabaseColumn]
        [Required]
        public string NM_Estado { get; set; }

        [DatabaseColumn]
        public string NR_DDD { get; set; }

        [DatabaseColumn]
        public string NR_Telefone { get; set; }

        [DatabaseColumn]
        public DateTime Dt_Cadastro { get; set; }

        [DatabaseColumn]
        public DateTime Hr_Cadastro { get; set; }

        [DatabaseColumn]
        public string NM_Permissao { get; set; }

        [DatabaseColumn]
        public string Senha { get; set; }
    }
}