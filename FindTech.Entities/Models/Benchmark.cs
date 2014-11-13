using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindTech.Entities.Models.Enums;
using Repository.Pattern.Ef6;

namespace FindTech.Entities.Models
{
    public class Benchmark : Entity
    {
        public int BenchmarkId { get; set; }
        public string BenchmarkName { get; set; }
        public string Description { get; set; }
        public BenchmarkDataType BenchmarkDataType { get; set; }
        public int BenchmarkGroupId { get; set; }
        public BenchmarkGroup BenchmarkGroup { get; set; }
    }
}
