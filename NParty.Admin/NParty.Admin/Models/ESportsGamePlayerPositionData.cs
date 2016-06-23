using NParty.Database.ESports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NParty.Admin.Models
{
    public class ESportsGamePlayerPositionData
    {
        public ESportsGame ESportsGame { get; set; }

        public List<ESportsGamePlayerPosition> ESportsGamePlayerPositions { get; set; }
    }
}