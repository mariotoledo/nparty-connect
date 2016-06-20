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
    public class GameEditor
    {
        public ESportsGame ESportsGame { get; set; }

        public bool Execute(int esportsGameId, NameValueCollection form, HttpPostedFileBase logoImage, GameEditorViewee viewee)
        {
            this.ESportsGame = ESportsDb<ESportsGame>.Instance.Select().Where("ESportsGameId", esportsGameId).SingleResult();

            if (this.ESportsGame == null)
            {
                viewee.OnGameNotFound();
                return false;
            }

            string gameName = form["GameName"];

            if (string.IsNullOrEmpty(gameName))
            {
                viewee.OnGameNameIsNullOrEmpty();
                return false;
            }

            if (gameName.Length > 50)
            {
                viewee.OnGameNameLengthIsLarge();
                return false;
            }

            this.ESportsGame.ESportsGameName = gameName;

            if (logoImage != null && logoImage.ContentLength > 0)
                this.ESportsGame.ESportsGameLogoURL = BlobHelper.Instance.UploadImage(logoImage.InputStream, "game_" + gameName.ToLower().Replace(' ', '_') + ".jpg");

            ESportsDb<ESportsGame>.Instance.Update(this.ESportsGame);

            return true;
        }
    }
}