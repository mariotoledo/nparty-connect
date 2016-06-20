using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NParty.Database.ESports
{
    [DatabaseTable]
    public class ESportsGame : ESportsDbModel<ESportsGame>
    {
        [DatabaseColumn(DatabaseColumnKind.Identity)]
        public int ESportsGameId { get; set; }

        [DatabaseColumn]
        public string ESportsGameName { get; set; }

        [DatabaseColumn]
        public string ESportsGameLogoURL { get; set; }
    }
}
