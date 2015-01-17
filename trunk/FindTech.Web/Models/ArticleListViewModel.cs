using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindTech.Web.Models
{
    public class ArticleListViewModel
    {
        public string Title { get; set; }
        public string TitleStyleClass { get; set; }
        public IEnumerable<ArticleViewModel> Articles { get; set; } 
    }
}