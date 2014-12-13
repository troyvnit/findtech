﻿using System;
using FindTech.Entities.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FindTech.Web.Areas.BO.Models
{
    public class ArticleBOViewModel
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
        public string SourceId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public BoxSize BoxSize { get; set; }
       
        [JsonConverter(typeof(StringEnumConverter))]
        public ArticleType ArticleType { get; set; }
        public bool? IsActived { get; set; }
        public bool? IsDeleted { get; set; }
        public int ArticleCategoryId { get; set; }
    }
}