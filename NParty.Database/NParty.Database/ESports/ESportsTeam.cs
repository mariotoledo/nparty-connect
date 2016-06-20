using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NParty.Database.ESports
{
    [DatabaseTable]
    public class ESportsTeam : ESportsDbModel<ESportsTeam>
    {
        [DatabaseColumn]
        public int ESportsTeamId { get; set; }

        [DatabaseColumn]
        public string ESportsTeamName { get; set; }

        [DatabaseColumn]
        public string ESportsTeamLogoURL { get; set; }
    }
}
