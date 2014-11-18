﻿using System;
using System.Collections.Generic;
using FindTech.Entities.Models.Enums;
using Repository.Pattern.Ef6;

namespace FindTech.Entities.Models
{
    public class Article : Entity
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Tags { get; set; }
        public int Priority { get; set; }
        public string Avatar { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public BoxSize BoxSize { get; set; }
        public ArticleType ArticleType { get; set; }
        public bool? IsActived { get; set; }
        public int ArticleCategoryId { get; set; }
        public ArticleCategory ArticleCategory { get; set; }
        public int SourceId { get; set; }
        public Source Source { get; set; }
        public ICollection<ContentSection> ContentSections { get; set; } 
    }
}
