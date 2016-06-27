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
    public class GamePlayerPositionCreator
    {
        public ESportsGame Game { get; private set; }

        public string GamePlayerPositionName { get; set; }

        public int GamePlayerPositionOrder { get; set; }

        public bool IsRequired { get; set; }

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

        public bool Execute(int gameId, NameValueCollection form, GamePlayerPositionCreatorViewee viewee)
        {
            if (!LoadESportsGame(gameId))
            {
                viewee.OnGameDoesNotExists();
                return false;
            }

            this.IsRequired = form["IsRequired"] != null;
            this.GamePlayerPositionName = form["ESportsGamePlayerPositionName"];

            if (string.IsNullOrEmpty(this.GamePlayerPositionName))
            {
                viewee.OnGamePlayerPositionNameIsNullOrEmpty();
                return false;
            }

            int gamePlayerPositionOrderAux = 0;
            bool orderParseResult = int.TryParse(form["ESportsGamePlayerPositionOrder"], out gamePlayerPositionOrderAux);

            if(orderParseResult == false || gamePlayerPositionOrderAux <= 0)
            {
                viewee.OnGamePlayerPositionOrderIsInvalid();
                return false;
            }

            this.GamePlayerPositionOrder = gamePlayerPositionOrderAux;

            ESportsGamePlayerPosition gamePosition =
                ESportsGamePlayerPosition.Select().Where("ESportsGameId", gameId).And("ESportsGamePositionName", this.GamePlayerPositionName).SingleResult();

            if(gamePosition != null)
            {
                viewee.OnGamePlayerPositionNameAlreadyExists();
                return false;
            }

            gamePosition = new ESportsGamePlayerPosition();
            gamePosition.ESportsGamePositionName = this.GamePlayerPositionName;
            gamePosition.ESportsGameId = gameId;
            gamePosition.Order = this.GamePlayerPositionOrder;
            gamePosition.IsRequired = this.IsRequired;

            ESportsDb<ESportsGamePlayerPosition>.Instance.Insert(gamePosition);

            return true;
        }
    }
}