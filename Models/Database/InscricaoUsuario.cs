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
    public class InscricaoUsuario : NPartyDbModel<InscricaoUsuario>
    {
        [DatabaseColumn]
        public int IdUsuario { get; set; }

        [DatabaseColumn]
        public int IdCampeonato { get; set; }

        [DatabaseColumn]
        public string NomeUsuario { get; set; }

        [DatabaseColumn]
        public string ApelidoUsuario { get; set; }

        [DatabaseColumn]
        public int NumeroClassificacao { get; set; }

        [DatabaseColumn]
        public int IsPago { get; set; }

        [DatabaseColumn]
        public string UrlFotoPerfil { get; set; }

        public string getClassificacao()
        {
            switch (NumeroClassificacao)
            {
                case 1:
                    return "Primeiro lugar";
                case 2:
                    return "Segundo lugar";
                case 3:
                    return "Terceiro lugar";
                default:
                    return "Não classificado";
            }
        }
    }
}