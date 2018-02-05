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
    public class UserConnections : NPartyDbModel<UserConnections>
    {
        [DatabaseColumn]
        public string PersonName1 { get; set; }

        [DatabaseColumn]
        public int PersonId1 { get; set; }

        [DatabaseColumn]
        public string PersonName2 { get; set; }

        [DatabaseColumn]
        public int PersonId2 { get; set; }

        [DatabaseColumn]
        public bool isPSN { get; set; }

        [DatabaseColumn]
        public bool isLive { get; set; }

        [DatabaseColumn]
        public bool isMiiverse { get; set; }

        [DatabaseColumn]
        public bool isFriendCode { get; set; }

        [DatabaseColumn]
        public string PsnId1 { get; set; }

        [DatabaseColumn]
        public string LiveId1 { get; set; }

        [DatabaseColumn]
        public string MiiverseId1 { get; set; }

        [DatabaseColumn]
        public string FriendCode1 { get; set; }

        [DatabaseColumn]
        public string PsnId2 { get; set; }

        [DatabaseColumn]
        public string LiveId2 { get; set; }

        [DatabaseColumn]
        public string MiiverseId2 { get; set; }

        [DatabaseColumn]
        public string FriendCode2 { get; set; }

        [DatabaseColumn]
        public string UrlFotoPerfil1 { get; set; }

        [DatabaseColumn]
        public string UrlFotoPerfil2 { get; set; }

        [DatabaseColumn]
        public bool Person1DidAdd { get; set; }

        [DatabaseColumn]
        public bool Person2DidAdd { get; set; }

        public string getUrlFotoPerfil1()
        {
            if (String.IsNullOrEmpty(this.UrlFotoPerfil1))
                return "/Static/img/playerPhoto/default.jpg";
            return this.UrlFotoPerfil1;
        }

        public string getUrlFotoPerfil2()
        {
            if (String.IsNullOrEmpty(this.UrlFotoPerfil2))
                return "/Static/img/playerPhoto/default.jpg";
            return this.UrlFotoPerfil2;
        }
    }
}