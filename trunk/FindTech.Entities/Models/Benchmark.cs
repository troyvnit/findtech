using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace FindTech.Entities.Models
{
    public class Benchmark : Entity
    {
        public int BenchmarkId { get; set; }
        public int DeviceId { get; set; }
        public Device Device { get; set; }
    }
}
