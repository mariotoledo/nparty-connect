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
    public class GameCreator
    {
        public string GameName { get; set; }

        public bool Execute(NameValueCollection form, HttpPostedFileBase logoImage, GameCreatorViewee viewee)
        {
            this.GameName = form["GameName"];

            if (string.IsNullOrEmpty(GameName))
            {
                viewee.OnGameNameIsNullOrEmpty();
                return false;
            }

            if (GameName.Length > 50)
            {
                viewee.OnGameNameLengthIsLarge();
                return false;
            }

            ESportsGame game = ESportsGame.WithMember("ESportsGameName", this.GameName);

            if (game != null)
            {
                viewee.OnGameAlreadyExists();
                return false;
            }

            game = new ESportsGame();
            game.ESportsGameName = this.GameName;

            if (logoImage != null && logoImage.ContentLength > 0)
                game.ESportsGameLogoURL = BlobHelper.Instance.UploadImage(logoImage.InputStream, "game_" + this.GameName.ToLower().Replace(' ', '_') + ".jpg");

            ESportsDb<ESportsGame>.Instance.Insert(game);

            return true;
        }
    }
}