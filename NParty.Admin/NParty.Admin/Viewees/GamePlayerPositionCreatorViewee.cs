using EixoX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NParty.Admin.Viewees
{
    public interface GamePlayerPositionCreatorViewee : Viewee
    {
        void OnGameDoesNotExists();
        void OnGamePlayerPositionNameAlreadyExists();
        void OnGamePlayerPositionNameIsNullOrEmpty();
        void OnGamePlayerPositionOrderIsInvalid();
    }
}