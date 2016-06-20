using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NParty.Database.ESports
{
    [DatabaseTable]
    public class ESportsChampionshipMatch : ESportsDbModel<ESportsChampionshipMatch>
    {
        [DatabaseColumn]
        public int ESportsChampionshipMatchId { get; set; }

        [DatabaseColumn]
        public int ESportsChampionshipId { get; set; }

        [DatabaseColumn]
        public DateTime EsportsChampionshipMatchDate { get; set; }

        [DatabaseColumn]
        public int ESportsGameRuleId { get; set; }

        [DatabaseColumn]
        public int Team1Id { get; set; }

        [DatabaseColumn]
        public int Team2Id { get; set; }

        [DatabaseColumn]
        public int Player1Id { get; set; }

        [DatabaseColumn]
        public int Player2Id { get; set; }

        [DatabaseColumn]
        public int Result1 { get; set; }

        [DatabaseColumn]
        public int Result2 { get; set; }

        [DatabaseColumn]
        public string VodUrl { get; set; }
    }
}
