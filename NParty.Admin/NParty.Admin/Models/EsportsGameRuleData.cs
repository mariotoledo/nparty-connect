using NParty.Database.ESports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NParty.Admin.Models
{
    public class ESportsGameRuleData
    {
        public ESportsGame ESportsGame { get; set; }

        public List<ESportsGameRule> ESportsGameRules { get; set; }
    }
}