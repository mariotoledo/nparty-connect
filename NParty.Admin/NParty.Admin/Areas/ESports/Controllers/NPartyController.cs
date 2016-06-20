using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Admin.Areas.ESports.Controllers
{
    public class NPartyController : Controller
    {
        public void FlashMessage(string message, MessageType messageType)
        {
            TempData["message"] = message;

            if (messageType == MessageType.Success)
            {
                TempData["messageType"] = "success";
            }
            else if (messageType == MessageType.Info)
            {
                TempData["messageType"] = "info";
            }
            else if (messageType == MessageType.Error)
            {
                TempData["messageType"] = "danger";
            }            
        }
    }

    public enum MessageType
    {
        Success, Error, Info
    }
}