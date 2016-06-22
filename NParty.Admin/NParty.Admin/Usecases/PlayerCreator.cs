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
    public class PlayerCreator
    {
        public string PlayerFirstName { get; set; }

        public string PlayerLastName { get; set; }

        public string PlayerNickname { get; set; }

        public bool Execute(NameValueCollection form, HttpPostedFileBase photoImage, PlayerCreatorViewee viewee)
        {
            this.PlayerFirstName = form["PlayerFirstName"];
            this.PlayerLastName = form["PlayerLastName"];
            this.PlayerNickname = form["PlayerNickname"];

            if (string.IsNullOrEmpty(PlayerFirstName))
            {
                viewee.OnPlayerFirstNameIsNullOrEmpty();
                return false;
            }

            if (PlayerFirstName.Length > 50)
            {
                viewee.OnPlayerFirstNameLengthIsLarge();
                return false;
            }

            if (string.IsNullOrEmpty(PlayerLastName))
            {
                viewee.OnPlayerLastNameIsNullOrEmpty();
                return false;
            }

            if (PlayerLastName.Length > 50)
            {
                viewee.OnPlayerLastNameLengthIsLarge();
                return false;
            }

            if (string.IsNullOrEmpty(PlayerNickname))
            {
                viewee.OnPlayerNicknameIsNullOrEmpty();
                return false;
            }

            if (PlayerNickname.Length > 50)
            {
                viewee.OnPlayerNicknameLengthIsLarge();
                return false;
            }

            ESportsPlayer player = ESportsPlayer.Select()
                                    .Where("ESportsPlayerFirstName", this.PlayerFirstName)
                                    .And("ESportsPlayerLastName", this.PlayerLastName)
                                    .And("ESportsPlayerNickname", this.PlayerNickname)
                                    .SingleResult();

            if (player != null)
            {
                viewee.OnPlayerAlreadyExists();
                return false;
            }

            player = new ESportsPlayer();
            player.ESportsPlayerFirstName = this.PlayerFirstName;
            player.ESportsPlayerLastName = this.PlayerLastName;
            player.ESportsPlayerNickname = this.PlayerNickname;

            if (photoImage != null && photoImage.ContentLength > 0)
                player.ESportsPlayerPhotoURL = 
                    BlobHelper.Instance.UploadImage(
                            photoImage.InputStream, 
                            "player_" +
                            this.PlayerFirstName.ToLower().Replace(' ', '_') + "_" +
                            this.PlayerNickname.ToLower().Replace(' ', '_') + "_" +
                            this.PlayerLastName.ToLower().Replace(' ', '_') +
                            ".jpg"
                        );

            ESportsDb<ESportsPlayer>.Instance.Insert(player);

            return true;
        }
    }
}