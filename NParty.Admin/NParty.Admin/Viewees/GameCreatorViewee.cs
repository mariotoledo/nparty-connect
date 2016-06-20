using EixoX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NParty.Admin.Viewees
{
    public interface GameCreatorViewee : Viewee
    {
        void OnGameNameIsNullOrEmpty();
        void OnGameNameLengthIsLarge();
        void OnGameAlreadyExists();
    }
}