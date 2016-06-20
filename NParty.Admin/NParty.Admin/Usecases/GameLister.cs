using EixoX;
using NParty.Database.ESports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NParty.Admin.Usecases
{
    public class GameLister
    {
        public List<ESportsGame> All(Viewee viewee)
        {
            try
            {
                return ESportsGame.Select().OrderBy("ESportsGameName").ToList();
            }
            catch (Exception e)
            {
                viewee.OnException(e);
                return null;
            }
        }

        public ESportsGame Single(int id, Viewee viewee)
        {
            try
            {
                return ESportsGame.Select().Where("ESportsGameId", id).SingleResult();
            }
            catch (Exception e)
            {
                viewee.OnException(e);
                return null;
            }
        }
    }
}