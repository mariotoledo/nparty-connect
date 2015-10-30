using NParty.Www.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NParty.Www.Helpers
{
    public class NPartyArticlesHelper : ArticlesHelper
    {
        private string[] blogIds;

        public NPartyArticlesHelper(params string[] blogIds)
        {
            this.blogIds = blogIds;
        }

        public List<Article> GetHomeHilights()
        {
            List<Article> articles = new List<Article>();

            try
            {
                foreach (string blogId in blogIds)
                {
                    articles.AddRange(GetArticlesFromBlog(0, 5, blogId, "Destaque"));
                }

               articles = (List <Article>)articles.OrderByDescending(c => c.DatePublished).Take(5).ToList();
            } catch (Exception e)
            {

            }

            return articles;
        }
    }
}