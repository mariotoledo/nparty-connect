using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CampeonatosNParty.Models.Cookie
{
    public interface CookieContext
    {
        string GetCookieId();
        void SetCookieId(string id);

        string GetUserAgent();
        string GetUserIP();
    }
}