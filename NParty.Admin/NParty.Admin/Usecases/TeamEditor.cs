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
    public class TeamEditor
    {
        public ESportsTeam ESportsTeam { get; set; }

        public bool Execute(int esportsTeamId, NameValueCollection form, HttpPostedFileBase logoImage, TeamEditorViewee viewee)
        {
            this.ESportsTeam = ESportsDb<ESportsTeam>.Instance.Select().Where("ESportsTeamId", esportsTeamId).SingleResult();

            if (this.ESportsTeam == null)
            {
                viewee.OnTeamNotFound();
                return false;
            }

            string teamName = form["TeamName"];

            if (string.IsNullOrEmpty(teamName))
            {
                viewee.OnTeamNameIsNullOrEmpty();
                return false;
            }

            if (teamName.Length > 50)
            {
                viewee.OnTeamNameLengthIsLarge();
                return false;
            }

            this.ESportsTeam.ESportsTeamName = teamName;

            if (logoImage != null && logoImage.ContentLength > 0)
                this.ESportsTeam.ESportsTeamLogoURL = BlobHelper.Instance.UploadImage(logoImage.InputStream, "team_" + teamName.ToLower().Replace(' ', '_') + ".jpg");

            ESportsDb<ESportsTeam>.Instance.Update(this.ESportsTeam);

            return true;
        }
    }
}