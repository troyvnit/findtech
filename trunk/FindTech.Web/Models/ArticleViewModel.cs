using System;
using System.Collections.Generic;
using FindTech.Entities.Models.Enums;
using FindTech.Web.Areas.BO.Models;

namespace FindTech.Web.Models
{
    public class ArticleViewModel
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string SeoTitle { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Tags { get; set; }
        public int Priority { get; set; }
        public string Avatar { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public BoxSize BoxSize { get; set; }
        public bool? IsActived { get; set; }
        public bool? IsHot { get; set; }
        public string ArticleCategoryColor { get; set; }
        public string ArticleCategoryName { get; set; }
        public string ArticleSeoCategoryName { get; set; }
        public string CreatedUserDisplayName { get; set; }
        public string SourceName { get; set; }
        public string SourceLogo { get; set; }
        public ICollection<ContentSectionBOViewModel> ContentSections { get; set; } 
    }
}