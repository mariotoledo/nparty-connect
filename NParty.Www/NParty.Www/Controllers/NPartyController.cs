﻿using System;
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

        public int MaxPosts
        {
            get
            {
                try
                {
                    return Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxPosts"]);
                } catch(Exception)
                {
                    return 10;
                }                
            }
        }
        
        public string[] NintendoSpecialTags
        {
            get
            {
                try
                {
                    string unsplitedSpecialTags = System.Configuration.ConfigurationManager.AppSettings["NintendoSpecialTags"];
                    return unsplitedSpecialTags.Split(',').Select(p => p.Trim()).ToArray();
                }
                catch (Exception)
                {
                    return new string[] { };
                }
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewData["NintendoSpecialTags"] = this.NintendoSpecialTags;

            base.OnActionExecuting(filterContext);
        }
    }
}