using EixoX;
using NParty.Database.ESports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NParty.Admin.Usecases
{
    public class GameRuleLister
    {
        public List<ESportsGameRule> AllFromGameId(int gameId, Viewee viewee)
        {
            try
            {
                return ESportsGameRule.Select().Where("ESportsGameId", gameId).OrderBy("ESportsGameRuleName").ToList();
            }
            catch (Exception e)
            {
                viewee.OnException(e);
                return null;
            }
        }
    }
}