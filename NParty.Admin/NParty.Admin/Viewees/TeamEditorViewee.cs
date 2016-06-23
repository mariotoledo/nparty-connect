using EixoX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NParty.Admin.Viewees
{
    public interface TeamEditorViewee : Viewee
    {
        void OnTeamNameIsNullOrEmpty();
        void OnTeamNameLengthIsLarge();
        void OnTeamNotFound();
    }
}