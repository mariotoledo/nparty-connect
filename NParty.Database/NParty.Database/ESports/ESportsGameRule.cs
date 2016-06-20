using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NParty.Database.ESports
{
    [DatabaseTable]
    public class ESportsGameRule : ESportsDbModel<ESportsGameRule>
    {
        [DatabaseColumn]
        public int ESportsGameRuleId { get; set; }

        [DatabaseColumn]
        public string ESportsGameRuleName { get; set; }

        [DatabaseColumn]
        public string ESportsGameRuleDescription { get; set; }

        [DatabaseColumn]
        public int ESportsGameId { get; set; }
    }
}
