using EixoX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NParty.Admin.Viewees
{
    public interface PlayerCreatorViewee : Viewee
    {
        void OnPlayerFirstNameIsNullOrEmpty();
        void OnPlayerFirstNameLengthIsLarge();
        void OnPlayerLastNameIsNullOrEmpty();
        void OnPlayerLastNameLengthIsLarge();
        void OnPlayerNicknameIsNullOrEmpty();
        void OnPlayerNicknameLengthIsLarge();
        void OnPlayerAlreadyExists();
    }
}