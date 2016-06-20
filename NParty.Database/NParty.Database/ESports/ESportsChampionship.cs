using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NParty.Database.ESports
{
    [DatabaseTable]
    public class ESportsChampionship : ESportsDbModel<ESportsChampionship>
    {
        [DatabaseColumn(DatabaseColumnKind.Identity)]
        public int ESportsChampionshipId { get; set; }

        [DatabaseColumn]
        public int ESportsGameId { get; set; }

        [DatabaseColumn]
        public string ESportsChampionshipName { get; set; }

        [DatabaseColumn]
        public string ESportsChampionshipDescription { get; set; }

        [DatabaseColumn]
        public string ESportsChampionshipLogoUrl { get; set; }

        [DatabaseColumn]
        public bool IsForTeams { get; set; }

        [DatabaseColumn]
        public string YoutubeStreamUrl { get; set; }

        [DatabaseColumn]
        public string TwitchStreamUrl { get; set; }

    }
}
