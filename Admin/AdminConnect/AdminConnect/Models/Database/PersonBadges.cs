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
    public class PersonBadges : NPartyDbModel<PersonBadges>
    {
        [DatabaseColumn]
        public int PersonId { get; set; }

        [DatabaseColumn]
        public string BadgeId { get; set; }

        [DatabaseColumn]
        public DateTime DateEarned { get; set; }
    }
}