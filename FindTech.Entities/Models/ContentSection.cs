using Repository.Pattern.Ef6;

namespace FindTech.Entities.Models
{
    public class ContentSection : Entity
    {
        public int ContentSectionId { get; set; }
        public string SectionTitle { get; set; }
        public string SectionDescription { get; set; }
        public string SectionContent { get; set; }
        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }
        public int BenchmarkId { get; set; }
        public virtual Benchmark Benchmark { get; set; }
    }
}
