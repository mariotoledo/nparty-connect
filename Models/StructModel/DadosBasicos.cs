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
    public class DadosBasicos
    {
        [DatabaseColumn]
        [MaxLength(50)]
        [UISingleline]
        public string Apelido { get; set; }

        [DatabaseColumn]
        [DigitsOnly]
        public int Id_Cidade { get; set; }

        [DatabaseColumn]
        [DigitsOnly]
        public int Id_Estado { get; set; }

        [DatabaseColumn]
        [DigitsOnly]
        public int BirthdayDay { get; set; }

        [DatabaseColumn]
        [DigitsOnly]
        public int BirthdayMonth { get; set; }

        [DatabaseColumn]
        [DigitsOnly]
        public int BirthdayYear { get; set; }

        [DatabaseColumn]
        [UISingleline]
        [DigitsOnly]
        [MaxLength(14)]
        public string Telefone { get; set; }

        [DatabaseColumn]
        [UICheckbox("Desejo receber novidades e anúncios dos eventos da N-Party em meu e-mail")]
        public bool Newsletter { get; set; }
    }
}