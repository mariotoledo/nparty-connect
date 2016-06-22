using EixoX;
using NParty.Database.ESports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NParty.Admin.Usecases
{
    public class PlayerLister
    {
        public List<ESportsPlayer> All(Viewee viewee)
        {
            try
            {
                return ESportsPlayer.Select().OrderBy("ESportsPlayerFirstName").ToList();
            }
            catch (Exception e)
            {
                viewee.OnException(e);
                return null;
            }
        }

        public ESportsPlayer Single(int id, Viewee viewee)
        {
            try
            {
                return ESportsPlayer.Select().Where("ESportsPlayerId", id).SingleResult();
            }
            catch (Exception e)
            {
                viewee.OnException(e);
                return null;
            }
        }
    }
}