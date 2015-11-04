using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace NParty.Www.Models
{
    public class Article
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public string CoverImage { get; set; }
        public string Summary { get; set; }
        public DateTime DatePublished { get; set; }
        public string ArticleLink { get; set; }
        public string NPartyArticleLink { get; set; }
        public string Id { get; set; }
        public string[] Labels { get; set; }

        public void GenerateNPartyArtileLink(string domain)
        {
            this.NPartyArticleLink = domain + "/Ler/" + Id + "/" + GetTitleForUrl();
        }

        public string GetTitleForUrl()
        {
            try
            {
                if (Title != null)
                {
                    string[] splittedTitle = Title.Normalize().ToLower().RemoveDiacritics().RemoveSpecialCharacters().Split(' ');
                    StringBuilder builder = new StringBuilder();
                    for(int i = 0; i < splittedTitle.Length && i < 6; i++)
                    {
                        builder.Append(splittedTitle[i]);
                        builder.Append('-');
                    }

                    return builder.ToString();
                }

                return "";
            } catch (Exception e)
            {
                return "";
            }            
        }
    }
}