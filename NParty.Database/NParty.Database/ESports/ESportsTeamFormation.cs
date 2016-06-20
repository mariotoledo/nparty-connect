using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NParty.Database.ESports
{
    [DatabaseTable]
    public class ESportsTeamFormation : ESportsDbModel<ESportsTeamFormation>
    {
        [DatabaseColumn]
        public int ESportsTeamFormationId { get; set; }

        [DatabaseColumn]
        public string ESportsGameId { get; set; }

        [DatabaseColumn]
        public DateTime DateFormation { get; set; }
    }
}
