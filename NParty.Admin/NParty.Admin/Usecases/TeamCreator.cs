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
    public class TeamCreator
    {
        public string TeamName { get; set; }

        public bool Execute(NameValueCollection form, HttpPostedFileBase photoImage, TeamCreatorViewee viewee)
        {
            this.TeamName = form["TeamName"];

            if (string.IsNullOrEmpty(TeamName))
            {
                viewee.OnTeamNameIsNullOrEmpty();
                return false;
            }

            if (TeamName.Length > 50)
            {
                viewee.OnTeamNameLengthIsLarge();
                return false;
            }

            ESportsTeam team = ESportsTeam.WithMember("ESportsTeamName", TeamName);

            if (team != null)
            {
                viewee.OnTeamAlreadyExists();
                return false;
            }

            team = new ESportsTeam();
            team.ESportsTeamName = this.TeamName;

            if (photoImage != null && photoImage.ContentLength > 0)
                team.ESportsTeamLogoURL = 
                    BlobHelper.Instance.UploadImage(
                            photoImage.InputStream, 
                            "team_" +
                            this.TeamName.ToLower().Replace(' ', '_') + ".jpg"
                        );

            ESportsDb<ESportsTeam>.Instance.Insert(team);

            return true;
        }
    }
}