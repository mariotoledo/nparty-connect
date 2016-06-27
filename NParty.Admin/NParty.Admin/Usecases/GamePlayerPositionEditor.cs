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
    public class GamePlayerPositionEditor
    {
        public ESportsGame Game { get; private set; }

        public ESportsGamePlayerPosition GamePlayerPosition { get; set; }

        public void LoadFromId(int gamePositionId)
        {
            try
            {
                this.GamePlayerPosition = ESportsGamePlayerPosition.WithIdentity(gamePositionId);
                this.Game = ESportsGame.WithIdentity(this.GamePlayerPosition.ESportsGameId);
            } catch (Exception) { }
        }

        public bool Execute(int gameId, NameValueCollection form, GamePlayerPositionEditorViewee viewee)
        {
            LoadFromId(gameId);

            if (Game == null)
            {
                viewee.OnGameDoesNotExists();
                return false;
            }

            if(GamePlayerPosition == null)
            {
                viewee.OnGamePlayerPositionDoesNotExists();
                return false;
            }

            GamePlayerPosition.IsRequired = form["IsRequired"] != null;
            GamePlayerPosition.ESportsGamePositionName = form["ESportsGamePlayerPositionName"];

            if (string.IsNullOrEmpty(this.GamePlayerPosition.ESportsGamePositionName))
            {
                viewee.OnGamePlayerPositionNameIsNullOrEmpty();
                return false;
            }

            int gamePlayerPositionOrderAux = 0;
            bool orderParseResult = int.TryParse(form["ESportsGamePlayerPositionOrder"], out gamePlayerPositionOrderAux);

            if (orderParseResult == false || gamePlayerPositionOrderAux <= 0)
            {
                viewee.OnGamePlayerPositionOrderIsInvalid();
                return false;
            }

            GamePlayerPosition.Order = gamePlayerPositionOrderAux;

            ESportsDb<ESportsGamePlayerPosition>.Instance.Update(GamePlayerPosition);

            return true;
        }
    }
}