using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NParty.Www.DatabaseModel
{
    [DatabaseTable]
    public class User
    {
        [DatabaseColumn(DatabaseColumnKind.Identity)]
        public int UserId { get; set; }

        [DatabaseColumn]
        public string Username { get; set; }

        [DatabaseColumn]
        public string FirstName { get; set; }

        [DatabaseColumn]
        public string LastName { get; set; }

        [DatabaseColumn]
        public string Email { get; set; }

        [DatabaseColumn]
        public bool IsValidEmail { get; set; }

        [DatabaseColumn]
        public string PasswordHash { get; set; }

        [DatabaseColumn(true)]
        public DateTime Birthdate { get; set; }

        [DatabaseColumn]
        public int StateId { get; set; }

        [DatabaseColumn]
        public int CityId { get; set; }

        [DatabaseColumn]
        public bool NewsletterOption { get; set; }

        [DatabaseColumn]
        public string PhotoUrl { get; set; }
    }
}