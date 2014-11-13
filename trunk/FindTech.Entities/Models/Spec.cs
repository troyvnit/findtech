using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace FindTech.Entities.Models
{
    public class Spec : Entity
    {
        public int SpecId { get; set; }
        public string SpecName { get; set; }
        public int Priority { get; set; }
        public int SpecGroupId { get; set; }
        public SpecGroup SpecGroup { get; set; }
    }
}
