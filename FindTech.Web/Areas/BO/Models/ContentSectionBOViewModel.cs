using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindTech.Entities.Models;

namespace FindTech.Web.Areas.BO.Models
{
    public class ContentSectionBOViewModel
    {
        public int ContentSectionId { get; set; }
        public string SectionTitle { get; set; }
        public string SectionDescription { get; set; }
        public string SectionContent { get; set; }
        public int ArticleId { get; set; }
        public virtual ArticleBOViewModel Article { get; set; }
        public virtual BenchmarkGroupBOViewModel BenchmarkGroup { get; set; }
        public virtual ICollection<Image> Images { get; set; } 
    }
}