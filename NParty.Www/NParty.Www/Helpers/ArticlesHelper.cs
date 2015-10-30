using System;
using System.Collections.Generic;
using Google.GData.Client;
using NParty.Www.Models;
using System.Xml;
using System.Text.RegularExpressions;

namespace NParty.Www.Helpers
{
    public class ArticlesHelper
    {
        public List<Article> GetArticlesFromBlog(int startIndex, int maxResults, string blogId, string postsLabel)
        {
            List<Article> articles = new List<Article>();
            try
            {
                Service service = new Service("blogger", "n-party");

                string labelAppend = string.IsNullOrEmpty(postsLabel) ? "" : "/-/" + postsLabel;
                string maxResultAppend = maxResults > 0 ? "&max-results=" + maxResults : "";

                AtomFeed feed = service.Query(new FeedQuery()
                {
                    Uri = new Uri("http://www.blogger.com/feeds/" + blogId + "/posts/default" + labelAppend  + "?start-index=" + startIndex + maxResultAppend)
                });

                foreach(var post in feed.Entries)
                {
                    Article article = new Article();
                    article.Title = post.Title.Text;
                    article.Content = post.Content.Content;
                    article.Author = post.Authors[0].Name;
                    article.DatePublished = post.Published;
                    article.CoverImage = GetPostImageManually(post);
                    article.Summary = GetSummaryManually(post.Content.Content, 200);
                    article.Id = GetPostIdManually(post.Id.AbsoluteUri);
                    article.ArticleLink = GetPostLinkManually(post.Links);

                    articles.Add(article);
                };
            }
            catch (Exception e)
            {

            }

            return articles;
        }

        public List<Article> GetArticlesFromBlog(int startIndex, int maxResults, string blogId)
        {
            return GetArticlesFromBlog(startIndex, maxResults, blogId, "");
        }

        public List<Article> GetArticlesFromBlog(int maxResults, string blogId)
        {
            return GetArticlesFromBlog(0, maxResults, blogId);
        }

        public List<Article> GetArticlesFromBlog(string blogId)
        {
            return GetArticlesFromBlog(0, 0, blogId);
        }

        private string GetPostImageManually(AtomEntry feedEntryXml)
        {
            string pattern = "<img.+?src=[\"'](.+?)[\"'].*?>";
            MatchCollection matches = Regex.Matches(feedEntryXml.Content.Content, pattern, RegexOptions.IgnoreCase);

            if (matches != null && matches.Count > 0)
                return matches[0].Groups[1].ToString();

            return "";
        }

        private string GetPostIdManually(string originalId)
        {
            string[] splitedId = originalId.Split('-');

            if (splitedId != null && splitedId.Length > 0)
                return splitedId[splitedId.Length - 1];

            return "";
        }

        private string GetPostLinkManually(AtomLinkCollection links)
        {
            foreach(AtomLink link in links)
            {
                if(link.Rel.CompareTo("alternate") == 0)
                    return link.HRef.Content;
            }

            return "";
        }

        private string GetSummaryManually(string htmlContent, int maxLength)
        {
            string result = Regex.Replace(htmlContent, @"<[^>]*>", String.Empty);
            if(result != null)
            {
                result = result.Trim();
                if(result.Length > maxLength)
                {
                    result = result.Substring(0, maxLength);
                }

                result += "...";
            }

            return result;
        }
    }
}