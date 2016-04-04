using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace NParty.Www.Models
{
    public class Podcast : Article
    {
        public override void GenerateNPartyArtileLink(string domain)
        {
            this.NPartyArticleLink = domain + "/Ouvir/" + Id + "/" + GetTitleForUrl();
        }
    }
}