using EixoX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NParty.Admin.Viewees
{
    public interface GameEditorViewee : Viewee
    {
        void OnGameNotFound();
        void OnGameNameIsNullOrEmpty();
        void OnGameNameLengthIsLarge();
    }
}