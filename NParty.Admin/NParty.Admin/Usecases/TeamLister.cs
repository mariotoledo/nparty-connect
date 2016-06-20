using EixoX;
using NParty.Database.ESports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NParty.Admin.Usecases
{
    public class TeamLister
    {
        public List<ESportsTeam> All(Viewee viewee)
        {
            try
            {
                return ESportsTeam.Select().OrderBy("ESportsTeamName").ToList();
            }
            catch (Exception e)
            {
                viewee.OnException(e);
                return null;
            }
        }
    }
}