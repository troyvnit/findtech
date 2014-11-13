using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace FindTech.Entities.Models
{
    public class BenchmarkDetail : Entity
    {
        public int BenchmarkDetailId { get; set; }
        public string Header { get; set; }
        public string Unit { get; set; }
        public string Value { get; set; }
        public string WayComparison { get; set; }
        public int BenchmarkId { get; set; }
        public Benchmark Benchmark { get; set; }
        public int DeviceId { get; set; }
        public Device Device { get; set; }
    }
}
