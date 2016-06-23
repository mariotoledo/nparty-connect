using EixoX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NParty.Admin.Viewees
{
    public interface TeamCreatorViewee : Viewee
    {
        void OnTeamNameIsNullOrEmpty();
        void OnTeamNameLengthIsLarge();
        void OnTeamAlreadyExists();
    }
}