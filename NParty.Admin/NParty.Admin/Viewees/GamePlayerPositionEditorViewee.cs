using EixoX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NParty.Admin.Viewees
{
    public interface GamePlayerPositionEditorViewee : Viewee
    {
        void OnGameDoesNotExists();
        void OnGamePlayerPositionDoesNotExists();
        void OnGamePlayerPositionNameIsNullOrEmpty();
        void OnGamePlayerPositionOrderIsInvalid();
    }
}