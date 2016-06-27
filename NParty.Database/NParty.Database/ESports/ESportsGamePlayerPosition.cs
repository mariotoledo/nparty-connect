using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NParty.Database.ESports
{
    [DatabaseTable]
    public class ESportsGamePlayerPosition : ESportsDbModel<ESportsGamePlayerPosition>
    {
        [DatabaseColumn(DatabaseColumnKind.Identity)]
        public int ESportsGamePlayerPositionId { get; set; }

        [DatabaseColumn]
        public int ESportsGameId { get; set; }

        [DatabaseColumn]
        public string ESportsGamePositionName { get; set; }

        [DatabaseColumn]
        public bool IsRequired { get; set; }

        [DatabaseColumn]
        public int Order { get; set; }
    }
}
