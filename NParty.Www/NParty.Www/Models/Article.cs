using System;
using System.Collections.Generic;
using System.Linq;
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
        public string Id { get; set; }
    }
}