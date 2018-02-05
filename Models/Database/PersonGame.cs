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
    public class PersonGame : NPartyDbModel<PersonGame>
    {
        [DatabaseColumn]
        public int PersonId { get; set; }

        [DatabaseColumn]
        public int GameId { get; set; }
    }
}