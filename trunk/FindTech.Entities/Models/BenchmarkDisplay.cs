using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace FindTech.Entities.Models
{
    public class BenchmarkDisplay : Entity
    {
        public int BenchmarkDisplayId { get; set; }
        public string Avatar { get; set; }
        public string Description { get; set; }
        public int BenchmarkId { get; set; }
        public Benchmark Benchmark { get; set; }
    }
}
