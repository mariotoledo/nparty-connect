using NParty.Admin.Helpers;
using NParty.Admin.Viewees;
using NParty.Database.ESports;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Admin.Usecases
{
    public class GameRuleEditor
    {
        public ESportsGame Game { get; private set; }

        public ESportsGameRule GameRule { get; set; }

        public void LoadFromId(int gameRuleId)
        {
            try
            {
                this.GameRule = ESportsGameRule.WithIdentity(gameRuleId);
                this.Game = ESportsGame.WithIdentity(this.GameRule.ESportsGameId);
            } catch (Exception) { }
        }

        public bool Execute(int gameId, NameValueCollection form, GameRuleEditorViewee viewee)
        {
            LoadFromId(gameId);

            if (GameRule == null || Game == null)
            {
                viewee.OnRuleDoesNotExists();
                return false;
            }

            GameRule.ESportsGameRuleName = form["ESportsGameRuleName"];
            GameRule.ESportsGameRuleDescription = form["ESportsGameRuleDescription"];

            if (string.IsNullOrEmpty(GameRule.ESportsGameRuleName))
            {
                viewee.OnGameRuleNameIsNullOrEmpty();
                return false;
            }

            if (string.IsNullOrEmpty(GameRule.ESportsGameRuleDescription))
            {
                viewee.OnGameRuleDescriptionIsNullOrEmpty();
                return false;
            }

            if(GameRule.ESportsGameRuleDescription.Length > 2000)
            {
                viewee.OnGameRuleDescriptionIsTooLong();
                return false;
            }

            ESportsDb<ESportsGameRule>.Instance.Update(GameRule);

            return true;
        }
    }
}