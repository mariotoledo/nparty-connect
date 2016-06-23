using EixoX;
using NParty.Admin.Models;
using NParty.Database.ESports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NParty.Admin.Usecases
{
    public class GamePlayerPositionLister
    {
        public ESportsGamePlayerPositionData AllFromGameId(int gameId, Viewee viewee)
        {
            try
            {
                ESportsGamePlayerPositionData model = new ESportsGamePlayerPositionData();
                model.ESportsGame = ESportsGame.WithIdentity(gameId);
                model.ESportsGamePlayerPositions = ESportsGamePlayerPosition.Select().Where("ESportsGameId", gameId).OrderBy("Order", EixoX.Data.SortDirection.Ascending).ToList();

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