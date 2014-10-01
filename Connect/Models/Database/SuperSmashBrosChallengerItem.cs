using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampeonatosNParty.Models.Database
{
    [DatabaseTable]
    public class SuperSmashBrosChallengerItem : NPartyDbModel<SuperSmashBrosChallengerItem>
    {
        [DatabaseColumn]
        public long IdUsuario { get; set; }

        [DatabaseColumn]
        public long IdPersonagem1 { get; set; }

        [DatabaseColumn]
        public long IdCorPersonagem1 { get; set; }

        [DatabaseColumn]
        public string NomePersonagem1 { get; set; }

        [DatabaseColumn]
        public long IdPersonagem2 { get; set; }

        [DatabaseColumn]
        public long IdCorPersonagem2 { get; set; }

        [DatabaseColumn]
        public string NomePersonagem2 { get; set; }

        [DatabaseColumn]
        public long IdPersonagem3 { get; set; }

        [DatabaseColumn]
        public long IdCorPersonagem3 { get; set; }

        [DatabaseColumn]
        public string NomePersonagem3 { get; set; }

        [DatabaseColumn]
        public string NomeUsuario { get; set; }

        [DatabaseColumn]
        public string FriendCode { get; set; }

        public string ImagemPersonagem1
        {
            get
            {
                return CampeonatosNParty.Helpers.ImageHelper.GetSmashCharacterImage((int)IdPersonagem1, (int)IdCorPersonagem1);
            }
        }

        public string ImagemPersonagem2
        {
            get
            {
                return CampeonatosNParty.Helpers.ImageHelper.GetSmashCharacterImage((int)IdPersonagem2, (int)IdCorPersonagem2);
            }
        }

        public string ImagemPersonagem3
        {
            get
            {
                return CampeonatosNParty.Helpers.ImageHelper.GetSmashCharacterImage((int)IdPersonagem3, (int)IdCorPersonagem3);
            }
        }
    }
}