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
    public class PlayerEditor
    {
        public ESportsPlayer ESportsPlayer { get; set; }

        public bool Execute(int esportsPlayerId, NameValueCollection form, HttpPostedFileBase photoImage, PlayerEditorViewee viewee)
        {
            this.ESportsPlayer = ESportsDb<ESportsPlayer>.Instance.Select().Where("ESportsPlayerId", esportsPlayerId).SingleResult();

            if (this.ESportsPlayer == null)
            {
                viewee.OnPlayerNotFound();
                return false;
            }

            string playerFirstName = form["PlayerFirstName"];
            string playerLastName = form["PlayerLastName"];
            string playerNickname = form["PlayerNickname"];

            if (string.IsNullOrEmpty(playerFirstName))
            {
                viewee.OnPlayerFirstNameIsNullOrEmpty();
                return false;
            }

            if (playerFirstName.Length > 50)
            {
                viewee.OnPlayerFirstNameLengthIsLarge();
                return false;
            }

            if (string.IsNullOrEmpty(playerLastName))
            {
                viewee.OnPlayerLastNameIsNullOrEmpty();
                return false;
            }

            if (playerLastName.Length > 50)
            {
                viewee.OnPlayerLastNameLengthIsLarge();
                return false;
            }

            if (string.IsNullOrEmpty(playerNickname))
            {
                viewee.OnPlayerNicknameIsNullOrEmpty();
                return false;
            }

            if (playerNickname.Length > 50)
            {
                viewee.OnPlayerNicknameLengthIsLarge();
                return false;
            }

            this.ESportsPlayer.ESportsPlayerFirstName = playerFirstName;
            this.ESportsPlayer.ESportsPlayerLastName = playerLastName;
            this.ESportsPlayer.ESportsPlayerNickname = playerNickname;

            if (photoImage != null && photoImage.ContentLength > 0)
                this.ESportsPlayer.ESportsPlayerPhotoURL =
                    BlobHelper.Instance.UploadImage(
                            photoImage.InputStream,
                            "player_" +
                            playerFirstName.ToLower().Replace(' ', '_') + "_" +
                            playerNickname.ToLower().Replace(' ', '_') + "_" +
                            playerLastName.ToLower().Replace(' ', '_') +
                            ".jpg"
                        );

            ESportsDb<ESportsPlayer>.Instance.Update(this.ESportsPlayer);

            return true;
        }
    }
}