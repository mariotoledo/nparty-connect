using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NParty.Database.ESports
{
    [DatabaseTable]
    public class ESportsPlayer : ESportsDbModel<ESportsPlayer>
    {
        [DatabaseColumn(DatabaseColumnKind.Identity)]
        public int ESportsPlayerId { get; set; }

        [DatabaseColumn]
        public string ESportsPlayerFirstName { get; set; }

        [DatabaseColumn]
        public string ESportsPlayerLastName { get; set; }

        [DatabaseColumn]
        public string ESportsPlayerNickname { get; set; }

        [DatabaseColumn]
        public string ESportsPlayerPhotoURL { get; set; }

        public string GetFullNameAndNickname()
        {
            return ESportsPlayerFirstName + " \"" + ESportsPlayerNickname + "\" " + ESportsPlayerLastName;
        }
    }
}
