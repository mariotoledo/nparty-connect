using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Www.Controllers
{
    public class NPartyController : Controller
    {
        public string NintendoBlogId
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["NintendoBlogId"];
            }
        }

        public string ESportsBlogId
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["eSportsBlogId"];
            }
        }

        public string EventosBlogId
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["EventosBlogId"];
            }
        }

        public string MainBlogId
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["MainBlogId"];
            }
        }
    }
}