using AdminConnect.Models.Attributes;
using CampeonatosNParty.Models.Database;
using EixoX.Web.AuthComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminConnect.Controllers
{
    public class NPartyController : Controller
    {
        public enum MessageType
        {
            Success, Info, Error
        }

        public bool IsLoggedIn
        {
            get
            {
                return CurrentUser != null;
            }
        }

        public Administrador CurrentUser
        {
            get
            {
                return (Administrador)Session["CurrentUser"];
            }

            set
            {
                Session["CurrentUser"] = value;
            }
        }

        protected void FlashMessage(string message, MessageType type)
        {
            TempData["FlashMessage"] = message;
            switch (type)
            {
                case MessageType.Success:
                    TempData["FlashMessageType"] = "success";
                    break;
                case MessageType.Info:
                    TempData["FlashMessageType"] = "info";
                    break;
                case MessageType.Error:
                    TempData["FlashMessageType"] = "danger";
                    break;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            object[] attributesFound = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AuthenticationRequired), true);

            if (attributesFound.Length == 1)
            {
                if (!IsLoggedIn && CurrentUser == null)
                {
                    string redirectUrl = String.Format("{0}?redir={1}", "~/Login", Url.Encode(Request.Url.ToString()));
                    filterContext.Result = new RedirectResult(redirectUrl);
                    return;
                }
            }

            object[] accessFound = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AccessRequired), true);

            if (accessFound.Length == 1)
            {
                if (!CurrentUser.CheckPermision(((AccessRequired)accessFound[0]).AccessType))
                {
                    filterContext.Result = new RedirectResult("~/Dashboard/Index");
                    FlashMessage("Você precisa de permissão para executar esta tarefa", MessageType.Error);
                    return;
                }
            }

            base.OnActionExecuting(filterContext);
        }

        public string LoginUrl
        {
            get { return "~/Login/"; }
        }
    }
}