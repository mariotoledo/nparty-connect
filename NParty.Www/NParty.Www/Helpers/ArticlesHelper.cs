﻿using System;
using System.Collections.Generic;
using Google.Apis.Blogger.v3;
using Google.GData.Client;
using NParty.Www.Models;
using System.Xml;
using System.Text.RegularExpressions;
using Google.Apis.Blogger.v3.Data;

namespace NParty.Www.Helpers
{
    public class ArticlesHelper
    {
        private string applicationName;
        private string apiKey;

        public ArticlesHelper() { }

        public ArticlesHelper(string applicationName, string apiKey)
        {
            this.applicationName = applicationName;
            this.apiKey = apiKey;
        }

        public Article GetSingleArticleFromBlog(string blogId, string postId)
        {
            Article article = null;
            try
            {
                BloggerService service = new BloggerService(new Google.Apis.Services.BaseClientService.Initializer()
                {
                    ApiKey = this.apiKey,
                    ApplicationName = this.applicationName
                });

                PostsResource.GetRequest resource = service.Posts.Get(blogId, postId);
                resource.FetchImages = true;

                Post post = resource.Execute();

                article = new Article();
                article.Id = postId;
                article.Title = post.Title;
                article.Author = post.Author != null ? post.Author.DisplayName : "";
                article.Content = post.Content;
                article.CoverImage = post.Images != null && post.Images.Count > 0 ? post.Images[0].Url : null;
                article.DatePublished = post.Published.HasValue ? article.DatePublished : DateTime.MinValue;
                article.GenerateNPartyArtileLink(blogId);

                if (post.Labels != null && post.Labels.Count > 0)
                {
                    article.Labels = new string[post.Labels.Count];
                    for (int i = 0; i < post.Labels.Count; i++)
                    {
                        article.Labels[i] = post.Labels[i];
                    }
                }
            }
            catch (Exception e)
            {

            }

            return article;
        }

        public List<Article> GetArticlesFromBlog(string blogId, string blogDomain, int maxResults, int startIndex, string postsLabel)
        {
            List<Article> articles = new List<Article>();
            try
            {
                Service service = new Service("blogger", "n-party");

                string labelAppend = string.IsNullOrEmpty(postsLabel) ? "" : "/-/" + postsLabel;
                string maxResultAppend = maxResults > 0 ? "&max-results=" + maxResults : "";

                AtomFeed feed = service.Query(new FeedQuery()
                {
                    Uri = new Uri("http://www.blogger.com/feeds/" + blogId + "/posts/default" + labelAppend + "?start-index=" + startIndex + maxResultAppend)
                });

                foreach (var post in feed.Entries)
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
                    article.Labels = GetLabelsArray(post.Categories);
                    article.GenerateNPartyArtileLink(blogDomain);

                    articles.Add(article);
                };
            }
            catch (Exception e)
            {

            }

            return articles;
        }

        public List<Article> GetArticlesFromBlog(string blogId, string blogDomain, int maxResults, int startIndex)
        {
            return GetArticlesFromBlog(blogId, blogDomain, maxResults, startIndex, "");
        }

        public List<Article> GetArticlesFromBlog(string blogId, string blogDomain, int maxResults)
        {
            return GetArticlesFromBlog(blogId, blogDomain, maxResults, 0);
        }

        public List<Article> GetArticlesFromBlog(string blogId, string blogDomain)
        {
            return GetArticlesFromBlog(blogId, blogDomain, 0, 10);
        }

        private string[] GetLabelsArray(AtomCategoryCollection postCategories)
        {
            if (postCategories != null && postCategories.Count > 0)
            {
                string[] labels = new string[postCategories.Count];
                for (int i = 0; i < postCategories.Count; i++)
                {
                    labels[i] = postCategories[i].Term;
                }

                return labels;
            }
            else
                return new string[] { };
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
            foreach (AtomLink link in links)
            {
                if (link.Rel.CompareTo("alternate") == 0)
                    return link.HRef.Content;
            }

            return "";
        }

        private string GetSummaryManually(string htmlContent, int maxLength)
        {
            string result = Regex.Replace(htmlContent, @"<[^>]*>", String.Empty);
            if (result != null)
            {
                result = result.Trim();
                if (result.Length > maxLength)
                {
                    result = result.Substring(0, maxLength);
                }

                result += "...";
            }

            return result;
        }
    }
}