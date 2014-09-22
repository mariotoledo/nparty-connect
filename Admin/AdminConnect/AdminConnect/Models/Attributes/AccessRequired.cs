using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminConnect.Models.Attributes
{
    public enum AccessType
    {
        CanCreateEvent, CanEditEvent, CanRemoveEvent, CanCreateUser, CanEditUser, CanRemoveUser, CanCreateChampionship, CanEditChampionship, CanRemoveChampionship
    }

    public class AccessRequired : Attribute
    {
        public AccessType AccessType { get; set; }

        public AccessRequired(AccessType accessType)
        {
            AccessType = accessType;
        }
    }
}