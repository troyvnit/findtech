using Repository.Pattern.Ef6;

namespace FindTech.Entities.Models
{
    public class BenchmarkGroup : Entity
    {
        public int BenchmarkGroupId { get; set; }
        public string Avatar { get; set; }
        public string Description { get; set; }
    }
}
