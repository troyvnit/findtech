using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace FindTech.Entities.Models
{
    public class BenchmarkDisplayGroup : Entity
    {
        public int BenchmarkDisplayGroupId { get; set; }
        public string BenchmarkDisplayGroupName { get; set; }
        public string Description { get; set; }
    }
}
