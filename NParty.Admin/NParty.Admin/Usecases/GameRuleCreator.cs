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
    public class GameRuleCreator
    {
        public ESportsGame Game { get; private set; }

        public string GameRuleName { get; set; }

        public string GameRuleDescription { get; set; }

        public bool LoadESportsGame(int gameId)
        {
            try
            {
                this.Game = ESportsGame.WithIdentity(gameId);
                return Game != null;
            } catch (Exception)
            {
                return false;
            }
        }

        public bool Execute(int gameId, NameValueCollection form, GameRuleCreatorViewee viewee)
        {
            if (!LoadESportsGame(gameId))
            {
                viewee.OnGameDoesNotExists();
                return false;
            }

            this.GameRuleName = form["ESportsGameRuleName"];
            this.GameRuleDescription = form["ESportsGameRuleDescription"];

            if (string.IsNullOrEmpty(this.GameRuleName))
            {
                viewee.OnGameRuleNameIsNullOrEmpty();
                return false;
            }

            ESportsGameRule gameRule = ESportsGameRule.Select().Where("ESportsGameId", gameId).And("ESportsGameRuleName", GameRuleName).SingleResult();

            if(gameRule != null)
            {
                viewee.OnGameRuleNameAlreadyExists();
                return false;
            }

            if (string.IsNullOrEmpty(this.GameRuleDescription))
            {
                viewee.OnGameRuleDescriptionIsNullOrEmpty();
                return false;
            }

            if(this.GameRuleDescription.Length > 2000)
            {
                viewee.OnGameRuleDescriptionIsTooLong();
                return false;
            }

            gameRule = new ESportsGameRule();
            gameRule.ESportsGameRuleName = this.GameRuleName;
            gameRule.ESportsGameRuleDescription = this.GameRuleDescription;
            gameRule.ESportsGameId = gameId;

            ESportsDb<ESportsGameRule>.Instance.Insert(gameRule);

            return true;
        }
    }
}