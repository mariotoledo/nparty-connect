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
    public class MinhasInformacoes
    {
        [DatabaseColumn]
        [MaxLength(50)]
        [UISingleline]
        public string Apelido { get; set; }

        [DatabaseColumn]
        public int Id_Cidade { get; set; }

        [DatabaseColumn]
        public int Id_Estado { get; set; }

        [DatabaseColumn]
        [UISingleline]
        [DigitsOnly]
        [MaxLength(14)]
        public string Telefone { get; set; }

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
        [UISingleline]
        [MaxLength(50)]
        public string MiiverseId { get; set; }

        [DatabaseColumn]
        [UISingleline]
        [MaxLength(14)]
        [MinLength(14)]
        public string FriendCode { get; set; }

        [DatabaseColumn]
        [UICheckbox]
        public bool Newsletter { get; set; }

        public bool hasPokemonFriendSafari { get; set; }
    }
}