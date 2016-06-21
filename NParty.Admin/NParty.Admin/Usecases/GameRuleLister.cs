using EixoX;
using NParty.Admin.Models;
using NParty.Database.ESports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NParty.Admin.Usecases
{
    public class GameRuleLister
    {
        public ESportsGameRuleData AllFromGameId(int gameId, Viewee viewee)
        {
            try
            {
                ESportsGameRuleData model = new ESportsGameRuleData();
                model.ESportsGame = ESportsGame.WithIdentity(gameId);
                model.ESportsGameRules = ESportsGameRule.Select().Where("ESportsGameId", gameId).OrderBy("ESportsGameRuleName").ToList();

                return model;
            }
            catch (Exception e)
            {
                viewee.OnException(e);
                return null;
            }
        }
    }
}