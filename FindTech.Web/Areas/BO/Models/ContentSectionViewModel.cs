using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindTech.Entities.Models;

namespace FindTech.Web.Areas.BO.Models
{
    public class ContentSectionViewModel
    {
        public int ContentSectionId { get; set; }
        public string SectionTitle { get; set; }
        public string SectionDescription { get; set; }
        public string SectionContent { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }
        public int BenchmarkId { get; set; }
        public Benchmark Benchmark { get; set; }
    }
}