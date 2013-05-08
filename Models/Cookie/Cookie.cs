using CampeonatosNParty.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EixoX.Restrictions;
using EixoX.Data;
using EixoX.Interceptors;

namespace CampeonatosNParty.Models.Cookie
{
    [DatabaseTable]
    public class Cookie : NPartyDbModel<Cookie>
    {
        [DatabaseColumn(DatabaseColumnKind.Identity)]
        public long Id { get; set; }

        [DatabaseColumn]
        [GetDateGenerator(DataScope.Insert)]
        public DateTime DateCreated { get; set; }

        [DatabaseColumn(true)]
        [GetDateGenerator(DataScope.Update)]
        public DateTime DateUpdated { get; set; }

        [DatabaseColumn]
        public bool IsLoggedIn { get; set; }

        [DatabaseColumn]
        public int UserId { get; set; }

        [DatabaseColumn]
        [MaxLength(255)]
        public string UserAgent { get; set; }

        [DatabaseColumn]
        [MaxLength(50)]
        public string UserIP { get; set; }

        [DatabaseColumn]
        public int PersonInviter { get; set; }

        public static Cookie EnsureContext(CookieContext context)
        {
            int cookieId;
            Cookie cookie = null;
            if (int.TryParse(context.GetCookieId(), out cookieId) && cookieId > 0)
                cookie = Cookie.WithIdentity(cookieId);

            if (cookie == null)
            {
                cookie = new Cookie();
                cookie.UserAgent = context.GetUserAgent();
                cookie.UserIP = context.GetUserIP();
                NPartyDb<Cookie>.Instance.Insert(cookie);
                context.SetCookieId(cookie.Id.ToString());
            }

            return cookie;
        }

        public Usuarios Login(string email, string password)
        {
            Usuarios usuario = null;

            if (
                !string.IsNullOrEmpty(email) &&
                !string.IsNullOrEmpty(password) &&
                EixoX.ValidationHelper.IsEmail(usuario.Email))
            {
                string pwdHash = EixoX.CryptoHelper.Md5Hash(password);
                usuario = NPartyDb<Usuarios>.Instance.Select().Where("Email", email).And("Senha", pwdHash).FirstOrDefault();
                if (usuario != null)
                {
                    this.UserId = usuario.Id;
                    this.IsLoggedIn = true;
                    this.DateUpdated = DateTime.Now;
                }
            }

            return usuario;

        }

        public void Logout()
        {
            this.IsLoggedIn = false;
            NPartyDb<Cookie>.Instance.Update(this);
        }
    }
}