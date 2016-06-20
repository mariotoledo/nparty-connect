using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NParty.Database.ESports
{
    [DatabaseTable]
    public class ESportsTeamFormationPlayer : ESportsDbModel<ESportsTeamFormationPlayer>
    {
        [DatabaseColumn]
        public int ESportsTeamFormationId { get; set; }

        [DatabaseColumn]
        public int ESportsPlayerId { get; set; }
    }
}
