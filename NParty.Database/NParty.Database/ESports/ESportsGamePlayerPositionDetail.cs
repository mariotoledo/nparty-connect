using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NParty.Database.ESports
{
    [DatabaseTable]
    public class ESportsGamePlayerPositionDetail
    {
        [DatabaseColumn]
        public int ESportsTeamFormationId { get; set; }

        [DatabaseColumn]
        public int ESportsGameId { get; set; }

        [DatabaseColumn]
        public DateTime DateFormation { get; set; }

        [DatabaseColumn]
        public int ESportsPlayerId { get; set; }

        [DatabaseColumn]
        public string ESportsPlayerNickname { get; set; }

        [DatabaseColumn]
        public int ESportsGamePlayerPositionId { get; set; }

        [DatabaseColumn]
        public string ESportsGamePositionName { get; set; }

        [DatabaseColumn]
        public bool IsRequired { get; set; }
    }
}
