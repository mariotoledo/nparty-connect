using EixoX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NParty.Admin.Viewees
{
    public interface GameRuleEditorViewee : Viewee
    {
        void OnRuleDoesNotExists();
        void OnGameRuleNameIsNullOrEmpty();
        void OnGameRuleDescriptionIsNullOrEmpty();
        void OnGameRuleDescriptionIsTooLong();
    }
}