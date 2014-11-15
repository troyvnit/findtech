using System.Collections.Generic;
using Repository.Pattern.Ef6;

namespace FindTech.Entities.Models
{
    public class SpecGroup : Entity
    {
        public int SpecGroupId { get; set; }
        public string SpecGroupName { get; set; }
        public int Priority { get; set; }
        public ICollection<Spec> Specs { get; set; } 
    }
}
