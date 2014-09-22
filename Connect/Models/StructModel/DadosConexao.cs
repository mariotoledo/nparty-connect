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
    public class DadosConexao
    {
        [DatabaseColumn]
        public string UserId { get; set; }

        public string FacebookId { get; set; }

        [DatabaseColumn]
        [MaxLength(16)]
        public string PSNId { get; set; }

        [DatabaseColumn]
        [MaxLength(16)]
        public string LiveId { get; set; }

        [DatabaseColumn]
        [MaxLength(16)]
        public string SteamId { get; set; }
    }
}