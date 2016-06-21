using EixoX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NParty.Admin.Viewees
{
    public interface GameRuleCreatorViewee : Viewee
    {
        void OnGameDoesNotExists();
        void OnGameRuleNameAlreadyExists();
        void OnGameRuleNameIsNullOrEmpty();
        void OnGameRuleDescriptionIsNullOrEmpty();
        void OnGameRuleDescriptionIsTooLong();
    }
}