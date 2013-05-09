using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EixoX;
using CampeonatosNParty.Models.Cookie;
using CampeonatosNParty.Models.Database;

namespace CampeonatosNParty.Controllers
{
    public abstract class AuthenticationBasedController : Controller, CookieContext
    {
        private Usuarios _CurrentUser;

        private Cookie _NPartyCookie;
        public Cookie NPartyCookie
        {
            get
            {
                return _NPartyCookie ?? (_NPartyCookie = Cookie.EnsureContext(this));
            }
        }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (this._CurrentUser == null)
            {
                Cookie npartyCookie = this.NPartyCookie;
                if (npartyCookie.IsLoggedIn && npartyCookie.UserId > 0)
                {
                    this._CurrentUser = Usuarios.WithMember("Id", npartyCookie.UserId);
                    ViewData["CurrentUser"] = this._CurrentUser;
                }
            }

            base.OnAuthorization(filterContext);
        }

        private HttpCookie _NPartyHttpCookie;

        public Usuarios CurrentUsuario
        {
            get { return this._CurrentUser; }
        }

        protected HttpCookie NPartyHttpCookie
        {
            get
            {
                return _NPartyHttpCookie ?? (_NPartyHttpCookie = Request.Cookies["NParty"]);
            }
        }

        public string GetCookieId()
        {
            HttpCookie npartyCookie = NPartyHttpCookie;
            return npartyCookie == null ? null : npartyCookie["cid"];
        }

        public void SetCookieId(string id)
        {
            HttpCookie npartyCookie = NPartyHttpCookie;
            if (npartyCookie == null)
                npartyCookie = new HttpCookie("NParty");

            npartyCookie.Expires = DateTime.Today.AddYears(10);
            npartyCookie["cid"] = id;
            Response.SetCookie(npartyCookie);
        }

        public string GetUserAgent()
        {
            return Request.UserAgent;
        }

        public string GetUserIP()
        {
            return Request.UserHostAddress;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            object[] attributesFound = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AuthenticationRequired), true);

            if (attributesFound.Length == 1)
            {
                if (CurrentUsuario == null)
                {
                    filterContext.Result = Redirect(Url.Content("~/Home/Login/?redir=" + Url.Encode(Request.Url.ToString())));
                    return;
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }

    public class AuthenticationRequired : Attribute
    {

    }
}